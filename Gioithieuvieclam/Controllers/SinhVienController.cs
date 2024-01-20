using Gioithieuvieclam.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;


namespace Gioithieuvieclam.Controllers
{
    public class SinhVienController : Controller
    {
        private jobEntities3 db = new jobEntities3();
        // GET: SinhVien
        public ActionResult Index()
        {
            int userId = (int)Session["UserID"];
            var sinhVien = db.SinhViens.FirstOrDefault(s => s.UserID == userId);

            if (sinhVien == null)
            {
                return HttpNotFound(); // Hoặc chuyển hướng đến trang 404
            }

            return View(sinhVien);
        }

        public ActionResult UpdateProfile(int? UserID)
        {
            UserID = (int)Session["UserID"];
            var sinhVien = db.SinhViens.FirstOrDefault(s => s.UserID == UserID);

            if (sinhVien == null)
            {
                return HttpNotFound(); // Hoặc chuyển hướng đến trang 404
            }

            return View(sinhVien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile(SinhVien sinhVien)
        {
            SinhVien updateSinhVien = db.SinhViens.FirstOrDefault(i => i.UserID == sinhVien.UserID);
            updateSinhVien.HoTen = sinhVien.HoTen;
            updateSinhVien.SoDienThoai = sinhVien.SoDienThoai;
            updateSinhVien.SoCCCD = sinhVien.SoCCCD;
            updateSinhVien.DiaChi = sinhVien.DiaChi;
            updateSinhVien.TrinhDo = sinhVien.TrinhDo;
            updateSinhVien.ViTri = sinhVien.ViTri;
            updateSinhVien.MucLuongMongMuon = sinhVien.MucLuongMongMuon;
            db.SaveChanges();
            db.Dispose();
            return Redirect("Index");
        }
        



    }

}
