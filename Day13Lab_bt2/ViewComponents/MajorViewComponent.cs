using Day13Lab_bt2.Data;
using Day13Lab_bt2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Day13Lab_bt2.ViewComponents
{
    public class MajorViewComponent:ViewComponent
    {
        SchoolContext db;
        List<Major> majors;

        public MajorViewComponent(SchoolContext _context)
        {
            db = _context;
            majors = db.Majors.ToList();
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("RenderMajor", majors);
        }
    }
}
