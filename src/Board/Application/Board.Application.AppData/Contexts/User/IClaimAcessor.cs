using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Doska.AppServices.Services.User
{
    public interface IClaimAcessor
    {
        Task<IEnumerable<Claim>> GetClaims(CancellationToken token);
    }
}
