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
    }
}
