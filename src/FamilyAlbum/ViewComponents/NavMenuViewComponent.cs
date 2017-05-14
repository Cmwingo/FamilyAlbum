using FamilyAlbum.Data;
using FamilyAlbum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyAlbum.ViewComponents
{
    public class NavMenuViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public NavMenuViewComponent(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentUser =  await _context.ApplicationUser.Where(au => au.UserName == User.Identity.Name).Include(au => au.Family).FirstOrDefaultAsync();
            if (currentUser?.Family != null)
            {
                return View(currentUser.Family);
            }
            else
            {
                return View();
            }
        }
    }
}
