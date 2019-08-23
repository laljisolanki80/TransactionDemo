using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.Domain.AggreagatesModels.Aggregate;

namespace Transaction.Domain.IService
{
    public interface IBuyerService
    {
        //Task<TransactionResponse> Execute(BuyerData buyerData);
        Task<TransactionResponse> Execute(TransactionModel transactionModel);
    }
}
