using System;
using System.Net.Http;
using CognitiveApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CognitiveApp
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddTransient<ILanguageService, LanguageService>();
            services.AddTransient<ISentimentService, SentimentService>();
           
            services.AddHttpClient<ISentimentService, SentimentService>("SentimentClient", c =>
            {
                c.BaseAddress = new Uri("http://sentiment.api:5000/");
            });
            services.AddHttpClient<ILanguageService, LanguageService>("LanguageClient", c =>
            {
                c.BaseAddress = new Uri("http://language.api:5000/");
            });

            services.AddHttpClient<ICustomVision, CustomVision>("CustomVisionClient", c =>
            {
                c.BaseAddress = new Uri("http://customvision.api/");
                c.DefaultRequestHeaders.Add("Prediction-Key", "eebf9f63e0c94fd295ab462a8fd93bac");
            });

            services.AddTransient<ICustomVision, CustomVision>(service =>
            {
                var clientFactory = service.GetRequiredService<IHttpClientFactory>();
                return new CustomVision(clientFactory.CreateClient("CustomVisionClient"), "1cec166c-ab17-4ec1-af53-5984e6e8ee71");
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
