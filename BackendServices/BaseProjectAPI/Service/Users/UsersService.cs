using BaseProjectAPI.Domain.Models;
using BaseProjectAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
}
