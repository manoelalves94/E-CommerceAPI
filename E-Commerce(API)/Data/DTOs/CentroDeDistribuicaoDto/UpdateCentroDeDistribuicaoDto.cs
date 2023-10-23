using E_Commerce_API_.Attributes;
using System.Globalization;

namespace E_Commerce_API_.Data.DTOs.CentroDeDistribuicaoDto;

public class UpdateCentroDeDistribuicaoDto
{
    private const string caracteresPermitidos = "^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ'\\s0-9]+$";
    TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
    private string _nome;
    public DateTime _dataEHoraModificacao = DateTime.Now;

    [Obrigatorio(ErrorMessage = "O campo Nome é obrigatório.")]
    [TamanhoMinimo(3, ErrorMessage = "O campo Nome deve conter pelo menos 3 caracteres.")]
    [TamanhoMaximo(128, ErrorMessage = "O campo Nome deve conter no máximo 128 caracteres.")]
    [ExpressaoRegular(caracteresPermitidos, ErrorMessage = "O campo Nome deve ser alfanumérico.")]
    public string Nome { get { return _nome; } set { _nome = ti.ToTitleCase(value.ToLower()); } }
    [Obrigatorio(ErrorMessage = "O campo Número é obrigatório.")]
    public uint Numero { get; set; }
    [Obrigatorio(ErrorMessage = "O campo Complemento é obrigatório.")]
    [TamanhoMinimo(3, ErrorMessage = "O campo Complemento deve conter pelo menos 3 caracteres.")]
    [TamanhoMaximo(128, ErrorMessage = "O campo Complemento deve conter no máximo 128 caracteres.")]
    public string Complemento { get; set; }
    [Obrigatorio(ErrorMessage = "O campo CEP é obrigatório.")]
    [TamanhoMinimo(8, ErrorMessage = "Digite o CEP com 8 números, sem hífen.")]
    [TamanhoMaximo(8, ErrorMessage = "Digite o CEP com 8 números, sem hífen.")]
    public string CEP { get; set; }
    [Obrigatorio(ErrorMessage = "O campo Status é obrigatório.")]
    public bool Status { get; set; }
}
