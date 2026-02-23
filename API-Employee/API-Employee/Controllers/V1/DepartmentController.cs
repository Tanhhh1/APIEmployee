using API_Blog.Controllers.Common;
using Application.Common;
using Application.CQRS.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.CQRS.Queries.Departments.GetAll;
using Application.CQRS.Queries.Departments.GetById;
using Application.CQRS.Commands.Departments.Create;
using Application.CQRS.Commands.Departments.Update;
using Application.CQRS.Commands.Departments.Delete;

namespace API_Employee.Controllers.V1
{
    public class DepartmentController : ApiController
    {
        private readonly IMediator _mediator;

        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<IEnumerable<DepartmentDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllDepartmentQuery());
            return Ok(ApiResult<IEnumerable<DepartmentDTO>>.Success(result));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResult<DepartmentDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetDepartmentByIdQuery(id));

            if (result == null)
                return NotFound(ApiResult<DepartmentDTO>.Failure(new[] { "Department not found" }));

            return Ok(ApiResult<DepartmentDTO>.Success(result));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResult<DepartmentDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResult<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Succeeded)
                return BadRequest(result);

            return CreatedAtAction(nameof(GetById), new { id = result.Result!.Id }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResult<DepartmentDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResult<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDepartmentCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);

            if (!result.Succeeded)
            {
                if (result.Errors.Contains("Department not found"))
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
            var result = await _mediator.Send(new DeleteDepartmentCommand { Id = id });

            if (result == null)
                return NotFound(ApiResult<DepartmentDTO>.Failure(new[] { "Department not found" }));

            return Ok(result);
        }
    }
}
