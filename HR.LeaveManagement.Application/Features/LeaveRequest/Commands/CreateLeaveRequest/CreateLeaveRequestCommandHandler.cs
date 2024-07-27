using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Unit>
    {
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveRequestCommandHandler(IEmailSender sender,
            IMapper mapper, ILeaveRequestRepository leaveRequestRepository,
            ILeaveTypeRepository leaveTypeRepository)
        {
            _emailSender = sender;
            _mapper = mapper;
            this._leaveRequestRepository = leaveRequestRepository;
            this._leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {

            var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Any())
            {
                throw new BadRequestException("Invalid LeaveResult Request", validationResult);
            }


            //Get requesting employees id 
            //Check on employees allocation
            //if allocations arent enough, return validation error with message 

            //create leave reqyest
            var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request);
            await _leaveRequestRepository.CreateAsync(leaveRequest);


            //send confirmation email

            var email = new EmailMessage
            {
                To = string.Empty, /*Get Email from employee record */
                Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} has been submitted successfully ",
                Subject = "Leave Request Submitted"

            };

            await _emailSender.SendEmail(email);
            return Unit.Value;
        }
    }
}
