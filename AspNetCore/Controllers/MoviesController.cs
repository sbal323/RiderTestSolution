using System;
using AspNetCore.BL.Contracts;
using AspNetCore.Configuration;
using AspNetCore.Filters;
using AspNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AspNetCore.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly AppSettings _settings;

        public MoviesController(IMovieRepository movieRepository, IOptions<AppSettings> settings)
        {
            _movieRepository = movieRepository;
            _settings = settings.Value;
        }

        public IActionResult Index()
        {
            var model = new MoviesModel("All Movies") {Movies = _movieRepository.GetMovies()};
            return View(model);
        }
            
        public IActionResult Random()
        {
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