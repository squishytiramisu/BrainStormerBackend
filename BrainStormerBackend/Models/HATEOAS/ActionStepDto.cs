
namespace BrainStormerBackend.Models.HATEOAS
{
    public class ActionStepDto: LinkedBaseDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int BrainStormId { get; set; }
        public BrainStormDto BrainStorm{ get; set; }
    }
}
