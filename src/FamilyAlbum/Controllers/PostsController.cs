using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FamilyAlbum.Data;
using FamilyAlbum.Models;
using Microsoft.AspNetCore.Authorization;

namespace FamilyAlbum.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Post.ToListAsync());
        }
        // GET: Family Posts
        public async Task<IActionResult> FamilyPosts()
        {
            var currentUser = _context.ApplicationUser.Where(au => au.UserName == User.Identity.Name).Include(au => au.Family).FirstOrDefault();

            return View(await _context.Post.Where(p => p.PostFamily == currentUser.Family).Include(p => p.User).Include(p => p.Replies).ToListAsync());
        }


        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.Where(m => m.PostId == id).Include(m => m.Replies).ThenInclude(r => r.User).SingleOrDefaultAsync();
            post.Replies = post.Replies.OrderByDescending(r => r.ReplyTime).ToList();
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            DateTime currentTime = DateTime.Now;
            ViewBag.Timestamp = currentTime;
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,Body,PostTime,TStamp,Title")] Post post)
        {
            if (ModelState.IsValid)
            {
                var currentUser = _context.ApplicationUser.Where(au => au.UserName == User.Identity.Name).Include(au => au.Family).ThenInclude(fa => fa.Posts).FirstOrDefault();
                var currentFamily = currentUser.Family;
                post.User = currentUser;
                post.PostFamily = currentFamily;
                _context.Add(post);
                await _context.SaveChangesAsync();
                currentFamily.Posts.Add(post);
                return RedirectToAction("FamilyPosts");
            }
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.SingleOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,Body,PostTime,TStamp,Title")] Post post)
        {
            if (id != post.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("FamilyPosts");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.SingleOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Post.SingleOrDefaultAsync(m => m.PostId == id);
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction("FamilyPosts");
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.PostId == id);
        }
    }
}
