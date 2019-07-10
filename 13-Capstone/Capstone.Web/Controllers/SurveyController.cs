using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public SurveyController(ISurveyDAO surveyDAO)
        {
            this.surveyDAO = surveyDAO;
        }

        [HttpGet]
        public IActionResult Survey()
        {
            // todo get list of park names for dropdown menu
            // todo get list of states for dropdown menu

            return View();
        }

        [HttpPost]
        public IActionResult Survey(Survey survey)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Survey");
            }

            int surveyAdded = surveyDAO.AddSurvey(survey);

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