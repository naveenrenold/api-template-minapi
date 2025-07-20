using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace DocumentProcessor.Startup
{
    public static class Authentication
    {

        public static void AddAuthenticationDI(this WebApplicationBuilder builder)
        {
            var issuer = builder.Configuration.GetSection("Authentication:Issuer").Value ?? "";
            var audience = builder.Configuration.GetSection("Authentication:Audience").Value ?? "";
                                  
            builder.Services.AddAuthentication().AddJwtBearer(
                options =>
                {
                    options.Authority = issuer;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = issuer, 
                        ValidAudience = audience
                    };
                }
                );
        }

        public static void AddAuthourizationDI(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthorization();
        }
    }
}
