using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.BaseRepo.Abstract;
using Domain.Repositories.BaseRepo.Concrete;
using Data.Models;

namespace Domain.Repositories.CompanyRepo.Abstract
{
    public interface ICompanyRepository : IRepositoryBase<Company>
    {
        Domain.BusinessObject.Company GetCompanyById(int id);
        IEnumerable<Domain.BusinessObject.Company> GetAllCompanies();
    }
}
