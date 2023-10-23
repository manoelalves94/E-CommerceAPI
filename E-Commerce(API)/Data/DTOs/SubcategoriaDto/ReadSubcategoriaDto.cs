using E_Commerce_API_.Models;

namespace E_Commerce_API_.Data.DTOs.SubcategoriaDto;

public class ReadSubcategoriaDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public bool Status { get; set; }
    public string Criacao { get; set; }
    public string Modificacao { get; set; }
    public string Categoria { get; set; }
}
