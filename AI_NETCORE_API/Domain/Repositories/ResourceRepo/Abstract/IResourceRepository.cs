using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.BaseRepo.Abstract;
using Domain.Repositories.BaseRepo.Concrete;
using Data.Models;
using Domain.Repositories.BaseRepo.Response;

namespace Domain.Repositories.ResourceRepo.Abstract
{
    public interface IResourceRepository : IRepositoryBase<Resource>
    {
        RepositoryResponse<IEnumerable<BusinessObject.Resource>> GetUserResources(int userId);
        RepositoryResponse<BusinessObject.Resource> GetResourceById(int id);
        RepositoryResponse<bool> UpdateResource(BusinessObject.Resource resource);
        RepositoryResponse<BusinessObject.Resource> AddResource(int amount, int userId, int companyId);
    }
}
