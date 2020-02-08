using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Assignment3API.Data;
using Assignment3API.Models.DataManager;
using Microsoft.EntityFrameworkCore;

namespace Assignment3API
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
            services.AddDbContext<NwbaContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("NwbaContext")));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowMyOrigin",
                builder =>
                {
                    builder.WithOrigins("http://localhost:5000")
                    .AllowCredentials()
                    .AllowAnyHeader();
                });
            });

            services.AddTransient<AdminManager>();

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
            app.UseCors("AllowMyOrigin");
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
