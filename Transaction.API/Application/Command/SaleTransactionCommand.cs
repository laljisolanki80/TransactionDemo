using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Transaction.API.Application.Command
{
    public class SaleTransactionCommand:IRequest<bool>
    {
        [DataMember]
        private readonly List<SellDTO> _sellItem;
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public decimal Quantity { get; set; }
        [DataMember]
        public IEnumerable<SellDTO> SellItem => _sellItem;
    }
    public class SellDTO
    {
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
    }
}
