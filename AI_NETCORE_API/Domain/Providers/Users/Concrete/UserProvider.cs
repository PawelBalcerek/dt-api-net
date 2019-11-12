using Domain.BusinessObject;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Providers.Users.Abstract;
using Domain.Providers.Users.Request.Abstract;
using Domain.Providers.Users.Response.Abstract;
using Domain.Providers.Users.Response.Concrete;
using Domain.Repositories.UserRepo.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Users.Concrete
{
    public class UserProvider : IUserProvider
    {
        private readonly ILogger _logger;
        private IUserRepository _users;
        public UserProvider(ILogger logger, IUserRepository users)
        {
            _logger = logger;
            _users = users;
        }

        public IGetUserByIdResponse GetUserById(IGetUserByIdRequest getUserByIdRequest)
        {
            try
            {
                var response = _users.GetUserById(getUserByIdRequest.Id);
                return new GetUserByIdResponse(response.Object, response.DatabaseTime);
            }
            catch(Exception ex)
            {
                _logger.Log(ex);
                return new GetUserByIdResponse();
            }
        }

        public ILoginUserResponse LoginUser(ILoginUserRequest loginUserRequest)
        {

            var response =_users.Authenticate(loginUserRequest.Email, loginUserRequest.Password);

            return new LoginUserResponse(response.Object, response.DatabaseTime);
        }
    }
}
