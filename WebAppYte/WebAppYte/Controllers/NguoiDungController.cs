using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppYte.Models;

namespace WebAppYte.Controllers
{
    public class NguoiDungController : Controller
    {
        private readonly WebAppYteContext _context;

        public NguoiDungController(WebAppYteContext context)
        {
            _context = context;
        }

        // GET: NguoiDung
        public async Task<IActionResult> Index()
        {
            var webAppYteContext = _context.NguoiDungs.Include(n => n.IdgioiTinhNavigation).Include(n => n.IdtinhNavigation);
            return View(await webAppYteContext.ToListAsync());
        }

        // GET: NguoiDung/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NguoiDungs == null)
            {
                return NotFound();
            }

            var nguoiDung = await _context.NguoiDungs
                .Include(n => n.IdgioiTinhNavigation)
                .Include(n => n.IdtinhNavigation)
                .FirstOrDefaultAsync(m => m.IdnguoiDung == id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            return View(nguoiDung);
        }

        // GET: NguoiDung/Create
        public IActionResult Create()
        {
            ViewData["IdgioiTinh"] = new SelectList(_context.GioiTinhs, "IdgioiTinh", "IdgioiTinh");
            ViewData["Idtinh"] = new SelectList(_context.TinhThanhs, "Idtinh", "Idtinh");
            return View();
        }

        // POST: NguoiDung/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdnguoiDung,HoTen,Email,DienThoai,TaiKhoan,MatKhau,IdgioiTinh,DiaChiCuThe,SoCmnd,Idtinh,NhomMau,ThongTinKhac")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nguoiDung);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdgioiTinh"] = new SelectList(_context.GioiTinhs, "IdgioiTinh", "IdgioiTinh", nguoiDung.IdgioiTinh);
            ViewData["Idtinh"] = new SelectList(_context.TinhThanhs, "Idtinh", "Idtinh", nguoiDung.Idtinh);
            return View(nguoiDung);
        }

        // GET: NguoiDung/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NguoiDungs == null)
            {
                return NotFound();
            }

            var nguoiDung = await _context.NguoiDungs.FindAsync(id);
            if (nguoiDung == null)
            {
                return NotFound();
            }
            ViewData["IdgioiTinh"] = new SelectList(_context.GioiTinhs, "IdgioiTinh", "IdgioiTinh", nguoiDung.IdgioiTinh);
            ViewData["Idtinh"] = new SelectList(_context.TinhThanhs, "Idtinh", "Idtinh", nguoiDung.Idtinh);
            return View(nguoiDung);
        }

        // POST: NguoiDung/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: NguoiDung/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdnguoiDung,HoTen,Email,DienThoai,TaiKhoan,MatKhau,IdgioiTinh,DiaChiCuThe,SoCmnd,Idtinh,NhomMau,ThongTinKhac")] NguoiDung nguoiDung)
        {
            if (id != nguoiDung.IdnguoiDung)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nguoiDung);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NguoiDungExists(nguoiDung.IdnguoiDung))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                // Redirect to Details action with the edited NguoiDung's ID
                return RedirectToAction(nameof(Details), new { id = nguoiDung.IdnguoiDung });
            }
            ViewData["IdgioiTinh"] = new SelectList(_context.GioiTinhs, "IdgioiTinh", "IdgioiTinh", nguoiDung.IdgioiTinh);
            ViewData["Idtinh"] = new SelectList(_context.TinhThanhs, "Idtinh", "Idtinh", nguoiDung.Idtinh);
            return View(nguoiDung);
        }


        // GET: NguoiDung/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NguoiDungs == null)
            {
                return NotFound();
            }

            var nguoiDung = await _context.NguoiDungs
                .Include(n => n.IdgioiTinhNavigation)
                .Include(n => n.IdtinhNavigation)
                .FirstOrDefaultAsync(m => m.IdnguoiDung == id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            return View(nguoiDung);
        }

        // POST: NguoiDung/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NguoiDungs == null)
            {
                return Problem("Entity set 'WebAppYteContext.NguoiDungs'  is null.");
            }
            var nguoiDung = await _context.NguoiDungs.FindAsync(id);
            if (nguoiDung != null)
            {
                _context.NguoiDungs.Remove(nguoiDung);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NguoiDungExists(int id)
        {
          return _context.NguoiDungs.Any(e => e.IdnguoiDung == id);
        }
    }
}
