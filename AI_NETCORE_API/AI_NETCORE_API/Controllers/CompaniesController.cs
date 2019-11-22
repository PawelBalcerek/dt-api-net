using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AI_NETCORE_API.Infrastructure.BuisnessObjectToModelsConverting.Abstract;
using AI_NETCORE_API.Infrastructure.GettingUserIdentifierFromRequest.Abstract;
using AI_NETCORE_API.Models.Objects;
using AI_NETCORE_API.Models.Request.Company;
using AI_NETCORE_API.Models.Response.Companies;
using AI_NETCORE_API.Models.Response.ExecutingTimes;
using Domain.Creators.Company.Abstract;
using Domain.Creators.Company.Response.Abstract;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Providers.Companies.Abstract;
using Domain.Providers.Companies.Response.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AI_NETCORE_API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICompaniesProvider _companiesProvider;
        private readonly ICompanyCreator _companyCreator;
        private readonly IBusinessObjectToModelsConverter _businessObjectToModelsConverter;
        private readonly IUserIdentifierFromHttpRequestProvider _userIdentifierFromHttpRequestProvider;

        public CompaniesController(ILogger logger, ICompaniesProvider companiesProvider, IBusinessObjectToModelsConverter businessObjectToModelsConverter, ICompanyCreator companyCreator, IUserIdentifierFromHttpRequestProvider userIdentifierFromHttpRequestProvider)
        {
            _logger = logger;
            _companiesProvider = companiesProvider;
            _businessObjectToModelsConverter = businessObjectToModelsConverter;
            _companyCreator = companyCreator;
            _userIdentifierFromHttpRequestProvider = userIdentifierFromHttpRequestProvider;
        }
        [HttpGet("companies")]
        [ProducesResponseType(200, Type = typeof(GetCompaniesResponseModel))]
        [ProducesResponseType(500)]
        public ActionResult<GetCompaniesResponseModel> GetCompanies()
        {
            try
            {
                Stopwatch timer = Stopwatch.StartNew();
                IGetCompaniesResponse getCompaniesResponse = _companiesProvider.GetCompanies();
                return PrepareResponseAfterGetCompanies(getCompaniesResponse,timer);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }



        [HttpPost("companies")]
        [ProducesResponseType(200, Type = typeof(CreateCompanyResponseModel))]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [Authorize("Bearer")]
        public ActionResult<CreateCompanyResponseModel> AddCompany(CreateCompanyRequest request)
        {
            try
            {
                int userId = _userIdentifierFromHttpRequestProvider.GetUserIdFromRequest(this.HttpContext);
                Stopwatch timer = Stopwatch.StartNew();
                if (!request.IsValid)
                {
                    return PrepareResponseInAddingCompanyWhenBadRequest(timer);
                }
                ICreateCompanyResponse companyCreationResponse = _companyCreator.CreateCompany(new Domain.Creators.Company.Request.Concrete.CreateCompanyRequest(userId, request.Name, request.ResourceAmount));
                timer.Stop();
                if (!companyCreationResponse.Success) return StatusCode(500);
                return Ok(new CreateCompanyResponseModel{ ExecutionDetails = new ExecutionDetails
                {
                    DbTime = companyCreationResponse.DatabaseExecutionTime,
                    ExecTime = timer.ElapsedMilliseconds
                }});
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }

        private ActionResult<CreateCompanyResponseModel> PrepareResponseInAddingCompanyWhenBadRequest(Stopwatch timer)
        {
            timer.Stop();
            return StatusCode(400,
                new CreateCompanyResponseModel
                { ExecutionDetails = new ExecutionDetails { ExecTime = timer.ElapsedMilliseconds } });
        }


        private ActionResult<GetCompaniesResponseModel> PrepareResponseAfterGetCompanies(IGetCompaniesResponse getCompaniesResponse,Stopwatch timer)
        {
            switch (getCompaniesResponse.ProvideResult)
            {
                case Domain.Providers.Common.Enum.ProvideEnumResult.Exception:
                    return StatusCode(500);
                case Domain.Providers.Common.Enum.ProvideEnumResult.Success:
                    GetCompaniesResponseModel response = PrepareSuccessResponseAfterGetCompanies(getCompaniesResponse, timer);
                    return Ok(response);
                case Domain.Providers.Common.Enum.ProvideEnumResult.NotFound:
                    return StatusCode(404);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private GetCompaniesResponseModel PrepareSuccessResponseAfterGetCompanies(IGetCompaniesResponse getCompaniesResponse,
            Stopwatch timer)
        {
            IList<CompanyModel> companiesModelList = getCompaniesResponse.Companies.ToList()
                .Select(x => _businessObjectToModelsConverter.ConvertCompany(x)).ToList();
            timer.Stop();
            GetCompaniesResponseModel response = new GetCompaniesResponseModel
            {
                Companies = companiesModelList,
                ExecutionDetails = new ExecutionDetails
                {
                    DbTime = getCompaniesResponse.DatabaseExecutionTime,
                    ExecTime = timer.ElapsedMilliseconds
                }
            };
            return response;
        }
    }
}