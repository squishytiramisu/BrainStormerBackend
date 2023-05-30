using BrainStormerBackend.Models.Entities;

namespace BrainStormerBackend.Models.Requests
{
    public class CreateActionStepRequest
    {
        public string Description { get; set; }

        public int BrainStormId { get; set; }
    }
}
