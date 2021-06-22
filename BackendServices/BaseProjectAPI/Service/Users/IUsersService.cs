using BaseProjectAPI.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace BaseProjectAPI.Service.Users
{
    public interface IUsersService
    {
        Task<User> Authenticate(string email, string password, CancellationToken cancellationToken);
    }

    public interface IUsersSecurityService
    {
        //AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress);
        //AuthenticateResponse RefreshToken(string token, string ipAddress);
        //bool RevokeToken(string token, string ipAddress);
        //IEnumerable<User> GetAll();
        //User GetById(int id);
        string GenerateJwtToken(User user);
    }
}
