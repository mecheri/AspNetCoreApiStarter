using AspNetCoreApiStarter.Model;
using AspNetCoreApiStarter.Resources;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AspNetCoreApiStarter.Security
{
    /// <summary>
    /// UserCtxResolver
    /// </summary>
    public class UserCtxResolver : IUserCtx
    {
        private readonly IHttpContextAccessor contextAccessor;

        public UserCtxResolver(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        public int Id => int.Parse(contextAccessor.HttpContext.User.FindFirst(Constants.JwtClaimIdentifiers.Id).Value);

        public string UserName => contextAccessor.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub).Value;

        public string Email => contextAccessor.HttpContext.User.FindFirst(Constants.JwtClaimIdentifiers.Email).Value;

        public string FirstName => contextAccessor.HttpContext.User.FindFirst(Constants.JwtClaimIdentifiers.FirstName).Value;

        public string LastName => contextAccessor.HttpContext.User.FindFirst(Constants.JwtClaimIdentifiers.LastName).Value;

        public void Check()
        {
            foreach( var c in contextAccessor.HttpContext.User.Claims)
            {
                System.Diagnostics.Debug.WriteLine( $"{c.Type} {c.Value}");
            }
        }
    }
}
