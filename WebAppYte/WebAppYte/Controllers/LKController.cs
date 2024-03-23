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
    public class LKController : Controller
    {
        private readonly WebAppYteContext _context;

        public LKController(WebAppYteContext context)
        {
            _context = context;
        }

        // GET: LK
        public async Task<IActionResult> Index()
        {
            var webAppYteContext = _context.LichKhams.Include(l => l.IdnguoiDungNavigation).Include(l => l.IdquanTriNavigation);
            return View(await webAppYteContext.ToListAsync());
        }

        // GET: LK/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LichKhams == null)
            {
                return NotFound();
            }

            var lichKham = await _context.LichKhams
                .Include(l => l.IdnguoiDungNavigation)
                .Include(l => l.IdquanTriNavigation)
                .FirstOrDefaultAsync(m => m.IdlichKham == id);
            if (lichKham == null)
            {
                return NotFound();
            }

            return View(lichKham);
        }

        // GET: LK/Create
        public IActionResult Create()
        {
            ViewData["IdnguoiDung"] = new SelectList(_context.NguoiDungs, "IdnguoiDung", "IdnguoiDung");
            ViewData["IdquanTri"] = new SelectList(_context.QuanTris, "IdquanTri", "IdquanTri");
            return View();
        }

        // POST: LK/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdlichKham,ChuDe,MoTa,BatDau,KetThuc,TrangThai,ZoomInfo,KetQuaKham,IdnguoiDung,IdquanTri")] LichKham lichKham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lichKham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdnguoiDung"] = new SelectList(_context.NguoiDungs, "IdnguoiDung", "IdnguoiDung", lichKham.IdnguoiDung);
            ViewData["IdquanTri"] = new SelectList(_context.QuanTris, "IdquanTri", "IdquanTri", lichKham.IdquanTri);
            return View(lichKham);
        }

        // GET: LK/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LichKhams == null)
            {
                return NotFound();
            }

            var lichKham = await _context.LichKhams.FindAsync(id);
            if (lichKham == null)
            {
                return NotFound();
            }
            ViewData["IdnguoiDung"] = new SelectList(_context.NguoiDungs, "IdnguoiDung", "IdnguoiDung", lichKham.IdnguoiDung);
            ViewData["IdquanTri"] = new SelectList(_context.QuanTris, "IdquanTri", "IdquanTri", lichKham.IdquanTri);
            return View(lichKham);
        }

        // POST: LK/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdlichKham,ChuDe,MoTa,BatDau,KetThuc,TrangThai,ZoomInfo,KetQuaKham,IdnguoiDung,IdquanTri")] LichKham lichKham)
        {
            if (id != lichKham.IdlichKham)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lichKham);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdnguoiDung"] = new SelectList(_context.NguoiDungs, "IdnguoiDung", "IdnguoiDung", lichKham.IdnguoiDung);
            ViewData["IdquanTri"] = new SelectList(_context.QuanTris, "IdquanTri", "IdquanTri", lichKham.IdquanTri);
            return View(lichKham);
        }

        // GET: LK/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LichKhams == null)
            {
                return NotFound();
            }

            var lichKham = await _context.LichKhams
                .Include(l => l.IdnguoiDungNavigation)
                .Include(l => l.IdquanTriNavigation)
                .FirstOrDefaultAsync(m => m.IdlichKham == id);
            if (lichKham == null)
            {
                return NotFound();
            }

            return View(lichKham);
        }

        // POST: LK/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LichKhams == null)
            {
                return Problem("Entity set 'WebAppYteContext.LichKhams'  is null.");
            }
            var lichKham = await _context.LichKhams.FindAsync(id);
            if (lichKham != null)
            {
                _context.LichKhams.Remove(lichKham);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LichKhamExists(int id)
        {
          return _context.LichKhams.Any(e => e.IdlichKham == id);
        }
    }
}
