using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API_.Models;

public class CentroDeDistribuicao
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    [MinLength(3)]
    [MaxLength(128)]
    public string Nome { get; set; }
    [Required]
    [MinLength(3)]
    [MaxLength(128)]
    public string Logradouro { get; set; }
    [Required]
    public uint Numero { get; set; }
    [Required]
    [MinLength(3)]
    [MaxLength(128)]
    public string Complemento { get; set; }
    [Required]
    [MinLength(3)]
    [MaxLength(128)]
    public string Bairro { get; set; }
    [Required]
    [MinLength(3)]
    [MaxLength(128)]
    public string Cidade { get; set; }
    [Required]
    [MinLength(2)]
    [MaxLength(2)]
    public string UF { get; set; }
    [Required]
    [MinLength(8)]
    [MaxLength(8)]
    public string CEP { get; set; }
    public bool Status { get; set; }
    public DateTime DataEHoraCriacao { get; set; }
    public DateTime DataEHoraModificacao { get; set; }
    public virtual ICollection<Produto> Produtos { get; set; }

}
