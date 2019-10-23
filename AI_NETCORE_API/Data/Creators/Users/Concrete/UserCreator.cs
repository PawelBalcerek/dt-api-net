using System;
using System.Collections.Generic;
using System.Text;
using Domain.BusinessObject;
using Domain.Creators.Users.Abstract;
using Domain.Creators.Users.Request.Abstract;
using Domain.Creators.Users.Response.Abstract;
using Domain.Creators.Users.Response.Concrete;
using Domain.Infrastructure.Logging.Abstract;

namespace Domain.Creators.Users.Concrete
{
    public class UserCreator : IUserCreator
    {
        private readonly ILogger _logger;

        public UserCreator(ILogger logger)
        {
            _logger = logger;
        }

        public IUserCreateResponse CreateUser(IUserCreateRequest userCreateRequest)
        {
            try
            {
                // todo take information from repository
                return new UserCreateResponse(true);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new UserCreateResponse();
            }
        }
    }
}
