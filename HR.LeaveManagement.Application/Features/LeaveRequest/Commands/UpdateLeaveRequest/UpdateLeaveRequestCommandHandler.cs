using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IAppLogger<UpdateLeaveRequestCommandHandler> _appLogger;

        public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository,
            IMapper mapper, IEmailSender emailSender, IAppLogger<UpdateLeaveRequestCommandHandler> appLogger)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
            this._emailSender = emailSender;
            _appLogger = appLogger;
        }
        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);
            if (leaveRequest == null)
            {
                throw new NotFoundException(nameof(LeaveRequest), request.Id);
            } 

            var validator = new UpdateLeaveRequestCommandValidator(_leaveRequestRepository, _leaveTypeRepository);
            var validtionResult = await validator.ValidateAsync(request);
            
            if (validtionResult.Errors.Any())
            {
                throw new BadRequestException("Invalid Leave Request", validtionResult);


            }

            _mapper.Map(request, leaveRequest);

            await _leaveRequestRepository.UpdateAsync(leaveRequest);


            //send confirmation email
            try
            {
                var email = new EmailMessage
                {
                    To = string.Empty,
                    Body = $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} " + $"has been updated Successfully.",
                    Subject = "Leave Request Updated"
                };

            }
            catch (Exception ex)
            {
                _appLogger.LogWarning(ex.Message);
            }

            return Unit.Value;
        }
    }
}
