using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Transaction.Domain.AggreagatesModels.BuyerAggregate;
using Transaction.Infrastructure.Repository;

namespace Transaction.API.Application.Command
{
    public class BuyTransactionCommandHandler : IRequestHandler<BuyTransactionCommand,bool>
    {
        private readonly IBuyerRepository _buyerRepository;
        private readonly IMediator _mediator;
        private readonly ILogger<BuyTransactionCommandHandler> _logger;

        public BuyTransactionCommandHandler(IMediator mediator,
            IBuyerRepository buyerRepository,
            ILogger<BuyTransactionCommandHandler> logger)
        {
            _buyerRepository = buyerRepository;
            _mediator = mediator;
            _logger = logger;
        }
        public async Task<bool> Handle(BuyTransactionCommand request, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
            var buyer = new Buyer(request.Id.ToString(), request.Price, request.Quantity);
            //foreach (var item in request.BuyItem)
            //{
            //    buyer.
            //}
            // _logger.LogInformation("----- Creating Order - Order: {@Order}",buyer);
            _buyerRepository.Add(buyer);
            return await _buyerRepository.UnitOfWork
                .SaveEntitiesAsync();
        }
    }
}
