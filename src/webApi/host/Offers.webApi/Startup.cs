using System.Linq;
using canalonline.data;
using crossapp.log.logging;
using crossapp.rules;
using crossapp.unitOfWork;
using domain;
using domain.rules.offer;
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
using movistar.model.bussines;
using movistar.userPreferences;

namespace Offers.webApi
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

            services.AddTransient<ILogHandler>(x=>new FileTracer(@"C:\LOG\MOVISTAR"));

            services.AddService<Offer>();
            services.AddService<OfferType>();

            services.AddService<OffersClients>();

            //Añadimos las reglas
            services.AddTransient<IRule<OffersClients>, Checkstate>();
            services.AddTransient<IRule<OffersClients>, CheckMaxState>();


            services.AddTransient<IEntityUnitOfWork, UnitOfWork>();
            services.AddTransient<IUnitOfWork>(x=>x.GetService<IEntityUnitOfWork>());

            //Añadimos las librerías externas

            //La razón para utilizar un patrón builder, es por si tenemos que pasarle configuración del sistema en el que nos encontramos
            services.AddTransient<IUserPreferencesManager>(x => UserPreferencesBuilder.Create());


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

                odataBuilder.EntitySet<Offer>("Offers");
                odataBuilder.EntitySet<OffersClients>("OffersClients");


                // New code:
                odataBuilder.Function("Preferences")
                    .Returns<IActionResult>();
                  

                /*
                 * odataBuilder.EntitySet<Client>("Clients");
                 * odataBuilder.EntityType<Client>()
                 .Action("Offers")
                 .Parameter<Guid>("key");

                odataBuilder.EntityType<Client>()
                 .Action("SetState")
                 .Parameter<Guid>("key");
                 */

                return odataBuilder.GetEdmModel();
            }
        }
    }
}
