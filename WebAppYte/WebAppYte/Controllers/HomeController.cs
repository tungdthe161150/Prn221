using Microsoft.AspNetCore.Mvc;
using WebAppYte.Models;
using System.Diagnostics;
using WebAppYte.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Project_Prn221.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        WebAppYteContext db = new WebAppYteContext();



        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Dangnhap(IFormCollection Dangnhap)
        {
            string tk = Dangnhap["TaiKhoan"].ToString();
            string mk = Dangnhap["MatKhau"].ToString();

            using (WebAppYteContext db = new WebAppYteContext())
            {
                var islogin = db.NguoiDungs.SingleOrDefault(x => x.TaiKhoan.Equals(tk) && x.MatKhau.Equals(mk));

                if (islogin != null)
                {
                    if (islogin.IdnguoiDung != null)
                    {
                        // Store user information in session
                        HttpContext.Session.SetString("user", JsonConvert.SerializeObject(islogin));
                        return RedirectToAction("Details", "Nguoidung", new { id = islogin.IdnguoiDung });
                    }
                    else
                    {
                        // Handle the case where IdnguoiDung is null
                        ViewBag.Fail = "User ID is null.";
                        return View("Dangnhap");
                    }
                }

                var isloginAdmin = db.QuanTris.SingleOrDefault(x => x.TaiKhoan.Equals(tk) && x.MatKhau.Equals(mk));

                if (isloginAdmin != null)
                {
                    if (isloginAdmin.VaiTro == 1)
                    {
                        // Store admin information in session
                        HttpContext.Session.SetString("userAdmin", JsonConvert.SerializeObject(isloginAdmin));
                        return RedirectToAction("Index", "HomeAdmin");
                    }
                    else if (isloginAdmin.VaiTro == 2)
                    {
                        // Store user information in session
                        HttpContext.Session.SetString("userBS", JsonConvert.SerializeObject(isloginAdmin));
                        return RedirectToAction("Details", "Bacsi", new { id = isloginAdmin.IdquanTri });
                    }
                }

                ViewBag.Fail = "Tài khoản hoặc mật khẩu không chính xác.";
                return View("Dangnhap");
            }
        }

        [HttpGet]
        public ActionResult Dangky()
        {
            ViewBag.IdgioiTinh = new SelectList(db.GioiTinhs, "IdgioiTinh", "GioiTinh1");
            ViewBag.Idtinh = new SelectList(db.TinhThanhs, "Idtinh", "TenTinh");
            return View();
        }

        // POST: Admin/NguoiDungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dangky([Bind("IDNguoiDung,HoTen,Email,DienThoai,TaiKhoan,MatKhau,IDGioiTinh,DiaChiCuThe,SoCMND,IDTinh,NhomMau,ThongTinKhac")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                db.NguoiDungs.Add(nguoiDung);
                db.SaveChanges();
                ViewData["nguoidung"] = nguoiDung;
                return RedirectToAction("Dangnhap");
            }

            ViewBag.IDGioiTinh = new SelectList(db.GioiTinhs, "IDGioiTinh", "GioiTinh1", nguoiDung.IdgioiTinh);
            ViewBag.IDTinh = new SelectList(db.TinhThanhs, "IDTinh", "TenTinh", nguoiDung.Idtinh);
            return View(nguoiDung);
        }
        public ActionResult DangXuat()
        {
            HttpContext.Session.SetString("user", "");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DangXuatBs()
        {
            HttpContext.Session.SetString("userBS", "");

            return RedirectToAction("Index", "Home");
        }
    }

}
