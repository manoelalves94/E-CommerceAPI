using E_Commerce_API_.Attributes;

namespace E_Commerce_API_.Data.DTOs.CentroDeDistribuicaoDto;

public class FiltroCentroDeDistribuicaoDto
{
    [TamanhoMinimo(3, ErrorMessage = "O campo Nome deve ter no mínimo 3 caracteres.")]
    [TamanhoMaximo(128, ErrorMessage = "O campo Nome de ter no máximo 128 caracteres.")]
    public string? Nome { get; set; }
    [TamanhoMinimo(3, ErrorMessage = "O campo Logradouro deve ter no mínimo 3 caracteres.")]
    [TamanhoMaximo(128, ErrorMessage = "O campo Logradouro deve ter no máximo 128 caracteres.")]
    public string? Logradouro { get; set; }
    [TamanhoMinimo(3, ErrorMessage = "O campo Bairro deve ter no mínimo 3 caracteres.")]
    [TamanhoMaximo(128, ErrorMessage = "O campo Bairro deve ter no máximo 128 caracteres.")]
    public string? Bairro { get; set; }
    [TamanhoMinimo(3, ErrorMessage = "O campo Cidade deve ter no mínimo 3 caracteres.")]
    [TamanhoMaximo(128, ErrorMessage = "O campo Cidade deve ter no máximo 128 caracteres.")]
    public string? Cidade { get; set; }
    [TamanhoMinimo(2, ErrorMessage = "O campo UF deve conter 2 letras.")]
    [TamanhoMaximo(2, ErrorMessage = "O campo UF deve conter 2 letras.")]
    public string? UF { get; set; }
    [TamanhoMinimo(8, ErrorMessage = "Digite o CEP com 8 números, sem hífen.")]
    [TamanhoMaximo(8, ErrorMessage = "Digite o CEP com 8 números, sem hífen.")]
    public string? CEP { get; set; }
    public Status Status { get; set; } = Status.Todos;
    public Ordem Ordem { get; set; } = Ordem.Crescente;
    public uint Registros { get; set; } = 30;
    public uint Pagina { get; set; } = 1;
}
