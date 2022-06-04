using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Managments.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restful_Api.Models;

namespace Restful_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository repo;
        public EmployeesController(IEmployeeRepository repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return Ok(await repo.GetEmployees());
        }
        [Route("{Id}")]
        [HttpGet]
        public async Task<ActionResult<Employee>> GetEmployee(int Id)
        {
            return await repo.GetEmployee(Id);
        }
        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee([FromBody]Employee employee)
        {
            try
            {
                var GetEmail = repo.GetEmployeeByEmail(employee.Email);

                if (GetEmail != null)
                {
                    ModelState.AddModelError("email", "Employee email already in use");

                    return BadRequest(ModelState);
                }

                if (employee == null) return BadRequest();

                await repo.AddEmployee(employee);

                return employee;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data  from database");
            }

        }
        [HttpPut("{Id:int}")] //await repo.UpdateEmployees(employee);
        public async Task<ActionResult<Employee>> UpdateEmp(int Id, Employee employee)
        {
            try
            {
                if (Id == employee.EmployeeId) return BadRequest("MisMatch");

                var get = repo.GetEmployee(Id);

                if (get != null) return NotFound($"Movcud deyil:{Id}");

                await repo.UpdateEmployee(employee);

                return employee;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data  from database");
            }
        }
        [Route("{Id:int}")]
        [HttpDelete]
        public async Task<ActionResult<Employee>> DeleteEmployee(int Id)
        {
            try
            {
                var get = repo.GetEmployee(Id);
                if (get == null)
                { return NotFound($"movcud deyil {Id}"); }

                return await repo.DeleteEmployee(Id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data  from database");
            }
        }

        //// [Route("{search}")]
        // [HttpGet("{search}")]
        // public async Task<ActionResult<IEnumerable<Employee>>>Search(string name,Gender gender)
        // {
        //     try
        //     {
        //         var get = await repo.Search(name, gender);
        //         if (get.Any())
        //         {
        //             return Ok(get);
        //         }
        //         return NotFound();
        //     }
        //     catch (Exception)
        //     {
        //         return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data  from database");
        //     }
        // }
        [Route("{search}")]
        [Route("{search}/{name}/{gender?}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> Search(string name, Gender? gender)
        {
            try
            {
                var result = await repo.Search(name, gender);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}