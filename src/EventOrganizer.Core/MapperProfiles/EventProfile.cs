using AutoMapper;
using EventOrganizer.Core.DTO;
using EventOrganizer.Domain.Models;

namespace EventOrganizer.Core.MapperProfiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<EventModel, EventDTO>()
                .ForMember(
                    dest => dest.EventTags,
                    opt => opt.MapFrom(src => src.EventTags.Select(t => t.Keyword).ToArray())
                )
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.StartDate)))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.HasValue
                    ? DateOnly.FromDateTime(src.EndDate.Value)
                    : (DateOnly?)null))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => TimeOnly.FromTimeSpan(src.StartTime)))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => TimeOnly.FromTimeSpan(src.StartTime)));

            CreateMap<EventModel, EventDetailDTO>()
                .ForMember(
                    dest => dest.EventTags,
                    opt => opt.MapFrom(src => src.EventTags.Select(t => t.Keyword).ToArray())
                )
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.StartDate)))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.HasValue
                    ? DateOnly.FromDateTime(src.EndDate.Value)
                    : (DateOnly?)null))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => TimeOnly.FromTimeSpan(src.StartTime)))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => TimeOnly.FromTimeSpan(src.StartTime)))
                .ForMember(dest => dest.EventTags, opt => opt.MapFrom(src => src.EventTags.Select(t => t.Keyword)));
        }
    }
}
