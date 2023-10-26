using EventOrganizer.Core.DTO;
using EventOrganizer.Core.Repositories;

namespace EventOrganizer.Core.Queries.TagQueries
{
    public class GetTagListQuery : IQuery<VoidParameters, IList<string>>
    {
        private readonly ITagRepository tagRepository;

        public GetTagListQuery(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository
                ?? throw new ArgumentNullException(nameof(tagRepository));
        }

        public IList<string> Execute(VoidParameters parameters)
        {
            var tags = tagRepository
                .GetAll()
                .Select(x => x.Keyword)
                .ToList();

            return tags;
        }
    }
}
