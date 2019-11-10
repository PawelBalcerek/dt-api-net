using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AI_NETCORE_API.Infrastructure.BuisnessObjectToModelsConverting.Abstract;
using AI_NETCORE_API.Models.Objects;
using AI_NETCORE_API.Models.Request;
using AI_NETCORE_API.Models.Response.Configurations;
using AI_NETCORE_API.Models.Response.ExecutingTimes;
using Domain.Updaters.Configurations.Abstract;
using Domain.Updaters.Configurations.Concrete;
using Domain.Updaters.Configurations.Request.Concrete;
using Domain.Updaters.Configurations.Response.Abstract;
using Domain.Infrastructure.Logging.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AI_NETCORE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IConfigurationUpdater _configurationsCreator;
        private readonly IBusinessObjectToModelsConverter _businessObjectToModelsConverter;

        public ConfigurationsController(ILogger logger, IConfigurationUpdater configurationsCreator, IBusinessObjectToModelsConverter businessObjectToModelsConverter)
        {
            _logger = logger;
            _configurationsCreator = configurationsCreator;
            _businessObjectToModelsConverter = businessObjectToModelsConverter;
        }

        [HttpPut("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpdateConfiguration([FromBody] UpdateConfigurationAPIRequest updateConfigurationAPIRequest)
        {
            try
            {
                Stopwatch timer = Stopwatch.StartNew();
                if (!TryValidateModel(updateConfigurationAPIRequest))
                {
                    return StatusCode(400);
                }
                IUpdateConfigurationResponse updateConfigurationResponse
                    = _configurationsCreator.UpdateConfiguration(new UpdateConfigurationRequest(updateConfigurationAPIRequest.Name, updateConfigurationAPIRequest.Value));
                if (updateConfigurationResponse.Success == true)
                {
                    return Ok(PrepareUpdateConfigurationResponseModel(updateConfigurationResponse, timer));
                }
                else
                {
                    return StatusCode(404);
                }
            }
            catch(Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }

        private UpdateConfigurationsResponseModel PrepareUpdateConfigurationResponseModel(IUpdateConfigurationResponse updateConfigurationResponse, Stopwatch timer)
        {
            timer.Stop();
            return new UpdateConfigurationsResponseModel
            {
                executionDetails = new ExecutionDetails
                {
                    DatabaseTime = updateConfigurationResponse.DbTime,
                    ExecutionTime = timer.ElapsedMilliseconds
                }
            };
        }


    }
}