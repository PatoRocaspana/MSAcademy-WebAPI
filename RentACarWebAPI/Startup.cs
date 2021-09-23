using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RentACarWebAPI.Interfaces.Repositories;
using RentACarWebAPI.Interfaces.Services;
using RentACarWebAPI.Repositories;
using RentACarWebAPI.Services;

namespace RentACarWebAPI
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
            services.AddDbContext<RentACarDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Database")
            ));

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RentACarWebAPI", Version = "v1" });
            });

            services.AddTransient<ICarRepository, CarRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IRentalRepository, RentalRepository>();

            services.AddTransient<ICarService, CarService>();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IRentalService, RentalService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RentACarWebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
