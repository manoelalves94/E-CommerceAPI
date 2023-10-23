using AutoMapper;
using E_Commerce_API_.Data.DTOs.CategoriaDto;
using E_Commerce_API_.Models;

namespace E_Commerce_API_.Profiles;

public class CategoriaProfile : Profile
{
	public CategoriaProfile()
	{
		CreateMap<CreateCategoriaDto, Categoria>()
			.ForMember(c => c.Id, opt => opt
			.MapFrom(dto => dto._id))
			.ForMember(c => c.Status, opt => opt
			.MapFrom(dto => dto._status))
			.ForMember(c => c.DataEHoraCriacao, opt => opt
			.MapFrom(dto => dto._dataEHoraCriacao));
		CreateMap<UpdateCategoriaDto, Categoria>()
			.ForMember(c => c.DataEHoraModificacao, opt => opt
			.MapFrom(dto => dto._dataEHoraModificacao));
		CreateMap<Categoria, ReadCategoriaDto>()
			.ForMember(categoriaDto => categoriaDto.Criacao, opt => opt
			.MapFrom(categoria => categoria.DataEHoraCriacao.ToString("g")))
			.ForMember(categoriaDto => categoriaDto.Modificacao, opt => opt
			.MapFrom(categoria => categoria.DataEHoraModificacao == default ? "Não houve modificações." : categoria.DataEHoraModificacao.ToString("g")));
	}
}
