using System;
using System.Net;
using System.Net.Http;
using AngleSharp.Html.Parser;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            services.AddScoped<IScrapper, Scrapper>();
            services.AddScoped<IMuzfan, Muzfan>();
            services.AddScoped<IMixmuz, Mixmuz>();
            services.AddScoped<IMusicRepository, MusicRepository>();
            services.AddScoped<IDownloadRepository, DownloadRepository>();
            services.AddScoped<WebClient>();
            services.AddScoped<HttpClient>();
            services.AddSignalR();

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
            app.UseCors("CorsPolicy");
            app.UseAuthorization();
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
