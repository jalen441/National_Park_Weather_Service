﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private IParkDAO dao = new IParkDAO(Startup.ConnectionString);

        public IActionResult Index()
        {
            IList<Park> parks = dao.GetParks();
            return View(parks);
        }

        public IActionResult Detail(string parkCode)
        {
            Park park = dao.GetParkByCode(parkCode);
            return View(park);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
