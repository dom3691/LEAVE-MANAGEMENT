using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest
{
    public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IEmailSender _emailSender;

        public CancelLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IEmailSender emailSender)
        {
            this._leaveRequestRepository = leaveRequestRepository;
            this._emailSender = emailSender;
        }
        public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

            if (leaveRequest == null)
            {
                throw new NotFoundException(nameof(leaveRequest), request.Id);
            }

            leaveRequest.Cancelled = true;

            //Re-Evaluate Leave Allocation for the leave type 

            //send Confirmation email
            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"Your Leave Request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been cancelled successfully.",
                Subject = "Leave Request Cancelled"
            };

            return Unit.Value;
        }
    }
}
