using Hr.LeaveManagement.BlazorUI.Contracts;
using Hr.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Hr.LeaveManagement.BlazorUI.Services;
using Microsoft.AspNetCore.Components;

namespace Hr.LeaveManagement.BlazorUI.Pages.LeaveTypes
{
    public partial class Index
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ILeaveTypeService LeaveTypeService { get; set; }
        public List<LeaveTypeVM> LeaveTypes { get; private set; }
        public string Message { get; set; } = string.Empty;
        protected void CreateLeaveType()
        {
            NavigationManager.NavigateTo("/leavetypes/create/");

        }

        protected void AllocateLeaveType(int id)
        {
            //use Leave Allocation Service Here 
        }

        protected void EditLeaveType(int id)
        {
            NavigationManager.NavigateTo($"/leavetypes/edit/{id}");

        }

        protected void DetailsLeaveType(int id)
        {
            NavigationManager.NavigateTo($"/leavetypes/details/{id}");

        }

        protected async Task DeleteLeaveType(int id)
        {
            var response = await LeaveTypeService.DeleateLeaveType(id);
            if (response.Success)
            {
                StateHasChanged();
            }
            else
            {
                Message = response.Message;
            }
        }


        protected override async Task OnInitializedAsync()
        {
            LeaveTypes = await LeaveTypeService.GetLeaveTypes();
        }
    }
}