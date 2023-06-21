

namespace BrainStormerBackend.Models.HATEOAS
{
    public class ProjectDto: LinkedBaseDto
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string ProjectDescription { get; set; }

        public bool Visibility { get; set; }

        public List<IssueDto> Issues { get; set; }

    }
}
