using Data.BuisnessObject;
using Data.Infrastructure.Logging.Abstract;
using Data.Providers.Resources.Abstract;
using Data.Providers.Resources.Request.Abstract;
using Data.Providers.Resources.Response.Abstract;
using Data.Providers.Resources.Response.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Providers.Resources.Concrete
{
    public class MockResourcesProvider : IResourcesProvider
    {
        private readonly ILogger _logger;
        private readonly IList<Resource> _resources;

        public MockResourcesProvider(ILogger logger)
        {
            _logger = logger;
            _resources = PrepareMockedResources();
        }

        private IList<Resource> PrepareMockedResources()
        {
            return new List<Resource>
            {
                new Resource(1,1,1,2000),
                new Resource(2,1,2,100),
                new Resource(3,2,1,100),
                new Resource(4,2,2,1000)
            };
        }

        public IGetResourceByIdResponse GetResourceById(IGetResourceByIdRequest getResourceByIdRequest)
        {
            try
            {
                return new GetResourceByIdResponse(_resources.ToList().FirstOrDefault(x => x.Id == getResourceByIdRequest.ResourceId));
            }
            catch(Exception ex)
            {
                _logger.Log(ex);
                return new GetResourceByIdResponse();
            }
        }

        public IGetResourcesResponse GetResources()
        {
            try
            {
                return new GetResourcesResponse(_resources);
            }
            catch(Exception ex)
            {
                _logger.Log(ex);
                return new GetResourcesResponse();
            }
        }
    }
}
