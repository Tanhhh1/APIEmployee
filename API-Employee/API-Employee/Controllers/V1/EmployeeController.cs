using API_Blog.Controllers.Common;
using Application.Common;
using Application.CQRS.Commands.Employees.Create;
using Application.CQRS.Commands.Employees.Delete;
using Application.CQRS.Commands.Employees.Update;
using Application.CQRS.Queries.Employees.GetAll;
using Application.CQRS.Queries.Employees.GetById;
using Application.CQRS.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API_Employee.Controllers.V1
{
    public class EmployeeController : ApiController
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<PageList<EmployeeDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllEmployeeQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(ApiResult<PageList<EmployeeDTO>>.Success(result));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResult<EmployeeDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetEmployeeByIdQuery(id));

            if (result == null)
                return NotFound(ApiResult<EmployeeDTO>.Failure(new[] { "Employee not found" }));

            return Ok(ApiResult<EmployeeDTO>.Success(result));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResult<EmployeeDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResult<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Succeeded)
                return BadRequest(result);

            return CreatedAtAction(nameof(GetById), new { id = result.Result!.Id }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResult<EmployeeDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResult<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEmployeeCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);

            if (!result.Succeeded)
            {
                if (result.Errors.Contains("Employee not found"))
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
            var result = await _mediator.Send(new DeleteEmployeeCommand { Id = id });

            if (result == null)
                return NotFound(ApiResult<EmployeeDTO>.Failure(new[] { "Employee not found" }));

            return Ok(result);
        }
    }
}
