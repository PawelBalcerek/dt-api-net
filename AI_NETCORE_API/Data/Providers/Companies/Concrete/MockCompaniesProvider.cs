using Data.BuisnessObject;
using Data.Infrastructure.Logging.Abstract;
using Data.Providers.Companies.Abstract;
using Data.Providers.Companies.Request.Abstract;
using Data.Providers.Companies.Response.Abstract;
using Data.Providers.Companies.Response.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Providers.Companies.Concrete
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
