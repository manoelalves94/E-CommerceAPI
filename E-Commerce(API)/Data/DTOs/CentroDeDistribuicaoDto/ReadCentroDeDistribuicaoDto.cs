using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace E_Commerce_API_.Data.DTOs.CentroDeDistribuicaoDto;

public class ReadCentroDeDistribuicaoDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Logradouro { get; set; }
    public uint Numero { get; set; }
    public string Complemento { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string UF { get; set; }
    public string CEP { get; set; }
    public bool Status { get; set; }
    public string Criacao { get; set; }
    public string Modificacao { get; set; }
}
