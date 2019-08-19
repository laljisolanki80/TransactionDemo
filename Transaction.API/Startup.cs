using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventBusRabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Transaction.API.Infrastructure.AutofacModule;
using Transaction.Domain.AggreagatesModels.BuyerAggregate;
using Transaction.Infrastructure.Database;
using Transaction.Infrastructure.Repository;

namespace Transaction.API
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
            services.AddTransient<IBuyerRepository, BuyerRepository>(); // add Akshay
            services.AddOptions();
            services.AddDbContext<TransactionDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
            
            //System.Console.WriteLine("ConnectionString");

            //services.AddEntityFrameworkStores<BuyerDbContext>();
            var container = new ContainerBuilder();
            container.Populate(services);
            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new ApplicationModule(Configuration["ConnectionString"]));
            new AutofacServiceProvider(container.Build());
        }

        private void AddRabbitMQConfigs(IServiceCollection services)
        {
            //configure rabbitMQ connection By Lalji 13/08/2019
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var factory = new ConnectionFactory()
                {
                    HostName = Configuration["EventBusConnection"]
                };

                if (!string.IsNullOrEmpty(Configuration["EventBusUserName"]))
                {
                    factory.UserName = Configuration["EventBusUserName"];
                }

                if (!string.IsNullOrEmpty(Configuration["EventBusPassword"]))
                {
                    factory.Password = Configuration["EventBusPassword"];
                }

                return new DefaultRabbitMQPersistentConnection(factory);
            });

            //Configure rabbitmq queue and channel  -Lalji 13-08-2019
            services.AddSingleton<IRabbitMQOperation>(sp =>
            {
                var connection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var queueName = Configuration["GlobalQueue"];

                return new RabbitMQOperations(connection, queueName);
            });

        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                //add appsettings.json by Lalji 12:40PM 12/08/2019
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Transaction}/{action=BuyTrade}");
            });
        }
    }
}
