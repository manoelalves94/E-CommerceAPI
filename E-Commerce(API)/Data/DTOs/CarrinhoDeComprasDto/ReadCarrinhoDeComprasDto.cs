namespace E_Commerce_API_.Data.DTOs.CarrinhoDeComprasDto;

public class ReadCarrinhoDeComprasDto
{
    public ReadCarrinhoDeComprasDto()
    {
        Produtos = new List<ResumoDoProduto>();
    }

    public Guid Id { get; set; }
    public List<ResumoDoProduto> Produtos { get; set; }
    public string Subtotal { get; set; }
    public string CupomDeDesconto { get; set; }
    public string Desconto { get; set; }
    public string Total { get; set; }
    public EnderecoDeEntrega EnderecoDeEntrega { get; set; }

    public bool ShouldSerializeEnderecoDeEntrega()
    {
        return EnderecoDeEntrega.CEP != null;
    }

    public bool ShouldSerializeCupomDeDesconto()
    {
        return CupomDeDesconto != null;
    }
    public bool ShouldSerializeDesconto()
    {
        return CupomDeDesconto != null;
    }
    public bool ShouldSerializeSubtotal()
    {
        return CupomDeDesconto != null;
    }
}

public class ResumoDoProduto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string ValorUnitario { get; set; }
    public uint Quantidade { get; set; }
    public string? Atencao { get; set; }


    public bool ShouldSerializeAtencao()
    {
        return Atencao != null;
    }
}

public class EnderecoDeEntrega
{
    public string Logradouro { get; set; }
    public string Numero { get; set; }
    public string Complemento { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string UF { get; set; }
    public string CEP { get; set; }
}


