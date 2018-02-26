using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Sca.Tech.Prototypes.DynamoApi.Framework;
using Sca.Tech.Prototypes.DynamoApi.Framework.Models.Contracts.Services;
using Sca.Tech.Prototypes.DynamoApi.Framework.Repositories;
using Sca.Tech.Prototypes.DynamoApi.Framework.Services;

namespace Sca.Radio.ReferenceApi
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
            services.AddMvc();
            //injecting Settings in the Options accessor model
            services.Configure<AppSettings>(Options =>
            {
                Options.ConnectionString = Configuration.GetSection("DatabaseConnection:ConnectionString").Value;
                Options.Database = Configuration.GetSection("DatabaseConnection:Database").Value;
            });

            services.AddTransient<IDynamoDataRepository, DynamoDataRepository>();
            services.AddTransient<IAdministrationService, AdministrationService>();
            services.AddTransient<ISearchService, SearchService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
