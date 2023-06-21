namespace BrainStormerBackend.Models.HATEOAS
{
    public abstract class LinkedBaseDto
    {
        public List<LinkDto> Links { get; set; } = new List<LinkDto>();
    }
}
