using AutoMapper;
using EventOrganizer.Core.DTO;
using EventOrganizer.Domain.Models;

namespace EventOrganizer.Core.MapperProfiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMapEventModelToEventDTO();
            CreateMapEventModelToEventDetailDTO();
            CreateMapEventDetailDTOToEventModel();
        }

        public void CreateMapEventModelToEventDTO()
        {
            CreateMap<EventModel, EventDTO>()
                .ForMember(
                    dest => dest.EventTags,
                    opt => opt.MapFrom(src => src.EventTags.Select(t => t.Keyword)));
        }

        public void CreateMapEventModelToEventDetailDTO()
        {
            CreateMap<EventModel, EventDetailDTO>()
                .ForMember(
                    dest => dest.EventTags,
                    opt => opt.MapFrom(src => src.EventTags.Select(t => t.Keyword)));
        }

        public void CreateMapEventDetailDTOToEventModel()
        {
            CreateMap<EventDetailDTO, EventModel>()
                .ForMember(
                    dest => dest.TagToEvents,
                    opt => opt.MapFrom((src, dest) => src.EventTags
                        .Select(t => new TagToEvent { Keyword = t , EventModel = dest })))
                .ForMember(dest => dest.EventTags, opt => opt.Ignore())
                .ForMember(dest => dest.Owner, opt => opt.Ignore())
                .ForMember(dest => dest.Members, opt => opt.Ignore());     
        }
    }
}
