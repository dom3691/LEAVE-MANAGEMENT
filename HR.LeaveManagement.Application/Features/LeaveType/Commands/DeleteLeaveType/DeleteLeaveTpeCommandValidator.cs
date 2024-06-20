using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType
{
    public class DeleteLeaveTpeCommandValidator : AbstractValidator<DeleteLeaveTypeCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public DeleteLeaveTpeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
        {

            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("Id Is required")
                .NotNull().WithMessage("Id is requred")
                ;

            this._leaveTypeRepository = leaveTypeRepository;
        }


    }
}
