using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Scbwi.Models;
using Microsoft.AspNet.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Scbwi.Controllers {
    [Authorize]
    public class AdminController : Controller {
        private readonly ApplicationDbContext db;

        public AdminController(ApplicationDbContext appctx) {
            db = appctx;
        }

        public IActionResult Index() => View();

        public IActionResult RegistrationTypes() => View();

        public IActionResult Tracks() => View();

        public IActionResult Comprehensives() => View();

        public IActionResult Dates() => View();

        public IActionResult Coupons() => View();

        public IActionResult Registrations() => View();

        public IActionResult GetPackages() => Json(db.Packages.ToList());

        public IActionResult GetComprehensives() => Json(db.Comprehensives.ToList());

        public IActionResult GetDates() => Json(db.Dates.ToList());

        public IActionResult GetRegistrations() => Json(db.Registrations.ToList());

        public IActionResult GetCoupons() => Json(db
            .CouponCodes
            .ToList()
            .Select(x => new {
                type = x.type.ToString(),
                value = x.value,
                text = x.text
            }));

        [HttpPost]
        public IActionResult AddPackage([FromBody] Package p) {
            if (ModelState.IsValid) {
                db.Packages.Add(p);
                db.SaveChanges();
            }

            return Json(true);
        }

        public IActionResult AddTrack([FromBody] Track t) {
            if (ModelState.IsValid) {
                db.Tracks.Add(t);
                db.SaveChanges();
            }

            return Json(true);
        }

        public IActionResult AddComprehensive([FromBody] Comprehensive c) {
            if (ModelState.IsValid) {
                db.Comprehensives.Add(c);
                db.SaveChanges();
            }

            return Json(true);
        }

        public IActionResult AddCoupon([FromBody] CouponCode c) {
            if (ModelState.IsValid) {
                db.CouponCodes.Add(c);
                db.SaveChanges();
            }

            return Json(true);
        }

        public IActionResult AddDate([FromBody] ImportantDate d) {
            if (ModelState.IsValid) {
                db.Dates.Add(d);
                db.SaveChanges();
            }

            return Json(true);
        }

        public IActionResult DeleteBad() {
            var p = db.Tracks.Single(x => x.id == 1);
            db.Tracks.Remove(p);
            db.SaveChanges();

            return Json(true);
        }
    }
}