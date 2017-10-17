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

namespace ZenithWebSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ActivityTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ActivityTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.Activities.OrderBy(a => a.ActivityDescription).ToListAsync());
        }

        // GET: ActivityTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityType activityType = await db.Activities.FindAsync(id);
            if (activityType == null)
            {
                return HttpNotFound();
            }
            return View(activityType);
        }

        // GET: ActivityTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActivityTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ActivityTypeId,ActivityDescription")] ActivityType activityType)
        {
            if (ModelState.IsValid)
            {
                activityType.CreationDate = DateTime.Now;
                db.Activities.Add(activityType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(activityType);
        }

        // GET: ActivityTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityType activityType = await db.Activities.FindAsync(id);
            if (activityType == null)
            {
                return HttpNotFound();
            }
            return View(activityType);
        }

        // POST: ActivityTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ActivityTypeId,ActivityDescription")] ActivityType activityType)
        {
            if (ModelState.IsValid)
            {
                activityType.CreationDate = DateTime.Now;
                db.Entry(activityType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(activityType);
        }

        // GET: ActivityTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityType activityType = await db.Activities.FindAsync(id);
            if (activityType == null)
            {
                return HttpNotFound();
            }
            return View(activityType);
        }

        // POST: ActivityTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ActivityType activityType = await db.Activities.FindAsync(id);
            db.Activities.Remove(activityType);
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
