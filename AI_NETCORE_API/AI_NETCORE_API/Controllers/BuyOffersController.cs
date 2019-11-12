using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AI_NETCORE_API.Infrastructure.BuisnessObjectToModelsConverting.Abstract;
using AI_NETCORE_API.Models.Objects;
using AI_NETCORE_API.Models.Response.BuyOffers;
using AI_NETCORE_API.Models.Response.ExecutingTimes;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Providers.BuyOffers.Abstract;
using Domain.Providers.BuyOffers.Request.Abstract;
using Domain.Providers.BuyOffers.Request.Concrete;
using Domain.Providers.BuyOffers.Response.Abstract;
using Microsoft.AspNetCore.Authorization;
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
                case Domain.Providers.Common.Enum.ProvideEnumResult.Exception:
                    return StatusCode(500);
                case Domain.Providers.Common.Enum.ProvideEnumResult.Success:
                    return Ok(_businessObjectToModelsConverter.ConvertBuyOffer(getBuyOfferByIdResponse.BuyOffer));
                case Domain.Providers.Common.Enum.ProvideEnumResult.NotFound:
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
                case Domain.Providers.Common.Enum.ProvideEnumResult.Exception:
                    return StatusCode(500);
                case Domain.Providers.Common.Enum.ProvideEnumResult.Success:
                    return Ok(getBuyOffersResponse.BuyOffers.ToList()
                        .Select(x => _businessObjectToModelsConverter.ConvertBuyOffer(x)));
                case Domain.Providers.Common.Enum.ProvideEnumResult.NotFound:
                    return StatusCode(404);
                default:
                    throw new ArgumentOutOfRangeException();
            }
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

    }
}