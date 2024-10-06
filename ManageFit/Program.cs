
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Services;
using Storage.DatabaseContext;
using Storage.Repositories.Client;
using Storage.Repositories.Client.Interface;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Register repositories
builder.Services.AddTransient<IClientRepository, ClientRepository>();

// Databse setup
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ManageFitDbContext>(options => options.UseSqlServer(connectionString));

// corse
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.WithOrigins("http://example.com",
                                                  "http://www.contoso.com",
                                                  "http://localhost:4200")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                          });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication1", Version = "v1" });
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows()
        {
            Implicit = new OpenApiOAuthFlow()
            {
                AuthorizationUrl = new Uri("https://login.microsoftonline.com/f3910c39-2790-4371-a675-b6fb202ef251/oauth2/v2.0/authorize"),
                TokenUrl = new Uri("https://login.microsoftonline.com/f3910c39-2790-4371-a675-b6fb202ef251/oauth2/v2.0/token"),
                Scopes = new Dictionary<string, string> {
                    {
                        "api://c161d7a8-dbeb-4262-8de8-e3529f173c69/access_as_user", "API permission description"
                    }
                }
            }
        }
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth2"
                },
                Scheme = "oauth2",
                Name = "oauth2",
                In = ParameterLocation.Header
            },
            new List < string > ()
        }
    });
});

//MediatR setup
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(EntitiesMediatR).GetTypeInfo().Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ServicesMediatR).GetTypeInfo().Assembly));

var app = builder.Build();

/*// Configure the HTTP request pipeline.
*/
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.OAuthAppName("Swagger Client");
        options.OAuthClientId("c161d7a8-dbeb-4262-8de8-e3529f173c69");
        options.OAuthClientSecret("client_secret");
        options.OAuthUseBasicAuthenticationWithAccessCodeGrant();
    });
}
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
