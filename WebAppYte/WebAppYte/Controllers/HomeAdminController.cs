﻿using Microsoft.AspNetCore.Mvc;

namespace WebAppYte.Controllers
{
    public class HomeAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
