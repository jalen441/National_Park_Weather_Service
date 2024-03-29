﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using System.Dynamic;
using Microsoft.AspNetCore.Http;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private IParkDAO parkDAO = new ParkDAO(Startup.ConnectionString);
        private IForecastDAO forecastDAO = new ForecastDAO(Startup.ConnectionString);

        public IActionResult Index()
        {
            IList<Park> parks = parkDAO.GetParks();
            return View(parks);
        }

        public IActionResult Detail(string parkCode)
        {
            dynamic mymodel = new ExpandoObject();

            if(HttpContext.Session.GetString("TemperatureUnit") == null)
            {
                HttpContext.Session.SetString("TemperatureUnit", "F");
            }
            
            ViewBag.TemperatureUnit = HttpContext.Session.GetString("TemperatureUnit");

            mymodel.Forecast = forecastDAO.GetWeatherByParkCode(parkCode);
            mymodel.Park = parkDAO.GetParkByParkCode(parkCode);

            return View(mymodel);
        }

        public IActionResult ChangeTemperatureUnits(string unit, string parkCode)
        {
            HttpContext.Session.SetString("TemperatureUnit", unit);

            return RedirectToAction("Detail", "Home", new { parkCode }, "ForecastHeader");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
