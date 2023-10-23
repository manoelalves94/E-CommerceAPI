using E_Commerce_API_.Attributes;
using System.Globalization;

namespace E_Commerce_API_.Data.DTOs.ProdutoDto;

public class CreateProdutoDto
{
    private const string caracteresPermitidos = "^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ'\\s0-9]+$";
    TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
    private string _nome;
    public Guid _id = Guid.NewGuid();
    public bool _status = true;
    public DateTime _dataEHoraCriacao = DateTime.Now;

    [Obrigatorio]
    [TamanhoMaximo(128)]
    [ExpressaoRegular(caracteresPermitidos)]
    public string Nome { get { return _nome; } set { _nome = ti.ToTitleCase(value.ToLower()); } }
    [Obrigatorio]
    [TamanhoMaximo(512)]
    [ExpressaoRegular(caracteresPermitidos)]
    public string Descricao { get; set; }
    [Obrigatorio]
    public double Peso { get; set; }
    [Obrigatorio]
    public double Altura { get; set; }
    [Obrigatorio]
    public double Largura { get; set; }
    [Obrigatorio]
    public double Comprimento { get; set; }
    [Obrigatorio]
    public double Valor { get; set; }
    [Obrigatorio]
    public int QuantidadeEmEstoque { get; set; }
    [Obrigatorio]
    public Guid CentroDeDistribuicaoId { get; set; }
    [Obrigatorio]
    public Guid SubcategoriaId { get; set; }
}
