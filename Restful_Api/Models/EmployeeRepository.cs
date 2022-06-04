using Managments.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restful_Api.Models
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly AppDbContext app;
        public EmployeeRepository(AppDbContext app)
        {
            this.app = app;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var emp = await app.Employees.AddAsync(employee);
            await app.SaveChangesAsync();
            return emp.Entity;
        }

        public async Task<Employee> DeleteEmployee(int Id)
        {
            var emp = await app.Employees.FindAsync(Id);
            if (emp != null)
            {
                app.Employees.Remove(emp);
                await app.SaveChangesAsync();
            }
            return emp;
        }

        public async Task<Employee> GetEmployee(int Id)
        {
            //var result = await app.Employees.FindAsync(Id);
            //var result = await app.Employees.Where(e => e.EmployeeId == Id).FirstOrDefaultAsync();
            var result = await app.Employees.Include(e => e.Department).FirstOrDefaultAsync(e=>e.EmployeeId==Id);
            return result;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await app.Employees.ToListAsync();
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var find = await app.Employees.FindAsync(employee.EmployeeId);
            if (find != null)
            {

                find.FirstName = employee.FirstName;
                find.LastName = employee.LastName;
                find.Email = employee.Email;
                find.DateOfBrith = employee.DateOfBrith;
                find.Gender = employee.Gender;
                find.DepartmentId = employee.DepartmentId;

                await app.SaveChangesAsync();
                return find;
            }
            return null;

        }
        public async Task<Employee> UpdateEmployees(Employee employee)
        {
            app.Entry(employee).State = EntityState.Modified;
            await app.SaveChangesAsync();
            return employee;
        }
        public async Task<Employee> GetEmployeeByEmail(string Email)
        {
            return await app.Employees.FindAsync(Email);
        }

        public async Task<IEnumerable<Employee>> Search(string name, Gender? gender)
        {
            IQueryable<Employee> ls = app.Employees;
            if (!string.IsNullOrEmpty(name))
            {
                ls = ls.Where(e => e.FirstName.Contains(name) || e.LastName.Contains(name));

            }
            if (gender != null)
            {
                ls = ls.Where(e => e.Gender == gender);
            }
            return await ls.ToListAsync();
        }
    }
}
