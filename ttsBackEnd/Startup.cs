using System;
using System.Net;
using System.Net.Http;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ttsBackEnd.Data;
using ttsBackEnd.HubConfig;
using ttsBackEnd.Models;
using ttsBackEnd.Services;

namespace ttsBackEnd
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
            services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", Builder => Builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
            services.Configure<Sources>(Configuration.GetSection("Sources"));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IScrapper, Scrapper>();
            services.AddScoped<IMuzfan, Muzfan>();
            services.AddScoped<IMixmuz, Mixmuz>();
            services.AddScoped<IMusicRepository, MusicRepository>();
            services.AddScoped<IDownloadRepository, DownloadRepository>();
            services.AddScoped<ILoggerRepository, LoggerRepository>();
            services.AddScoped<WebClient>();
            services.AddScoped<HttpClient>();
            services.AddSignalR();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("CorsPolicy");
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<StatusHub>("/status");
                endpoints.MapControllers();
                // endpoints.MapFallbackToController("Index", "Fallback");
            });
        }
    }
}
