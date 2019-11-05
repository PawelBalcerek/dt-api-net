using Domain.BusinessObject;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Providers.Companies.Abstract;
using Domain.Providers.Companies.Request.Abstract;
using Domain.Providers.Companies.Response.Abstract;
using Domain.Providers.Companies.Response.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Repositories.BaseRepo.Response;
using Domain.Repositories.CompanyRepo.Abstract;


namespace Domain.Providers.Companies.Concrete
{
    public class CompaniesProvider : ICompaniesProvider
    {
        private readonly ILogger _logger;
        private readonly ICompanyRepository _companies;

        public CompaniesProvider(ILogger logger, ICompanyRepository companies)
        {
            _logger = logger;
            _companies = companies;
        }

        public IGetCompaniesResponse GetCompanies()
        {
            try
            {
                RepositoryResponse<IEnumerable<Company>> result = _companies.GetAllCompanies();
                return new GetCompaniesResponse(result.Object.ToList(),result.DatabaseTime);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new GetCompaniesResponse();
            }
        }

        public IGetCompanyByIdResponse GetCompanyById(IGetCompanyByIdRequest getCompanyByIdRequest)
        {
            try
            {
                RepositoryResponse<Company> result = _companies.GetCompanyById(getCompanyByIdRequest.CompanyId);
                return new GetCompanyByIdResponse(result.Object,result.DatabaseTime);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new GetCompanyByIdResponse();
            }
        }
    }
}

