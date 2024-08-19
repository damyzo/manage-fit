using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
