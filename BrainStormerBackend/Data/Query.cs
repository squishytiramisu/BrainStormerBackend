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



        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [Authorize]
        public IQueryable<BrainStorm> GetBrainStorms([Service] BrainStormerDBContext context) =>
            context.BrainStorms;


        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [Authorize]
        public IQueryable<Issue> GetIssues([Service] BrainStormerDBContext context) =>
            context.Issues;

    }
}
