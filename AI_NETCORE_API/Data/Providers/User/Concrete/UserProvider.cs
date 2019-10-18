using Data.Infrastructure.Logging.Abstract;
using Data.Providers.User.Abstract;
using Data.Providers.User.Request.Abstract;
using Data.Providers.User.Response.Abstract;
using Data.Providers.User.Response.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.User.Concrete
{
    public class UserProvider : IUserProvider
    {
        private readonly ILogger _logger;
        public UserProvider(ILogger logger)
        {
            _logger = logger;
        }

        public IGetUserByIdResponse GetUserById(IGetUserByIdRequest getUserByIdRequest)
        {
            try
            {
                //TODO get from repository
                return new GetUserByIdResponse(new { Name = "Nazwa", Email = "email@email.pl" });
            }
            catch(Exception ex)
            {
                return new GetUserByIdResponse();
            }
        }
    }
}
