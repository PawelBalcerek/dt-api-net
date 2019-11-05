using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.BaseRepo.Abstract;
using Domain.Repositories.BaseRepo.Concrete;
using Data.Models;
namespace Domain.Repositories.ResourceRepo.Abstract
{
    public interface IResourceRepository : IRepositoryBase<Resource>
    {
        BusinessObject.Resource GetResourceById(int id);
        IEnumerable<BusinessObject.Resource> GetAllResources(int userId);
    }
}
