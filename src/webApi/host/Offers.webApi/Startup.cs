using System;
using System.Linq;
using canalonline.data;
using crossapp.file.logging;
using crossapp.repository;
using crossapp.rules;
using crossapp.services;
using crossapp.unitOfWork;
using domain.rules;
using domain.rules.offer;
using domain.services._base;
using entities;
using entities._base;
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

namespace Offers.webApi
{
    public static class ExtensionHelper
    {
        public static IServiceCollection AddService<T>(this IServiceCollection services) where T : class, IEntity
        {
            services.AddTransient<IEntityRepository<T>, canalonline.data._base.RepositoryBase<T>>();
            services.AddTransient<IRepository<T>>(x => x.GetService<IEntityRepository<T>>());

            services.AddTransient<IRuleProcessor<T>, RuleProcessor<T>>();

            services.AddTransient<IService<T>, ServiceGeneric<T>>();

            return services;
        }
    }

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

            services.AddService<Client>();
            services.AddService<OffersClients>();

            //Añadimos las reglas
            services.AddTransient<IRule<OffersClients>, Checkstate>();
            services.AddTransient<IRule<OffersClients>, CheckMaxState>();


            services.AddTransient<IEntityUnitOfWork, UnitOfWork>();
            services.AddTransient<IUnitOfWork>(x=>x.GetService<IEntityUnitOfWork>());




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
                odataBuilder.EntitySet<Client>("Clients");

                odataBuilder.EntityType<Client>()
                 .Action("Offers")
                 .Parameter<Guid>("key");

                odataBuilder.EntityType<Client>()
                 .Action("SetState")
                 .Parameter<Guid>("key");


                return odataBuilder.GetEdmModel();
            }
        }
    }
}
