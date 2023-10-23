using AutoMapper;
using E_Commerce_API_.Data.DTOs.GeradorDeCupomDto;
using E_Commerce_API_.Exceptions;
using E_Commerce_API_.Interfaces;
using E_Commerce_API_.Models;

namespace E_Commerce_API_.Services;

public class GeradorDeCupomService : IGeradorDeCupomService
{
    private readonly IGeradorDeCupomDAO _geradorDeCupomDAO;
    private readonly IMapper _mapper;

    public GeradorDeCupomService(IGeradorDeCupomDAO geradorDeCupomDAO, IMapper mapper)
    {
        _geradorDeCupomDAO = geradorDeCupomDAO;
        _mapper = mapper;
    }

    public void AtivarOuInativarCupom(string cupom, bool status)
    {
        CupomDeDesconto c = _geradorDeCupomDAO.BuscarCupom(cupom);

        c.Status = status;

        _geradorDeCupomDAO.UpdateCupom(c);
    }

    public ReadCupomDeDescontoDto GerarCupomDeDesconto(CreateCupomDeDescontoDto create)
    {
        if (create.ValorDoDesconto <= 0)
        {
            throw new BadRequestException("O Valor Do Desconto deve ser maior que zero.");
        }

        var cupom = _mapper.Map<CupomDeDesconto>(create);

        _geradorDeCupomDAO.GerarCupomDeDesconto(cupom);

        var readCupom = _mapper.Map<ReadCupomDeDescontoDto>(cupom);

        return readCupom;
    }

    public CupomDeDesconto GetCupomDeDesconto(string cupom)
    {
        return _geradorDeCupomDAO.GetCupomDeDesconto(cupom);
    }

    public List<ReadCupomDeDescontoDto> ListarCupons()
    {
        var listaDeCupons = _geradorDeCupomDAO.GetCuponsDeDesconto();

        var listaDeReadCupons = _mapper.Map<List<ReadCupomDeDescontoDto>>(listaDeCupons);

        return listaDeReadCupons;
    }
}
