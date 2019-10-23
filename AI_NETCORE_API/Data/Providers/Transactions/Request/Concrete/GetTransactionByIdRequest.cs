using Domain.Providers.Transactions.Request.Abstract;

namespace Domain.Providers.Transactions.Request.Concrete
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
