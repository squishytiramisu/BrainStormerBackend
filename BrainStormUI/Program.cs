using BrainStormUI;
using BrainStormUI.Services;
using BrainStormUI.Services.Interfaces;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


    var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7125/") });
builder.Services.AddScoped(sp => new GraphQLHttpClient("https://localhost:7125/graphql", new NewtonsoftJsonSerializer()));



builder.Services.AddScoped<IBrainStormerService, BrainStormerService>();



await builder.Build().RunAsync();
