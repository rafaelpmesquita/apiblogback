using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Business;

namespace api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "Connection string 'DefaultConnection' was not configured. " +
                    "Set the ConnectionStrings__DefaultConnection environment variable."
                );
            }

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped<EspecialidadeService>();
            services.AddScoped<TimeLineService>();
            services.AddScoped<NavBarService>();
            services.AddScoped<UsuarioService>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
