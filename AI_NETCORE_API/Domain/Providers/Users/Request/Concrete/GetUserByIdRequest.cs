
using Domain.Providers.Users.Request.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Providers.Users.Request.Concrete
{
    public class GetUserByIdRequest : IGetUserByIdRequest
    {
        public GetUserByIdRequest(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
