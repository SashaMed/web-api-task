using Entities.DataTransferObjects;
using Entities.Helpers;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> CreateToken();

		Task<string> CreateToken(User user);

		RefreshToken GenerateRefreshToken();
        Task UpdateUser(RefreshToken newRefreshToken);

        Task<User> GetUserFromJwtAsync(string token);

    }
}
