using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.BaseRepo.Concrete;
using Domain.Repositories.ResourceRepo.Abstract;
using Data.Models;
using System.Linq;
using System.Linq.Expressions;
using Domain.DTOToBOConverting;
using Domain.Repositories.BaseRepo.Response;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

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

        public RepositoryResponse<IEnumerable<BusinessObject.Resource>> GetUserResources(int userId)
        {
            var timer = Stopwatch.StartNew();
            var resources = FindByCondition(r => r.UserId == userId).Include(r => r.Comp).Select(r => _converter.ConvertResource(r));
            timer.Stop();
            var time = timer.ElapsedMilliseconds;
            return new RepositoryResponse<IEnumerable<BusinessObject.Resource>>(resources, time);
        }

        public RepositoryResponse<BusinessObject.Resource> GetResourceById(int id)
        {
            Stopwatch timer = Stopwatch.StartNew();
            var resource = FindByCondition(r => r.Id == id).FirstOrDefault();
            var resourceBO = _converter.ConvertResource(resource);
            timer.Stop();

            return new RepositoryResponse<BusinessObject.Resource>(resourceBO, timer.ElapsedMilliseconds);
        }

        public RepositoryResponse<bool> UpdateResource(BusinessObject.Resource resource)
        {
            bool success;
            Stopwatch timer = Stopwatch.StartNew();

            var dbResource = FindByCondition(r => r.Id == resource.Id).FirstOrDefault();
            if (dbResource == null)
            {
                timer.Stop();
                success = false;
            }
            else
            {
                dbResource.Amount = resource.Amount;
                RepositoryContext.Update(dbResource);
                RepositoryContext.SaveChanges();
                timer.Stop();
                success = true;
            }

            long time = timer.ElapsedMilliseconds;
            return new RepositoryResponse<bool>(success, time);
        }

        public RepositoryResponse<BusinessObject.Resource> AddResource(int amount, int userId,int companyId)
        {
            Stopwatch timer = Stopwatch.StartNew();
            Resource resource = new Resource
            {
                Amount = amount,
                UserId = userId,
                CompId = companyId
            };
            RepositoryContext.Resources.Add(resource);
            
            RepositoryContext.SaveChanges();
            Resource resourceWithCompanyParameters = RepositoryContext.Resources.Include(r => r.Comp).FirstOrDefault(x => x.Id == resource.Id);
            BusinessObject.Resource businessResource = _converter.ConvertResource(resourceWithCompanyParameters);
            timer.Stop();
            return new RepositoryResponse<BusinessObject.Resource>(businessResource, timer.ElapsedMilliseconds);
        }
    }
}
