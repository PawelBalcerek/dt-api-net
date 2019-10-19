using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI_NETCORE_API.Infrastructure.BuisnessObjectToModelsConverting.Abstract;
using AI_NETCORE_API.Models.Objects;
using Data.Infrastructure.Logging.Abstract;
using Data.Providers.BuyOffers.Abstract;
using Data.Providers.BuyOffers.Request.Abstract;
using Data.Providers.BuyOffers.Request.Concrete;
using Data.Providers.BuyOffers.Response.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AI_NETCORE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyOffersController : ControllerBase
    {
        private readonly IBusinessObjectToModelsConverter _businessObjectToModelsConverter;
        private readonly ILogger _logger;
        private readonly IBuyOffersProvider _buyOffersProvider;

        public BuyOffersController(IBusinessObjectToModelsConverter businessObjectToModelsConverter, ILogger logger, IBuyOffersProvider buyOffersProvider)
        {
            _businessObjectToModelsConverter = businessObjectToModelsConverter;
            _logger = logger;
            _buyOffersProvider = buyOffersProvider;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200, Type = typeof(BuyOfferModel))]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public ActionResult<BuyOfferModel> GetBuyOfferById(int id)
        {
            try
            {
                IGetBuyOfferByIdRequest getBuyOfferByIdRequest = new GetBuyOfferByIdRequest(id);
                IGetBuyOfferByIdResponse getBuyOfferByIdResponse = _buyOffersProvider.GetBuyOfferById(getBuyOfferByIdRequest);
                return PrepareResponseAfterGetBuyOfferById(getBuyOfferByIdResponse);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }

        private ActionResult<BuyOfferModel> PrepareResponseAfterGetBuyOfferById(IGetBuyOfferByIdResponse getBuyOfferByIdResponse)
        {
            switch (getBuyOfferByIdResponse.ProvideResult)
            {
                case Data.Providers.Common.Enum.ProvideEnumResult.Exception:
                    return StatusCode(500);
                case Data.Providers.Common.Enum.ProvideEnumResult.Success:
                    return Ok(_businessObjectToModelsConverter.ConvertBuyOffer(getBuyOfferByIdResponse.BuyOffer));
                case Data.Providers.Common.Enum.ProvideEnumResult.NotFound:
                    return StatusCode(404);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        

        [HttpGet("")]
        [ProducesResponseType(200, Type = typeof(IList<BuyOfferModel>))]
        [ProducesResponseType(500)]
        public ActionResult<IList<BuyOfferModel>> GetBuyOffers()
        {
            try
            {
                IGetBuyOffersResponse getBuyOffersResponse = _buyOffersProvider.GetBuyOffers();
                return PrepareResponseAfterGetBuyOffers(getBuyOffersResponse);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }

        private ActionResult<IList<BuyOfferModel>> PrepareResponseAfterGetBuyOffers(IGetBuyOffersResponse getBuyOffersResponse)
        {
            switch (getBuyOffersResponse.ProvideResult)
            {
                case Data.Providers.Common.Enum.ProvideEnumResult.Exception:
                    return StatusCode(500);
                case Data.Providers.Common.Enum.ProvideEnumResult.Success:
                    return Ok(getBuyOffersResponse.BuyOffers.ToList()
                        .Select(x => _businessObjectToModelsConverter.ConvertBuyOffer(x)));
                case Data.Providers.Common.Enum.ProvideEnumResult.NotFound:
                    return StatusCode(404);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }
}