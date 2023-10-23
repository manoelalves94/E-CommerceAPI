using Serilog;
using AutoMapper;
using E_Commerce_API_.Models;
using E_Commerce_API_.Exceptions;
using E_Commerce_API_.Interfaces;
using E_Commerce_API_.Data.DTOs.CategoriaDto;
using E_Commerce_API_.Data.DTOs.SubcategoriaDto;

namespace E_Commerce_API_.Services;

public class SubcategoriaService : ISubcategoriaService
{
    private IMapper _mapper;
    private ISubcategoriaDAO _iSubcategoriaDAO;
    private readonly ICategoriaDAO _iCategoriaDAO;
    private readonly IProdutoDAO _iProdutoDAO;
    private readonly ICentroDeDistribuicaoDAO _iCentroDeDistribuicaoDAO;

    public SubcategoriaService(IMapper mapper, ISubcategoriaDAO iSubcategoriaDAO, ICategoriaDAO iCategoriaDAO, IProdutoDAO iProdutoDAO, ICentroDeDistribuicaoDAO iCentroDeDistribuicaoDAO)
    {
        _mapper = mapper;
        _iSubcategoriaDAO = iSubcategoriaDAO;
        _iCategoriaDAO = iCategoriaDAO;
        _iProdutoDAO = iProdutoDAO;
        _iCentroDeDistribuicaoDAO = iCentroDeDistribuicaoDAO;
    }

    public List<ReadSubcategoriaDto> ListaFiltrada(FiltroSubcategoriaDto filtro)
    {
        Log.Information("Iniciada a pesquisa de subcategorias com os filtros informados: {@filtro}", filtro);

        return _iSubcategoriaDAO.GetListaSubcategorias(filtro);
    }

    public ReadSubcategoriaDto AdicionarSubcategoria(CreateSubcategoriaDto subcategoriaDto)
    {
        Log.Information("Iniciada adição de uma nova subcategoria.");

        CadastroValido(subcategoriaDto);

        var subcategoria = _mapper.Map<Subcategoria>(subcategoriaDto);

        _iSubcategoriaDAO.Create(subcategoria);

        var readSubcategoriaDto = _mapper.Map<ReadSubcategoriaDto>(subcategoria);

        Log.Information("Subcategoria cadastrada com sucesso.");
        return readSubcategoriaDto;
    }

    private void CadastroValido(CreateSubcategoriaDto subcategoriaDto)
    {
        var categoria = _iCategoriaDAO.GetCategorias().FirstOrDefault(categoria => categoria.Id == subcategoriaDto.CategoriaId);

        if (categoria == null)
            throw new NotFoundException($"O Id '{subcategoriaDto.CategoriaId}' de categoria informado não existe.");

        if (categoria.Status == false)
            throw new BadRequestException($"A categoria '{categoria.Id}' está inativa e não pode receber novos cadastros.");

        if (NomeRepetido(subcategoriaDto.Nome))
            throw new BadRequestException($"O nome '{subcategoriaDto.Nome}' já existe.");
    }

    public void AtualizarSubcategoria(Guid id, UpdateSubcategoriaDto subcategoriaDto)
    {
        Log.Information("Iniciada edição da subcategoria {@id}.", id);

        var subcategoria = BuscarSubcategoriaPorId(id);
        if (subcategoria == null) throw new NotFoundException("Id de subcategoria informada para edição não existe.");

        EdicaoValida(subcategoriaDto, subcategoria);

        _iSubcategoriaDAO.Update(subcategoriaDto, subcategoria);

        Log.Information("Subcategoria editada com sucesso.");
    }

    private void EdicaoValida(UpdateSubcategoriaDto subcategoriaDto, Subcategoria subcategoria)
    {
        if (subcategoria.Nome == subcategoriaDto.Nome && subcategoria.Status == subcategoriaDto.Status)
            throw new BadRequestException("Nenhum valor foi alterado, a edição não foi realizada.");

        if (subcategoria.Categoria.Status == false)
            throw new BadRequestException($"A categoria de Id '{subcategoria.Categoria.Id}' está inativa, suas subcategorias não podem ser editadas.");

        if (subcategoriaDto.Status == false && subcategoria.Produtos.Any())
            throw new BadRequestException("A subcategoria não pode ser inativada, pois existem produtos cadastrados.");

        if (NomeRepetido(subcategoriaDto.Nome, subcategoria.Id))
            throw new BadRequestException($"O nome '{subcategoriaDto.Nome}' já existe.");
    }

    public void RemoverSubcategoria(Guid id)
    {
        Log.Information("Iniciada remoção da subcategoria {@id}.", id);

        var subcategoria = BuscarSubcategoriaPorId(id);
        if (subcategoria == null)
            throw new NotFoundException("Id de subcategoria informada para remoção não existe.");

        _iSubcategoriaDAO.Delete(subcategoria);

        Log.Information("Subcategoria excluída com sucesso.");
    }

    public ReadSubcategoriaDto PesquisarSubcategoriaId(Guid id)
    {
        Log.Information("Iniciada pesquisa de subcategoria {@id}.", id);

        var subcategoria = BuscarSubcategoriaPorId(id);
        if (subcategoria == null) throw new NotFoundException("Id de subcategoria informado não existe.");

        var subcategoriaDto = _mapper.Map<ReadSubcategoriaDto>(subcategoria);

        return subcategoriaDto;
    }

    public Subcategoria? BuscarSubcategoriaPorId(Guid id)
    {
        return _iSubcategoriaDAO.GetSubcategorias().FirstOrDefault(s => s.Id == id);
    }

    public void AlterarStatusSubcategorias(UpdateCategoriaDto categoriaDto, Categoria categoria)
    {
        if (categoriaDto.Status != categoria.Status)
        {
            var subcategorias = categoria.Subcategorias.ToList();
            foreach (var subcategoria in subcategorias)
            {
                subcategoria.Status = categoriaDto.Status;
                subcategoria.DataEHoraModificacao = DateTime.Now;
                Log.Information("Status da subcategoria {@Id} alterado.", subcategoria.Id);
            }
        }
    }

    private bool NomeRepetido(string nome, Guid? id = null)
    {
        var existeCategoria = _iCategoriaDAO.GetCategorias().Any(c => c.Nome == nome);

        var existeSubcategoria = _iSubcategoriaDAO.GetSubcategorias().Any(s => s.Nome == nome && s.Id != id);

        var existeProduto = _iProdutoDAO.GetProdutos().Any(p => p.Nome == nome);

        var existeCD = _iCentroDeDistribuicaoDAO.GetCentros().Any(cd => cd.Nome == nome);

        return existeCategoria || existeSubcategoria || existeProduto || existeCD;
    }
}
