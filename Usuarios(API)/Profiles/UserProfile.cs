using AutoMapper;
using Usuarios_API_.Data.DTOs;
using Usuarios_API_.Models;

namespace Usuarios_API_.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
	{
		CreateMap<CreateUserDto, CustomIdentityUser>()
			.ForMember(i => i.UserName, opt => opt
			.MapFrom(c => c.Nome));
		CreateMap<Endereco, CustomIdentityUser>();
		CreateMap<UpdateUserDto, CustomIdentityUser>()
			.ForMember(i => i.UserName, opt => opt
			.MapFrom(c => c.Nome))
			.ForMember(i => i.DataEHoraModificacao, opt => opt
			.MapFrom(u => u._dataAtualizacao));
		CreateMap<CustomIdentityUser, ReadUserDto>()
			.ForMember(cDto => cDto.DataNascimento, opt => opt
            .MapFrom(c => c.DataNascimento.ToString("dd/MM/yyyy")))
            .ForMember(cDto => cDto.Criacao, opt => opt
            .MapFrom(c => c.DataEHoraCriacao.ToString("dd/MM/yyyy HH:mm:ss")))
            .ForMember(cDto => cDto.Modificacao, opt => opt
            .MapFrom(c => c.DataEHoraModificacao == default ? "Não houve modificações."
            : c.DataEHoraModificacao.ToString("dd/MM/yyyy HH:mm:ss")))
			.ForMember(c => c.Nome, opt => opt
            .MapFrom(i => i.UserName));
    }
}
