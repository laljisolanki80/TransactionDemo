using EventBusRabbitMQ.EventHandlers;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Interfaces;
using EventBusRabbitMQ.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Transaction.Domain.IRepository;
using Transaction.Domain.IService;
using Transaction.Infrastructure.DataBase;
using Transaction.Infrastructure.Repository;
using Transaction.Infrastructure.Service;

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
            services.AddTransient<ISellerRepository, SellerRepository>();
            services.AddTransient<IBuyerRepository, BuyerRepository>();
            services.AddTransient<ISellerService, SellerService>();
            services.AddTransient<IBuyerService, BuyerService>();
            services.AddTransient<ILedgerRepository, LedgerRepository>();
            services.AddOptions();
            services.AddDbContext<TransactionDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
            services.AddMediatR(typeof(Startup));
            AddRabbitMQConfigs(services);

        }

        private void AddRabbitMQConfigs(IServiceCollection services)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
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

                return new DefaultRabbitMQPersistentConnection(factory, logger);
            });


            //Configure rabbitmq queue and channel  -Lalji 13-08-2019
            services.AddSingleton<IEventBus, RabbitMQEventBus>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();

                return new RabbitMQEventBus(rabbitMQPersistentConnection, logger);
            });



        }
        public void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            // eventBus.Subscribe<IEventBus, IEventBusSubscriptionsManager>();    
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               //add appsettings.json by Lalji  12/08/2019
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
