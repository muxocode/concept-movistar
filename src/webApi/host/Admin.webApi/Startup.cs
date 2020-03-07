using System;
using System.Linq;
using canalonline.data;
using crossapp.unitOfWork;
using entities;
using LogHelper;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using domain;
using crossapp.log.logging;

namespace Admin.webApi
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
            services.AddDbContext<movistarContext>();

            services.AddTransient<ILogHandler>(x => new FileTracer(@"C:\LOG\MOVISTAR"));
            services.AddService<Client>();
            services.AddService<Offer>();
            services.AddService<OffersClients>();


            services.AddTransient<IEntityUnitOfWork, UnitOfWork>();
            services.AddTransient<IUnitOfWork>(x => x.GetService<IEntityUnitOfWork>());

            services.AddControllers(options =>
            {
                options.EnableEndpointRouting = false;
            });

            services.AddOData();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.Select().Filter().Count().Expand().OrderBy().MaxTop(100);
                routeBuilder.MapODataServiceRoute("api", "api", GetEdmModel());
            });

            IEdmModel GetEdmModel()
            {
                var odataBuilder = new ODataConventionModelBuilder();



                odataBuilder.EntitySet<Client>("Clients");
                odataBuilder.EntityType<Client>()
                .Action("Offers")
                .Parameter<Guid>("key");


                return odataBuilder.GetEdmModel();
            }
        }
    }
}
