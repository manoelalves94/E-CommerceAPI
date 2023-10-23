namespace Usuarios_API_.Data.DTOs;

public class ReadUserDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string DataNascimento { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }
    public string Logradouro { get; set; }
    public uint Numero { get; set; }
    public string Complemento { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string UF { get; set; }
    public string CEP { get; set; }
    public string TipoDeUsuario { get; set; }
    public bool Status { get; set; }
    public string Criacao { get; set; }
    public string Modificacao { get; set; }
}
