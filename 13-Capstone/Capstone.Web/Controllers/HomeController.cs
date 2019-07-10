using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using System.Dynamic;

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
            IList<Forecast> forecast = forecastDAO.GetWeatherByParkCode(parkCode);
            Park park = parkDAO.GetParkByParkCode(parkCode);
            mymodel.Forecast = forecast;
            mymodel.Park = park;

            return View(mymodel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
