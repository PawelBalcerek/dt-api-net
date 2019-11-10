using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Diagnostics;
using System.Threading.Tasks;
using AI_NETCORE_API.Infrastructure.BuisnessObjectToModelsConverting.Abstract;
using AI_NETCORE_API.Models.Objects;
using AI_NETCORE_API.Models.Response.Resources;
using AI_NETCORE_API.Models.Response.ExecutingTimes;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Providers.Resources.Abstract;
using Domain.Providers.Resources.Request.Abstract;
using Domain.Providers.Resources.Request.Concrete;
using Domain.Providers.Resources.Response.Abstract;
using Domain.Providers.Transactions.Response.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AI_NETCORE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IResourcesProvider _resourcesProvider;
        private readonly IBusinessObjectToModelsConverter _businessObjectToModelsConverter;

        public ResourcesController(ILogger logger, IResourcesProvider resourcesProvider, IBusinessObjectToModelsConverter businessObjectToModelsConverter)
        {
            _logger = logger;
            _resourcesProvider = resourcesProvider;
            _businessObjectToModelsConverter = businessObjectToModelsConverter;
        }

        [HttpGet("/api/users/resources")]
        [ProducesResponseType(200, Type = typeof(IList<ResourceModel>))]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [Authorize("Bearer")]
        public ActionResult<IList<ResourceModel>> GetUserResources()
        {
            try
            {
                Stopwatch timer = Stopwatch.StartNew();
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userId = int.Parse(identity.Claims.Where(c => c.Type == "Id").FirstOrDefault().Value);
                IGetUserResourcesRequest getUserResourcesRequest = new GetUserResourcesRequest(userId);
                IGetUserResourcesResponse getResourcesResponse = _resourcesProvider.GetUserResources(getUserResourcesRequest);
                return PrepareResponseAfterGetResources(getResourcesResponse, timer);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }

        private ActionResult<IList<ResourceModel>> PrepareResponseAfterGetResources(IGetUserResourcesResponse getResourcesResponse, Stopwatch timer)
        {
            switch (getResourcesResponse.ProvideResult)
            {
                case Domain.Providers.Common.Enum.ProvideEnumResult.Exception:
                    return StatusCode(500);
                case Domain.Providers.Common.Enum.ProvideEnumResult.Success:
                    GetUserResourcesResponseModel response = PrepareSuccessResponseAfterGetUserResources(getResourcesResponse, timer);
                    return Ok(response);
                case Domain.Providers.Common.Enum.ProvideEnumResult.NotFound:
                    return StatusCode(404);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private GetUserResourcesResponseModel PrepareSuccessResponseAfterGetUserResources(IGetUserResourcesResponse getResourcesResponse, Stopwatch timer)
        {
            IList<ResourceModel> resourceModelsList = getResourcesResponse.Resources
                .Select(x => _businessObjectToModelsConverter.ConvertResource(x)).ToList();
            timer.Stop();

            GetUserResourcesResponseModel response = new GetUserResourcesResponseModel
            {
                Resources = resourceModelsList,
                ExecutionDetails = new ExecutionDetails
                {
                    DatabaseTime = getResourcesResponse.DatabaseExecutionTime,
                    ExecutionTime = timer.ElapsedMilliseconds
                }
            };
            return response;
        }
    }
}