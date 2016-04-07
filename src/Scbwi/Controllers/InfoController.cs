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

        private async Task<Totals> calc(RegistrationViewModel r) {
            try {
                // package
                var package = await db.Packages.SingleOrDefaultAsync(x => x.id == r.packageid);

                // comprehensive
                var comprehensive = await db.Comprehensives.SingleOrDefaultAsync(x => x.id == r.comprehensiveid);

                // critiques
                var portfolio = r.portfolio * 50;
                var manuscript = r.manuscript * 50;

                // subtotal
                var late = await db.Dates.SingleOrDefaultAsync(x => x.title == "latereg") ?? new ImportantDate { date = new DateTime(2016, 6, 6) };

                var subtotal = 0.0m;

                if (package != null) {
                    subtotal += DateTime.Now < late.date ? package.regularprice : package.lateprice;
                }

                if (comprehensive != null) {
                    subtotal += comprehensive.price;
                }

                subtotal += portfolio;
                subtotal += manuscript;

                // coupons
                var coupon = await db.CouponCodes.SingleOrDefaultAsync(x => x.text == r.coupon);

                var total = subtotal;

                if (coupon != null) {
                    switch (coupon.type) {
                        case CodeType.Percent:
                            total = subtotal * coupon.value;
                            break;
                        case CodeType.Total:
                            total = coupon.value;
                            break;
                        case CodeType.Critique:
                            if (portfolio >= 50 || manuscript >= 50) total = subtotal - 50;
                            break;
                        default: break;
                    }
                }

                return new Totals {
                    subtotal = subtotal,
                    total = total
                };
            } catch {
                return new Totals {
                    subtotal = 0,
                    total = 0
                };
            }
        }

        public async Task<IActionResult> CalculateTotal([FromBody] RegistrationViewModel r) => Json(await calc(r));

        public async Task<IActionResult> Submit([FromBody] RegistrationViewModel r) {
            var totals = await calc(r);

            var paypalid = $"{r.first}{r.last}{DateTime.Now.Ticks}";

            var registration = new Registration {
                address1 = r.address1,
                address2 = r.address2,
                city = r.city,
                country = r.country,
                state = r.state,
                zip = r.zip,
                cleared = new DateTime(2000, 1, 1),
                code = await db.CouponCodes.SingleOrDefaultAsync(x => x.text == r.coupon),
                comprehensive = await db.Comprehensives.SingleOrDefaultAsync(x => x.id == r.comprehensiveid),
                critiques = new List<Critique>(),
                email = r.email,
                first = r.first,
                last = r.last,
                package = await db.Packages.SingleOrDefaultAsync(x => x.id == r.packageid),
                paid = new DateTime(2000, 1, 1),
                paypalid = paypalid,
                phone = r.phone,
                submitted = DateTime.Now,
                subtotal = totals.subtotal,
                total = totals.total,
                track = await db.Tracks.SingleOrDefaultAsync(x => x.id == r.trackid)
            };

            for (int i = 0; i < r.manuscript; i++) {
                registration.critiques.Add(new Critique {
                    price = 50,
                    type = "Manuscript"
                });
            }

            for (int i = 0; i < r.portfolio; i++) {
                registration.critiques.Add(new Critique {
                    price = 50,
                    type = "Portfolio"
                });
            }

            db.Registrations.Add(registration);
            await db.SaveChangesAsync();

            return Json(new {
                total = totals.total,
                paypalid = paypalid
            });
        }
    }

    public class Totals {
        public decimal subtotal { get; set; }
        public decimal total { get; set; }
    }
}
