using E_Commerce_API_.Attributes;

namespace E_Commerce_API_.Data.DTOs.CategoriaDto;

public class FiltroCategoriaDto
{
    [VerificacaoDaPesquisa]
    public string? Nome { get; set; }
    public Ordem Ordem { get; set; } = Ordem.Crescente;
    public Status Status { get; set; } = Status.Todos;
    public uint Registros { get; set; } = 10;
    public uint Pagina { get; set; } = 1;
}
