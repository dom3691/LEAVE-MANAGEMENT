﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType
{
    public class LeaveTypeRequest
    {
        public string Name { get; set; } = string.Empty;
        public int Decription { get; set; }
        public int DefaultDays { get; set; }
    }
}
