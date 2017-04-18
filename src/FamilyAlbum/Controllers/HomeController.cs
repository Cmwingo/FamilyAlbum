using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FamilyAlbum.Data;
using FamilyAlbum.Models;
using Microsoft.EntityFrameworkCore;

namespace FamilyAlbum.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var currentUser =  _context.ApplicationUser.Where(au => au.UserName == User.Identity.Name).Include(au => au.Family).FirstOrDefault();
            //if(currentUser != null)
            //{
            //    var family = currentUser?.Family;
            //    ViewBag.Family = family.Name;
            //}
            return View(currentUser);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
