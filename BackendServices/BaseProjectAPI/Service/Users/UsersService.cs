using BaseProjectAPI.Domain.Helpers;
using BaseProjectAPI.Domain.Models;
using BaseProjectAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BaseProjectAPI.Service.Users
{
    public class UsersService : IUsersService
    {
        private readonly BaseDataContext _context;

        public UsersService(BaseDataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Validates if the user exist
        /// </summary>
        /// <param name="email">email password</param>
        /// <param name="password">password</param>
        /// <param name="cancellationToken">cancellation token for the call</param>
        /// <returns>User data</returns>
        public async Task<User> Authenticate(string email, string password, CancellationToken cancellationToken)
        {
            return await _context.Users.Where(a => a.Email.Equals(email) && a.IsEnabled).AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }
    }

    /// <summary>
    /// Auxiliary functionality to provide authentication
    /// </summary>
    public class UsersSecurityService : IUsersSecurityService
    {
        /// <summary>
        /// JWT options binding
        /// </summary>
        private readonly JwtOptions _jwtOptions;

        private readonly BaseDataContext _context;

        public UsersSecurityService(
            IOptionsSnapshot<JwtOptions> jwtOptions, BaseDataContext context)
        {
            _jwtOptions = jwtOptions.Value;
            _context = context;
        }

        /// <summary>
        /// Generates a new JWT
        /// </summary>
        /// <param name="user">user data<see cref="User"/></param>
        /// <returns>JWT as string</returns>
        string IUsersSecurityService.GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
