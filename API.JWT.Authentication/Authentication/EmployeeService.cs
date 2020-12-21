using API.JWT.Authentication.Model;
using Dapper;
using DTO.Authentication;
using DTO.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.JWT.Authentication.Authentication
{
    public class EmployeeService : IEmployeeService 
    {
        private readonly TokenManagement _tokenManagement;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IConfiguration _config;
        public EmployeeService( IOptions<TokenManagement> tokenManagement, IEmployeeRepository employeeRepo, IConfiguration config)
        {
            _tokenManagement = tokenManagement.Value;
            _employeeRepo = employeeRepo;
            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("MyConnectionString"));
            }
        }

        public async Task<EmployeeLogin> Authenticate(string email, string password)
        {
            var user = await _employeeRepo.Login(email, password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenManagement.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email.ToString())
                }),
                Expires = DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return user; throw new System.NotImplementedException();
        }

        public async Task<List<Employee>> GetAll()
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT * FROM Employee";
                conn.Open();
                var result = await conn.QueryAsync<Employee>(sQuery, null);
                return result.ToList();
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
    }
}
