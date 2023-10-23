using E_Commerce_API_.Models;

namespace E_Commerce_API_.Constants;

public class ErrorMessages
{
    public const string ProdutoSemEstoque = "Não existe estoque para o produto informado.";

    public const string ProdutoInativo = "O produto informado está inativo e não pode ser adicionado ao carrinho.";

    public const string ProdutoConstaNoCarrinho = "O Id de produto informado já está no carrinho.";

    public const string ProdutoNaoConstaNoCarrinho = "O Id de produto informado não está cadastrado no Id de carrinho informado.";

    public const string QuantidadeNaoAlterada = "A quantidade informada é a mesma cadastrada. Alteração não realizada.";

    public const string ProdutoNaoEstaMaisDisponivel = "Este item não está mais disponível. O produto será retirado do carrinho.";

    public const string ProdutoInativoNoCarrinho = "Este item não está mais disponível pois está inativo. O produto será retirado do carrinho.";

    public const string CEPInvalido = "CEP informado não é válido.";

    public const string CarrinhoNaoExiste = "O Id de Carrinho de Compras informado não existe.";

    public const string CarrinhoVazio = "O carrinho está vazio.";
}
