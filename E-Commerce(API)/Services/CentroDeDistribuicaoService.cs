using AutoMapper;
using E_Commerce_API_.Data.DTOs.CentroDeDistribuicaoDto;
using E_Commerce_API_.Exceptions;
using E_Commerce_API_.Interfaces;
using E_Commerce_API_.Models;
using Serilog;
using System.Text.Json;

namespace E_Commerce_API_.Services;

public class CentroDeDistribuicaoService : ICentroDeDistribuicaoService
{
    private ICentroDeDistribuicaoDAO _iCentroDeDistribuicaoDAO;
    private ICategoriaDAO _iCategoriaDAO;
    private ISubcategoriaDAO _iSubcategoriaDAO;
    private IProdutoDAO _iProdutoDAO;
    private IMapper _mapper;

    public CentroDeDistribuicaoService(ICentroDeDistribuicaoDAO iCentroDeDistribuicaoDAO, IMapper mapper, ICategoriaDAO iCategoriaDAO, ISubcategoriaDAO iSubcategoriaDAO, IProdutoDAO iProdutoDAO)
    {
        _iCentroDeDistribuicaoDAO = iCentroDeDistribuicaoDAO;
        _mapper = mapper;
        _iCategoriaDAO = iCategoriaDAO;
        _iSubcategoriaDAO = iSubcategoriaDAO;
        _iProdutoDAO = iProdutoDAO;
    }

    public async Task<ReadCentroDeDistribuicaoDto> AdicionarCD(CreateCentroDeDistribuicaoDto centroDeDistribuicaoDto)
    {
        Log.Information("Iniciada adição de um novo centro de distribuição.");

        if (NomeRepetido(centroDeDistribuicaoDto.Nome)) throw new BadRequestException($"O nome '{centroDeDistribuicaoDto.Nome}' já existe.");

        var endereco = await GetEndereco(centroDeDistribuicaoDto.CEP);

        var centro = MapeamentosDeCriacao(centroDeDistribuicaoDto, endereco);

        if (EnderecoExiste(centro)) throw new BadRequestException($"O endereço (Logradouro: {centro.Logradouro}, Número: {centro.Numero}, Complemento: {centro.Complemento}, Bairro: {centro.Bairro}, Cidade: {centro.Cidade}, UF: {centro.UF}) já existe");

        _iCentroDeDistribuicaoDAO.Add(centro);

        Log.Information("Centro de distribuição cadastrado com sucesso.");
        return _mapper.Map<ReadCentroDeDistribuicaoDto>(centro);
    }

    private CentroDeDistribuicao MapeamentosDeCriacao(CreateCentroDeDistribuicaoDto centroDeDistribuicaoDto, Endereco endereco)
    {
        var centro = _mapper.Map<CentroDeDistribuicao>(centroDeDistribuicaoDto);
        _mapper.Map(endereco, centro);
        return centro;
    }

    private async Task<Endereco> GetEndereco(string cep)
    {
        var endereco = await EnderecoPorCep(cep);
        if (endereco.Erro == true) throw new BadRequestException($"O CEP '{cep}' não existe.");
        if (endereco.Logradouro.Length == 0)
            throw new BadRequestException($"Não é possível cadastrar pois o CEP '{cep}' é geral.");

        return endereco;
    }

    public async Task EditarCD(Guid id, UpdateCentroDeDistribuicaoDto centroDeDistribuicaoDto)
    {
        Log.Information("Iniciada edição do centro de distribuição {@id}.", id);

        var centro = BuscarCDPorId(id);
        if (centro == null) throw new NotFoundException("Id de centro de distribuição informado para edição não existe.");

        if (NomeRepetido(centroDeDistribuicaoDto.Nome, centro.Id))
            throw new BadRequestException($"O nome '{centroDeDistribuicaoDto.Nome}' já existe.");

        if (centroDeDistribuicaoDto.Status == false && centro.Produtos.Count > 0)
            throw new BadRequestException("Não é possível inativar um CD que possui produtos vinculados.");

        var endereco = await GetEndereco(centroDeDistribuicaoDto.CEP);

        MapeamentosDeAtualizacao(centro, centroDeDistribuicaoDto, endereco);

        if (EnderecoExiste(centro)) throw new BadRequestException($"O endereço (Logradouro: {centro.Logradouro}, Número: {centro.Numero}, Complemento: {centro.Complemento}, Bairro: {centro.Bairro}, Cidade: {centro.Cidade}, UF: {centro.UF}) já existe");

        _iCentroDeDistribuicaoDAO.Update(centro);
        Log.Information("Centro de distribuição editado com sucesso.");
    }

    private void MapeamentosDeAtualizacao(CentroDeDistribuicao centroDeDistribuicao, UpdateCentroDeDistribuicaoDto centroDeDistribuicaoDto, Endereco endereco)
    {
        _mapper.Map(endereco, centroDeDistribuicao);
        _mapper.Map(centroDeDistribuicaoDto, centroDeDistribuicao);
    }

    public async Task<List<ReadCentroDeDistribuicaoDto>> ListaFiltrada(FiltroCentroDeDistribuicaoDto filtro)
    {
        Log.Information("Iniciada a pesquisa de centros de distribuição com os filtros informados: {@filtro}", filtro);

        var centros = await _iCentroDeDistribuicaoDAO.GetCentrosDeDistribuicao(filtro);

        var centrosDto = _mapper.Map<List<ReadCentroDeDistribuicaoDto>>(centros);

        return centrosDto;
    }

    public void DeletarCD(Guid id)
    {
        Log.Information("Iniciada remoção do centro de distribuição {@id}.", id);

        var centroDeDistribuicao = BuscarCDPorId(id);
        if (centroDeDistribuicao == null) throw new NotFoundException("Id de centro de distribuição informado para edição não existe.");

        _iCentroDeDistribuicaoDAO.Delete(centroDeDistribuicao);
        Log.Information("Centro de distribuição excluído com sucesso.");
    }

    public ReadCentroDeDistribuicaoDto PesquisarCDPorId(Guid id)
    {
        Log.Information("Iniciada pesquisa de centro de distribuição {@id}.", id);

        var centro = BuscarCDPorId(id);
        if (centro == null) throw new NotFoundException("Id de centro de distribuição informado não existe.");

        return _mapper.Map<ReadCentroDeDistribuicaoDto>(centro);
    }

    public CentroDeDistribuicao? BuscarCDPorId(Guid id)
    {
        return _iCentroDeDistribuicaoDAO.GetCentros().FirstOrDefault(centros => centros.Id == id);
    }

    private bool NomeRepetido(string nome, Guid? id = null)
    {
        var existeCategoria = _iCategoriaDAO.GetCategorias().Any(c => c.Nome == nome);

        var existeSubcategoria = _iSubcategoriaDAO.GetSubcategorias().Any(s => s.Nome == nome);

        var existeProduto = _iProdutoDAO.GetProdutos().Any(p => p.Nome == nome);

        var existeCD = _iCentroDeDistribuicaoDAO.GetCentros().Any(cd => cd.Nome == nome && cd.Id != id);

        return existeCategoria || existeSubcategoria || existeProduto || existeCD;
    }

    private async Task<Endereco> EnderecoPorCep(string cep)
    {
        Log.Information("Iniciada pesquisa do CEP '{@cep}'.", cep);
        using (HttpClient client = new HttpClient())
        {
            var resposta = await client.GetStringAsync($"https://viacep.com.br/ws/{cep}/json/");
            Log.Information("Pesquisa do CEP finalizada. Resposta: {@resposta}", resposta);
            return JsonSerializer.Deserialize<Endereco>(resposta)!;
        }
    }

    private bool EnderecoExiste(CentroDeDistribuicao centro)
    {
        return _iCentroDeDistribuicaoDAO.GetCentros().Any(centros => centros.CEP == centro.CEP 
        && centros.Numero == centro.Numero && centros.Complemento == centro.Complemento && centros.Id != centro.Id);
    }
}
