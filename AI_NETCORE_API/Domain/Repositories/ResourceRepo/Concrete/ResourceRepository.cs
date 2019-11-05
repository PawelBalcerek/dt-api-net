using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.BaseRepo.Concrete;
using Domain.Repositories.ResourceRepo.Abstract;
using Data.Models;
using System.Linq;
using Domain.DTOToBOConverting;
namespace Domain.Repositories.ResourceRepo.Concrete
{
    public class ResourceRepository : RepositoryBase<Resource>, IResourceRepository
    {
        private readonly IDTOToBOConverter _converter;
        public ResourceRepository(RepositoryContext repositoryContext, IDTOToBOConverter converter)
            : base(repositoryContext)
        {
            _converter = converter;
        }

        public BusinessObject.Resource GetResourceById(int id)
        {
            Resource resource = FindByCondition(resExpr => resExpr.Id == id).FirstOrDefault();
            return _converter.ConvertResource(resource);
        }

        public IEnumerable<BusinessObject.Resource> GetAllResources(int userId)
        {
            return FindByCondition(r => r.UserId == userId).Select(r => _converter.ConvertResource(r));
        }
    }
}
