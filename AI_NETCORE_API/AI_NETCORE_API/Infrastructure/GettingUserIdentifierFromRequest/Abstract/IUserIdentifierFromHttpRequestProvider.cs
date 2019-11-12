using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AI_NETCORE_API.Infrastructure.GettingUserIdentifierFromRequest.Abstract
{
    public interface IUserIdentifierFromHttpRequestProvider
    {
        int GetUserIdFromRequest(HttpContext context);
    }
}
