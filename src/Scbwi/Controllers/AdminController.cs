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

        public IActionResult Index() {
            return View();
        }

        public IActionResult RegistrationTypes() {
            return View();
        }

        public IActionResult Tracks() {
            return View();
        }
        public IActionResult Comprehensives() {
            return View();
        }

        public IActionResult GetPackages() => Json(db.Packages.ToList());

        public IActionResult GetComprehensives() => Json(db.Comprehensives.ToList());

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

        public IActionResult DeleteBad() {
            var p = db.Tracks.Single(x => x.id == 1);
            db.Tracks.Remove(p);
            db.SaveChanges();

            return Json(true);
        }
    }
}