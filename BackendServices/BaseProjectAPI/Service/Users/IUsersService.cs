using BaseProjectAPI.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace BaseProjectAPI.Service.Users
{
    public interface IUsersService
    {
        Task<User> Authenticate(string email, string password, CancellationToken cancellationToken);
    }
}
