using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppYte.Models;

namespace WebAppYte.Controllers
{
    public class LichkhamController : Controller
    {
        private WebAppYteContext db = new WebAppYteContext();

        // GET: Lichkham
        public ActionResult Index(int? id, int? page)
        {
            var lichKhams = db.LichKhams.Include(l => l.IdnguoiDungNavigation).Include(l => l.IdquanTriNavigation).
                Where(h => h.IdnguoiDung == id).OrderByDescending(x => x.BatDau).ThenBy(x => x.IdlichKham);
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            ViewBag.id = id;
            return View(lichKhams);
        }

        public ActionResult Dangxuly(int? id, int? page)
        {
            var lichKhams = db.LichKhams.Include(l => l.IdnguoiDungNavigation).Include(l => l.IdquanTriNavigation).
                Where(h => h.IdnguoiDung == id && h.TrangThai == 0).OrderByDescending(x => x.BatDau).ThenBy(x => x.IdlichKham);
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            ViewBag.id = id;
            return View(lichKhams);
        }
        public ActionResult Daxacnhan(int? id, int? page)
        {
            var lichKhams = db.LichKhams.Include(l => l.IdnguoiDungNavigation).Include(l => l.IdquanTriNavigation).
                Where(h => h.IdnguoiDung == id && h.TrangThai == 1).OrderByDescending(x => x.BatDau).ThenBy(x => x.IdlichKham);
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            ViewBag.id = id;
            return View(lichKhams);
        }
        public ActionResult Datuvanxong(int? id, int? page)
        {
            var lichKhams = db.LichKhams.Include(l => l.IdnguoiDungNavigation).Include(l => l.IdquanTriNavigation).
                Where(h => h.IdnguoiDung == id && h.TrangThai == 2).OrderByDescending(x => x.BatDau).ThenBy(x => x.IdlichKham);
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            ViewBag.id = id;
            return View(lichKhams);
        }

        // GET: Lichkham/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            LichKham lichKham = db.LichKhams.Find(id);
            if (lichKham == null)
            {
                return NotFound();
            }
            return View(lichKham);
        }

        // GET: Lichkham/Create
        public ActionResult Create()
        {
            ViewBag.IdnguoiDung = new SelectList(db.NguoiDungs, "IdnguoiDung", "HoTen");
            ViewBag.IdquanTri = new SelectList(db.QuanTris.Where(n => n.VaiTro == 2), "IdquanTri", "HoTen");
            return View();
        }

        // POST: Lichkham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("IdlichKham,ChuDe,MoTa,BatDau,KetThuc,TrangThai,ZoomInfo,KetQuaKham,IdnguoiDung,IdquanTri")] LichKham lichKham)
        {
            if (ModelState.IsValid)
            {
                lichKham.TrangThai = 0;
                db.LichKhams.Add(lichKham);
                db.SaveChanges();
                return RedirectToAction("Index", "LichKham", new { id = lichKham.IdnguoiDung });
            }

            ViewBag.IdnguoiDung = new SelectList(db.NguoiDungs, "IdnguoiDung", "HoTen", lichKham.IdnguoiDung);
            ViewBag.IDQuanTri = new SelectList(db.QuanTris, "IdquanTri", "HoTen", lichKham.IdquanTri);
            return View(lichKham);
        }

        // GET: Lichkham/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            LichKham lichKham = db.LichKhams.Find(id);
            if (lichKham == null)
            {
                return NotFound();
            }
            ViewBag.IdnguoiDung = new SelectList(db.NguoiDungs, "IdnguoiDung", "HoTen", lichKham.IdnguoiDung);
            ViewBag.IdquanTri = new SelectList(db.QuanTris, "IdquanTri", "TaiKhoan", lichKham.IdquanTri);
            return View(lichKham);
        }

        // POST: Lichkham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("IdlichKham,ChuDe,MoTa,BatDau,KetThuc,TrangThai,ZoomInfo,KetQuaKham,IdnguoiDung,IdquanTri")] LichKham lichKham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lichKham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdnguoiDung = new SelectList(db.NguoiDungs, "IdnguoiDung", "HoTen", lichKham.IdnguoiDung);
            ViewBag.IdquanTri = new SelectList(db.QuanTris, "IdquanTri", "TaiKhoan", lichKham.IdquanTri);
            return View(lichKham);
        }

        // GET: Lichkham/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            LichKham lichKham = db.LichKhams.Find(id);
            if (lichKham == null)
            {
                return NotFound();
            }
            return View(lichKham);
        }

        // POST: Lichkham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LichKham lichKham = db.LichKhams.Find(id);
            db.LichKhams.Remove(lichKham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       /* public JsonResult Lichdangluoi()
        {
            //db.Configuration.ProxyCreationEnabled = false;
            List<LichKham> l = db.LichKhams.ToList();
            // events = db.LichKhams.ToList();
            var events = l.Select(ll => new
            {
                id = ll.IdlichKham,
                title = ll.ChuDe,
                start = ll.BatDau,
                end = ll.KetThuc,
            });
            Console.WriteLine(events);
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
*/
        public ActionResult lichhen()
        {
            return View();

        }
        private static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
