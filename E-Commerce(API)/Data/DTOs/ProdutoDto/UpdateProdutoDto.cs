using E_Commerce_API_.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace E_Commerce_API_.Data.DTOs.ProdutoDto;

public class UpdateProdutoDto
{
    private const string caracteresPermitidos = "^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ'\\s0-9]+$";
    TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
    private string _nome;
    public DateTime _dataEHoraModificacao = DateTime.Now;

    [TamanhoMaximo(128)]
    [ExpressaoRegular(caracteresPermitidos)]
    public string Nome { get { return _nome; } set { _nome = ti.ToTitleCase(value.ToLower()); } }
    [TamanhoMaximo(512)]
    [ExpressaoRegular(caracteresPermitidos)]
    public string Descricao { get; set; }
    public double Peso { get; set; }
    public double Altura { get; set; }
    public double Largura { get; set; }
    public double Comprimento { get; set; }
    public double Valor { get; set; }
    public int QuantidadeEmEstoque { get; set; }
    public bool Status { get; set; }
}
