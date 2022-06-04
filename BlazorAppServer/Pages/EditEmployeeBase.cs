using Managments.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAppServer.Pages
{
    public class EditEmployeeBase:ComponentBase
    {
        public Employee Employee { get; set; } = new Employee();
    }
}
