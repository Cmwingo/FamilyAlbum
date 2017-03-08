using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FamilyAlbum.Data;
using FamilyAlbum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace FamilyAlbum.Controllers
{
    public class FamiliesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _env;
        private readonly UserManager<ApplicationUser> _userManager;

        public FamiliesController(ApplicationDbContext context, IHostingEnvironment env, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _env = env;
            _userManager = userManager;
        }

        // GET: Families
        public async Task<IActionResult> Index()
        {
            return View(await _context.Family.ToListAsync());
        }

        // GET: Families/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var family = await _context.Family.SingleOrDefaultAsync(m => m.FamilyId == id);
            if (family == null)
            {
                return NotFound();
            }

            return View(family);
        }

        // GET: Families/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Families/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FamilyId,Motto,Name,PhotoURL")] Family family)
        {
            if (ModelState.IsValid)
            {
                _context.Add(family);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(family);
        }

        // GET: Families/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var family = await _context.Family.SingleOrDefaultAsync(m => m.FamilyId == id);
            if (family == null)
            {
                return NotFound();
            }
            return View(family);
        }

        // POST: Families/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FamilyId,Motto,Name,PhotoURL")] Family family)
        {
            if (id != family.FamilyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(family);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FamilyExists(family.FamilyId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(family);
        }

        // GET: Families/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var family = await _context.Family.SingleOrDefaultAsync(m => m.FamilyId == id);
            if (family == null)
            {
                return NotFound();
            }

            return View(family);
        }

        // POST: Families/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var family = await _context.Family.SingleOrDefaultAsync(m => m.FamilyId == id);
            _context.Family.Remove(family);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool FamilyExists(int id)
        {
            return _context.Family.Any(e => e.FamilyId == id);
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, string familyId)
        {
            var webRoot = _env.WebRootPath;
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            var currentFamily = familyId;
            var upload = Path.Combine(webRoot, "uploads");
            var uploads = Path.Combine(upload, currentUser.Id);

            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }


            if (file != null)
            {
                long size = file.Length;
                using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                var filePath = Path.Combine("/uploads/" + currentUser.Id, file.FileName);
                TempData["Path"] = filePath;
                return RedirectToAction("Edit", new { Id = currentFamily });
            }
            else
            {
                return RedirectToAction("Edit");
            }
        }

        public async Task<IActionResult> DoUpload(IFormFile file, string familyId)
        {
            var webRoot = _env.WebRootPath;
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            var currentFamily = familyId;
            var upload = Path.Combine(webRoot, "uploads");
            var uploads = Path.Combine(upload, currentUser.Id);

            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }


            if (file != null)
            {
                long size = file.Length;
                using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                var filePath = Path.Combine("/uploads/" + currentUser.Id, file.FileName);
                TempData["Path"] = filePath;
                return RedirectToAction("Edit", new { Id = currentFamily });
            }
            else
            {
                return RedirectToAction("Edit");
            }
        }

    }
}
