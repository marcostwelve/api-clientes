using AutoMapper;
using clientes.Models;

namespace clientes.DTO_s.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<ClienteDTO, Cliente>().ForMember(logr => logr.Logradouro, opt => opt.MapFrom(src => src.Logradouro));
            CreateMap<Logradouro, LogradouroDTO>().ReverseMap();
        }
    }
}
