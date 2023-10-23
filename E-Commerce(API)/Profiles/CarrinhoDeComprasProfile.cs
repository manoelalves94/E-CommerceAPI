using AutoMapper;
using E_Commerce_API_.Data.DTOs.CarrinhoDeComprasDto;
using E_Commerce_API_.Models;

namespace E_Commerce_API_.Profiles;

public class CarrinhoDeComprasProfile : Profile
{
	public CarrinhoDeComprasProfile()
	{
		CreateMap<Endereco, CarrinhoDeCompras>();
		CreateMap<CreateCarrinhoDeComprasDto, CarrinhoDeCompras>();
		CreateMap<CarrinhoDeCompras, ReadCarrinhoDeComprasDto>()
			.ForMember(readCarrinho => readCarrinho.Subtotal, opt => opt
			.MapFrom(carrinho => carrinho.Subtotal.ToString("C")))
            .ForMember(readCarrinho => readCarrinho.Produtos, opt => opt
			.Ignore())
			.ForPath(readCarrinhoDto => readCarrinhoDto.EnderecoDeEntrega.Logradouro, opt => opt
			.MapFrom(carrinho => carrinho.Logradouro))
            .ForPath(readCarrinhoDto => readCarrinhoDto.EnderecoDeEntrega.Numero, opt => opt
            .MapFrom(carrinho => carrinho.Numero.ToString()))
            .ForPath(readCarrinhoDto => readCarrinhoDto.EnderecoDeEntrega.Complemento, opt => opt
            .MapFrom(carrinho => carrinho.Complemento))
            .ForPath(readCarrinhoDto => readCarrinhoDto.EnderecoDeEntrega.Bairro, opt => opt
            .MapFrom(carrinho => carrinho.Bairro))
            .ForPath(readCarrinhoDto => readCarrinhoDto.EnderecoDeEntrega.Cidade, opt => opt
            .MapFrom(carrinho => carrinho.Cidade))
            .ForPath(readCarrinhoDto => readCarrinhoDto.EnderecoDeEntrega.UF, opt => opt
            .MapFrom(carrinho => carrinho.UF))
            .ForPath(readCarrinhoDto => readCarrinhoDto.EnderecoDeEntrega.CEP, opt => opt
            .MapFrom(carrinho => carrinho.CEP))
			.ForMember(readCarrinho => readCarrinho.CupomDeDesconto, opt => opt
			.MapFrom(carrinho => carrinho.CupomDeDesconto.Cupom))
			.ForMember(readCarrinho => readCarrinho.Desconto, opt => opt
			.MapFrom(carrinho => carrinho.CupomDeDesconto.TipoDeDesconto == nameof(TipoDeDesconto.Percentual) ? carrinho.CupomDeDesconto.ValorDoDesconto + "%" : carrinho.CupomDeDesconto.ValorDoDesconto.ToString("C")))
			.ForMember(readCarrinho => readCarrinho.Total, opt => opt
			.MapFrom(carrinho => carrinho.Total.ToString("C")));
		CreateMap<UpdateCarrinhoDeComprasDto, ProdutoNoCarrinho>()
			.ForMember(p => p.ProdutoId, opt => opt
			.Ignore());
		CreateMap<AddEnderecoDeEntregaDto, CarrinhoDeCompras>();
	}
}
