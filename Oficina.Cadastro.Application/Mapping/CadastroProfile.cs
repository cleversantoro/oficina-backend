using AutoMapper;
using Oficina.Cadastro.Application.Dtos;
using Oficina.Cadastro.Domain.Entities;

namespace Oficina.Cadastro.Application.Mapping;

public class CadastroProfile : Profile
{
    public CadastroProfile()
    {
        CreateMap<Cliente, ClienteDto>()
            .ForCtorParam("Email", opt => opt.MapFrom(x => x.Email.Value))
            .ForCtorParam("Telefone", opt => opt.MapFrom(x => x.Telefone.Value));

        CreateMap<Moto, MotoDto>()
            .ForCtorParam("Placa", opt => opt.MapFrom(x => x.Placa.Value));
    }
}
