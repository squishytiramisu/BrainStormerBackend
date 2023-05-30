namespace BrainStormerBackend.Models.Entities
{
    public class Issue
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public Project Project { get; set; }

        public int ProjectId { get; set; }

        public List<Brainstorm> Brainstorms { get; set; }

    }
}
