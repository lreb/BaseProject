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

        public async Task<User> Authenticate(string email, string password, CancellationToken cancellationToken)
        {
            return await _context.Users.Where(a => a.Email.Equals(email)).AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }
    }

    public class UsersSecurityService : IUsersSecurityService
    {
        private readonly AppSettings _appSettings;

        public UsersSecurityService(
            IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        string IUsersSecurityService.GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
