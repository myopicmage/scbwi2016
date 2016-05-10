using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Scbwi.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.Data.Entity;
using CsvHelper;
using Microsoft.Data.Entity.Query.Internal;
using Newtonsoft.Json;

namespace Scbwi.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext db;

        public AdminController(ApplicationDbContext appctx)
        {
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

        public IActionResult GetRegistrations()
        {
            var r = db.Registrations
               .Include(x => x.comprehensive)
               .Include(x => x.track)
               .Include(x => x.package)
               .Include(x => x.code)
               .Include(x => x.critiques)
               .ToList();

            return Json(ToDTOList(r));
        }

        public IActionResult GetCoupons() => Json(db
            .CouponCodes
            .ToList()
            .Select(x => new
            {
                type = x.type.ToString(),
                x.value,
                x.text
            }));

        public IActionResult DumpCSV()
        {
            var records = db.Registrations
                .Include(x => x.comprehensive)
                .Include(x => x.track)
                .Include(x => x.package)
                .Include(x => x.code)
                .Include(x => x.critiques)
                .Select(ToDTO)
                .ToList();

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            var csv = new CsvWriter(writer);
            csv.WriteRecords(records);
            writer.Flush();
            stream.Position = 0;

            return File(stream, "text/csv", $"registrations-{DateTime.Now.ToString("yyyy-M-d")}.csv");
        }

        [HttpPost]
        public IActionResult AddPackage([FromBody] Package p)
        {
            if (ModelState.IsValid)
            {
                db.Packages.Add(p);
                db.SaveChanges();
            }

            return Json(true);
        }

        [HttpPost]
        public IActionResult AddTrack([FromBody] Track t)
        {
            if (ModelState.IsValid)
            {
                db.Tracks.Add(t);
                db.SaveChanges();
            }

            return Json(true);
        }

        [HttpPost]
        public IActionResult AddComprehensive([FromBody] Comprehensive c)
        {
            if (ModelState.IsValid)
            {
                db.Comprehensives.Add(c);
                db.SaveChanges();
            }

            return Json(true);
        }

        [HttpPost]
        public IActionResult AddCoupon([FromBody] CouponCode c)
        {
            if (ModelState.IsValid)
            {
                db.CouponCodes.Add(c);
                db.SaveChanges();
            }

            return Json(true);
        }

        [HttpPost]
        public IActionResult AddDate([FromBody] ImportantDate d)
        {
            if (ModelState.IsValid)
            {
                db.Dates.Add(d);
                db.SaveChanges();
            }

            return Json(true);
        }

        [HttpPost]
        public IActionResult DeleteRegistration(int regid)
        {
            var r = db.Registrations.SingleOrDefault(x => x.id == regid);

            if (r == null)
            {
                return Json(false);
            }

            try
            {
                db.Registrations.Remove(r);
                db.SaveChanges();
            }
            catch
            {
                return Json(false);
            }

            return Json(true);
        }

        private List<RegistrationDTO> ToDTOList(IEnumerable<Registration> list) => list.Select(ToDTO).ToList();

        private RegistrationDTO ToDTO(Registration r) => new RegistrationDTO
        {
            first = r.first,
            last = r.last,
            address1 = r.address1,
            address2 = r.address2,
            city = r.city,
            cleared = r.cleared,
            comprehensive = r.comprehensive?.title ?? "None",
            country = r.country,
            coupon = r.code?.text ?? "None",
            email = r.email,
            id = r.id,
            manuscript = r.critiques?.Count(x => x.type == "Manuscript") ?? 0,
            package = r.package?.title ?? "None",
            paid = r.paid,
            phone = r.phone,
            portfolio = r.critiques?.Count(x => x.type == "Portfolio") ?? 0,
            state = r.state,
            submitted = r.submitted,
            subtotal = r.subtotal,
            total = r.total,
            track = r.track?.title ?? "None",
            zip = r.zip
        };
    }
}