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
            int surveyAdded = 0;

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Survey");
            }

            surveyAdded = surveyDAO.AddSurvey(survey);

            if (surveyAdded == 1)
            {
                return RedirectToAction("Favorites");
            }

            return RedirectToAction("Survey");
        }

        public IActionResult Favorites()
        {
            List<Park> parks = surveyDAO.GetFavorites();
            
            return View(parks);
        }
    }
}