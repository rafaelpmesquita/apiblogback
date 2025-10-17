using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;
using api.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("Default")));
            services.AddScoped<EspecialidadeService>();
            services.AddScoped<TimeLineService>();
            services.AddScoped<NavBarService>();
            services.AddScoped<UsuarioService>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            // var signingConfigurations = new SigningConfigurations();
            // services.AddSingleton(signingConfigurations);

            // var tokenConfigurations = new TokenConfigurations();
            // new ConfigureFromConfigurationOptions<TokenConfigurations>(
            //     Configuration.GetSection("TokenConfigurations"))
            //         .Configure(tokenConfigurations);
            // services.AddSingleton(tokenConfigurations);


            // services.AddAuthentication(authOptions =>
            // {
            //     authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //     authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            // }).AddJwtBearer(bearerOptions =>
            // {
            //     var paramsValidation = bearerOptions.TokenValidationParameters;
            //     paramsValidation.IssuerSigningKey = signingConfigurations.Key;
            //     paramsValidation.ValidAudience = tokenConfigurations.Audience;
            //     paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

            //     // Valida a assinatura de um token recebido
            //     paramsValidation.ValidateIssuerSigningKey = true;

            //     // Verifica se um token recebido ainda é válido
            //     paramsValidation.ValidateLifetime = true;

            //     // Tempo de tolerância para a expiração de um token (utilizado
            //     // caso haja problemas de sincronismo de horário entre diferentes
            //     // computadores envolvidos no processo de comunicação)
            //     paramsValidation.ClockSkew = TimeSpan.Zero;
            // });

            // // Ativa o uso do token como forma de autorizar o acesso
            // // a recursos deste projeto
            // services.AddAuthorization(auth =>
            // {
            //     auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
            //         .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
            //         .RequireAuthenticatedUser().Build());
            // });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var key = Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization();

            services.AddMvc();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseMvc();
        }


    }
}