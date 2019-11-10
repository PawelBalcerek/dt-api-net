using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AI_NETCORE_API.Infrastructure.BuisnessObjectToModelsConverting.Abstract;
using AI_NETCORE_API.Models.Objects;
using AI_NETCORE_API.Models.Request.SellOffers;
using AI_NETCORE_API.Models.Response.ExecutingTimes;
using AI_NETCORE_API.Models.Response.SellOffers;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Providers.SellOffers.Abstract;
using Domain.Providers.SellOffers.Request.Abstract;
using Domain.Providers.SellOffers.Request.Concrete;
using Domain.Providers.SellOffers.Response.Abstract;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Method to get valid user sell offers
        /// </summary>
        /// <returns>SellOffersModel</returns>
        [ProducesResponseType(200, Type = typeof(IList<SellOfferModel>))]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [HttpGet("users/sell-offers")]
        [Authorize("Bearer")]
        public ActionResult<IList<SellOfferModel>> GetSellOffersByUserId()
        {
            try
            {
                Stopwatch timer = Stopwatch.StartNew();
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userId = int.Parse(identity.Claims.Where(c => c.Type == "Id").FirstOrDefault().Value);
                IGetSellOffersByUserIdRequest request = new GetSellOffersByUserIdRequest(userId);
                IGetSellOffersByUserIdResponse getSellOffersByUserIdResponse = _sellOffersProvider.GetSellOffersByUserId(request);
                return PrepareResponseAfterGetSellOffersByUserId(getSellOffersByUserIdResponse, timer);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }

        private ActionResult<IList<SellOfferModel>> PrepareResponseAfterGetSellOffersByUserId(IGetSellOffersByUserIdResponse getUserByIdResponse, Stopwatch timer)
        {
            switch (getUserByIdResponse.ProvideResult)
            {
                case Domain.Providers.Common.Enum.ProvideEnumResult.Exception:
                    return StatusCode(500);
                case Domain.Providers.Common.Enum.ProvideEnumResult.Success:
                    GetSellOffersByUserIdResponseModel response = PrepareSuccessResponseAfterGetSellOffersByUserId(getUserByIdResponse, timer);
                    return Ok(response);
                case Domain.Providers.Common.Enum.ProvideEnumResult.NotFound:
                    return StatusCode(404);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private GetSellOffersByUserIdResponseModel PrepareSuccessResponseAfterGetSellOffersByUserId(IGetSellOffersByUserIdResponse getUserByIdResponse, Stopwatch timer)
        {
            IList<SellOfferModel> resourceModelsList = getUserByIdResponse.SellOffer
                .Select(x => _businessObjectToModelsConverter.ConvertSellOffer(x)).ToList();
            timer.Stop();

            GetSellOffersByUserIdResponseModel response = new GetSellOffersByUserIdResponseModel
            {
                SellOffers = resourceModelsList,
                ExecDetails = new ExecutionDetails
                {
                    DatabaseTime = getUserByIdResponse.DatabaseExecutionTime,
                    ExecutionTime = timer.ElapsedMilliseconds
                }
            };
            return response;
        }
        /*
        /// <summary>
        /// Method to post new sell offer
        /// </summary>
        /// <param name="sell-offers"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(IList<SellOfferModel>))]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [HttpPost("sell-offers")]
        [Authorize("Bearer")]
        public ActionResult<IList<SellOfferModel>> PostSellOffers([FromBody] CreateSellOfferRequest request)
        {
            try
            {
                IGetSellOffersResponse getSellOffersResponse = _sellOffersProvider.GetSellOffers();
                return PrepareResponseAfterPostSellOffers(getSellOffersResponse);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }

        private ActionResult<IList<SellOfferModel>> PrepareResponseAfterPostSellOffers(IGetSellOffersResponse getSellOffersResponse)
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
        }   */
    }
}