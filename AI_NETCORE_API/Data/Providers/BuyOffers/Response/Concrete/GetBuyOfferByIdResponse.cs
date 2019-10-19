using Data.BuisnessObject;
using Data.Providers.BuyOffers.Response.Abstract;
using Data.Providers.Common.Enum;

namespace Data.Providers.BuyOffers.Response.Concrete
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
