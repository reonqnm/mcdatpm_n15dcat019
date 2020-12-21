using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.JWT.Authentication.Authentication;
using DTO.Authentication;
using DTO.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.JWT.Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] EmployeeLogin employee)
        {
            var user = await _employeeService.Authenticate(employee.Email, employee.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _employeeService.GetAll();
            return Ok(users);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await _employeeService.GetByID(id);
            if (res != null)
                return Ok(res);
            else
                return BadRequest(); 
        }

        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> Edit(Employee model)
        {
            var res = await _employeeService.Edit(model);
            if (res > 0)
                return Ok(res);
            else
                return BadRequest(res);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Employee model)
        {
            var res =  await _employeeService.Create(model);
            if (res > 0)
                return Ok(res);
            else
                return BadRequest(res);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _employeeService.Delete(id);
            if (res > 0)
                return Ok(res);
            else
                return BadRequest(res);
        }
    }
}