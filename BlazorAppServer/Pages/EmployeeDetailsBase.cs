using BlazorAppServer.Services;
using Managments.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAppServer.Pages
{
    public class EmployeeDetailsBase:ComponentBase
    {
        public Employee emp { get; set; } = new Employee();
        [Inject]
        public IEmployeeService repo { get; set; }
        [Parameter]
        public string Id { get; set; }

        protected string ButtonText { get; set; } = "Hide Footer";
        protected string CssClass { get; set; }
        //protected override async Task OnInitializedAsync()
        //{
        //    Id = Id ?? "1";
        //    emp = await repo.GetEmployee(int.Parse(Id));
        //}
        protected void Button_Click()
        {
            if(ButtonText== "Hide Footer")
            {
                ButtonText = "Show Footer";
                CssClass = "HideFooter";
            }
            else
            {
                ButtonText = "Hide Footer";
                CssClass = null;
            }
        }
    }
}
