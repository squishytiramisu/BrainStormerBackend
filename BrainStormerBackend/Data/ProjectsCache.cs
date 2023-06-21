using BrainStormerBackend.Models.Entities;
using LazyCache;
using LazyCache.Providers;
using Microsoft.Extensions.Caching.Memory;

namespace BrainStormerBackend.Data
{
    public class ProjectsCache
    {
        IAppCache cache = new CachingService();
        public List<Project> projects = new List<Project>();

        private readonly BrainStormerDBContext _brainStormerDBContext;
        public ProjectsCache(BrainStormerDBContext brainStormerDbContext)
        {
            _brainStormerDBContext = brainStormerDbContext;
            var provider = new MemoryCacheProvider(
                new MemoryCache(
                    new MemoryCacheOptions()));
            cache = new CachingService(provider);
            projects = _brainStormerDBContext.Projects.ToList();
        }


        public void ResetCache()
        {
            cache.CacheProvider.Dispose();
            var provider = new MemoryCacheProvider(
                               new MemoryCache(
                                                      new MemoryCacheOptions()));
            cache = new CachingService(provider);
        }

        public Project GetProject(int id)
        {
            foreach (var project in projects)
            {
                if (project.Id == id)
                {
                    return project;
                }
                
            }
            return null;
            
        }

        public Project GetProjectFromCache(int id)
        {
            var project = cache.Get<Project>(id.ToString());
            if (project == null)
            {
                project = GetProject(id);
                cache.Add(id.ToString(), project);
            }
            return project;
        }
    }
}
