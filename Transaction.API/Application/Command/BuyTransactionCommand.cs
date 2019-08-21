using MediatR;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.API.Application.Command
{
    public class BuyTransactionCommand : IRequest<bool>
    {


        [DataMember]
        private readonly List<BuyDTO> _buyItem;
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public decimal Quantity { get; set; }
        [DataMember]
        public IEnumerable<BuyDTO> BuyItem => _buyItem;
        public BuyTransactionCommand()
        {
            _buyItem = new List<BuyDTO>();
        }
        public BuyTransactionCommand(Guid id,decimal price,decimal quantity)
        {
            Id = id;
            Price = price;
            Quantity = quantity;
        }
        public class BuyDTO
        {
            public decimal Price { get; set; }
            public decimal Quantity { get; set; }
        }

    }
}
       


