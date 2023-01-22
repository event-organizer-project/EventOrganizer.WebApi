using System.Collections.Generic;
using System;

namespace EventOrganizer.WebApi.Views
{
    public class EventViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public int Status { get; set; }

        public IList<int> EventTagIds { get; set; }
    }
}
