using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI_NETCORE_API.Infrastructure.BuisnessObjectToModelsConverting.Abstract;
using AI_NETCORE_API.Models.Objects;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Providers.Resources.Abstract;
using Domain.Providers.Resources.Request.Abstract;
using Domain.Providers.Resources.Request.Concrete;
using Domain.Providers.Resources.Response.Abstract;
using Domain.Providers.Transactions.Response.Abstract;
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
                case Domain.Providers.Common.Enum.ProvideEnumResult.Exception:
                    return StatusCode(500);
                case Domain.Providers.Common.Enum.ProvideEnumResult.Success:
                    return Ok(_businessObjectToModelsConverter.ConvertResource(getResourceByIdResponse.Resource));
                case Domain.Providers.Common.Enum.ProvideEnumResult.NotFound:
                    return StatusCode(404);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [HttpGet("")]
        [ProducesResponseType(200, Type = typeof(IList<ResourceModel>))]
        [ProducesResponseType(500)]
        public ActionResult<IList<ResourceModel>> GetResources()
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

        private ActionResult<IList<ResourceModel>> PrepareResponseAfterGetResources(IGetResourcesResponse getResourcesResponse)
        {
            switch (getResourcesResponse.ProvideResult)
            {
                case Domain.Providers.Common.Enum.ProvideEnumResult.Exception:
                    return StatusCode(500);
                case Domain.Providers.Common.Enum.ProvideEnumResult.Success:
                    return Ok(getResourcesResponse.Resources.ToList()
                        .Select(x => _businessObjectToModelsConverter.ConvertResource(x)));
                case Domain.Providers.Common.Enum.ProvideEnumResult.NotFound:
                    return StatusCode(404);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}