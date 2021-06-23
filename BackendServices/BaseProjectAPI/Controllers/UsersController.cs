using BaseProjectAPI.Service.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace BaseProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// Mediator service
        /// </summary>
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Authenticate User
        /// </summary>
        /// <param name="model">User parameters to perform this action user and password <see cref="AuthenticateQuery"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns>User data and JWT<see cref="AuthenticateQuery"/></returns>
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateQuery model, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(model, cancellationToken);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
    }
}
