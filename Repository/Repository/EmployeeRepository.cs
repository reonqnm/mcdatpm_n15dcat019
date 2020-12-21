using Dapper;
using DTO.Authentication;
using DTO.Core;
using Microsoft.Extensions.Configuration;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration _config;

        public EmployeeRepository(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("MyConnectionString"));
            }
        }

        public async Task<Employee> GetByID(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT * FROM Employee WHERE Id = @Id";
                conn.Open();
                var result = await conn.QueryAsync<Employee>(sQuery, new { Id = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<List<Employee>> GetAll(string search)
        {
            using (IDbConnection conn = Connection)
            {
                search = search == null ? "" : search;
                #region sql injection
                //string sQuery = "SELECT * FROM Employee e WHERE e.FullName LIKE '%" + search + "%'";
                //conn.Open();
                //var result = await conn.QueryAsync<Employee>(sQuery, null);
                //return result.ToList();
                #endregion
                #region No SQLinjection
                var user = conn.Query<Employee>("spGetEmployee", new { Search = search}, commandType: CommandType.StoredProcedure);
                //var result = await conn.QueryAsync<Employee>(sQuery, null);
                return user.ToList();
                #endregion

            }
        }
        

        public async Task<int> Edit(Employee model)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    //string sQuery = "SELECT * FROM Employee WHERE Email = ";
                    conn.Open();
                    var result = conn.Query<int>("spEditEmployee", new { Id = model.Id, FullName = model.FullName, Phone = model.Phone, Address = model.Address, Email = model.Email, Password = model.Password, BirthDay = model.BirthDay }, commandType: CommandType.StoredProcedure).First();
                    //var result = await conn.QueryAsync<Employee>(sQuery, null);
                    return result;
                }
            }
            catch (Exception ex)
            {

                //throw ex;
                return 0;
            }

        }

        public async Task<int> Create(Employee model)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    //string sQuery = "SELECT * FROM Employee WHERE Email = ";
                    conn.Open();
                    var result = conn.Query<int>("spCreateEmployee", new { Id = model.Id, FullName = model.FullName, Phone = model.Phone, Address = model.Address, Email = model.Email, Password = model.Password, BirthDay = model.BirthDay }, commandType: CommandType.StoredProcedure).First();
                    //var result = await conn.QueryAsync<Employee>(sQuery, null);
                    return result;
                }
            }
            catch (Exception ex)
            {

                //throw ex;
                return 0;
            }

        }

        public async Task<int> Delete(int id)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    //string sQuery = "SELECT * FROM Employee WHERE Email = ";
                    conn.Open();
                    var result = conn.Query<int>("spDeleteEmployee", new { Id = id }, commandType: CommandType.StoredProcedure).First();
                    //var result = await conn.QueryAsync<Employee>(sQuery, null);
                    return result;
                }
            }
            catch (Exception ex)
            {

                //throw ex;
                return 0;
            }

        }

        public async Task<EmployeeLogin> Login(string email, string password)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    //string sQuery = "SELECT * FROM Employee WHERE Email = ";
                    conn.Open();
                    var user = conn.Query<EmployeeLogin>("spLogin", new { Email = email, Password = password }, commandType: CommandType.StoredProcedure).First();
                    //var result = await conn.QueryAsync<Employee>(sQuery, null);
                    return user;
                }
            }
            catch (Exception ex)
            {

                //throw ex;
                return null;
            }
            
        }
    }
}
