using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval
{
    public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
    {
        private readonly IEmailSender _emailSender;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public ChangeLeaveRequestApprovalCommandHandler(IEmailSender emailSender, ILeaveRequestRepository leaveRequestRepository, 
                                                        IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            this._emailSender = emailSender;
            this._leaveRequestRepository = leaveRequestRepository;
            this._mapper = mapper;
            this._leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

            if (leaveRequest == null)
                throw new NotFoundException(nameof(LeaveRequest), request.Id);

            leaveRequest.Approved = request.Approved;
            await _leaveRequestRepository.UpdateAsync(leaveRequest);

            //IF request is approved, get and update the employees allocation

         //   var employeeAllocation = await _leaveTypeRepository.GetEmployeeAllocationsByType(leaveRequest.EmployeeId, leaveRequest.LeaveTypeId);

            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"The Approval Status for your leave Request for  {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been updated.",
                Subject = "Leave Request approval status updated "
            };

            return Unit.Value;
        }
    }
}
