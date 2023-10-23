using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Usuarios_API_.Models;

public class Endereco
{
    [JsonPropertyName("logradouro")]
    public string Logradouro { get; set; }
    [JsonPropertyName("bairro")]
    public string Bairro { get; set; }
    [JsonPropertyName("localidade")]
    public string Cidade { get; set; }
    [JsonPropertyName("uf")]
    public string UF { get; set; }
    [JsonPropertyName("erro")]
    public bool Erro { get; set; }
}
