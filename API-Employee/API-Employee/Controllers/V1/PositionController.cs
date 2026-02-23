using API_Blog.Controllers.Common;
using Application.Common;
using Application.CQRS.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.CQRS.Queries.Positions.GetAll;
using Application.CQRS.Queries.Positions.GetById;
using Application.CQRS.Commands.Positions.Create;
using Application.CQRS.Commands.Positions.Update;
using Application.CQRS.Commands.Positions.Delete;

namespace API_Employee.Controllers.V1
{
    public class PositionController : ApiController
    {
        private readonly IMediator _mediator;

        public PositionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<IEnumerable<PositionDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllPositionQuery());
            return Ok(ApiResult<IEnumerable<PositionDTO>>.Success(result));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResult<PositionDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetPositionByIdQuery(id));

            if (result == null)
                return NotFound(ApiResult<PositionDTO>.Failure(new[] { "Position not found" }));

            return Ok(ApiResult<PositionDTO>.Success(result));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResult<PositionDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResult<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreatePositionCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Succeeded)
                return BadRequest(result);

            return CreatedAtAction(nameof(GetById), new { id = result.Result!.Id }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResult<PositionDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResult<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePositionCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);

            if (!result.Succeeded)
            {
                if (result.Errors.Contains("Position not found"))
                    return NotFound(result);
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResult<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult<bool>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeletePositionCommand { Id = id });

            if (result == null)
                return NotFound(ApiResult<PositionDTO>.Failure(new[] { "Position not found" }));

            return Ok(result);
        }
    }
}
