using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace E_Commerce_API_.Data.DTOs;

public class LinkDto
{
    public string Href { get; private set; }
    public string Rel { get; private set; }
    public string Method { get; private set; }
    public List<string> Arguments { get; private set; }

    public LinkDto(string href, string rel, string method, List<string> arguments = null)
    {
        Href = href;
        Rel = rel;
        Method = method;
        Arguments = arguments;
    }

    public bool ShouldSerializeArguments()
    {
        return Arguments != null;
    }

    public static List<LinkDto> CreateLinks(ControllerBase controller, Guid id, string type)
    {
        var links = new List<LinkDto>
        {
            new LinkDto(controller.Url.Link("Get" + type, new { id }),
            "self",
            "GET"),

            new LinkDto(controller.Url.Link("Update" + type, new { id }),
            "self",
            "PUT",
            ArgumentosPut(type)),

            new LinkDto(controller.Url.Link("Delete" + type, new { id }),
            "self",
            "DELETE")
        };

        Log.Information("Criado os links de acesso: {@links}.", links);
        return links;
    }

    private static List<string> ArgumentosPut(string type)
    {
        if (type == "Produto")
        {
            return new List<string> { "nome", "descricao", "peso", "altura", "largura", "comprimento", "valor", "quantidadeEmEstoque", "status", "subcategoriaId", "centroDeDistribuicaoId" };
        }

        if (type == "CentroDeDistribuicao")
        {
            return new List<string> { "nome", "cep", "numero", "complemento", "status" };
        }
       
        return new List<string> { "nome", "status" };
    }
}
