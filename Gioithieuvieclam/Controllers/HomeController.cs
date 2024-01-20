using Gioithieuvieclam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gioithieuvieclam.Controllers
{
    public class HomeController : Controller
    {
        private jobEntities3 job = new jobEntities3();
        
        public ActionResult Index()
        {
            return View(job.CongViecs.ToList());
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, String password)
        {
            ViewBag.error = "";
            if(ModelState.IsValid)
            {
                var data = job.NguoiDungs.Where(s => s.Email.Equals(email) && s.Password.Equals(password)).ToList();
                if (data.Count() > 0)
                {
                    Session["UserID"] = data.FirstOrDefault().UserID;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["RoleID"] = data.FirstOrDefault().RoleID;
                }
                else
                {
                    ViewBag.error = "Sai email hoặc password";
                    return View();
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
        [HttpGet]

        public ActionResult Register()
        {
            ViewBag.RoleID = new SelectList(job.PhanQuyens.Where(data => data.Name != "Admin"), "RoleID", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult Register(NguoiDung user)
        {
           
            var newUser = job.NguoiDungs.Where(data => data.Email.Equals(user.Email)).ToList();
            if(ModelState.IsValid)
            {
                if (newUser.Count() > 0)
                {
                    ViewBag.error = "Email đã tồn tại!";
                }
                else
                {
                    if(user.RoleID == 2)
                    {
                        var nhaTuyenDung = new NhaTuyenDung();
                        nhaTuyenDung.UserID = user.UserID;
                        job.NhaTuyenDungs.Add(nhaTuyenDung);
                    }
                    if(user.RoleID == 1) {
                        var sinhVien = new SinhVien();
                        sinhVien.UserID = user.UserID;
                        job.SinhViens.Add(sinhVien);
                    }
                    job.NguoiDungs.Add(user);
                    job.SaveChanges();
                }
            }
           
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Apply(int jobId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Apply(int jobId, HoSoUngTuyen hoSo)
        {
            // Logic xử lý nộp hồ sơ ứng tuyển từ sinh viên
            var currentUserId = (int)Session["UserID"];
            var sinhVien = job.SinhViens.FirstOrDefault(x => x.UserID == currentUserId);
            var hoSoUngTuyen = new HoSoUngTuyen
            {
                SinhVienID = sinhVien.ID,
                HocVan = hoSo.HocVan,
                KinhNghiemLamViec = hoSo.KinhNghiemLamViec,
                // Thêm các trường thông tin khác từ đơn ứng tuyển
            };

            job.HoSoUngTuyens.Add(hoSoUngTuyen);

            var ungVien = new UngVien
            {
                CongViecID = jobId,
                SinhVienID = sinhVien.ID,
                HoSoID = hoSo.ID,
                // Thêm các trường thông tin khác từ đơn ứng tuyển
            };

            job.UngViens.Add(ungVien);

            job.SaveChanges();

            // Logic chuyển hướng hoặc thông báo nếu cần

            return RedirectToAction("Index", "Home");
        }

        


    }


}
