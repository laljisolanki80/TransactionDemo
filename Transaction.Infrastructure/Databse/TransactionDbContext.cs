using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Transaction.Domain.AggreagatesModels.BuyerAggregate;
using Transaction.Domain.SeedWork;

namespace Transaction.Infrastructure.Database
{
    //IUnitOfWork Add letter by akshay
    public class TransactionDbContext : DbContext, IUnitOfWork   //DbContext class rename by Lalji previous BuyerDbContext
    {
        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

        //add BuyerDbContext by Lalji 5:10PM 10/08/2019
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options) { }

        public DbSet<BuyerInformartion> BuyerInformartions { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<BuyerTransactionStatus> BuyerTransactionStatus { get;set;}

        //public DbSet<BuyerTransactionStatus> BuyerTransactionStatuse { get; set; }
        // Add by akshay
        public IDbContextTransaction GetCurrentTransaction => _currentTransaction;

        public TransactionDbContext(DbContextOptions<TransactionDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));


            System.Diagnostics.Debug.WriteLine("OrderingContext::ctor ->" + this.GetHashCode());
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);
            var result = await base.SaveChangesAsync();
            return true;
        }
    }
    public class BuyerContextDesignFactory : IDesignTimeDbContextFactory<TransactionDbContext>
    {
        public TransactionDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TransactionDbContext>()
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=TransactioDb;Trusted_Connection=True;Integrated Security=True");

            return new TransactionDbContext(optionsBuilder.Options, new NoMediator());
        }

        class NoMediator : IMediator
        {

            public Task Publish(object notification, CancellationToken cancellationToken = default)
            {
                return Task.CompletedTask;
            }

            public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
            {
                throw new NotImplementedException();
            }

            public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
            {
                return Task.FromResult<TResponse>(default(TResponse));
            }

            public Task Send(IRequest request, CancellationToken cancellationToken = default(CancellationToken))
            {
                return Task.CompletedTask;
            }
        }
    }
}
