using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.BaseRepo.Concrete;
using Domain.Repositories.ResourceRepo.Abstract;
using Data.Models;
namespace Domain.Repositories.ResourceRepo.Concrete
{
    public class ResourceRepository: RepositoryBase<Resource>, IResourceRepository
    {
        public ResourceRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
