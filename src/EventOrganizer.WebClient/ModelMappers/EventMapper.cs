using EventOrganizer.Domain.Models;
using EventOrganizer.WebClient.Views;
using System;
using System.Collections.Generic;

namespace EventOrganizer.WebClient.ModelMappers
{
    public class EventMapper
    {
        public static EventModel MapViewToModel(EventViewModel eventViewModel)
        {
            throw new NotImplementedException();
        }

        public static EventViewModel MapModelToView(EventModel eventViewModel)
        {
            throw new NotImplementedException();
        }

        public static IList<EventViewModel> MapModelListToPreviewList(IList<EventModel> eventViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
