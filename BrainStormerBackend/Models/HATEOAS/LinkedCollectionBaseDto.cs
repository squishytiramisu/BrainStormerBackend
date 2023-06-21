namespace BrainStormerBackend.Models.HATEOAS
{
    public class LinkedCollectionBaseDto<T> : LinkedBaseDto
        where T : LinkedBaseDto
    {
        public IEnumerable<T> Value { get; set; }

        public LinkedCollectionBaseDto(IEnumerable<T> value)
        {
            this.Value = value;
        }
    }
}
