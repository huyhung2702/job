using Gioithieuvieclam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Gioithieuvieclam.Controllers
{
    
    public class JobController : Controller
{
        private jobEntities3 db = new jobEntities3();
        private jobEntities3 job = new jobEntities3();
        // Danh sách các bài đăng việc làm (giả định sử dụng List, có thể thay thế bằng cơ sở dữ liệu)
        private static List<CongViec> jobPosts = new List<CongViec>();

    public ActionResult Index()
    {
            int userID = (int)Session["UserID"];
            NhaTuyenDung nhaTuyenDung = db.NhaTuyenDungs.FirstOrDefault(x => x.UserID == userID);
            IEnumerable<CongViec> danhSachCongViec = db.CongViecs.Where(x => x.NhaTuyenDungID == nhaTuyenDung.ID).ToList();

            return View(danhSachCongViec);
    }

    [HttpGet]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(CongViec jobPost)
    {
            if (Session["UserID"] != null )
            {
                int userID = (int)Session["UserID"];
                NhaTuyenDung nhaTuyenDung = db.NhaTuyenDungs.FirstOrDefault(x => x.UserID == userID);
                if (nhaTuyenDung != null)
                {
                    jobPost.NhaTuyenDungID = nhaTuyenDung.ID;
                    // Bạn có thể sử dụng nhaTuyenDung ở đây nếu tìm thấy
                }
                

            }
            // Thêm bài đăng mới vào danh sách (hoặc lưu vào cơ sở dữ liệu)
            jobPosts.Add(jobPost);
            if (ModelState.IsValid)
            {
                db.CongViecs.Add(jobPost);
                db.SaveChanges();
            }
        return RedirectToAction("Index");
    }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongViec congViec = db.CongViecs.Find(id);
            if (congViec == null)
            {
                return HttpNotFound();
            }
            return View(congViec);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MoTaCongViec,YeuCau,ViTri,MucLuong,ThongTinLienHe")] CongViec congViec)
        {
            if (ModelState.IsValid)
            {
                db.Entry(congViec).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index"); // Chuyển hướng về trang danh sách sau khi sửa
            }
            return View(congViec);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongViec congViec = db.CongViecs.Find(id);
            if (congViec == null)
            {
                return HttpNotFound();
            }
            return View(congViec);
        }

        // POST: CongViec/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CongViec congViec = db.CongViecs.Find(id);
            db.CongViecs.Remove(congViec);
            db.SaveChanges();
            return RedirectToAction("Index"); // Chuyển hướng về trang danh sách sau khi xóa
        }


        public ActionResult Search()
        {

            return View(job.CongViecs.ToList());
        }

        public ActionResult Detail(int id)
        {
            if(id == 0) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                CongViec congViec = db.CongViecs.FirstOrDefault(x => x.ID == id);
                if (congViec == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    IEnumerable<UngVien> ungViens = db.UngViens.Where(x => x.CongViecID == congViec.ID).ToList();
                    return View(ungViens);
                }
                

            }
            
        }
        public ActionResult ChiTietHoSo(int id)
        {
            UngVien ungVien = db.UngViens.FirstOrDefault(x => x.ID == id);
            // Lấy thông tin ứng viên từ cơ sở dữ liệu dựa trên ID
            var hoSo = job.HoSoUngTuyens.FirstOrDefault(s => s.ID == ungVien.HoSoID);

            // Kiểm tra xem ứng viên có tồn tại hay không
            if (hoSo == null)
            {
                // Trả về trang thông báo nếu không tìm thấy
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Trả về trang xem chi tiết với thông tin ứng viên
            return View(hoSo);
        }

        /*[HttpPost]
        public ActionResult DuyetHoSo(int hoSoId, bool ketQua)
        {
            try
            {
                // Lấy hoSo từ cơ sở dữ liệu và cập nhật trường KetQua
                var hoSo = db.HoSoUngTuyens.FirstOrDefault(h => h.ID == hoSoId);
                if (hoSo != null)
                {
                    hoSo.KetQua = ketQua;
                    db.SaveChanges();
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Không tìm thấy hồ sơ ứng viên." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        [HttpPost]
        public ActionResult TuChoiHoSo(int hoSoId)
        {
            try
            {
                // Lấy hoSo từ cơ sở dữ liệu và cập nhật trường KetQua là false (từ chối)
                var hoSo = db.HoSoUngTuyens.FirstOrDefault(h => h.ID == hoSoId);
                if (hoSo != null)
                {
                    hoSo.KetQua = false;
                    db.SaveChanges();
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Không tìm thấy hồ sơ ứng viên." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }*/

    }
}