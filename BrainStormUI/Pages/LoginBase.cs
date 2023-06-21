using BrainStormUI.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Shared.Models;

namespace BrainStormUI.Pages
{
    public class LoginBase: ComponentBase
    {
        [Inject]
        public IBrainStormerService BrainStormerService { get; set; }

        public bool IsLoggedIn { get; set; } = false;

   
    }
}
