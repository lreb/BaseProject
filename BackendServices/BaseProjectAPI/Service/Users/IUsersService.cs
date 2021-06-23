using BaseProjectAPI.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace BaseProjectAPI.Service.Users
{
    public interface IUsersService
    {
        /// <summary>
        /// Validates if the user exist
        /// </summary>
        /// <param name="email">email password</param>
        /// <param name="password">password</param>
        /// <param name="cancellationToken">cancellation token for the call</param>
        /// <returns>User data</returns>
        Task<User> Authenticate(string email, string password, CancellationToken cancellationToken);
    }

    public interface IUsersSecurityService
    {
        //AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress);
        //AuthenticateResponse RefreshToken(string token, string ipAddress);
        //bool RevokeToken(string token, string ipAddress);
        //IEnumerable<User> GetAll();
        //User GetById(int id);

        /// <summary>
        /// Generates JWT for specific user
        /// </summary>
        /// <param name="user">User data</param>
        /// <returns>JWT</returns>
        string GenerateJwtToken(User user);
    }
}
