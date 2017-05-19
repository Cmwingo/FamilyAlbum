using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FamilyAlbum.Data;
using FamilyAlbum.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace FamilyAlbum.Controllers
{
    public class ImagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _env;
        private readonly UserManager<ApplicationUser> _userManager;

        public ImagesController(ApplicationDbContext context, IHostingEnvironment env, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _env = env;
            _userManager = userManager;   
        }

        // GET: Images
        public async Task<IActionResult> Index()
        {
            return View(await _context.Image.ToListAsync());
        }

        // GET: Images/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Image.Where(m => m.ImageId == id).Include(m => m.PhotoAlbum).ThenInclude(pa => pa.User).SingleOrDefaultAsync();
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // GET: Images/Create
        public IActionResult Create()
        {
            var currentUser = _context.ApplicationUser.Where(au => au.UserName == User.Identity.Name).FirstOrDefault();
            var photoAlbums = _context.PhotoAlbum.Where(pa => pa.User == currentUser).ToList();
            ViewBag.PhotoAlbumId = new SelectList(photoAlbums, "PhotoAlbumId", "Name");
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImageId,Caption,Date,FilePath,Name,UploadTime")] Image image)
        {
            if (ModelState.IsValid)
            {
                if(Request.Form["PhotoAlbumId"].ToString() != null)
                {
                    var albumId = Request.Form["PhotoAlbumId"].ToString();
                    var intAlbumId = Int32.Parse(albumId);
                    var currentAlbum = _context.PhotoAlbum.Where(pa => pa.PhotoAlbumId == intAlbumId).Include(pa => pa.Images).FirstOrDefault();
                    currentAlbum.Images.Add(image);
                    _context.Entry(currentAlbum).State = EntityState.Modified;
                }
                _context.Add(image);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(image);
        }

        // GET: Images/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Image.SingleOrDefaultAsync(m => m.ImageId == id);
            if (image == null)
            {
                return NotFound();
            }
            return View(image);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImageId,Caption,Date,FilePath,Name,UploadTime")] Image image)
        {
            if (id != image.ImageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(image);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageExists(image.ImageId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("/photoalbums/useralbums");
            }
            return View(image);
        }

        // GET: Images/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Image.SingleOrDefaultAsync(m => m.ImageId == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var image = await _context.Image.SingleOrDefaultAsync(m => m.ImageId == id);
            _context.Image.Remove(image);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ImageExists(int id)
        {
            return _context.Image.Any(e => e.ImageId == id);
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var webRoot = _env.WebRootPath;
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            var upload = Path.Combine(webRoot, "uploads");
            var uploads = Path.Combine(upload, currentUser.Id);

            if(!Directory.Exists(uploads))
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
                return RedirectToAction("Create");
            }
            else
            {
                return RedirectToAction("Create");
            }
        }
    }
}
