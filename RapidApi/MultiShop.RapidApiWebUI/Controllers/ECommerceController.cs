﻿using Microsoft.AspNetCore.Mvc;

namespace MultiShop.RapidApiWebUI.Controllers
{
    public class ECommerceController : Controller
    {
        public async Task< IActionResult> ECommerceList()
        {
            return View();
        }
    }
}
