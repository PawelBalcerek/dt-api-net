using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI_NETCORE_API.Models.Objects;
using Data.Infrastructure.Logging.Abstract;
using Data.Providers.Resources.Abstract;
using Data.Providers.Resources.Request.Abstract;
using Data.Providers.Resources.Request.Concrete;
using Data.Providers.Resources.Response.Abstract;
using Data.Providers.Transactions.Response.Abstract;
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

        public ResourcesController(ILogger logger,IResourcesProvider resourcesProvider)
        {
            _logger = logger;
            _resourcesProvider = resourcesProvider;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200, Type = typeof(ResourceModel))]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public ActionResult<ResourceModel> GetResourceById(int id)
        {
            try
            {
                IGetResourceByIdRequest getResourceByIdRequest = new GetResourceByIdRequest(id);
                IGetResourceByIdResponse getResourceByIdResponse = _resourcesProvider.GetResourceById(getResourceByIdRequest);
                return PrepareResponseAfterGetResourceById(getResourceByIdResponse);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }

        private ActionResult<ResourceModel> PrepareResponseAfterGetResourceById(IGetResourceByIdResponse getResourceByIdResponse)
        {
            switch (getResourceByIdResponse.ProvideResult)
            {
                case Data.Providers.Common.Enum.ProvideEnumResult.Exception:
                    return StatusCode(500);
                case Data.Providers.Common.Enum.ProvideEnumResult.Success:
                    return Ok(getResourceByIdResponse.Resource);
                case Data.Providers.Common.Enum.ProvideEnumResult.NotFound:
                    return StatusCode(404);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [HttpGet("")]
        [ProducesResponseType(200, Type = typeof(IList<ResourceModel>))]
        [ProducesResponseType(500)]
        public ActionResult<ResourceModel> GetTransaction()
        {
            try
            {
                IGetResourcesResponse getResourcesResponse = _resourcesProvider.GetResources();
                return PrepareResponseAfterGetResources(getResourcesResponse);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }

        private ActionResult<ResourceModel> PrepareResponseAfterGetResources(IGetResourcesResponse getResourcesResponse)
        {
            switch (getResourcesResponse.ProvideResult)
            {
                case Data.Providers.Common.Enum.ProvideEnumResult.Exception:
                    return StatusCode(500);
                case Data.Providers.Common.Enum.ProvideEnumResult.Success:
                    return Ok(getResourcesResponse.Resources);
                case Data.Providers.Common.Enum.ProvideEnumResult.NotFound:
                    return StatusCode(404);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}