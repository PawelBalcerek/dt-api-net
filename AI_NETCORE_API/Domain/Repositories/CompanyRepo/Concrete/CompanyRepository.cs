using System.Collections.Generic;
using Domain.Repositories.BaseRepo.Concrete;
using Domain.Repositories.CompanyRepo.Abstract;
using Data.Models;
using Domain.DTOToBOConverting;
using System.Linq;
namespace Domain.Repositories.CompanyRepo.Concrete
{
    public class CompanyRepository: RepositoryBase<Company>, ICompanyRepository
    {
        private readonly IDTOToBOConverter _converter;
        public CompanyRepository(RepositoryContext repositoryContext, IDTOToBOConverter converter)
            : base(repositoryContext)
        {
            _converter = converter;
        }
        public BusinessObject.Company GetCompanyById(int id)
        {
            var company = FindByCondition(comp => comp.Id == id).FirstOrDefault();
            return _converter.ConvertCompany(company);
        }

        public IEnumerable<BusinessObject.Company> GetAllCompanies()
        {
            return FindAll().Select(c => _converter.ConvertCompany(c));
        }
    }
}
