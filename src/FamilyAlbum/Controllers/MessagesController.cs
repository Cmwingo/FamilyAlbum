using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FamilyAlbum.Data;
using FamilyAlbum.Models;
using FamilyAlbum.Models.MessageViewModels;

namespace FamilyAlbum.Controllers
{
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MessagesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Messages
        public async Task<IActionResult> Index()
        {
            var currentUser = _context.ApplicationUser.Where(au => au.UserName == User.Identity.Name).Include(au => au.Family).FirstOrDefault();
            ViewBag.CurrentUserId = currentUser.Id;
            return View(await _context.Message.Include(m => m.Recipients).ToListAsync());
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message.SingleOrDefaultAsync(m => m.MessageId == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: Messages/Create
        public IActionResult Create()
        {
            var currentUser = _context.ApplicationUser.Where(au => au.UserName == User.Identity.Name).Include(au => au.Family).FirstOrDefault();
            var family = _context.Family.Where(fa => fa.FamilyId == currentUser.Family.FamilyId).Include(fa => fa.Members).FirstOrDefault();
            ViewBag.Members = new SelectList(family.Members, "Id", "FirstName");
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecipientIds,Body,Title")] CreateMessageViewModel message)
        {
            if (ModelState.IsValid)
            {
                var currentUser = _context.ApplicationUser.Where(au => au.UserName == User.Identity.Name).FirstOrDefault();
                var recipient = _context.ApplicationUser.Where(au => au.Id == message.RecipientIds).FirstOrDefault();
                Message newMessage = new Message()
                {
                    Title = message.Title,
                    Body = message.Body,
                    Sender = currentUser,
                    Read = false,
                };
                _context.Add(newMessage);
                await _context.SaveChangesAsync();

                var userMessage = new ApplicationUserMessage()
                {
                    Message = newMessage,
                    User = recipient,
                    ApplicationUserId = recipient.Id,
                    MessageId = newMessage.MessageId
                };
                _context.Add(userMessage);
                await _context.SaveChangesAsync();
                var messageWithRecipients = _context.Message.Where(m => m.MessageId == newMessage.MessageId).Include(m => m.Recipients).FirstOrDefault();
                messageWithRecipients.Recipients.Add(userMessage);
                currentUser.OutgoingMessages.Add(newMessage);
                recipient.IncomingMessages.Add(userMessage);
                await _context.SaveChangesAsync();

                //message.Sender = _context.ApplicationUser.Where(au => au.UserName == User.Identity.Name).FirstOrDefault();

                return RedirectToAction("Index");
            }
            return View(message);
        }

        // GET: Messages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message.SingleOrDefaultAsync(m => m.MessageId == id);
            if (message == null)
            {
                return NotFound();
            }
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MessageId,Body,PostTime,Read,Title")] Message message)
        {
            if (id != message.MessageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(message);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(message.MessageId))
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
            return View(message);
        }

        // GET: Messages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message.SingleOrDefaultAsync(m => m.MessageId == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _context.Message.SingleOrDefaultAsync(m => m.MessageId == id);
            _context.Message.Remove(message);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MessageExists(int id)
        {
            return _context.Message.Any(e => e.MessageId == id);
        }
    }
}
