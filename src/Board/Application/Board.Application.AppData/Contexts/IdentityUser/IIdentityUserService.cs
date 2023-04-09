using Board.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.IdentityUser
{
    public interface IIdentityUserService
    {
        public Task<string> Login(LoginUserRequest userLogin, CancellationToken cancellation);
        public Task<string> RegisterIdentityUser(RegisterUserRequest userReg, CancellationToken cancellation);

        public Task<string> GetCurrentUserId(CancellationToken cancellation);

        public Task DeleteAsync(string Id, CancellationToken cancellation);

        public Task<InfoUserResponse> GetCurrentUser(CancellationToken cancellation);
    }
}
