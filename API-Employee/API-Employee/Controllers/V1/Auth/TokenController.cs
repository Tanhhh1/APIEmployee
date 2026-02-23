using API_Blog.Controllers.Common;
using Application.Common;
using Application.CQRS.Commands.Auth.RefreshToken;
using Application.CQRS.Commands.Auth.RevokeToken;
using Application.CQRS.DTOs.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API_Employee.Controllers.V1.Auth
{
    public class TokenController : ApiController
    {
        private readonly IMediator _mediator;

        public TokenController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Refresh access token using refresh token
        /// </summary>
        [HttpPost("refresh")]
        [ProducesResponseType(typeof(ApiResult<RefreshTokenDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult<object>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RefreshToken(
            [FromBody] RefreshTokenCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(ApiResult<RefreshTokenDTO>.Success(result));
        }

        /// <summary>
        /// Revoke refresh token
        /// </summary>
        [HttpPost("revoke")]
        [ProducesResponseType(typeof(ApiResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RevokeToken(
            [FromBody] RevokeTokenCommand command)
        {
            await _mediator.Send(command);
            return Ok(ApiResult<object>.Success(null));
        }
    }
}
