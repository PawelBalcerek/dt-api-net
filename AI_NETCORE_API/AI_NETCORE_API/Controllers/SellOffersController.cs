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
using Domain.Creators.SellOffer.Abstract;
using Domain.Creators.SellOffer.Request.Concrete;
using Domain.Creators.SellOffer.Response.Abstract;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Providers.SellOffers.Abstract;
using Domain.Providers.SellOffers.Request.Abstract;
using Domain.Providers.SellOffers.Request.Concrete;
using Domain.Providers.SellOffers.Response.Abstract;
using Domain.Updaters.SellOffers.Abstract;
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
        private readonly ISellOfferCreator _sellOfferCreator;
        private readonly ISellOfferUpdater _sellOfferUpdater;

        public SellOffersController(
            IBusinessObjectToModelsConverter businessObjectToModelsConverter,
            ILogger logger,
            ISellOfferProvider sellOfferProvider,
            ISellOfferCreator sellOfferCreator,
            ISellOfferUpdater sellOfferUpdater)
        {
            _businessObjectToModelsConverter = businessObjectToModelsConverter;
            _logger = logger;
            _sellOffersProvider = sellOfferProvider;
            _sellOfferCreator = sellOfferCreator;
            _sellOfferUpdater = sellOfferUpdater;
        }

        /// <summary>
        /// Method to get valid user sell offers
        /// </summary>
        /// <returns>SellOffersModel</returns>
        [ProducesResponseType(200, Type = typeof(GetSellOffersByUserIdResponseModel))]
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
                    DbTime = getUserByIdResponse.DatabaseExecutionTime,
                    ExecTime = timer.ElapsedMilliseconds
                }
            };
            return response;
        }

        /// <summary>
        /// Method to post new sell offer
        /// </summary>
        /// <param name="request">Data for sell offer creation.</param>
        /// <returns>Response with time</returns>
        [ProducesResponseType(200, Type = typeof(CreateSellOfferResponseModel))]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpPost("sell-offers")]
        [Authorize("Bearer")]
        public async Task<ActionResult> PostSellOffers([FromBody] CreateSellOfferRequest request)
        {
            try
            {
                Stopwatch timer = Stopwatch.StartNew();
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userId = int.Parse(identity.Claims.Where(c => c.Type == "Id").FirstOrDefault().Value);
                ISellOfferCreateResponse getSellOffersCreateResponse = _sellOfferCreator.CreateSellOffer(new SellOfferCreateRequest(request.ResourceId, request.Amount, request.Price, userId));
                if(getSellOffersCreateResponse.Success == false && getSellOffersCreateResponse.ProvideResult == Domain.Providers.Common.Enum.ProvideEnumResult.Success)
                {
                    return StatusCode(403);
                }
                return await PrepareResponseAfterPostSellOffers(getSellOffersCreateResponse, timer);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }

        private async Task<ActionResult> PrepareResponseAfterPostSellOffers(ISellOfferCreateResponse getSellOffersCreateResponse, Stopwatch timer)
        {
            switch (getSellOffersCreateResponse.ProvideResult)
            {
                case Domain.Providers.Common.Enum.ProvideEnumResult.Exception:
                    return StatusCode(500);
                case Domain.Providers.Common.Enum.ProvideEnumResult.Success:
                    CreateSellOfferResponseModel response = PrepareSuccessResponseAfterPostSellOffer(getSellOffersCreateResponse, timer);
                    return Ok(response);
                case Domain.Providers.Common.Enum.ProvideEnumResult.NotFound:
                    return StatusCode(404);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private CreateSellOfferResponseModel PrepareSuccessResponseAfterPostSellOffer(ISellOfferCreateResponse getSellOffersCreateResponse, Stopwatch timer)
        {
            timer.Stop();

            CreateSellOfferResponseModel response = new CreateSellOfferResponseModel
            {
                ExecDetails = new ExecutionDetails
                {
                    DbTime = getSellOffersCreateResponse.DatabaseExecutionTime,
                    ExecTime = timer.ElapsedMilliseconds
                }
            };
            return response;
        }

        /// <summary>
        /// Method to withdraw sell offer
        /// </summary>
        /// <param name="id">Id of sell offer.</param>
        /// <returns>Response with time</returns>
        [ProducesResponseType(200, Type = typeof(WithdrawSellOfferResponseModel))]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [HttpPut("sell-offers/:id")]
        [Authorize("Bearer")]
        public async Task<ActionResult> GetSellOffersByUserId(int id)
        {
            try
            {
                Stopwatch timer = Stopwatch.StartNew();
                IWithdrawSellOfferByIdRequest request = new WithdrawSellOfferByIdRequest(id);
                IWithdrawSellOfferByIdResponse withdrawSellOfferByIdResponse = _sellOfferUpdater.WithdrawSellOfferById(request);
                return await PrepareResponseAfterWithdrawSellOfferById(withdrawSellOfferByIdResponse, timer);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }

        private async Task<ActionResult> PrepareResponseAfterWithdrawSellOfferById(IWithdrawSellOfferByIdResponse withdrawSellOfferResponse, Stopwatch timer)
        {
            switch (withdrawSellOfferResponse.ProvideResult)
            {
                case Domain.Providers.Common.Enum.ProvideEnumResult.Exception:
                    return StatusCode(500);
                case Domain.Providers.Common.Enum.ProvideEnumResult.Success:
                    WithdrawSellOfferResponseModel response = PrepareSuccessResponseAfterWithdrawSellOfferById(withdrawSellOfferResponse, timer);
                    return Ok(response);
                case Domain.Providers.Common.Enum.ProvideEnumResult.NotFound:
                    return StatusCode(404);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private WithdrawSellOfferResponseModel PrepareSuccessResponseAfterWithdrawSellOfferById(IWithdrawSellOfferByIdResponse withdrawSellOfferResponse, Stopwatch timer)
        {
            timer.Stop();

            WithdrawSellOfferResponseModel response = new WithdrawSellOfferResponseModel
            {
                ExecDetails = new ExecutionDetails
                {
                    DbTime = withdrawSellOfferResponse.DatabaseExecutionTime,
                    ExecTime = timer.ElapsedMilliseconds
                }
            };
            return response;
        }
    }
}