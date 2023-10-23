using Serilog;
using AutoMapper;
using E_Commerce_API_.Models;
using Microsoft.AspNetCore.Mvc;
using E_Commerce_API_.Exceptions;
using E_Commerce_API_.Interfaces;
using E_Commerce_API_.Data.DTOs.CategoriaDto;

namespace E_Commerce_API_.Services;

public class CategoriaService : ICategoriaService
{
    private readonly IMapper _mapper;
    private readonly ICategoriaDAO _iCategoriaDAO;
    private readonly ISubcategoriaService _iSubcategoriaService;
    private readonly ISubcategoriaDAO _iSubcategoriaDAO;
    private readonly IProdutoDAO _iProdutoDAO;
    private readonly ICentroDeDistribuicaoDAO _iCentroDeDistribuicaoDAO;

    public CategoriaService(IMapper mapper, ICategoriaDAO categoriaDAO, ISubcategoriaDAO iSubcategoriaDAO, IProdutoDAO iProdutoDAO, ICentroDeDistribuicaoDAO iCentroDeDistribuicaoDAO, ISubcategoriaService iSubcategoriaService)
    {
        _mapper = mapper;
        _iCategoriaDAO = categoriaDAO;
        _iSubcategoriaDAO = iSubcategoriaDAO;
        _iProdutoDAO = iProdutoDAO;
        _iCentroDeDistribuicaoDAO = iCentroDeDistribuicaoDAO;
        _iSubcategoriaService = iSubcategoriaService;
    }

    public async Task<List<ReadCategoriaDto>> ListaFiltrada(FiltroCategoriaDto filtro)
    {
        Log.Information("Iniciada a pesquisa de categoria com os filtros informados: {@filtro}", filtro);

        var categorias = await _iCategoriaDAO.GetListaCategorias(filtro);

        var categoriasDto = _mapper.Map<List<ReadCategoriaDto>>(categorias);

        return categoriasDto;
    }

    public ReadCategoriaDto AdicionarCategoria(CreateCategoriaDto categoriaDto)
    {
        Log.Information("Iniciada adição de uma nova categoria: '{@Nome}'.", categoriaDto.Nome);

        if (NomeRepetido(categoriaDto.Nome))
            throw new BadRequestException($"O nome '{@categoriaDto.Nome}' já existe.");

        var categoria = _mapper.Map<Categoria>(categoriaDto);

        _iCategoriaDAO.Add(categoria);

        var readCategoriaDto = _mapper.Map<ReadCategoriaDto>(categoria);

        Log.Information("Categoria cadastrada com sucesso.");
        return readCategoriaDto;
    }

    public void AtualizarCategoria(Guid id, [FromBody] UpdateCategoriaDto categoriaDto)
    {
        Log.Information("Iniciada edição da categoria {@id}.", id);

        var categoria = BuscarCategoriaPorId(id);
        if (categoria == null)
            throw new NotFoundException("Id de categoria informada para edição não existe.");

        EdicaoValida(categoriaDto, categoria);

        _iSubcategoriaService.AlterarStatusSubcategorias(categoriaDto, categoria);

        _mapper.Map(categoriaDto, categoria);

        _iCategoriaDAO.Update(categoria);

        Log.Information("Categoria editada com sucesso.");
    }

    public void RemoverCategoria(Guid id)
    {
        Log.Information("Iniciada remoção da categoria {@id}.", id);

        var categoria = BuscarCategoriaPorId(id);
        if (categoria == null)
            throw new NotFoundException("Id de categoria informada para remoção não existe.");

        _iCategoriaDAO.Delete(categoria);

        Log.Information("Categoria excluída com sucesso.");
    }

    public ReadCategoriaDto PesquisarCategoriaId(Guid id)
    {
        Log.Information("Iniciada pesquisa de categoria {@id}.", id);

        var categoria = BuscarCategoriaPorId(id);
        if (categoria == null) throw new NotFoundException("Id de categoria informado não existe.");

        var categoriaDto = _mapper.Map<ReadCategoriaDto>(categoria);
        return categoriaDto;
    }

    private void EdicaoValida(UpdateCategoriaDto categoriaDto, Categoria categoria)
    {
        if (categoria.Nome == categoriaDto.Nome && categoria.Status == categoriaDto.Status)
            throw new BadRequestException("A edição não foi realizada, pois os valores não foram alterados.");

        if (categoriaDto.Status == false && categoria.Subcategorias.Any(s => s.Produtos.Any()))
            throw new BadRequestException("A categoria não pode ser inativada, pois existem produtos cadastrados em suas subcategorias.");

        if (NomeRepetido(categoriaDto.Nome, categoria.Id))
            throw new BadRequestException($"O nome '{categoriaDto.Nome}' já existe.");
    }

    public Categoria? BuscarCategoriaPorId(Guid id)
    {
        return _iCategoriaDAO.GetCategorias().FirstOrDefault(c => c.Id == id);
    }

    private bool NomeRepetido(string nome, Guid? id = null)
    {
        var existeCategoria = _iCategoriaDAO.GetCategorias().Any(c => c.Nome == nome && c.Id != id);

        var existeSubcategoria = _iSubcategoriaDAO.GetSubcategorias().Any(s => s.Nome == nome);

        var existeProduto = _iProdutoDAO.GetProdutos().Any(p => p.Nome == nome);

        var existeCD = _iCentroDeDistribuicaoDAO.GetCentros().Any(cd => cd.Nome == nome);

        return existeCategoria || existeSubcategoria || existeProduto || existeCD;
    }
}