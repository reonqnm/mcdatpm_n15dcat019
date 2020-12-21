using DTO.Authentication;
using DTO.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.JWT.Authentication.Authentication
{
    public interface IEmployeeService
    {
        Task<EmployeeLogin> Authenticate(string email, string password);
        Task<List<Employee>> GetAll();
        Task<Employee> GetByID(int id);
        Task<int> Edit(Employee model);
        Task<int> Create(Employee model);
        Task<int> Delete(int id);
    }
}
