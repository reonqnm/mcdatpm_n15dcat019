using DTO.Authentication;
using DTO.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetByID(int id);
        //Task<List<Employee>> GetByDateOfBirth(DateTime dateOfBirth);
        Task<List<Employee>> GetAll(string search);
        Task<EmployeeLogin> Login(string email, string password);
        Task<int> Edit(Employee model);
        Task<int> Create(Employee model);
        Task<int> Delete(int id);
    }
}
