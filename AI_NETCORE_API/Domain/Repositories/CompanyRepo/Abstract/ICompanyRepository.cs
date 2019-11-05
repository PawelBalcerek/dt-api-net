using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.BaseRepo.Abstract;
using Domain.Repositories.BaseRepo.Concrete;
using Data.Models;
using Domain.Repositories.BaseRepo.Response;

namespace Domain.Repositories.CompanyRepo.Abstract
{
    public interface ICompanyRepository : IRepositoryBase<Company>
    {
        RepositoryResponse<BusinessObject.Company> GetCompanyById(int id);
        RepositoryResponse<IEnumerable<Domain.BusinessObject.Company>> GetAllCompanies();
    }
}
