using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Transaction.Domain.AggreagatesModels.SellerAggregate;

namespace Transaction.API.Application.Command
{
    public class SaleTransactionCommandHandler:IRequestHandler<SaleTransactionCommand, bool>
    {
        private readonly ISellerRepository _sellerRepository;
        private readonly IMediator _mediator;
        public SaleTransactionCommandHandler(IMediator mediator,
            ISellerRepository sellerRepository)
        {
            _sellerRepository = sellerRepository;
            _mediator = mediator;
        }
        public Task<bool> Handle(SaleTransactionCommand request, CancellationToken cancellationToken)
        {
            var seller = new Seller(request.Id, request.Price, request.Quantity);
            _sellerRepository.Add()
        }
    }
}
