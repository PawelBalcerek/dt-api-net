using System;
using System.Collections.Generic;
using System.Text;
using Domain.BusinessObject;
using Domain.Creators.Company.Abstract;
using Domain.Creators.Company.Request.Abstract;
using Domain.Creators.Company.Response.Abstract;
using Domain.Creators.Company.Response.Concrete;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Repositories.BaseRepo.Response;
using Domain.Repositories.CompanyRepo.Abstract;
using Domain.Repositories.ResourceRepo.Abstract;

namespace Domain.Creators.Company.Concrete
{
    public class CompanyCreator : ICompanyCreator
    {
        private readonly ILogger _logger;
        private readonly ICompanyRepository _companyRepository;
        private readonly IResourceRepository _resourceRepository;

        public CompanyCreator(ILogger logger, ICompanyRepository companyRepository, IResourceRepository resourceRepository)
        {
            _logger = logger;
            _companyRepository = companyRepository;
            _resourceRepository = resourceRepository;
        }

        public ICreateCompanyResponse CreateCompany(ICreateCompanyRequest createCompanyRequest)
        {
            try
            {
                RepositoryResponse<BusinessObject.Company> companyResponse = _companyRepository.CreateCompany(createCompanyRequest);
                RepositoryResponse<Resource> resourceResponse = _resourceRepository.AddResource(createCompanyRequest.ResourceAmount, createCompanyRequest.UserId, companyResponse.Object.Id);
                return new CreateCompanyResponse(resourceResponse.DatabaseTime + companyResponse.DatabaseTime);
            }

            catch (Exception ex)
            {
                _logger.Log(ex);
                return new CreateCompanyResponse();
            }
        }
    }
}
