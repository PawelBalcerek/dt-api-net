using System.Collections.Generic;
using Domain.Repositories.BaseRepo.Abstract;
using Data.Models;
using Domain.Creators.Company.Request.Abstract;
using Domain.Repositories.BaseRepo.Response;

namespace Domain.Repositories.CompanyRepo.Abstract
{
    public interface ICompanyRepository : IRepositoryBase<Company>
    {
        RepositoryResponse<BusinessObject.Company> GetCompanyById(int id);
        RepositoryResponse<IEnumerable<BusinessObject.Company>> GetAllCompanies();
        RepositoryResponse<BusinessObject.Company> CreateCompany(ICreateCompanyRequest createCompanyRequest);

        long ClearAll();

    }
}
