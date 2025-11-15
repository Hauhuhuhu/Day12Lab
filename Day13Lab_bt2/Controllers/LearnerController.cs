using Day13Lab_bt2.Data;
using Day13Lab_bt2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Day13Lab_bt2.Controllers
{
    public class LearnerController : Controller
    {
        private SchoolContext db;

        public LearnerController(SchoolContext context)
        {
            db = context;
        }

        public IActionResult Index(int? mid)
        {
            if (mid == null)
            {
                var learners = db.Learners
                    .Include(m => m.Major)
                    .ToList();

                return View(learners);
            }
            else
            {
                var learners = db.Learners
                    .Where(l => l.MajorID == mid)
                    .Include(m => m.Major)
                    .ToList();

                return View(learners);
            }
        }

        public IActionResult LearnerByMajorID(int mid)
        {
            var learners = db.Learners
                .Where(l => l.MajorID == mid)
                .Include(m => m.Major)
                .ToList();

            return PartialView("LearnerTable", learners);
        }

    }
}
