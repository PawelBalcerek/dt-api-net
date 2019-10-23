using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI_NETCORE_API.Infrastructure.BuisnessObjectToModelsConverting.Abstract;
using AI_NETCORE_API.Models.Objects;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Providers.Companies.Abstract;
using Domain.Providers.Companies.Request.Abstract;
using Domain.Providers.Companies.Request.Concrete;
using Domain.Providers.Companies.Response.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AI_NETCORE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICompaniesProvider _companiesProvider;
        private readonly IBusinessObjectToModelsConverter _businessObjectToModelsConverter;

        public CompaniesController(ILogger logger, ICompaniesProvider companiesProvider, IBusinessObjectToModelsConverter businessObjectToModelsConverter)
        {
            _logger = logger;
            _companiesProvider = companiesProvider;
            _businessObjectToModelsConverter = businessObjectToModelsConverter;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200, Type = typeof(CompanyModel))]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public ActionResult<CompanyModel> GetCompanyById(int id)
        {
            try
            {
                IGetCompanyByIdRequest getCompanyByIdRequest  = new GetCompanyByIdRequest(id);
                IGetCompanyByIdResponse getCompanyByIdResponse = _companiesProvider.GetCompanyById(getCompanyByIdRequest);
                return PrepareResponseAfterGetCompanyById(getCompanyByIdResponse);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }

        private ActionResult<CompanyModel> PrepareResponseAfterGetCompanyById(IGetCompanyByIdResponse getCompanyByIdResponse)
        {
            switch (getCompanyByIdResponse.ProvideResult)
            {
                case Domain.Providers.Common.Enum.ProvideEnumResult.Exception:
                    return StatusCode(500);
                case Domain.Providers.Common.Enum.ProvideEnumResult.Success:
                    return Ok(_businessObjectToModelsConverter.ConvertCompany(getCompanyByIdResponse.Company));
                case Domain.Providers.Common.Enum.ProvideEnumResult.NotFound:
                    return StatusCode(404);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        [HttpGet("")]
        [ProducesResponseType(200, Type = typeof(IList<CompanyModel>))]
        [ProducesResponseType(500)]
        public ActionResult<IList<CompanyModel>> GetCompanies()
        {
            try
            {
                IGetCompaniesResponse getCompaniesResponse = _companiesProvider.GetCompanies();
                return PrepareResponseAfterGetCompanies(getCompaniesResponse);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }

        private ActionResult<IList<CompanyModel>> PrepareResponseAfterGetCompanies(IGetCompaniesResponse getCompaniesResponse)
        {
            switch (getCompaniesResponse.ProvideResult)
            {
                case Domain.Providers.Common.Enum.ProvideEnumResult.Exception:
                    return StatusCode(500);
                case Domain.Providers.Common.Enum.ProvideEnumResult.Success:
                    return Ok(getCompaniesResponse.Companies.ToList()
                        .Select(x=> _businessObjectToModelsConverter.ConvertCompany(x)));
                case Domain.Providers.Common.Enum.ProvideEnumResult.NotFound:
                    return StatusCode(404);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        
    }
}