using Gioithieuvieclam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gioithieuvieclam.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private jobEntities3 db = new jobEntities3();
        private jobEntities3 job = new jobEntities3();
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View(job.CongViecs.ToList());
        }

        public ActionResult QLSinhVien()
        {
            var sinhViens = db.NguoiDungs.Where(s => s.RoleID == 1).ToList();
            

            ViewBag.DanhSachSinhVien = sinhViens;
            

            return View();
        }

        public ActionResult QLNhaTuyenDung()
        {
            var nhaTuyenDungs = db.NguoiDungs.Where(ntd => ntd.RoleID == 2).ToList();
            ViewBag.DanhSachNhaTuyenDung = nhaTuyenDungs;
            return View();
        }
    }
}