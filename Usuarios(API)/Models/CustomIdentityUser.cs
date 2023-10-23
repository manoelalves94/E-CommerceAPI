using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Usuarios_API_.Models;

public class CustomIdentityUser : IdentityUser<Guid>
{
    public CustomIdentityUser()
    {
        Id = Guid.NewGuid();
        DataEHoraCriacao = DateTime.Now;
        Status = true;
    }
    public string CPF { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Logradouro { get; set; }
    public uint Numero { get; set; }
    public string Complemento { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string UF { get; set; }
    public string CEP { get; set; }
    public bool Status { get; set; }
    public DateTime DataEHoraCriacao { get; set; }
    public DateTime DataEHoraModificacao { get; set; }
    public string TipoDeUsuario { get; set; }
}
