using AutoMapper;
using BaseProjectAPI.Domain.ViewModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BaseProjectAPI.Service.Users.Queries
{
    public class AuthenticateQuery : IRequest<UserAuthenticateViewModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public class AuthenticateUserQueryHandler : IRequestHandler<AuthenticateQuery, UserAuthenticateViewModel>
        {
            public IUsersService _userService;
            public IUsersSecurityService _usersSecurityService;
            /// <summary>
            /// Auto mapper service
            /// </summary>
            private readonly IMapper _mapper;

            public AuthenticateUserQueryHandler(IUsersService userService, IMapper mapper, IUsersSecurityService usersSecurityService)
            {
                _userService = userService;
                _mapper = mapper;
                _usersSecurityService = usersSecurityService;
            }

            public async Task<UserAuthenticateViewModel> Handle(AuthenticateQuery request, CancellationToken cancellationToken)
            {
                var user = await _userService.Authenticate(request.Email, request.Password, cancellationToken);
                var authenticatedUser = _mapper.Map<UserAuthenticateViewModel>(user);
                authenticatedUser.Token = _usersSecurityService.GenerateJwtToken(user);
                return authenticatedUser;
            }
        }
    }
}
