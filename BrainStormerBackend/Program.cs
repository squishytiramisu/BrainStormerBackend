using System.Security.Claims;
using System.Text;
using BrainStormerBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<BrainStormerDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//No authorization
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtSettings:Secret").Value)),
            ValidateLifetime = true,
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true
        };
    }
    );

builder.Services.AddGraphQLServer().AddAuthorization().AddQueryType<Query>().AddProjections().AddFiltering().AddSorting();

builder.Services.AddAuthorization(
    options =>
    {
        options.AddPolicy("User", policy => policy.RequireClaim(ClaimTypes.Role, "User"));
    }
    );

//builder.Services.AddDbContext<BrainStormerDBContext>(options => options.UseSqlServer())


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();



app.UseCors(policyName =>
policyName.AllowAnyOrigin()
    .AllowAnyHeader()
           .AllowAnyMethod());

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapGraphQL((PathString)"/graphql");

app.MapControllers();


app.Run();
