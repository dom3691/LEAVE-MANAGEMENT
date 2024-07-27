using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveRequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<LeaveRequestsController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveRequestListDto>>> Get(bool isLoggedInUser = false)
        {
            var leaveRequests = await _mediator.Send (new GetLeaveRequestListQuery());
            return Ok(leaveRequests);
        }

        // GET api/<LeaveRequestsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveRequestDetailDto>>Get(int id)
        {
            var leaveRequest = await _mediator.Send(new GetLeaveRequestDetailQuery { Id = id });
            return Ok(leaveRequest);
        }

        // POST api/<LeaveRequestsController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task <ActionResult> Post(CreateLeaveRequestCommand reuest)
        {
            await _mediator.Send(reuest);
            return Ok();
            
        }


        // PUT api/<LeaveRequestsController>/5
        [HttpPut]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(UpdateLeaveRequestCommand leaveRequest)
        {
            await _mediator.Send(leaveRequest);
            return NoContent();
        }

        // DELETE api/<LeaveRequestsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var deleteRequest = new DeleteLeaveRequestCommand { Id = id };
            await _mediator.Send(deleteRequest);
            return NoContent();
        }

        // PUT API/<LeaveRequestsController>/5
        [HttpPut]
        [Route("CancelRequest")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CancelRequest(CancelLeaveRequestCommand cancelLeaveRequest)
        {
            await _mediator.Send(cancelLeaveRequest);
            return NoContent();

        }


        // PUT API/<LeaveRequestsController>/5
        [HttpPut]
        [Route("UpdateApproval")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateApproval(UpdateLeaveRequestCommand updateLeaveRequest)
        {
            await _mediator.Send(updateLeaveRequest);
            return NoContent();

        }

    }
}
