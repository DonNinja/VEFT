using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TechnicalRadiation.Repositories.Contexts;
using Microsoft.EntityFrameworkCore.Design;
using TechnicalRadiation.Services.Interfaces;
using TechnicalRadiation.Services.Implementations;
using TechnicalRadiation.Repositories.Interfaces;
using TechnicalRadiation.Repositories.Implementations;
using TechnicalRadiation.WebApi.ExceptionHandlerExtensions;


namespace TechnicalRadiation.WebApi
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

            
            services.AddDbContext<NewsDbContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("TechnicalRadiationDatabase"), options =>
                {
                    options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TechnicalRadiation.WebApi", Version = "v1" });
            });

            services.AddTransient<INewsItemService, NewsItemService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<INewsItemRepository, NewsItemRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TechnicalRadiation.WebApi v1"));
            }

            app.UseHttpsRedirection();
            app.UseGlobalExceptionHandler();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
