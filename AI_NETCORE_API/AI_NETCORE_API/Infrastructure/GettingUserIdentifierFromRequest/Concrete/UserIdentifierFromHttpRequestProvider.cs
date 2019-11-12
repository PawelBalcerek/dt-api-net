using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AI_NETCORE_API.Infrastructure.GettingUserIdentifierFromRequest.Abstract;
using Microsoft.AspNetCore.Http;

namespace AI_NETCORE_API.Infrastructure.GettingUserIdentifierFromRequest.Concrete
{
    public class UserIdentifierFromHttpRequestProvider : IUserIdentifierFromHttpRequestProvider
    {
        public int GetUserIdFromRequest(HttpContext context)
        {
            if(!(context.User.Identity is ClaimsIdentity identity)) throw new InvalidProgramException("Use get user id only in methods required authentication");
            Claim claim = identity.Claims.FirstOrDefault(c => c.Type == "Id");
            if (claim == null) throw new InvalidProgramException("Use get user id only in methods required authentication");
            if(int.TryParse(claim.Value, out int result))
            {
                return result;
            }
            throw new InvalidProgramException("Value in User Identity is not int");
        }
    }
}
