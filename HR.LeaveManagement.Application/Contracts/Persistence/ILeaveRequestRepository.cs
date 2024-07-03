using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
    {

        Task <LeaveRequest> GetLeaveRequestWithDetals (int id);
        Task <List<LeaveRequest>> GetLeaveRequestWithDetals ();
        Task <List<LeaveRequest>> GetLeaveRequestByEmployeeId (string userId);

    }

}
