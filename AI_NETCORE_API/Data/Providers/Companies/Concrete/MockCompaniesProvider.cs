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

namespace Domain.Providers.Companies.Concrete
{
    public class MockCompaniesProvider : ICompaniesProvider
    {
        private readonly ILogger _logger;
        private readonly IList<Company> _companies;

        public MockCompaniesProvider(ILogger logger)
        {
            _logger = logger;
            _companies = new List<Company>
            {
                new Company(1,"Company1",1),
                new Company(2,"Company2",2)
            };
        }

        public IGetCompaniesResponse GetCompanies()
        {
            try
            {
                return new GetCompaniesResponse(_companies);
            }
            catch(Exception ex)
            {
                _logger.Log(ex);
                return new GetCompaniesResponse();
            }
        }

        public IGetCompanyByIdResponse GetCompanyById(IGetCompanyByIdRequest getCompanyByIdRequest)
        {
            try
            {
                return new GetCompanyByIdResponse(_companies.ToList().FirstOrDefault(x => x.Id == getCompanyByIdRequest.CompanyId));
            }
            catch(Exception ex)
            {
                _logger.Log(ex);
                return new GetCompanyByIdResponse();
            }
        }
    }
}
