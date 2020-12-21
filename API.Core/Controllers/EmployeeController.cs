using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;

namespace API.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepo;

        public EmployeeController(IEmployeeRepository employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<Employee> GetById(int id)
        {
            var res = await _employeeRepo.GetByID(id);
            return res;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<Employee>> GetAll(string search)
        {
            var res = await _employeeRepo.GetAll(search);
            return res;
        }

        [HttpPost]
        [Route("Edit")]
        public async Task<int> Edit(Employee model)
        {
            return await _employeeRepo.Edit(model);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<int> Create(Employee model)
        {
            return await _employeeRepo.Create(model);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<int> Delete(int id)
        {
            return await _employeeRepo.Delete(id);
        }
    }
}