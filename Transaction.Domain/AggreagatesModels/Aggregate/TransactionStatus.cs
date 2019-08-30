
namespace Transaction.Domain.AggreagatesModels.Aggregate
{
    public enum TransactionStatus
    {
        //Hold = 0,
        //Success = 1,
        //OperatorFail = 2,
        //SystemFail = 3,
        //PartialSettle = 4,
        //Refunded = 5
        Initialise = 0,  //- only entry
        Success = 1,     //--full settlement1
        ProviderFail = 2,    //--also use for cancellation
        Validationfail = 3,  //-- validation fail
        Pending = 4         //--no settlement found
    }
}
