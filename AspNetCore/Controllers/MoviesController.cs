using System;
using System.Linq;
using AspNetCore.BL.Contracts;
using AspNetCore.Configuration;
using AspNetCore.Filters;
using AspNetCore.Models;
using AspNetCore.Models.Input;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AspNetCore.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly AppSettings _settings;
        private readonly ILogger _logger;
        private readonly ILogger _logger1;

        public MoviesController(IMovieRepository movieRepository, IOptions<AppSettings> settings,
            ILoggerFactory loggerFactory, ILogger<MoviesController> logger)
        {
            _movieRepository = movieRepository;
            _settings = settings.Value;
            _logger = loggerFactory.CreateLogger("Movie Controller");
            _logger1 = logger;
        }

        public IActionResult Index(string success)
        {
            _logger.LogCritical("Inside index method");
            var model = new MoviesModel("All Movies") {Movies = _movieRepository.GetMovies()};
            if (!string.IsNullOrWhiteSpace(success))
            {
                model.SuccessMessage = success;
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Manage()
        {
            var model = new MoviesModel("Manage Movies") {Movies = _movieRepository.GetMovies()};
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var model = new MoviesModel("Manage Movies") {Movies = _movieRepository.GetMovies()};
            model.Movies = model.Movies.Where(x => x.Id != id).ToList();
            return PartialView("_ListOfMovies", model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = new EditMovieModel(id==0?"Add Monie":"Edit Movie");
            if (id != 0)
            {
                model.Id = id;
                model.Name = "Taxi 5";
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult EditResult(EditMovieModel model)
        {
            
            return View("Edit",model);
        }
        [HttpPost]
        public IActionResult Edit(MovieInputModel movie)
        {
            var model = new EditMovieModel("Edit Movie");
            if (movie.Action == FormOptions.Delete)
            {
                model.ErrorMessage = "Can't delete such a great movie";
                model.Id = movie.Id;
                model.Name = movie.Name;

            }
            else if (movie.Action == FormOptions.Save)
            {
                model.SuccessMessage = "Movie saved successfully";
                model.Id = movie.Id;
                model.Name = movie.Name;
            }
            else if (movie.Action == FormOptions.Add)
            {
                return RedirectToAction("Index", "Movies", new { success = "Movie added successfully!"});
            }
            else if (movie.Action == FormOptions.None)
            {
                if (movie.Id == 111)
                {
                    HttpContext.Response.StatusCode = 500;
                    return Content("Can't save this item");
                }
                return Json(new { message="Added successfully"});
            }

            return RedirectToAction("EditResult", model);
        }

        [RequiresModule(modules:new string[]{"HR", "Performance"})]
        public IActionResult Random()
        {
            _logger1.LogError("Method of the day hit");
            //CultureAttribute.SetCultureCookie(Response,"UK-ua");
            var model = new RandomMovieModel("Movie of the day " + _settings.Copyright) {RandomMovie = _movieRepository.GetRandomMovie()};
            return View(model);
        }

        [AjaxOnly]
        public IActionResult AjaxRandom()
        {
            return Json(_movieRepository.GetRandomMovie());
        }
    }
}