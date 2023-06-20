using BrainStormerBackend.Models.Entities;
using HotChocolate.Authorization;


namespace BrainStormerBackend.Data
{
    public class Query
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [Authorize]
        public IQueryable<Project> GetProjects([Service] BrainStormerDBContext context) =>
            context.Projects;
    }
}
