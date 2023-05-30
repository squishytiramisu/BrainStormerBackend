namespace BrainStormerBackend.Models.Entities
{
    public class ActionStep
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int IssueId { get; set; }
        public Issue Issue { get; set; }
    }
}
