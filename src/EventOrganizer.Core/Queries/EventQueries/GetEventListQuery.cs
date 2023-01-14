using EventOrganizer.Domain.Models;
using System;
using System.Collections.Generic;

namespace EventOrganizer.Core.Queries.EventQueries
{
    public class GetEventListQuery : IQuery<GetEventListQueryParamters, IList<EventModel>>
    {
        public IList<EventModel> Execute(GetEventListQueryParamters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
