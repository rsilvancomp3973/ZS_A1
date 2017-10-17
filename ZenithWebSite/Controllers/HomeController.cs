using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ZenithDataLib.Models;
using System.Data;
using System.Data.Entity;




namespace ZenithWebSite.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Home
        public async Task<ActionResult> Index()
        {

            var culture = new CultureInfo("en-gb");
            var diff = DateTime.Now.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
                diff += 7;

            var firstdayoftheweek = DateTime.Now.AddDays(-diff).Date;
            var lastdayoftheweek = firstdayoftheweek.AddDays(7);

            ViewBag.firstday = firstdayoftheweek;
            ViewBag.lastday = lastdayoftheweek;

            var events = db.Events.Include(a => a.ActivityType).Include(a => a.EnteredBy).OrderBy(a => a.EventFrom).Where(a => a.EventFrom >= firstdayoftheweek && a.EventTo <= lastdayoftheweek);



            return View(await events.ToListAsync());
        }
    }
}