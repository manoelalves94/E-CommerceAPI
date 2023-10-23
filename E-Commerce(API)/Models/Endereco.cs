using System.Text.Json.Serialization;

namespace E_Commerce_API_.Models;

public class Endereco
{
    [JsonPropertyName("logradouro")]
    public string? Logradouro { get; set; }
    [JsonPropertyName("bairro")]
    public string? Bairro { get; set; }
    [JsonPropertyName("localidade")]
    public string? Cidade { get; set; }
    [JsonPropertyName("uf")]
    public string? UF { get; set; }
    [JsonPropertyName("erro")]
    public bool Erro { get; set; }
}
