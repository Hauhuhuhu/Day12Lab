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
        private int pageSize = 1;
        public IActionResult Index(int? mid)
        {
            var learners = (IQueryable<Learner>)db.Learners
                    .Include(m => m.Major);
            if (mid != null)
            {
                learners = (IQueryable<Learner>)db.Learners
                            .Where(l => l.MajorID == mid)
                            .Include(m => m.Major);
            }
            //tính số trang
            int pageNum = (int)Math.Ceiling(learners.Count() / (float)pageSize);
            //trả số trang về view để hiển thị nav-trang
            ViewBag.pageNum = pageNum;
            //lấy dữ liệu trang đầu
            var result = learners.Take(pageSize).ToList();
            return View(result);
        }

        public IActionResult LearnerByMajorID(int mid)
        {
            var learners = db.Learners
                .Where(l => l.MajorID == mid)
                .Include(m => m.Major)
                .ToList();

            return PartialView("LearnerTable", learners);
        }

        public IActionResult LernerFilter(int? mid, string? keyword, int? pageIndex)
        {
            //lấy toàn bộ learners trong dbset chuyển về IQurrable<Learner> để query
            var learners = (IQueryable<Learner>)db.Learners;
            //lấy chỉ số trang, nếu chỉ số trang null thì gán ngầm định bằng 1
            int page = (int)(pageIndex == null || pageIndex <= 0 ? 1 : pageIndex);
            //nếu có mid thì lọc learner theo mid (chuyên ngành)
            if (mid != null)
            {
                //lọc
                learners = learners.Where(l => l.MajorID == mid);
                //gửi mid về view để ghi lại trên nav–phân trang
                ViewBag.mid = mid;
            }
            //nếu có keyword thì tìm kiếm theo tên
            if (keyword != null)
            {
                //tìm kiếm
                learners = learners.Where(l => l.FirstMidName.ToLower()
                                       .Contains(keyword.ToLower()));
                //gửi keyword về view để ghi trên nav–phân trang
                ViewBag.keyword = keyword;
            }
            //tính số trang
            int pageNum = (int)Math.Ceiling(learners.Count() / (float)pageSize);
            //gửi số trang về view để hiển thị nav–trang
            ViewBag.pageNum = pageNum;
            //chọn dữ liệu trong trang hiện tại
            var result = learners.Skip(pageSize * (page - 1))
                                 .Take(pageSize)
                                 .Include(m => m.Major);
            return PartialView("LearnerTable", result);
        }
    }
}
