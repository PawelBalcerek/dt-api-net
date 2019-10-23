using Domain.BusinessObject;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Providers.Users.Abstract;
using Domain.Providers.Users.Request.Abstract;
using Domain.Providers.Users.Response.Abstract;
using Domain.Providers.Users.Response.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Users.Concrete
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
