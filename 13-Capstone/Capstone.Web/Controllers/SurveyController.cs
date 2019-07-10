using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Web.Controllers
{
    public class SurveyController : Controller
    {
        private ISurveyDAO surveyDAO;

        public SurveyController(SurveyDAO surveyDAO)
        {
            this.surveyDAO = surveyDAO;
        }

        [HttpGet]
        public IActionResult Survey()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Survey(Survey survey)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Survey");
            }
            // todo add results to survey table via SurveyDAO
            return RedirectToAction("Favorites");
        }

        public IActionResult Favorites()
        {
            List<Park> parks = new List<Park>();
            // todo add parks to list based on survey table info via SurveyDAO
            return View(parks);
        }
    }
}