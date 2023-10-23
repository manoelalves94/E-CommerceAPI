using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using Usuarios_API_.Data.DTOs;
using Usuarios_API_.Data.Requests;
using Usuarios_API_.Data.Responses;
using Usuarios_API_.Exceptions;
using Usuarios_API_.Interfaces;
using Usuarios_API_.Models;

namespace Usuarios_API_.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly UserManager<CustomIdentityUser> _userManager;

    public UserService(IMapper mapper, UserManager<CustomIdentityUser> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<ReadUserDto> CadastraUser(CreateUserDto userDto, TipoUser role)
    {
        if (_userManager.Users.Any(u => u.CPF == userDto.CPF))
            throw new BadRequestException("CPF já cadastrado.");

        var endereco = await GetEndereco(userDto.CEP);

        var identityUser = _mapper.Map<CustomIdentityUser>(userDto);
        _mapper.Map(endereco, identityUser);
        identityUser.TipoDeUsuario = role.ToString();

        var result = _userManager.CreateAsync(identityUser, userDto.Senha).Result;

        if (result.Errors.Any())
            throw new AggregateException(Erros(result.ToString()));

        await _userManager.AddToRoleAsync(identityUser, role.ToString());

        var readDto = _mapper.Map<ReadUserDto>(identityUser);
        return readDto;
    }

    public async Task EditaUser(Guid id, UpdateUserDto userDto)
    {
        var identityUser = _userManager.Users.FirstOrDefault(u => u.Id == id);
        if (identityUser == null)
            throw new NotFoundException("O Id informado não existe na base de cadastros.");

        var endereco = await GetEndereco(userDto.CEP);

        Mapeamentos(userDto, identityUser, endereco);

        var result = _userManager.UpdateAsync(identityUser).Result;

        if (result.Errors.Any())
            throw new AggregateException(Erros(result.ToString()));
    }

    private void Mapeamentos(UpdateUserDto userDto, CustomIdentityUser identityUser, Endereco endereco)
    {
        _mapper.Map(userDto, identityUser);
        _mapper.Map(endereco, identityUser);
    }

    public List<ReadUserDto> ListaUsuarios(FiltroUserDto filtro)
    {
        var lista = FiltraUsuarios(filtro);

        var readUsersDto = _mapper.Map<List<ReadUserDto>>(lista);

        return readUsersDto;
    }

    public CodigoDeRecuperacaoResponse SolicitaResetSenha(SolicitaResetSenhaRequest request)
    {
        var identityUser = _userManager.FindByEmailAsync(request.Email).Result;
        if (identityUser == null)
            throw new NotFoundException("E-mail não existe na base de cadastros.");

        var codigoRecuperacao = _userManager.GeneratePasswordResetTokenAsync(identityUser).Result;

        return new CodigoDeRecuperacaoResponse(codigoRecuperacao);
    }

    public void ResetaSenha(EfetuaResetSenhaRequest request)
    {
        var identityUser = _userManager.FindByEmailAsync(request.Email).Result;
        if (identityUser == null)
            throw new NotFoundException("E-mail não existe na base de cadastros.");

        var result = _userManager.ResetPasswordAsync(identityUser, request.CodigoDeRedefinicao, request.Senha).Result;

        if (result.Errors.Any())
            throw new AggregateException(Erros(result.ToString()));
    }

    public void DeletaUser(Guid id)
    {
        var identityUser = _userManager.Users.FirstOrDefault(u => u.Id == id);
        if (identityUser == null)
            throw new NotFoundException("O Id informado não existe na base de cadastros.");

        var result = _userManager.DeleteAsync(identityUser).Result;

        if (result.Errors.Any())
            throw new AggregateException(Erros(result.ToString()));
    }

    private async Task<Endereco> GetEndereco(string cep)
    {
        using (HttpClient client = new HttpClient())
        {
            var resposta = await client.GetStringAsync($"https://viacep.com.br/ws/{cep}/json/");
            var endereco = JsonSerializer.Deserialize<Endereco>(resposta)!;

            return endereco.Erro == false 
                ? endereco 
                : throw new BadRequestException("CEP informado não é válido.");
        }
    }

    public void EditaPermissaoUser(EditaPermissaoRequest request)
    {
        var identityUser = _userManager.Users.FirstOrDefault(u => u.Id == request.Id);
        if (identityUser == null)
            throw new NotFoundException("O Id informado não existe na base de cadastros.");

        if (identityUser.TipoDeUsuario == TipoUser.Cliente.ToString())
            throw new BadRequestException("O usuário informado é um Cliente, e não pode ter suas permissões alteradas.");

        var result = EditaPermissao(identityUser, request.TipoDeUsuario).Result;

        if (result.Errors.Any())
            throw new AggregateException(Erros(result.ToString()));
    }

    public void EditaStatusUser(EditaStatusRequest request)
    {
        var identityUser = _userManager.Users.FirstOrDefault(u => u.Id == request.Id);
        if (identityUser == null)
            throw new NotFoundException("O Id informado não existe na base de cadastros.");

        if (identityUser.Status == request.Status)
            throw new BadRequestException("O usuário já possui o status informado. Edição não realizada.");

        identityUser.Status = request.Status;
        identityUser.DataEHoraModificacao = DateTime.Now;

        var result = _userManager.UpdateAsync(identityUser).Result;

        if (result.Errors.Any())
            throw new AggregateException(Erros(result.ToString()));
    }

    public ReadUserDto UsuarioLogado(string value)
    {
        var identityUser = _userManager.Users.FirstOrDefault(u => u.Id == Guid.Parse(value));
        if (identityUser == null)
            throw new NotFoundException("O Id informado não existe na base de cadastros.");

        var userDto = _mapper.Map<ReadUserDto>(identityUser);

        return userDto;
    }

    private List<Exception> Erros(string errors)
    {
        var exceptions = new List<Exception>();

        if (errors.Contains("PasswordTooShort"))
            exceptions.Add(new BadRequestException("A senha é muito curta. Digite ao menos 8 caracteres."));

        if (errors.Contains("PasswordRequiresNonAlphanumeric"))
            exceptions.Add(new BadRequestException("Caractere especial é necessário na senha."));

        if (errors.Contains("PasswordRequiresDigit"))
            exceptions.Add(new BadRequestException("É necessário digitar um número na senha."));

        if (errors.Contains("PasswordRequiresUpper"))
            exceptions.Add(new BadRequestException("É necessário ao menos um caractere maiúsculo na senha."));

        if (errors.Contains("PasswordRequiresLower"))
            exceptions.Add(new BadRequestException("É necessário ao menos um caractere minúsculo na senha."));

        if (errors.Contains("InvalidEmail"))
            exceptions.Add(new BadRequestException("O e-mail informado está num formato inválido."));

        if (errors.Contains("DuplicateEmail"))
            exceptions.Add(new BadRequestException("O e-mail informado já está cadastrado."));

        if (errors.Contains("DuplicateUserName"))
            exceptions.Add(new BadRequestException("O nome informado já está cadastrado."));

        if (exceptions.Any())
            return exceptions;

        throw new Exception(errors);
    }

    private List<CustomIdentityUser> FiltraUsuarios(FiltroUserDto filtro)
    {
        var lista = _userManager.Users.ToList();

        if (filtro.Status == Status.Inativo)
        {
            lista = lista.FindAll(users => users.Status == false);
        }
        else if (filtro.Status == Status.Ativo)
        {
            lista = lista.FindAll(users => users.Status == true);
        }

        if (filtro.Nome != null)
        {
            lista = lista.FindAll(users => users.NormalizedUserName.Contains(filtro.Nome.ToUpper()));
        }

        if (filtro.Email != null)
        {
            lista = lista.FindAll(users => users.NormalizedEmail == filtro.Email.ToUpper());
        }

        if (filtro.CPF != null)
        {
            lista = lista.FindAll(users => users.CPF == filtro.CPF);
        }

        return lista.OrderBy(u => u.UserName).ToList();
    }

    private async Task<IdentityResult> EditaPermissao(CustomIdentityUser identityUser, string role)
    {
        var roleAtual = _userManager.GetRolesAsync(identityUser).Result.First();

        if (roleAtual == role.ToLower())
            throw new BadRequestException($"O perfil do usuário {identityUser.UserName} já é {role}. Edição não realizada.");

        await _userManager.RemoveFromRoleAsync(identityUser, roleAtual);

        await _userManager.AddToRoleAsync(identityUser, role);

        identityUser.DataEHoraModificacao = DateTime.Now;
        identityUser.TipoDeUsuario = role;

        return _userManager.UpdateAsync(identityUser).Result;
    }
}

