using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI_NETCORE_API.Infrastructure.BuisnessObjectToModelsConverting.Abstract;
using AI_NETCORE_API.Models.Objects;
using Data.Infrastructure.Logging.Abstract;
using Data.Providers.Companies.Abstract;
using Data.Providers.Companies.Request.Abstract;
using Data.Providers.Companies.Request.Concrete;
using Data.Providers.Companies.Response.Abstract;
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
                case Data.Providers.Common.Enum.ProvideEnumResult.Exception:
                    return StatusCode(500);
                case Data.Providers.Common.Enum.ProvideEnumResult.Success:
                    return Ok(_businessObjectToModelsConverter.ConvertCompany(getCompanyByIdResponse.Company));
                case Data.Providers.Common.Enum.ProvideEnumResult.NotFound:
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
                case Data.Providers.Common.Enum.ProvideEnumResult.Exception:
                    return StatusCode(500);
                case Data.Providers.Common.Enum.ProvideEnumResult.Success:
                    return Ok(getCompaniesResponse.Companies.ToList()
                        .Select(x=> _businessObjectToModelsConverter.ConvertCompany(x)));
                case Data.Providers.Common.Enum.ProvideEnumResult.NotFound:
                    return StatusCode(404);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        
    }
}