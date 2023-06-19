using Microsoft.AspNetCore.Mvc;

namespace BrainStormerBackend.Models.Requests
{
    public class TokenRequest 
    {
       public string Username { get; set; }

       public string Secret { get; set; }
    }
}
