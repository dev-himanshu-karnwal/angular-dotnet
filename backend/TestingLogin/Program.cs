
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Text;
using TestingLogin.Data;

namespace TestingLogin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseKestrel(options => options.ListenAnyIP(5000));
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AuthDbContext>(
            options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBMS")));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(
                options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<AuthDbContext>().AddDefaultTokenProviders();

            builder.Services.AddAuthentication(
               options =>
               {
                   options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                   options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
               })
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = false,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = builder.Configuration["Jwt:Issuer"],
                       IssuerSigningKey = new SymmetricSecurityKey(
             Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
                       ClockSkew = TimeSpan.Zero,
                       NameClaimType = ClaimTypes.NameIdentifier // Explicitly map name identifier
                   };
                   options.MapInboundClaims = false;
               });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
                options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));

            });
            //adding CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontendApp",
                    policy => policy
                        .AllowAnyOrigin()// your frontend URL
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });
            if (builder.Environment.IsEnvironment("Test"))
            {
                builder.Services.AddDbContext<AuthDbContext>(options =>
         options.UseSqlServer(builder.Configuration.GetConnectionString("DBMS")));
                
            }

            var app = builder.Build();

          
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseHttpsRedirection();
            app.UseCors("AllowFrontendApp");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            app.Urls.Add("http://0.0.0.0:5000");
            app.Run();
        }
    }
}
