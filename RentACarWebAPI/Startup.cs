using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RentACarWebAPI.Helpers;
using RentACarWebAPI.Interfaces;
using RentACarWebAPI.Models;
using RentACarWebAPI.Options;
using RentACarWebAPI.Repositories;

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
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RentACarWebAPI", Version = "v1" });
            });

            services.Configure<StorageOptions>(Configuration
                .GetSection(nameof(StorageOptions)));

            services.AddTransient<ICarRepository, CarRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IRentalRepository, RentalRepository>();

            services.AddTransient<IRepositoryHelper<Car>, RepositoryHelper<Car>>();
            services.AddTransient<IRepositoryHelper<Client>, RepositoryHelper<Client>>();
            services.AddTransient<IRepositoryHelper<Rental>, RepositoryHelper<Rental>>();
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
