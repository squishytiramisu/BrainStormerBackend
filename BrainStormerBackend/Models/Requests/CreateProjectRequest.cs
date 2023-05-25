namespace BrainStormerBackend.Models.Requests
{
    public class CreateProjectRequest
    {
        public string Name { get; set; }

        public string ProjectDescription { get; set; }

        public Boolean Visibility { get; set; }

    }
}
