using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Data.SQLite;
using System.IO;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
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

        public IActionResult UsersList()
        {
            var UsersList = new UserService().GetListUsers();
            
            return View(UsersList);
        }

        public IActionResult CreateUsers()
        {
            var result = new UserService().CreateTableUsers();
            return Redirect("UsersList");
        }


        [HttpGet]
        public IActionResult SetBunUser(int id, int ban)
        {
            //if (id==0)
            //{
            //    Redirect("UsersList");
            //}

            ban = ban == 1 ? 0 : 1;


            var result = new UserService().SetBanStatusUsers(id, ban);

            return RedirectToAction("UsersList");
        }
    }
}
