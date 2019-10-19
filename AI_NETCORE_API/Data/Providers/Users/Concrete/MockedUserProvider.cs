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
    public class MockedUserProvider : IUserProvider
    {
        private readonly ILogger _logger;

        public MockedUserProvider(ILogger logger)
        {
            _logger = logger;
        }

        public IGetUserByIdResponse GetUserById(IGetUserByIdRequest getUserByIdRequest)
        {
            try
            {
                //TODO get from repository
                User mockedUser = new User(getUserByIdRequest.Id, "Name", "email@email.pl", "__xZsd123Zdc!a3");
                return new GetUserByIdResponse(mockedUser);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return new GetUserByIdResponse();
            }
        }
    }
}
