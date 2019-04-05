using AspNetCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Controllers
{
    public class CountriesController : Controller
    {
        // GET
        public IActionResult Index()
        {
            var model = new CountriesModel("Countries - Mustache example");
            model.Codes.Add("FR");
            model.Codes.Add("IT");
            model.Codes.Add("US");
            model.Codes.Add("CA");
            model.Codes.Add("UK");
            return View(model);
        }

        public IActionResult All()
        {
            return Json(new {countryCodes = new string[] {"FR", "UK", "IT", "US", "CA"}});
        }
        public IActionResult Details(string id)
        {
            if (id == "UK")
            {
                return Json(new {Name = "Ukraine", TelPref = "+380", Capital = new {Name = "Kyiv"}});
            }
            else if (id == "IT")
            {
                return Json(new {Name = "Itally", TelPref = "+39", Capital = new {Name = "Rome"}});
            }
            else if (id == "US")
            {
                return Json(new {Name = "United States", TelPref = "+1", Capital = new {Name = "Washington"}});
            }
            else if (id == "CA")
            {
                return Json(new {Name = "Canada", TelPref = "+1", Capital = new {Name = "Ottawa"}});
            }
            else
            {
                return Json(new {Name = "France", TelPref = "+33", Capital = new {Name = "Paris"}}); 
            }
            
        }

        public IActionResult IndexKo()
        {
            var model = new CountriesModel("Countries - Knockout example");
            model.Codes.Add("FR");
            model.Codes.Add("IT");
            model.Codes.Add("US");
            model.Codes.Add("CA");
            model.Codes.Add("UK");
            return View(model);
        }

        public IActionResult IndexVue()
        {
            var model = new CountriesModel("Countries - Vue example");
                        model.Codes.Add("FR");
                        model.Codes.Add("IT");
                        model.Codes.Add("US");
                        model.Codes.Add("CA");
                        model.Codes.Add("UK");
                        return View(model);
        }
    }
}