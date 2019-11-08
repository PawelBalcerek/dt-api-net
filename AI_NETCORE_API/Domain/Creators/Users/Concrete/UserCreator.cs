using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.BusinessObject;
using Domain.Creators.Users.Abstract;
using Domain.Creators.Users.Request.Abstract;
using Domain.Creators.Users.Response.Abstract;
using Domain.Creators.Users.Response.Concrete;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Repositories.UserRepo.Abstract;

namespace Domain.Creators.Users.Concrete
{
    public class UserCreator : IUserCreator
    {
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;

        public UserCreator(ILogger logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public IUserCreateResponse CreateUser(IUserCreateRequest userCreateRequest)
        {
            try
            {
                var response = _userRepository.CreateUser(userCreateRequest.UserName,userCreateRequest.Password,userCreateRequest.Email);
    
                return new UserCreateResponse(response.Object, response.DatabaseTime);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new UserCreateResponse();
            }
        }
    }
}
