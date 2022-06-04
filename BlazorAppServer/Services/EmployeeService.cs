using Managments.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Components;

namespace BlazorAppServer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient client;
        public EmployeeService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<Employee> GetEmployee(int Id)
        {
            return await client.GetJsonAsync<Employee>($"api/employees/{Id}");
        }
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var r = await client.GetJsonAsync<Employee[]>("api/Employees");
            return r;
        }    
    }
}
