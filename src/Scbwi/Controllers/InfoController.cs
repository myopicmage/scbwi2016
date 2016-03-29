using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Scbwi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scbwi.Controllers {
    public class InfoController : Controller {
        private readonly ApplicationDbContext db;

        public InfoController(ApplicationDbContext appctx) {
            db = appctx;
        }

        public IActionResult GetPackages() {
            var late = db.Dates.SingleOrDefault(x => x.title == "latereg");
            var db_packages = db.Packages.ToList();

            return Json(db_packages.Select(x => new {
                description = x.description,
                id = x.id,
                maxattendees = x.max,
                ismemberpackage = x.member,
                price = late == null || late.date.Date >= DateTime.Now.Date ? x.regularprice : x.lateprice,
                title = x.title
            }));
        }

        public async Task<IActionResult> GetComprehensives() => Json(
            await db.Comprehensives
                .Where(x => db.Registrations.Where(y => y.comprehensive.id == x.id).Count() < x.maxattendees)
                .ToListAsync()
            );

        public IActionResult GetTracks() => Json(db.Tracks.ToList());

        public IActionResult GetCritiques() => Json(db.Critiques.ToList());

        public async Task<IActionResult> ValidateCoupon(string code) {
            var c_code = await db.CouponCodes.SingleOrDefaultAsync(x => x.text == code);

            if (c_code == null) {
                return Json(new {
                    Success = false,
                    Error = "Invalid coupon code"
                });
            } else {
                return Json(new {
                    Success = true,
                    Code = c_code
                });
            }
        }
    }
}
