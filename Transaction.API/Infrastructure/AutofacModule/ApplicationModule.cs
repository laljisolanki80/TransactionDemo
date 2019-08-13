using Autofac;
using EventBus.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Transaction.API.Application.Command;
using Transaction.API.Application.Queries;
using Transaction.Domain.AggreagatesModels.BuyerAggregate;
using Transaction.Infrastructure.Repository;

namespace Transaction.API.Infrastructure.AutofacModule
{
    public class ApplicationModule: Autofac.Module
    {
        public string QueriesConnectionString { get; }

        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;

        }

        protected override void Load(ContainerBuilder builder)
        {

            builder.Register(c => new BuyerQueries(QueriesConnectionString))
                .As<IBuyerQueries>()
                .InstancePerLifetimeScope();

            builder.RegisterType<BuyerRepository>()
                .As<IBuyerRepository>()
                .InstancePerLifetimeScope();

            //builder.RegisterType<OrderRepository>()
            //    .As<IOrderRepository>()
            //    .InstancePerLifetimeScope();

            //builder.RegisterType<RequestManager>()
            //   .As<IRequestManager>()
            //   .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(BuyTransactionCommandHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

        }
    }
}
