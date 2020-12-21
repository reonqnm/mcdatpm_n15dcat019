using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO.Authentication;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;

namespace API.Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepo;

        public EmployeeController(IEmployeeRepository employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login([FromBody] EmployeeLogin employee)
        {
            var res = await _employeeRepo.Login(employee.Email , employee.Password);
            if (res == null)
                return BadRequest();
            return Ok(res); 
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<string>> GetAll()
        {
            return "Hello";
        }

        //[HttpGet]
        //[Route("GetAll")]
        //public async Task<ActionResult<List<Employee>>> GetByID(DateTime dateOfBirth)
        //{
        //    return await _employeeRepo.GetByDateOfBirth(dateOfBirth);
        //}
    }
}