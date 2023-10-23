using AutoMapper;
using E_Commerce_API_.Attributes;
using E_Commerce_API_.Data.DAO;
using E_Commerce_API_.Data.DTOs.SubcategoriaDto;
using E_Commerce_API_.Exceptions;
using E_Commerce_API_.Profiles;
using E_Commerce_API_.Services;

namespace E_Commerce_API_.Testes.SubcategoriaTestes;

public class SubcategoriaTestes
{
    private readonly SubcategoriaService _subcategoriaService;
    private readonly IMapper _mapper;
    private readonly MapperConfiguration _mapperConfig;
    private readonly SubcategoriaDAO _subcategoriaDAO;
    private readonly CategoriaDAO _categoriaDAO;
    private readonly ProdutoDAO _produtoDAO;
    private readonly CentroDeDistribuicaoDAO _centroDeDistribuicaoDAO;
    public SubcategoriaTestes()
    {
        _mapperConfig = new MapperConfiguration(opt =>
        {
            opt.AddProfile<SubcategoriaProfile>();
        });
        _mapper = _mapperConfig.CreateMapper();

        var dbInMemory = new DBInMemory();
        var context = dbInMemory.GetContext();

        _subcategoriaDAO = new SubcategoriaDAO(context, _mapper);
        _categoriaDAO = new CategoriaDAO(context);
        _produtoDAO = new ProdutoDAO(context, _mapper);
        _centroDeDistribuicaoDAO = new CentroDeDistribuicaoDAO(context);

        _subcategoriaService = new SubcategoriaService(_mapper, _subcategoriaDAO,
                                                       _categoriaDAO, _produtoDAO,
                                                       _centroDeDistribuicaoDAO);
    }

    [Fact]
    public void TesteCadastroSubcategoriaNomeSomenteAlfabeto()
    {
        //Arrange
        var nome = "carne";

        //Act
        var resultado = new VerificacaoDoNomeAttribute().IsValid(nome);

        //Assert
        Assert.True(resultado);
    }

    [Fact]
    public void TesteCadastroSubcategoriaComStatusAtivo()
    {
        //Arrange
        var dto = new CreateSubcategoriaDto()
        {
            Nome = "Massa",
            CategoriaId = Guid.Parse("3e987cc6-3acf-4814-8c70-b27df656b001")
        };

        //Act
        var readDto = _subcategoriaService.AdicionarSubcategoria(dto);

        //Assert
        Assert.True(readDto.Status);
    }

    [Fact]
    public void TesteCadastroSubcategoriaComDataEHoraCriacao()
    {
        //Arrange
        var dto = new CreateSubcategoriaDto()
        {
            Nome = "Massa",
            CategoriaId = Guid.Parse("3e987cc6-3acf-4814-8c70-b27df656b001")
        };

        //Act
        var readDto = _subcategoriaService.AdicionarSubcategoria(dto);

        //Assert
        Assert.Equal(DateTime.Now.ToString("g"), readDto.Criacao);
    }

    [Theory]
    [InlineData("carn3")]
    [InlineData("c@rne")]
    public void TesteCadastroSubcategoriaNomeComCaracteresNaoPermitidos(string nome)
    {
        //Assert
        Assert.Throws<BadRequestException>(
            //Act
            () => new VerificacaoDoNomeAttribute().IsValid(nome));
    }

    [Fact]
    public void TesteCadastroSubcategoriaNomeComMaisDe128Caracteres()
    {
        //Arrange
        var nome = "Carneeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee";

        //Assert
        Assert.Throws<BadRequestException>(
            //Act
            () => new VerificacaoDoNomeAttribute().IsValid(nome));
    }

    [Fact]
    public void TesteCadastroSubcategoriaNomeComMenosDe3Caracteres()
    {
        //Arrange
        var nome = "Ab";

        //Assert
        Assert.Throws<BadRequestException>(
            //Act
            () => new VerificacaoDoNomeAttribute().IsValid(nome));
    }

    [Fact]
    public void TesteCadastroSubcategoriaSemNome()
    {
        //Arrange
        var dto = new CreateSubcategoriaDto();

        //Assert
        Assert.Throws<BadRequestException>(
            //Act
            () => new ObrigatorioAttribute().IsValid(dto.Nome));
    }

    [Theory]
    [InlineData("Comida")]
    [InlineData("Suco")]
    public void TesteCadastroSubcategoriaNomeRepetido(string nome)
    {
        //Arrange
        var dto = new CreateSubcategoriaDto()
        {
            Nome = nome,
            CategoriaId = Guid.Parse("3e987cc6-3acf-4814-8c70-b27df656b001")
        };

        //Assert
        Assert.Throws<BadRequestException>(
            //Act
            () => _subcategoriaService.AdicionarSubcategoria(dto));
    }

    [Fact]
    public void TesteCadastrarSubcategoriaEmCategoriaInativa()
    {
        //Arrange
        var dto = new CreateSubcategoriaDto()
        {
            Nome = "Monitor",
            CategoriaId = Guid.Parse("3e987cc6-3acf-4814-8c70-b27df656b004")
        };

        //Assert
        Assert.Throws<BadRequestException>(
            //Act
            () => _subcategoriaService.AdicionarSubcategoria(dto));
    }

    [Fact]
    public void TesteCadastrarSubcategoriaEmCategoriaInexistente()
    {
        //Arrange
        var dto = new CreateSubcategoriaDto()
        {
            Nome = "Monitor",
            CategoriaId = Guid.Parse("99999cc6-3acf-4814-8c70-b27df656b004")
        };

        //Assert
        Assert.Throws<NotFoundException>(
            //Act
            () => _subcategoriaService.AdicionarSubcategoria(dto));
    }
}
