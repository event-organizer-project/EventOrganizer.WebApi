using System.Collections.Generic;
using System;

namespace EventOrganizer.WebClient.Views
{
    public class EventViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public int Status { get; set; }

        public IList<int> EventTagIds { get; set; }
    }
}
