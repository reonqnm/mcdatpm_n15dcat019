using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebInside.Models;
using Microsoft.AspNetCore.Http;
namespace WebInside.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //var token = "Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6Ijg5YzJkMDI0MmE3Yjk1ZWE5MjdkYTJlODJkN2Q2OTU2IiwidHlwIjoiSldUIn0.eyJuYmYiOjE1NzA0Mzk4OTgsImV4cCI6MTU3MzAzMTg5OCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNTAiLCJhdWQiOlsiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNTAvcmVzb3VyY2VzIiwiQVBJLkNvcmUiXSwiY2xpZW50X2lkIjoiSW5zaWRlIiwic2NvcGUiOlsiQVBJLkNvcmUiXX0.TQ9Cs0BeFyqCppg0BA-lHClrBGCro8OC0gx89EeJH1kQH3UCMybSRFjYKi7ZpRQaCrGb12q2LtgF8hB2Y6MshVBST_J4fdGMzOgOFwO274GxSlqfnz_7RV55-qJ-0MuZ8A983ypZCayeQ4U3I4dUxELhGxy2NSDTrL_Y7_P3jhcOg_wt0UI3WWJXAtjy8L5BAXI_hTPHGYiP-L_GXkikzU3f2odu4Fo84ld68lKpSBykwqswF3J_Y74Q1jTTqLQqvCfnUfdU3JYhszs7m6EPnSen4QHSGTEHTVp3tpSE7wPP5FtHlHoBT1aRVW3PhBrhZvlTo8QuPPW2BS48OjxfGw";
            //HttpContext.Session.SetString("Token", token);
            ViewBag.Session = HttpContext.Session.GetString("Token");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
