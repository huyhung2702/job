using Gioithieuvieclam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gioithieuvieclam.Controllers
{
    public class NhaTuyenDungController : Controller
    {
        private jobEntities3 job = new jobEntities3();
        // GET: NhaTuyenDung
        public ActionResult Index()
        {
            int userID = (int)Session["UserID"];
            NhaTuyenDung nhaTuyenDung = job.NhaTuyenDungs.FirstOrDefault(x => x.UserID == userID);
            /*IEnumerable<CongViec> danhSachCongViec = job.CongViecs.Where(x => x.NhaTuyenDungID == nhaTuyenDung.ID).ToList();*/
            /*CongViec congViec = job.CongViecs.Where()*/
            return View(job.HoSoUngTuyens.ToList());

        }
    }
}