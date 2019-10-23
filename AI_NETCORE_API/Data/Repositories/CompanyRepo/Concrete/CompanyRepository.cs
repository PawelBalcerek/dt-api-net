using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.BaseRepo.Concrete;
using Domain.Repositories.CompanyRepo.Abstract;
using Data.Models;
using Domain.DTOToBOConverting;
using System.Linq;
namespace Domain.Repositories.CompanyRepo.Concrete
{
    public class CompanyRepository: RepositoryBase<Company>, ICompanyRepository
    {
        private IDTOToBOConverter _converter;
        public CompanyRepository(RepositoryContext repositoryContext, IDTOToBOConverter converter)
            : base(repositoryContext)
        {
            _converter = converter;
        }
        public Domain.BusinessObject.Company GetCompanyById(int id)
        {
            var company = FindByCondition(comp => comp.Id == id).FirstOrDefault();
            return _converter.ConvertCompany(company);
        }

        public IEnumerable<Domain.BusinessObject.Company> GetAllCompanies()
        {
            var companiesBO = new List<Domain.BusinessObject.Company>();
            var companiesDTO = FindAll().ToList();
            companiesDTO.ForEach(comp =>
            {
                companiesBO.Add(_converter.ConvertCompany(comp));
            });
            return companiesBO;
        }
    }
}
