using AutoMapper;
using E_Commerce_API_.Data.DTOs.ProdutoDto;
using E_Commerce_API_.Models;

namespace E_Commerce_API_.Profiles;

public class ProdutoProfile : Profile
{
	public ProdutoProfile()
	{
        CreateMap<CreateProdutoDto, Produto>()
            .ForMember(p => p.Id, opt => opt
            .MapFrom(dto => dto._id))
            .ForMember(p => p.Status, opt => opt
            .MapFrom(dto => dto._status))
            .ForMember(p => p.DataEHoraCriacao, opt => opt
            .MapFrom(dto => dto._dataEHoraCriacao));
        CreateMap<Produto, ReadProdutoDto>()
            .ForMember(dto => dto.Subcategoria, opt => opt
            .MapFrom(p => p.Subcategoria.Nome))
            .ForMember(dto => dto.Categoria, opt => opt
            .MapFrom(p => p.Subcategoria.Categoria.Nome))
            .ForMember(dto => dto.CentroDeDistribuicao, opt => opt
            .MapFrom(p => p.CentroDeDistribuicao.Nome))
            .ForMember(dto => dto.Criacao, opt => opt
            .MapFrom(p => p.DataEHoraCriacao.ToString("dd/MM/yyyy HH:mm:ss")))
            .ForMember(dto => dto.Modificacao, opt => opt
            .MapFrom(p => p.DataEHoraModificacao == default ? "Não houve modificações." 
            : p.DataEHoraModificacao.ToString("dd/MM/yyyy HH:mm:ss")));
        CreateMap<UpdateProdutoDto, Produto>()
            .ForMember(p => p.DataEHoraModificacao, opt => opt
            .MapFrom(dto => dto._dataEHoraModificacao));
    }
}
