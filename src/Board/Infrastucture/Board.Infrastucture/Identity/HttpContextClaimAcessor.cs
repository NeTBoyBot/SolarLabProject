using Doska.AppServices.Services.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Doska.Infrastructure.Identity
{
    public class HttpContextClaimAcessor : IClaimAcessor
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public HttpContextClaimAcessor(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public async Task<IEnumerable<Claim>> GetClaims(CancellationToken cancellation)
        {
            return _contextAccessor.HttpContext.User.Claims;
        }
    }
}
