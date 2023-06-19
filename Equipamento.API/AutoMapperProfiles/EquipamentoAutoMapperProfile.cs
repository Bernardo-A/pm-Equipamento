using AutoMapper;
using Equipamento.API.ViewModels;

namespace Equipamento.API.AutoMapperProfiles
{
    public class EquipamentoAutoMapperProfile : Profile
    {
        public EquipamentoAutoMapperProfile() 
        {
            CreateMap<BicicletaInsertViewModel, BicicletaViewModel>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => 0));
        }
    }
}
