using Domain.BusinessObject;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Providers.Resources.Abstract;
using Domain.Providers.Resources.Request.Abstract;
using Domain.Providers.Resources.Response.Abstract;
using Domain.Providers.Resources.Response.Concrete;
using Domain.Repositories.ResourceRepo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Providers.Resources.Concrete
{
    public class ResourcesProvider : IResourcesProvider
    {
        private readonly ILogger _logger;
        private readonly IResourceRepository _resources;

        public ResourcesProvider(ILogger logger, IResourceRepository resources)
        {
            _logger = logger;
            _resources = resources;
        }

        public IGetResourceByIdResponse GetResourceById(IGetResourceByIdRequest getResourceByIdRequest)
        {
            try
            {
                return new GetResourceByIdResponse(_resources.GetResourceById(getResourceByIdRequest.ResourceId));
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new GetResourceByIdResponse();
            }
        }

        public IGetResourcesResponse GetResources(IGetUserResourcesRequest getUserResourcesRequest)
        {
            try
            {
                return new GetResourcesResponse(_resources.GetAllResources(getUserResourcesRequest.UserId).ToList());
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new GetResourcesResponse();
            }
        }
    }
}
