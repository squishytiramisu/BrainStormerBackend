namespace BrainStormerBackend.Models.Entities
{

    public class BrainStorm
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool Visibility { get; set; }

        public int IssueId { get; set; }

        public Issue Issue { get; set; }

        public List<ActionStep> ActionSteps{ get; set; }

        public bool IsChosen { get; set; }

    }
}
