using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FamilyAlbum.Data;
using FamilyAlbum.Models;
using FamilyAlbum.Models.ReplyViewModel;

namespace FamilyAlbum.Controllers
{
    public class RepliesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RepliesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Replies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reply.ToListAsync());
        }

        // GET: Replies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reply = await _context.Reply.SingleOrDefaultAsync(m => m.ReplyId == id);
            if (reply == null)
            {
                return NotFound();
            }

            return View(reply);
        }

        // GET: Replies/Create
        public IActionResult Create(int postId)
        {
            ViewBag.PostId = postId;
            return View();
        }

        // POST: Replies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Body,PostId")] CreateReplyViewModel reply)
        {
            if (ModelState.IsValid)
            {
                var currentUser = _context.ApplicationUser.Where(au => au.UserName == User.Identity.Name).FirstOrDefault();
                var currentPost = await _context.Post.Where(p => p.PostId == reply.PostId).FirstOrDefaultAsync();
                Reply newReply = new Reply()
                {
                    Body = reply.Body,
                    Post = currentPost,
                    User = currentUser
                };
                _context.Add(newReply);
                currentPost.Replies.Add(newReply);
                await _context.SaveChangesAsync();
                return Redirect("/posts/familyposts");
            }
            return View(reply);
        }

        // GET: Replies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reply = await _context.Reply.SingleOrDefaultAsync(m => m.ReplyId == id);
            if (reply == null)
            {
                return NotFound();
            }
            return View(reply);
        }

        // POST: Replies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReplyId,Body")] Reply reply)
        {
            if (id != reply.ReplyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reply);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReplyExists(reply.ReplyId))
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
            return View(reply);
        }

        // GET: Replies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reply = await _context.Reply.SingleOrDefaultAsync(m => m.ReplyId == id);
            if (reply == null)
            {
                return NotFound();
            }

            return View(reply);
        }

        // POST: Replies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reply = await _context.Reply.SingleOrDefaultAsync(m => m.ReplyId == id);
            _context.Reply.Remove(reply);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ReplyExists(int id)
        {
            return _context.Reply.Any(e => e.ReplyId == id);
        }
    }
}
