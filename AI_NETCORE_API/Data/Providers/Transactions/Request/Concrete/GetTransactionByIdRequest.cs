using Data.Providers.Transactions.Request.Abstract;

namespace Data.Providers.Transactions.Request.Concrete
{
    public class GetTransactionByIdRequest : IGetTransactionByIdRequest
    {
        public GetTransactionByIdRequest(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
