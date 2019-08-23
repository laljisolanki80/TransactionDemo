using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Transaction.Domain.AggreagatesModels.Aggregate;
using Transaction.Domain.SeedWork;

namespace Transaction.Infrastructure.DataBase
{
    public class TransactionDbContext:DbContext,IUnitOfWork
    {
        private readonly IMediator _mediator;

        public TransactionDbContext(DbContextOptions<TransactionDbContext> options,
            IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        public DbSet<SellerData> SellerDatas { get; set; }
        public DbSet<BuyerData> BuyerDatas { get; set; }
        public DbSet<Ledger> Ledgers { get; set; }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);            
            var result = await base.SaveChangesAsync();

            return true;
        }
    }
}
