using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail
{
    public class GetLeaveRequestDetailQueryHandler : IRequestHandler<GetLeaveRequestDetailQuery, LeaveRequestDetailDto>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveRequestRepository _leaveRequestRepository;

        public GetLeaveRequestDetailQueryHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository)
        {
            _mapper = mapper;
            _leaveRequestRepository = leaveRequestRepository;
        }
        public async Task<LeaveRequestDetailDto> Handle(GetLeaveRequestDetailQuery request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetLeaveRequestWithDetals(request.Id);
            var requestDetail = _mapper.Map<LeaveRequestDetailDto>(leaveRequest);

            //Add Employees details as needed 
            return requestDetail;
        }
    }
}
