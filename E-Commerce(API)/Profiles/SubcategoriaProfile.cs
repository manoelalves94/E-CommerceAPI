using AutoMapper;
using E_Commerce_API_.Data.DTOs.SubcategoriaDto;
using E_Commerce_API_.Interfaces;
using E_Commerce_API_.Models;

namespace E_Commerce_API_.Profiles;

public class SubcategoriaProfile : Profile
{
    public SubcategoriaProfile()
    {
        CreateMap<CreateSubcategoriaDto, Subcategoria>();
        CreateMap<Subcategoria, ReadSubcategoriaDto>()
            .ForMember(subcategoriaDto => subcategoriaDto.Categoria, opt => opt
            .MapFrom(subcategoria => subcategoria.Categoria.Nome))
            .ForMember(subcategoriaDto => subcategoriaDto.Criacao, opt => opt
            .MapFrom(subcategoria => subcategoria.DataEHoraCriacao.ToString("g")))
            .ForMember(subcategoriaDto => subcategoriaDto.Modificacao, opt => opt.MapFrom(subcategoria => subcategoria.DataEHoraModificacao == new DateTime() ? "Não houve modificação" : subcategoria.DataEHoraModificacao.ToString("g")));
        CreateMap<UpdateSubcategoriaDto, Subcategoria>();
        CreateMap<ReadSubcategoriaDto, ReadSubcategoriaDto>();
    }
}
