namespace BrainStormerBackend.Models.Entities
{
    public class Brainstorm
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool Visibility { get; set; }

        public int IssueId { get; set; }

    }
}
