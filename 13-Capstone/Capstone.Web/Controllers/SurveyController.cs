using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Capstone.Web.Controllers
{
    public class SurveyController : HomeController
    {
        private ISurveyDAO surveyDAO;
        private IParkDAO parkDAO;

        public SurveyController(ISurveyDAO surveyDAO, IParkDAO parkDAO)
        {
            this.surveyDAO = surveyDAO;
            this.parkDAO = parkDAO;
        }

        [HttpGet]
        public IActionResult Survey()
        {
            ViewBag.Parks = parkDAO.GetParks();
            ViewBag.States = states;

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

        public List<string> states = new List<string>
        {
                {"AK - Alaska" },
                {"AL - Alabama" },
                {"AR - Arkansas" },
                {"AZ - Arizona" },
                {"CA - California" },
                {"CO - Colorado" },
                {"CT - Connecticut" },
                {"DC - District of Columbia" },
                {"DE - Delaware" },
                {"FL - Florida" },
                {"GA - Georgia" },
                {"HI - Hawaii" },
                {"IA - Iowa" },
                {"ID - Idaho" },
                {"IL - Illinois" },
                {"IN - Indiana" },
                {"KS - Kansas" },
                {"KY - Kentucky" },
                {"LA - Louisiana" },
                {"MA - Massachusetts" },
                {"MD - Maryland" },
                {"ME - Maine" },
                {"MI - Michigan" },
                {"MN - Minnesota" },
                {"MO - Missouri" },
                {"MS - Mississippi" },
                {"MT - Montana" },
                {"NC - North Carolina" },
                {"ND - North Dakota" },
                {"NE - Nebraska" },
                {"NH - New Hampshire" },
                {"NJ - New Jersey" },
                {"NM - New Mexico" },
                {"NV - Nevada" },
                {"NY - New York" },
                {"OH - Ohio" },
                {"OK - Oklahoma" },
                {"OR - Oregon" },
                {"PA - Pennsylvania" },
                {"PR - Puerto Rico" },
                {"RI - Rhode Island" },
                {"SC - South Carolina" },
                {"SD - South Dakota" },
                {"TN - Tennessee" },
                {"TX - Texas" },
                {"UT - Utah" },
                {"VA - Virginia" },
                {"VT - Vermont" },
                {"WA - Washington" },
                {"WI - Wisconsin" },
                {"WV - West Virginia" },
                {"WY - Wyoming" }
        };
    }
}