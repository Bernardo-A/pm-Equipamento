using AutoMapper;
using Equipamento.API.ViewModels;

namespace Equipamento.API.AutoMapperProfiles
{
    public class EquipamentoAutoMapperProfile : Profile
    {
        public EquipamentoAutoMapperProfile() 
        {
            CreateMap<TotemInsertViewModel, TotemViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => 0))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); ;
            CreateMap<TrancaInsertViewModel, TrancaViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => 0))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); ;
        }
    }
}
