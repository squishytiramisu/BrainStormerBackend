namespace BrainStormerBackend.Models.Entities
{
    public class ActionStep
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int BrainStormId { get; set; }
        public BrainStorm BrainStorm{ get; set; }
    }
}
