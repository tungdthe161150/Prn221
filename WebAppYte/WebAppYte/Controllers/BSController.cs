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
    public class BSController : Controller
    {
        private readonly WebAppYteContext _context;

        public BSController(WebAppYteContext context)
        {
            _context = context;
        }

        // GET: BS
        public async Task<IActionResult> Index()
        {
            var webAppYteContext = _context.QuanTris.Include(q => q.IdkhoaNavigation);
            return View(await webAppYteContext.ToListAsync());
        }

        // GET: BS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.QuanTris == null)
            {
                return NotFound();
            }

            var quanTri = await _context.QuanTris
                .Include(q => q.IdkhoaNavigation)
                .FirstOrDefaultAsync(m => m.IdquanTri == id);
            if (quanTri == null)
            {
                return NotFound();
            }

            return View(quanTri);
        }

        // GET: BS/Create
        public IActionResult Create()
        {
            ViewData["Idkhoa"] = new SelectList(_context.Khoas, "Idkhoa", "Idkhoa");
            return View();
        }

        // POST: BS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdquanTri,TaiKhoan,MatKhau,VaiTro,ThongTinBacSi,TrinhDo,Idkhoa,HoTen,AnhBia,ThongtinZoom")] QuanTri quanTri)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quanTri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idkhoa"] = new SelectList(_context.Khoas, "Idkhoa", "Idkhoa", quanTri.Idkhoa);
            return View(quanTri);
        }

        // GET: BS/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.QuanTris == null)
            {
                return NotFound();
            }

            var quanTri = await _context.QuanTris.FindAsync(id);
            if (quanTri == null)
            {
                return NotFound();
            }
            ViewData["Idkhoa"] = new SelectList(_context.Khoas, "Idkhoa", "Idkhoa", quanTri.Idkhoa);
            return View(quanTri);
        }

        // POST: BS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdquanTri,TaiKhoan,MatKhau,VaiTro,ThongTinBacSi,TrinhDo,Idkhoa,HoTen,AnhBia,ThongtinZoom")] QuanTri quanTri)
        {
            if (id != quanTri.IdquanTri)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quanTri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuanTriExists(quanTri.IdquanTri))
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
            ViewData["Idkhoa"] = new SelectList(_context.Khoas, "Idkhoa", "Idkhoa", quanTri.Idkhoa);
            return View(quanTri);
        }

        // GET: BS/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.QuanTris == null)
            {
                return NotFound();
            }

            var quanTri = await _context.QuanTris
                .Include(q => q.IdkhoaNavigation)
                .FirstOrDefaultAsync(m => m.IdquanTri == id);
            if (quanTri == null)
            {
                return NotFound();
            }

            return View(quanTri);
        }

        // POST: BS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.QuanTris == null)
            {
                return Problem("Entity set 'WebAppYteContext.QuanTris'  is null.");
            }
            var quanTri = await _context.QuanTris.FindAsync(id);
            if (quanTri != null)
            {
                _context.QuanTris.Remove(quanTri);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuanTriExists(int id)
        {
          return _context.QuanTris.Any(e => e.IdquanTri == id);
        }
    }
}
