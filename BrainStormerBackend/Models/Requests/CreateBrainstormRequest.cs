namespace BrainStormerBackend.Models.Requests
{
    public class CreateBrainstormRequest
    {
        public string Name { get; set; }
        public bool Visibility { get; set; }
        public int IssueId { get; set; }

    }
}
