using Managments.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restful_Api.Models
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(int Id);
        Task<Employee> GetEmployeeByEmail(string Email);
        Task<IEnumerable<Employee>> Search(string name, Gender? gender);
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> UpdateEmployees(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);
        Task<Employee> DeleteEmployee(int Id);
    }
}
