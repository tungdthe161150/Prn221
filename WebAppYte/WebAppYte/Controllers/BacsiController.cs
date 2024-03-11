using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebAppYte.Models;

namespace WebAppYte.Controllers
{
    public class BacsiController : Controller
    {
        private readonly WebAppYteContext db ;
        public BacsiController(WebAppYteContext context)
        {
            db = context;
        }
        // GET: Bacsi
        public ActionResult Index()
        {
            var quanTris = db.QuanTris.Include(q => q.IdkhoaNavigation).Where(x => x.VaiTro == 2);
            return View(quanTris.ToList());
        }

        //GET: Bacsi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return  BadRequest();
            }
            QuanTri quanTri = db.QuanTris.Find(id);
            if (quanTri == null)
            {
                return NotFound();
            }
            return View(quanTri);
        }
        // GET: Bacsi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return  BadRequest();
            }
            QuanTri quanTri = db.QuanTris.Find(id);
            if (quanTri == null)
            {
                return NotFound();
            }
            ViewBag.IDKhoa = new SelectList(db.Khoas, "IDKhoa", "TenKhoa", quanTri.Idkhoa);
            return View(quanTri);
        }

        // POST: Bacsi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("IDQuanTri,TaiKhoan,MatKhau,VaiTro,ThongTinBacSi,TrinhDo,IDKhoa,HoTen,AnhBia")] QuanTri quanTri)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quanTri).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDKhoa = new SelectList(db.Khoas, "IDKhoa", "TenKhoa", quanTri.Idkhoa);
            return View(quanTri);
        }

        // GET: Admin/QuanTris/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            QuanTri quanTri = db.QuanTris.Find(id);
            if (quanTri == null)
            {
                return NotFound();
            }
            return View(quanTri);
        }

        // POST: Admin/QuanTris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuanTri quanTri = db.QuanTris.Find(id);
            db.QuanTris.Remove(quanTri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


      /*  public ActionResult Quanlyhoidap(int? page)
        {
            if (page == null) page = 1;
            var hoiDaps = db.HoiDaps.Include(h => h.IdnguoiDungNavigation).Include(h => h.IdquanTriNavigation).Where(n => n.TrangThai == 0).OrderByDescending(a => a.NgayGui).ThenBy(a => a.Idhoidap).ToList();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(hoiDaps.ToPagedList(pageNumber, pageSize));
        }
*/
        public ActionResult Traloicauhoi(int? id)
        {
            if (id == null)

            {
                return  BadRequest();
            }
            HoiDap hoiDap = db.HoiDaps.Find(id);
            if (hoiDap == null)
            {
                return NotFound();
            }
            ViewBag.IDNguoiDung = new SelectList(db.NguoiDungs, "IDNguoiDung", "HoTen", hoiDap.IdnguoiDung);
            ViewBag.IDQuanTri = new SelectList(db.QuanTris, "IDQuanTri", "TaiKhoan", hoiDap.IdquanTri);
            return View(hoiDap);
        }

        // POST: Hoidap/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Traloicauhoi([Bind("IDHoidap,CauHoi,TraLoi,IDNguoiDung,IDQuanTri,NgayGui,GhiChu,TrangThai")] HoiDap hoiDap)
        {
            if (ModelState.IsValid)
            {
                hoiDap.TrangThai = 1;
                db.Entry(hoiDap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Quanlyhoidap");
            }
            ViewBag.IDNguoiDung = new SelectList(db.NguoiDungs, "IDNguoiDung", "HoTen", hoiDap.IdnguoiDung);
            ViewBag.IDQuanTri = new SelectList(db.QuanTris, "IDQuanTri", "TaiKhoan", hoiDap.IdquanTri);
            return View(hoiDap);
        }

        public ActionResult Kiemtralichhen(int? page,int id)
        {
            // Check if db.LichKhams is not null
            if (db.LichKhams != null)
            {
                var lich = db.LichKhams
                    .Include(l => l.IdnguoiDungNavigation)
                    .Where(l => l.IdquanTri == id)  // Include the navigation property
                    .OrderByDescending(x => x.BatDau)
                    .ThenBy(y => y.IdlichKham)
                    .ToList();

                int pageSize = 5;
                int pageNumber = (page ?? 1);

                return View(lich);
            }
            else
            {
                // Handle the case where db.LichKhams is null
                // You can return an appropriate view or redirect to an error page
                return View("Error");
            }
        }

        public ActionResult Lichdangcho(int? page)
        {
            var lich = db.LichKhams.OrderByDescending(x => x.BatDau).ThenBy(y => y.IdlichKham).Where(x => x.TrangThai == 0).ToList();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(lich);

            //return View(lich.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Lichdaxacnhan(int? page)
        {
            var lich = db.LichKhams.OrderByDescending(x => x.BatDau).ThenBy(y => y.IdlichKham).Where(x => x.TrangThai == 1).ToList();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(lich);

            // return View(lich.ToPagedList(pageNumber, pageSize));
        }

        // GET: LichKham/Edit/5
        public async Task<IActionResult> Xacnhanlichhen(int? id)
        {
            if (id == null || db.LichKhams == null)
            {
                return NotFound();
            }

            var lichKham = await db.LichKhams.FindAsync(id);
            if (lichKham == null)
            {
                return NotFound();
            }
            ViewData["IdnguoiDung"] = new SelectList(db.NguoiDungs, "IdnguoiDung", "HoTen", lichKham.IdnguoiDung);
            ViewData["IdquanTri"] = new SelectList(db.QuanTris, "IdquanTri", "HoTen", lichKham.IdquanTri);
            return View(lichKham);
        }

        // POST: LichKham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Xacnhanlichhen(int id, [Bind("IdlichKham,ChuDe,MoTa,BatDau,KetThuc,TrangThai,ZoomInfo,KetQuaKham,IdnguoiDung,IdquanTri")] LichKham lichKham)
        {
            if (id != lichKham.IdlichKham)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(lichKham);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LichKhamExists(lichKham.IdlichKham))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Kiemtralichhen));
            }
            ViewData["IdnguoiDung"] = new SelectList(db.NguoiDungs, "IdnguoiDung", "HoTen", lichKham.IdnguoiDung);
            ViewData["IdquanTri"] = new SelectList(db.QuanTris, "IdquanTri", "HoTen", lichKham.IdquanTri);
            return View(lichKham);
        }

        private bool LichKhamExists(int id)
        {
            return db.LichKhams.Any(e => e.IdlichKham == id);
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
