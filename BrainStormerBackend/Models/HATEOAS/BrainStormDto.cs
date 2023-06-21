

namespace BrainStormerBackend.Models.HATEOAS
{

    public class BrainStormDto: LinkedBaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool Visibility { get; set; }

        public int IssueId { get; set; }

        public IssueDto Issue { get; set; }

        public List<ActionStepDto> ActionSteps{ get; set; }

        public bool IsChosen { get; set; }

    }
}
