using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI_NETCORE_API.Infrastructure.BuisnessObjectToModelsConverting.Abstract;
using AI_NETCORE_API.Models.Objects;
using AI_NETCORE_API.Models.Response.SellOffers;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Providers.SellOffers.Abstract;
using Domain.Providers.SellOffers.Request.Abstract;
using Domain.Providers.SellOffers.Request.Concrete;
using Domain.Providers.SellOffers.Response.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AI_NETCORE_API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class SellOffersController : ControllerBase
    {
        private readonly IBusinessObjectToModelsConverter _businessObjectToModelsConverter;
        private readonly ILogger _logger;
        private readonly ISellOfferProvider _sellOffersProvider;

        public SellOffersController(IBusinessObjectToModelsConverter businessObjectToModelsConverter, ILogger logger, ISellOfferProvider sellOfferProvider)
        {
            _businessObjectToModelsConverter = businessObjectToModelsConverter;
            _logger = logger;
            _sellOffersProvider = sellOfferProvider;
        }

        [HttpGet("sell-offers/{id:int}")]
        [ProducesResponseType(200, Type = typeof(SellOfferModel))]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public ActionResult<SellOfferModel> GetSellOfferById(int id)
        {
            try
            {
                IGetSellOfferByIdRequest getSellOfferByIdRequest = new GetSellOfferByIdRequest(id);
                IGetSellOfferByIdResponse getSellOfferByIdResponse = _sellOffersProvider.GetSellOfferById(getSellOfferByIdRequest);
                return PrepareResponseAfterGetSellOfferById(getSellOfferByIdResponse);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }

        private ActionResult<SellOfferModel> PrepareResponseAfterGetSellOfferById(IGetSellOfferByIdResponse getSellOfferByIdResponse)
        {
            switch (getSellOfferByIdResponse.ProvideResult)
            {
                case Domain.Providers.Common.Enum.ProvideEnumResult.Exception:
                    return StatusCode(500);
                case Domain.Providers.Common.Enum.ProvideEnumResult.Success:
                    return Ok(_businessObjectToModelsConverter.ConvertSellOffer(getSellOfferByIdResponse.SellOffer));
                case Domain.Providers.Common.Enum.ProvideEnumResult.NotFound:
                    return StatusCode(404);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        [HttpGet("sell-offers")]
        [ProducesResponseType(200, Type = typeof(IList<SellOfferModel>))]
        [ProducesResponseType(500)]
        public ActionResult<IList<SellOfferModel>> GetSellOffers()
        {
            try
            {
                IGetSellOffersResponse getSellOffersResponse = _sellOffersProvider.GetSellOffers();
                return PrepareResponseAfterGetSellOffers(getSellOffersResponse);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }

        private ActionResult<IList<SellOfferModel>> PrepareResponseAfterGetSellOffers(IGetSellOffersResponse getSellOffersResponse)
        {
            switch (getSellOffersResponse.ProvideResult)
            {
                case Domain.Providers.Common.Enum.ProvideEnumResult.Exception:
                    return StatusCode(500);
                case Domain.Providers.Common.Enum.ProvideEnumResult.Success:
                    return Ok(getSellOffersResponse.SellOffers.ToList()
                        .Select(x => _businessObjectToModelsConverter.ConvertSellOffer(x)));
                case Domain.Providers.Common.Enum.ProvideEnumResult.NotFound:
                    return StatusCode(404);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Method to get valid user sell offers
        /// </summary>
        /// <param name="sell-offers"></param>
        /// <returns>SellOffersModel</returns>
        [ProducesResponseType(200, Type = typeof(GetSellOffersByUserIdResponseModel))]
        [ProducesResponseType(500)]
        [HttpGet("user/sell-offers")]
        public ActionResult<SellOfferModel> GetSellOffersByUserId()
        {
            try
            {
                GetSellOffersByUserIdRequest request = new GetSellOffersByUserIdRequest(0/*Get id from JWT*/);
                IGetSellOffersByUserIdResponse getSellOffersByUserIdResponse = _sellOffersProvider.GetSellOffersByUserId(request);
                return PrepareResponseAfterGetSellOffersByUserId(getSellOffersByUserIdResponse);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }

        private ActionResult<SellOfferModel> PrepareResponseAfterGetSellOffersByUserId(IGetSellOffersByUserIdResponse getUserByIdResponse)
        {
            switch (getUserByIdResponse.ProvideResult)
            {
                case Domain.Providers.Common.Enum.ProvideEnumResult.Exception:
                    return StatusCode(500);
                case Domain.Providers.Common.Enum.ProvideEnumResult.Success:
                    return Ok(_businessObjectToModelsConverter.ConvertSellOffer(getUserByIdResponse.SellOffer));
                case Domain.Providers.Common.Enum.ProvideEnumResult.NotFound:
                    return StatusCode(404);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}