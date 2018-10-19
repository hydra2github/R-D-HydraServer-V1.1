using AutoMapper;
using Ocuco.Hydra.WebMVC21.V2.Data.Entities;
using Ocuco.Hydra.WebMVC21.V2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocuco.Hydra.WebMVC21.V2.Data
{
    public class HydraMappingProfile : Profile
    {
        public HydraMappingProfile()
        {
            CreateMap<ArtOrder, ArtOrderViewModel>()
                .ForMember(o => o.OrderId, ex => ex.MapFrom(o => o.Id))
                .ReverseMap();

            CreateMap<ArtOrderItem, ArtOrderItemViewModel>()
                .ReverseMap();
        }
    }
}
