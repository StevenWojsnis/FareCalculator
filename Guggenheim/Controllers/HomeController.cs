using GuggenheimTest.DataModels;
using GuggenheimTest.Models;
using GuggenheimTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuggenheimTest.Controllers
{
    //[RoutePrefix("Home")]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        //[Route("CalculateFare")]
        public JsonResult CalculateFare(FareCalculatorViewModel model)
        {
            var calculator = new FareCalculatorService();
            //Call service method with Model

            var dataModel = new FareCalculatorDataModel
            {
                Date = model.Date,
                Time = model.Time,
                DistanceUnitsBelowSixMph = model.DistanceUnitsBelowSixMph,
                MinutesAboveSixMph = model.MinutesAboveSixMph,
            };

            return Json( new { totalFare = calculator.CalculateFare(dataModel) }, JsonRequestBehavior.AllowGet);
        }
    }
}