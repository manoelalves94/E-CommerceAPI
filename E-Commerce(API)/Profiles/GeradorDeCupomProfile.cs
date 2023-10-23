using AutoMapper;
using E_Commerce_API_.Data.DTOs.GeradorDeCupomDto;
using E_Commerce_API_.Models;

namespace E_Commerce_API_.Profiles;

public class GeradorDeCupomProfile : Profile
{
	public GeradorDeCupomProfile()
	{
		CreateMap<CreateCupomDeDescontoDto, CupomDeDesconto>()
			.ForMember(cupom => cupom.Uso, opt => opt
			.MapFrom(create => create.Uso.ToString()))
			.ForMember(cupom => cupom.TipoDeDesconto, opt => opt
			.MapFrom(create => create.TipoDeDesconto.ToString()));
		CreateMap<CupomDeDesconto, ReadCupomDeDescontoDto>();
	}
}
