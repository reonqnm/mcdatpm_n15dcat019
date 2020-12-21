using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WebInside.Models;

namespace WebInside.Controllers
{
    public class EmployeeController : Controller
    {
        private IConfiguration Configuration { get; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public EmployeeController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            Configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index(string search)
        {
            var token = HttpContext.Session.GetString("Token");
            if (String.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login");
            }
            else
            {
                //var res = await Read();
                search = search == null ? "" : search;
                var res = await Read(search);
                var model = res == null ? new List<EmployeeViewModel>() : res;
                return View(model);
            }

        }

        public IActionResult Login(string message, string ReturnUrl = null)
        {
            var token = HttpContext.Session.GetString("Token");
            if (String.IsNullOrEmpty(token))
            {
                ViewBag.ReturnUrl = ReturnUrl;
                ViewBag.Message = message;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Employee");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Index", "Home");
        }

        #region identity
        [HttpPost]
        public async Task<IActionResult> Login(EmployeeLoginViewModel model)
        {
            var employeeLogin = new EmployeeLoginViewModel();
            // ViewBag.Error = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44350/api/Employee/");

                //HTTP POST
                var postTask = await client.PostAsJsonAsync<EmployeeLoginViewModel>("Login", model);
                //postTask.Wait();

                var result = postTask.Content.ReadAsStringAsync();
                employeeLogin = JsonConvert.DeserializeObject<EmployeeLoginViewModel>(result.Result);

            }
            if (employeeLogin == null)
            {
                //ViewBag.Error = "Email or Password is not correct!";
                return RedirectToAction("Login", new { message = "Email or Password is not correct!" });

            }
            else
            {
                try
                {
                    var tokenClient = new TokenClient(Configuration["AuthenticationServer:Authority"] + "connect/token", "Inside", Configuration["Client:Inside:Secret"]);
                    var tokenResponse = await tokenClient.RequestClientCredentialsAsync(Configuration["AuthenticationServer:ApiName"]);
                    if (!String.IsNullOrEmpty(employeeLogin.Email))
                        HttpContext.Session.SetString("Token", tokenResponse.AccessToken);
                    //return EmployeeViewModelList == null ? new List<EmployeeViewModel>() : EmployeeViewModelList;
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    throw ex;
                    throw;
                }
            }

        }

        [HttpGet]
        public async Task<List<EmployeeViewModel>> Read(string search)
        {
            var token = String.IsNullOrEmpty(_session.GetString("Token")) == true ? "" : _session.GetString("Token");

            List<EmployeeViewModel> EmployeeViewModelList = new List<EmployeeViewModel>();

            using (var httpClient = new HttpClient())
            {
                //httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                httpClient.SetBearerToken(token);
                using (var response = await httpClient.GetAsync("https://localhost:44362/api/Employee/GetAll?search=" + search))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    EmployeeViewModelList = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(apiResponse);
                }
            }
            return EmployeeViewModelList == null ? new List<EmployeeViewModel>() : EmployeeViewModelList;
        }

        public async Task<IActionResult> Form(int id, string message)
        {
            var token = String.IsNullOrEmpty(_session.GetString("Token")) == true ? "" : _session.GetString("Token");
            var employee = new EmployeeViewModel();
            if (id > 0)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44362/api/Employee/");
                    client.SetBearerToken(token);
                    //HTTP GET
                    var ressponse = await client.GetAsync("GetById/" + id);
                    //postTask.Wait();

                    var result = ressponse.Content.ReadAsStringAsync();

                    employee = JsonConvert.DeserializeObject<EmployeeViewModel>(result.Result);
                    if (employee == null)
                    {
                        return RedirectToAction("Index");
                    }

                }
            }
            employee.BirthDay = DateTime.Now.Date;
            ViewBag.Message = message;
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeViewModel model)
        {
            var token = String.IsNullOrEmpty(_session.GetString("Token")) == true ? "" : _session.GetString("Token");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44362/api/Employee/");
                client.SetBearerToken(token);
                //HTTP POST
                var postTask = await client.PostAsJsonAsync<EmployeeViewModel>("Edit", model);
                //postTask.Wait();

                var result = postTask.Content.ReadAsStringAsync();
                if (Int32.Parse(result.Result) > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Edit", new { id = model.Id, message = "Error has occurred" });
                }
                //employeeLogin = JsonConvert.DeserializeObject<EmployeeLoginViewModel>(result.Result);

            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            var token = String.IsNullOrEmpty(_session.GetString("Token")) == true ? "" : _session.GetString("Token");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44362/api/Employee/");
                client.SetBearerToken(token);
                //HTTP POST
                var postTask = await client.PostAsJsonAsync<EmployeeViewModel>("Create", model);
                //postTask.Wait();

                var result = postTask.Content.ReadAsStringAsync();
                if (Int32.Parse(result.Result) > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Create", new { id = model.Id, message = "Error has occurred" });
                }
                //employeeLogin = JsonConvert.DeserializeObject<EmployeeLoginViewModel>(result.Result);

            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var token = String.IsNullOrEmpty(_session.GetString("Token")) == true ? "" : _session.GetString("Token");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44362/api/Employee/");
                client.SetBearerToken(token);
                //HTTP DELETE
                var deleteTask = await client.DeleteAsync("Delete/" + id);
                //postTask.Wait();

                var result = deleteTask.Content.ReadAsStringAsync();
                return RedirectToAction("Index");
                //employeeLogin = JsonConvert.DeserializeObject<EmployeeLoginViewModel>(result.Result);

            }
        }
        #endregion
    }
}