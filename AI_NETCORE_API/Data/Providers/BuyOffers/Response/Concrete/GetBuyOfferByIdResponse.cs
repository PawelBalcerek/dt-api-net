using Domain.BusinessObject;
using Domain.Providers.BuyOffers.Response.Abstract;
using Domain.Providers.Common.Enum;

namespace Domain.Providers.BuyOffers.Response.Concrete
{
    public class GetBuyOfferByIdResponse : IGetBuyOfferByIdResponse
    {
        public GetBuyOfferByIdResponse()
        {
        }

        public GetBuyOfferByIdResponse(BuyOffer buyOffer)
        {
            if (buyOffer != null)
            {
                ProvideResult = ProvideEnumResult.Success;
                BuyOffer = buyOffer;
            }
            else
            {
                ProvideResult = ProvideEnumResult.NotFound;
            }
        }

        public BuyOffer BuyOffer { get; }

        public ProvideEnumResult ProvideResult { get; }
    }
}
