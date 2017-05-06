using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Microsoft.AspNet.Identity;

namespace WebApi.Controllers.Helpers
{
    public class AuthorizationHelper
    {
        public static bool IsValidAuthorization(int userId, HttpRequestMessage request)
        {
            if (request.GetRequestContext().Principal.IsInRole("Admin"))
            {
                return true;
            }
            return userId == Convert.ToInt32(request.GetRequestContext().Principal.Identity.GetUserId());
        }
    }
}