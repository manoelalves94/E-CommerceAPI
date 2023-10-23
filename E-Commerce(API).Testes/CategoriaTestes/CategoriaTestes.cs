using AutoMapper;
using E_Commerce_API_.Attributes;
using E_Commerce_API_.Data.DAO;
using E_Commerce_API_.Data.DTOs.CategoriaDto;
using E_Commerce_API_.Exceptions;
using E_Commerce_API_.Profiles;
using E_Commerce_API_.Services;

namespace E_Commerce_API_.Testes.CategoriaTestes;

public class CategoriaTestes
{
    private readonly CategoriaDAO _categoriaDAO;
    private readonly CategoriaService _categoriaService;
    private readonly SubcategoriaDAO _subcategoriaDAO;
    private readonly ProdutoDAO _produtoDAO;
    private readonly CentroDeDistribuicaoDAO _centroDistribuicaoDAO;
    private readonly SubcategoriaService _subcategoriaService;
    private readonly IMapper _mapper;
    private readonly MapperConfiguration _mapperConfig;


    public CategoriaTestes()
    {
        var dbInMemory = new DBInMemory();
        var context = dbInMemory.GetContext();

        _mapperConfig = new MapperConfiguration(opt =>
        {
            opt.AddProfile(new CategoriaProfile());
        });
        _mapper = _mapperConfig.CreateMapper();

        _categoriaDAO = new CategoriaDAO(context);
        _subcategoriaDAO = new SubcategoriaDAO(context, _mapper);
        _produtoDAO = new ProdutoDAO(context, _mapper);
        _centroDistribuicaoDAO = new CentroDeDistribuicaoDAO(context);
        _subcategoriaService = new SubcategoriaService(_mapper, _subcategoriaDAO,
                                                       _categoriaDAO, _produtoDAO,
                                                       _centroDistribuicaoDAO);

        _categoriaService = new CategoriaService(_mapper,
                                                 _categoriaDAO,
                                                 _subcategoriaDAO,
                                                 _produtoDAO,
                                                 _centroDistribuicaoDAO,
                                                 _subcategoriaService);
    }

    [Fact]
    public void TesteCadastroCategoriaComNomeSomenteAlfabeto()
    {
        //Arrange
        string nome = "Limpeza";

        //Act
        var resultado = new VerificacaoDoNomeAttribute().IsValid(nome);

        //Assert
        Assert.True(resultado);
    }

    [Fact]
    public void TesteCadastroCategoriaComStatusAtivo()
    {
        //Arrange
        var createDto = new CreateCategoriaDto { Nome = "Teste de status" };

        //Act
        var readDto = _categoriaService.AdicionarCategoria(createDto);

        //Assert
        Assert.True(readDto.Status);
    }

    [Fact]
    public void TesteCadastroCategoriaComDataEHoraCriacao()
    {
        //Arrange
        var createDto = new CreateCategoriaDto() { Nome = "Teste de data" };

        //Act
        var readDto = _categoriaService.AdicionarCategoria(createDto);

        //Assert
        Assert.Equal(DateTime.Now.ToString("g"), readDto.Criacao);
    }

    [Theory]
    [InlineData(1, "3e987cc6-3acf-4814-8c70-b27df656b001")]
    [InlineData(2, "3e987cc6-3acf-4814-8c70-b27df656b002")]
    public void TesteQuantidadeDeSubcategoriasEmUmaCategoria(int quantidade, Guid id)
    {
        //Act
        var categoria = _categoriaService.BuscarCategoriaPorId(id);

        //Assert
        Assert.Equal(quantidade, categoria.Subcategorias.Count);
    }

    [Fact]
    public void TesteCadastroCategoriaSemNome()
    {
        //Arrange
        var dto = new CreateCategoriaDto();

        //Assert
        Assert.Throws<BadRequestException>(
            //Act
            () => new ObrigatorioAttribute().IsValid(dto.Nome));
    }


    [Theory]
    [InlineData("L1mpeza")]
    [InlineData("Limpez@")]
    public void TesteCadastroCategoriaNomeComCaracteresNaoPermitidos(string nome)
    {
        //Assert
        Assert.Throws<BadRequestException>(
            //Act
            () => new VerificacaoDoNomeAttribute().IsValid(nome));
    }

    [Fact]
    public void TesteCadastroCategoriaNomeComMaisDe128Caracteres()
    {
        //Arrange
        var nome = "Limpezaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

        //Assert
        Assert.Throws<BadRequestException>(
            //Act
            () => new VerificacaoDoNomeAttribute().IsValid(nome));
    }

    [Fact]
    public void TesteCadastroCategoriaNomeComMenosDe3Caracteres()
    {
        //Arrange
        var nome = "Te";

        //Assert
        Assert.Throws<BadRequestException>(
            //Act
            () => new VerificacaoDoNomeAttribute().IsValid(nome));
    }

    [Fact]
    public void TesteNomeCategoriaRepetido()
    {
        //Arrange
        var dto = new CreateCategoriaDto() { Nome = "Comida" };

        //Assert
        Assert.Throws<BadRequestException>(
            //Act
            () => _categoriaService.AdicionarCategoria(dto));
    }
}