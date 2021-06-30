using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace DogsPowerAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // For Entity Framework
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // For Identity
            // AddIdentity adds cookie based authentication
            // Adds scoped classes for things like UserManager, SignInManager, PasswordHashers etc..
            services.AddIdentity<ApplicationUser, IdentityRole>()
                // Adds UserStore and RoleStore from this context
                // That are consumed by the UserManager and RoleManager
                .AddEntityFrameworkStores<ApplicationDbContext>()
                // Adds a provider that generates unique keys and hashes for things like
                // forgot password links, phone number verification codes etc...
                .AddDefaultTokenProviders();

            // For Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

                // Adding JwtBearer
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    // Set validation parameters
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        // Validate issuer
                        ValidateIssuer = true,
                        // Validate audience
                        ValidateAudience = true,
                        // Set issuer
                        ValidAudience = Configuration["Jwt:Audience"],
                        // Set audience
                        ValidIssuer = Configuration["Jwt:Issuer"],

                        // Set signing key
                        IssuerSigningKey = new SymmetricSecurityKey(
                            // Get our secret key from configuration
                            Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"]))
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // Add Authentication
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
