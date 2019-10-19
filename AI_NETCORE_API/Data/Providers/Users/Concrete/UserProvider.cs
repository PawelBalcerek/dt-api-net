using Data.BuisnessObject;
using Data.Infrastructure.Logging.Abstract;
using Data.Providers.Users.Abstract;
using Data.Providers.Users.Request.Abstract;
using Data.Providers.Users.Response.Abstract;
using Data.Providers.Users.Response.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Providers.Users.Concrete
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
                return new GetUserByIdResponse(new User(getUserByIdRequest.Id,"Nazwa","Email@email.pl","__acxzzzXczx12zA"));
            }
            catch(Exception ex)
            {
                _logger.Log(ex);
                return new GetUserByIdResponse();
            }
        }
        
    }
}
