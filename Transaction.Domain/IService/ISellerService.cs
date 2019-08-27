using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transaction.Domain.AggreagatesModels.Aggregate;

namespace Transaction.Domain.IService
{
    public interface ISellerService
    {
        //Task<TransactionResponse> Execute(SellerData sellerData );
        Task<TransactionResponse> Execute(TransactionModel transactionModel );

        Task<BizResponse> CancelTransaction(TransactionCancelModel transactionCancelModel);
    }
}
