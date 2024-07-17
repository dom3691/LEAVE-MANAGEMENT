﻿using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public record UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IAppLogger<UpdateLeaveTypeCommandHandler> _logger;

        public UpdateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, IAppLogger<UpdateLeaveTypeCommandHandler> logger)
        {
            this._mapper = mapper;
            this._leaveTypeRepository = leaveTypeRepository;
            this._logger = logger;
        }

        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            //validate incoming data
            var validator = new UpdateLeaveTypeCommandValidtor(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);
            
            if (validationResult.Errors.Any())
            {
                _logger.LogWarning("Validation Error in update request for {0}, -{1}", nameof (LeaveType), request.Id);
                throw new BadRequestException("invalid Leave Type", validationResult);
            }

            //convert to domain entity object 
            var leaveTypeToUpdate = _mapper.Map<Domain.LeaveType>(request);

            //add to database
            await _leaveTypeRepository.UpdateAsync(leaveTypeToUpdate);

            //return unit value  

            return Unit.Value;
        }
    }
}
