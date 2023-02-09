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
                    dest => dest.StartDate,
                    opt => opt.MapFrom(src => DateOnly.FromDateTime(src.StartDate)))
                .ForMember(
                    dest => dest.EndDate,
                    opt => opt.MapFrom(src => src.EndDate.HasValue
                        ? DateOnly.FromDateTime(src.EndDate.Value)
                        : (DateOnly?)null))
                .ForMember(
                    dest => dest.StartTime,
                    opt => opt.MapFrom(src => TimeOnly.FromTimeSpan(src.StartTime)))
                .ForMember(
                    dest => dest.EndTime,
                    opt => opt.MapFrom(src => TimeOnly.FromTimeSpan(src.StartTime)))
                .ForMember(
                    dest => dest.EventTags,
                    opt => opt.MapFrom(src => src.EventTags.Select(t => t.Keyword)));
        }

        public void CreateMapEventModelToEventDetailDTO()
        {
            CreateMap<EventModel, EventDetailDTO>()
                .ForMember(
                    dest => dest.StartDate,
                    opt => opt.MapFrom(src => DateOnly.FromDateTime(src.StartDate)))
                .ForMember(
                    dest => dest.EndDate,
                    opt => opt.MapFrom(src => src.EndDate.HasValue
                        ? DateOnly.FromDateTime(src.EndDate.Value)
                        : (DateOnly?)null))
                .ForMember(
                    dest => dest.StartTime,
                    opt => opt.MapFrom(src => TimeOnly.FromTimeSpan(src.StartTime)))
                .ForMember(
                    dest => dest.EndTime,
                    opt => opt.MapFrom(src => TimeOnly.FromTimeSpan(src.StartTime)))
                .ForMember(
                    dest => dest.EventTags,
                    opt => opt.MapFrom(src => src.EventTags.Select(t => t.Keyword)));
        }

        public void CreateMapEventDetailDTOToEventModel()
        {
            CreateMap<EventDetailDTO, EventModel>()
                .ForMember(
                    dest => dest.StartDate,
                    opt => opt.MapFrom(src => src.StartDate.ToDateTime(TimeOnly.MinValue)))
                .ForMember(
                    dest => dest.EndDate,
                    opt => opt.MapFrom(src => src.EndDate.HasValue
                        ? src.EndDate.Value.ToDateTime(TimeOnly.MinValue)
                        : (DateTime?)null))
                .ForMember(
                    dest => dest.StartTime, 
                    opt => opt.MapFrom(src => src.StartTime.ToTimeSpan()))
                .ForMember(
                    dest => dest.EndTime,
                    opt => opt.MapFrom(src => src.StartTime.ToTimeSpan()))

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
