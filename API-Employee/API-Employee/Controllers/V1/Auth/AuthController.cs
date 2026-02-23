using API_Blog.Controllers.Common;
using Application.Common;
using Application.CQRS.Commands.Auth.SignIn;
using Application.CQRS.Commands.Auth.SignUp;
using Application.CQRS.DTOs.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API_Employee.Controllers.V1.Auth
{
    public class AuthController : ApiController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Sign in
        /// </summary>
        [HttpPost("sign-in")]
        [ProducesResponseType(typeof(ApiResult<SignInDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignIn([FromBody] SignInCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Sign up
        /// </summary>
        [HttpPost("sign-up")]
        [ProducesResponseType(typeof(ApiResult<SignUpDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignUp([FromBody] SignUpCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
