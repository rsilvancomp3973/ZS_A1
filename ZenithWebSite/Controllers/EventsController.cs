using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZenithDataLib.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Web.Security;

namespace ZenithWebSite.Controllers
{
    [Authorize(Roles = "Member, Admin")]
    public class EventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Events
        public async Task<ActionResult> Index()
        {
            var events = db.Events.Include(a => a.ActivityType).Include(a => a.EnteredBy).OrderBy(a => a.EventFrom);


           
            return View(await events.ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Include(a => a.ActivityType).Include(a => a.EnteredBy).FirstOrDefault(a => a.EventId == id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.ActivityTypeId = new SelectList(db.Activities, "ActivityTypeId", "ActivityDescription");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([Bind(Include = "EventId,EventFrom,EventTo,isActive,ActivityTypeId")] Event @event)
        {
            if (@event.EventFrom >= @event.EventTo || @event.EventFrom.ToShortDateString() != @event.EventTo.ToShortDateString()) {
                ViewBag.Message = "Make sure the event occurs on the same day and that the event end time comes after the event start time.";
                ViewBag.ActivityTypeId = new SelectList(db.Activities, "ActivityTypeId", "ActivityDescription");
                return View();
            }
            else if (ModelState.IsValid)
            {
                var store = new UserStore<ApplicationUser>(db);
                var manager = new UserManager<ApplicationUser>(store);
                var user = manager.FindById(User.Identity.GetUserId());

                @event.CreationDate = DateTime.Now;
                @event.EnteredBy = user;
                db.Events.Add(@event);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ActivityTypeId = new SelectList(db.Activities, "ActivityTypeId", "ActivityDescription", @event.ActivityTypeId);
            return View(@event);
        }

        // GET: Events/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = await db.Events.FindAsync(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityTypeId = new SelectList(db.Activities, "ActivityTypeId", "ActivityDescription", @event.ActivityTypeId);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit([Bind(Include = "EventId,EventFrom,EventTo,isActive,ActivityTypeId")] Event @event)
        {
            if (@event.EventFrom >= @event.EventTo || @event.EventFrom.ToShortDateString() != @event.EventTo.ToShortDateString())
            {
                ViewBag.Message = "Make sure the event occurs on the same day and that the event end time comes after the event start time.";
                ViewBag.ActivityTypeId = new SelectList(db.Activities, "ActivityTypeId", "ActivityDescription");
                return View(@event);
            }

            else if (ModelState.IsValid)
            {

                var store = new UserStore<ApplicationUser>(db);
                var manager = new UserManager<ApplicationUser>(store);
                var user = manager.FindById(User.Identity.GetUserId());

                @event.CreationDate = DateTime.Now;
                @event.EnteredBy = user;
                db.Entry(@event).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityTypeId = new SelectList(db.Activities, "ActivityTypeId", "ActivityDescription", @event.ActivityTypeId);
            return View(@event);
        }

        // GET: Events/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Include(a => a.ActivityType).Include(a => a.EnteredBy).FirstOrDefault(a => a.EventId == id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Event @event = await db.Events.FindAsync(id);
            db.Events.Remove(@event);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
