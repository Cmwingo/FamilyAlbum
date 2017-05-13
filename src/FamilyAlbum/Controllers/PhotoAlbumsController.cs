using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FamilyAlbum.Data;
using FamilyAlbum.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace FamilyAlbum.Controllers
{
    public class PhotoAlbumsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _env;
        private readonly UserManager<ApplicationUser> _userManager;

        public PhotoAlbumsController(ApplicationDbContext context, IHostingEnvironment env, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _env = env;
            _userManager = userManager;
        }

        // GET: PhotoAlbums
        public async Task<IActionResult> Index()
        {
            return View(await _context.PhotoAlbum.ToListAsync());
        }

        //GET: User's PhotoAlbums
        public async Task<IActionResult> UserAlbums()
        {
            var currentUser = _context.ApplicationUser.Where(au => au.UserName == User.Identity.Name).Include(au => au.Family).FirstOrDefault();

            return View(await _context.PhotoAlbum.Where(pa => pa.User == currentUser).Include(pa => pa.Images).ToListAsync());

        }

        //GET: Family's PhotoAlbums
        public async Task<IActionResult> FamilyAlbums()
        {
            var currentUser = _context.ApplicationUser.Where(au => au.UserName == User.Identity.Name).Include(au => au.Family).FirstOrDefault();

            return View(await _context.PhotoAlbum.Where(pa => pa.Family == currentUser.Family).Include(pa => pa.Images).ToListAsync());

        }

        // GET: PhotoAlbums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photoAlbum = await _context.PhotoAlbum.Include(pa => pa.Images).SingleOrDefaultAsync(m => m.PhotoAlbumId == id);
            if (photoAlbum == null)
            {
                return NotFound();
            }

            return View(photoAlbum);
        }

        // GET: PhotoAlbums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PhotoAlbums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhotoAlbumId,DateEnd,DateStart,Description,Name")] PhotoAlbum photoAlbum)
        {
            if (ModelState.IsValid)
            {
                var currentUser = _context.ApplicationUser.Where(au => au.UserName == User.Identity.Name).Include(au => au.Family).FirstOrDefault();
                var currentFamily = currentUser.Family;
                photoAlbum.User = currentUser;
                photoAlbum.Family = currentFamily;
                _context.Add(photoAlbum);
                await _context.SaveChangesAsync();
                return RedirectToAction("UserAlbums");
            }
            return View(photoAlbum);
        }

        // GET: PhotoAlbums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photoAlbum = await _context.PhotoAlbum.SingleOrDefaultAsync(m => m.PhotoAlbumId == id);
            if (photoAlbum == null)
            {
                return NotFound();
            }
            return View(photoAlbum);
        }

        // POST: PhotoAlbums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PhotoAlbumId,DateEnd,DateStart,Description,Name")] PhotoAlbum photoAlbum)
        {
            if (id != photoAlbum.PhotoAlbumId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(photoAlbum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhotoAlbumExists(photoAlbum.PhotoAlbumId))
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
            return View(photoAlbum);
        }

        // GET: PhotoAlbums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photoAlbum = await _context.PhotoAlbum.SingleOrDefaultAsync(m => m.PhotoAlbumId == id);
            if (photoAlbum == null)
            {
                return NotFound();
            }

            return View(photoAlbum);
        }

        // POST: PhotoAlbums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var photoAlbum = await _context.PhotoAlbum.Where(m => m.PhotoAlbumId == id).Include(m => m.Images).FirstOrDefaultAsync();
            foreach(var image in photoAlbum.Images)
            {
                _context.Image.Remove(image);
            }
            _context.PhotoAlbum.Remove(photoAlbum);
            await _context.SaveChangesAsync();
            return RedirectToAction("UserAlbums");
        }

        private bool PhotoAlbumExists(int id)
        {
            return _context.PhotoAlbum.Any(e => e.PhotoAlbumId == id);
        }

        public async Task<IActionResult> Add(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photoAlbum = await _context.PhotoAlbum.SingleOrDefaultAsync(m => m.PhotoAlbumId == id);
            if (photoAlbum == null)
            {
                return NotFound();
            }

            return View(photoAlbum);
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, string albumId, string name)
        {
            var webRoot = _env.WebRootPath;
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
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
                var currentAlbum = await _context.PhotoAlbum.Where(pa => pa.PhotoAlbumId.ToString() == albumId).Include(pa => pa.Images).FirstOrDefaultAsync();
                Image newImage = new Image
                {
                    Name = name,
                    FilePath = filePath
                };
                currentAlbum.Images.Add(newImage);
                await _context.SaveChangesAsync();
                ViewBag.Result = "File Uploaded!";
                return RedirectToAction("Add", new { id = albumId });
            }
            else
            {
                ViewBag.Result = "Error Uploading!";
                return RedirectToAction("Add", new { id = albumId });
            }
        }

    }
}
