

namespace BrainStormerBackend.Models.HATEOAS
{
    public class IssueDto: LinkedBaseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public ProjectDto Project { get; set; }

        public int ProjectId { get; set; }

        public List<BrainStormDto> BrainStorms { get; set; }

    }
}
