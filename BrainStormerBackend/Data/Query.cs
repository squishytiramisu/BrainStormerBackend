using BrainStormerBackend.Models.Entities;

namespace BrainStormerBackend.Data
{
    public class Query
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Project> GetProjects([Service] BrainStormerDBContext context) =>
            context.Projects;
    }
}
