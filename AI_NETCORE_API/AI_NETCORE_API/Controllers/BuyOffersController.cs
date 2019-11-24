using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AI_NETCORE_API.Infrastructure.BuisnessObjectToModelsConverting.Abstract;
using AI_NETCORE_API.Models.Objects;
using AI_NETCORE_API.Models.Request.BuyOffers;
using AI_NETCORE_API.Models.Response.BuyOffers;
using AI_NETCORE_API.Models.Response.ExecutingTimes;
using Domain.Creators.BuyOffer.Abstract;
using Domain.Creators.BuyOffer.Request.Concrete;
using Domain.Creators.BuyOffer.Response.Abstract;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Providers.BuyOffers.Abstract;
using Domain.Providers.BuyOffers.Request.Abstract;
using Domain.Providers.BuyOffers.Request.Concrete;
using Domain.Providers.BuyOffers.Response.Abstract;
using Domain.Updaters.BuyOffers.Abstract;
using Domain.Updaters.BuyOffers.Request.Abstract;
using Domain.Updaters.BuyOffers.Request.Concrete;
using Domain.Updaters.BuyOffers.Response.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AI_NETCORE_API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class BuyOffersController : ControllerBase
    {
        private readonly IBusinessObjectToModelsConverter _businessObjectToModelsConverter;
        private readonly ILogger _logger;
        private readonly IBuyOffersProvider _buyOffersProvider;
        private readonly IBuyOfferCreator _buyOfferCreator;
        private readonly IBuyOfferUpdater _buyOfferUpdater;

        public BuyOffersController(
            IBusinessObjectToModelsConverter businessObjectToModelsConverter,
            ILogger logger,
            IBuyOffersProvider buyOffersProvider,
            IBuyOfferCreator buyOfferCreator,
            IBuyOfferUpdater buyOfferUpdater)
        {
            _businessObjectToModelsConverter = businessObjectToModelsConverter;
            _logger = logger;
            _buyOffersProvider = buyOffersProvider;
            _buyOfferCreator = buyOfferCreator;
            _buyOfferUpdater = buyOfferUpdater;
        }

        /// <summary>
        /// Method to get valid user buy offers
        /// </summary>
        /// <returns>Vaild buy offers for current user.</returns>
        [ProducesResponseType(200, Type = typeof(GetBuyOffersByUserIdResponseModel))]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [HttpGet("users/buy-offers")]
        [Authorize("Bearer")]
        public ActionResult<IList<BuyOfferModel>> GetBuyOffersByUserId()
        {
            try
            {
                Stopwatch timer = Stopwatch.StartNew();
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userId = int.Parse(identity.Claims.Where(c => c.Type == "Id").FirstOrDefault().Value);
                IGetBuyOffersByUserIdRequest request = new GetBuyOffersByUserIdRequest(userId);
                IGetBuyOffersByUserIdResponse getBuyOffersByUserIdResponse = _buyOffersProvider.GetBuyOffersByUserId(request);
                return PrepareResponseAfterGetBuyOffersByUserId(getBuyOffersByUserIdResponse, timer);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }

        private ActionResult<IList<BuyOfferModel>> PrepareResponseAfterGetBuyOffersByUserId(IGetBuyOffersByUserIdResponse getUserByIdResponse, Stopwatch timer)
        {
            switch (getUserByIdResponse.ProvideResult)
            {
                case Domain.Providers.Common.Enum.ProvideEnumResult.Exception:
                    return StatusCode(500);
                case Domain.Providers.Common.Enum.ProvideEnumResult.Success:
                    GetBuyOffersByUserIdResponseModel response = PrepareSuccessResponseAfterGetBuyOffersByUserId(getUserByIdResponse, timer);
                    return Ok(response);
                case Domain.Providers.Common.Enum.ProvideEnumResult.NotFound:
                    return StatusCode(404);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private GetBuyOffersByUserIdResponseModel PrepareSuccessResponseAfterGetBuyOffersByUserId(IGetBuyOffersByUserIdResponse getUserByIdResponse, Stopwatch timer)
        {
            IList<BuyOfferModel> resourceModelsList = getUserByIdResponse.BuyOffer
                .Select(x => _businessObjectToModelsConverter.ConvertBuyOffer(x)).ToList();
            timer.Stop();

            GetBuyOffersByUserIdResponseModel response = new GetBuyOffersByUserIdResponseModel
            {
                BuyOffers = resourceModelsList,
                ExecDetails = new ExecutionDetails
                {
                    DbTime = getUserByIdResponse.DatabaseExecutionTime,
                    ExecTime = timer.ElapsedMilliseconds
                }
            };
            return response;
        }

        /// <summary>
        /// Method to post new buy offer
        /// </summary>
        /// <param name="request">Data for sell offer creation.</param>
        /// <returns>Response with time</returns>
        [ProducesResponseType(200, Type = typeof(CreateBuyOfferResponseModel))]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [HttpPost("buy-offers")]
        [Authorize("Bearer")]
        public async Task<ActionResult> PostSellOffers([FromBody] CreateBuyOfferRequest request)
        {
            try
            {
                Stopwatch timer = Stopwatch.StartNew();
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userId = int.Parse(identity.Claims.Where(c => c.Type == "Id").FirstOrDefault().Value);
                IBuyOfferCreateResponse getBuyOffersCreateResponse = _buyOfferCreator.CreateBuyOffer(new BuyOfferCreateRequest(request.CompanyId, request.Amount, request.Price, userId));
                return await PrepareResponseAfterPostBuyOffers(getBuyOffersCreateResponse, timer);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }

        private async Task<ActionResult> PrepareResponseAfterPostBuyOffers(IBuyOfferCreateResponse getBuyOffersCreateResponse, Stopwatch timer)
        {
            switch (getBuyOffersCreateResponse.ProvideResult)
            {
                case Domain.Providers.Common.Enum.ProvideEnumResult.Exception:
                    return StatusCode(500);
                case Domain.Providers.Common.Enum.ProvideEnumResult.Success:
                    CreateBuyOfferResponseModel response = PrepareSuccessResponseAfterPostBuyOffer(getBuyOffersCreateResponse, timer);
                    return Ok(response);
                case Domain.Providers.Common.Enum.ProvideEnumResult.NotFound:
                    return StatusCode(404);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private CreateBuyOfferResponseModel PrepareSuccessResponseAfterPostBuyOffer(IBuyOfferCreateResponse getBuyOffersCreateResponse, Stopwatch timer)
        {
            timer.Stop();

            CreateBuyOfferResponseModel response = new CreateBuyOfferResponseModel
            {
                ExecDetails = new ExecutionDetails
                {
                    DbTime = getBuyOffersCreateResponse.DatabaseExecutionTime,
                    ExecTime = timer.ElapsedMilliseconds
                }
            };
            return response;
        }

        /// <summary>
        /// Method to withdraw buy offer
        /// </summary>
        /// <param name="id">Id of buy offer.</param>
        /// <returns>Response with time</returns>
        [ProducesResponseType(200, Type = typeof(WithdrawBuyOfferResponseModel))]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [HttpPut("buy-offers/{id}")]
        [Authorize("Bearer")]
        public async Task<ActionResult> WithdrawBuyOfferById(int id)
        {
            try
            {
                Stopwatch timer = Stopwatch.StartNew();
                IWithdrawBuyOfferByIdRequest request = new WithdrawBuyOfferByIdRequest(id);
                IWithdrawBuyOfferByIdResponse withdrawBuyOfferByIdResponse = _buyOfferUpdater.WithdrawBuyOfferById(request);
                return await PrepareResponseAfterWithdrawBuyOfferById(withdrawBuyOfferByIdResponse, timer);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }

        private async Task<ActionResult> PrepareResponseAfterWithdrawBuyOfferById(IWithdrawBuyOfferByIdResponse withdrawBuyOfferResponse, Stopwatch timer)
        {
            switch (withdrawBuyOfferResponse.ProvideResult)
            {
                case Domain.Providers.Common.Enum.ProvideEnumResult.Exception:
                    return StatusCode(500);
                case Domain.Providers.Common.Enum.ProvideEnumResult.Success:
                    WithdrawBuyOfferResponseModel response = PrepareSuccessResponseAfterWithdrawBuyOfferById(withdrawBuyOfferResponse, timer);
                    return Ok(response);
                case Domain.Providers.Common.Enum.ProvideEnumResult.NotFound:
                    return StatusCode(404);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private WithdrawBuyOfferResponseModel PrepareSuccessResponseAfterWithdrawBuyOfferById(IWithdrawBuyOfferByIdResponse withdrawBuyOfferResponse, Stopwatch timer)
        {
            timer.Stop();

            WithdrawBuyOfferResponseModel response = new WithdrawBuyOfferResponseModel
            {
                ExecDetails = new ExecutionDetails
                {
                    DbTime = withdrawBuyOfferResponse.DatabaseExecutionTime,
                    ExecTime = timer.ElapsedMilliseconds
                }
            };
            return response;
        }

    }
}