using Data.BuisnessObject;
using Data.Infrastructure.Logging.Abstract;
using Data.Providers.Users.Abstract;
using Data.Providers.Users.Request.Abstract;
using Data.Providers.Users.Response.Abstract;
using Data.Providers.Users.Response.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Providers.Users.Concrete
{
    public class MockedUserProvider : IUserProvider
    {
        private readonly ILogger _logger;
        private readonly IList<User> _users;
        public MockedUserProvider(ILogger logger)
        {
            _logger = logger;
            _users = PrepareMockedElements();
        }

        private IList<User> PrepareMockedElements()
        {
            return new List<User>
            {
                new User(1,"Name1","email1@email1.pl","__wqasfdaadascz123s="),
                new User(2,"Name2","email2@email2.pl","__wqasfdaadascz123s=")
            };
        }

        public IGetUserByIdResponse GetUserById(IGetUserByIdRequest getUserByIdRequest)
        {
            try
            {
                //TODO get from repository
                User mockedUser = _users.ToList().FirstOrDefault(x => x.Id == getUserByIdRequest.Id);
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
