using System;
using AspNetCore.BL.Contracts;
using AspNetCore.Filters;
using AspNetCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public IActionResult Index()
        {
            var model = new MoviesModel("All Movies") {Movies = _movieRepository.GetMovies()};
            return View(model);
        }
            
        public IActionResult Random()
        {
            //CultureAttribute.SetCultureCookie(Response,"UK-ua");
            var model = new RandomMovieModel("Movie of the day") {RandomMovie = _movieRepository.GetRandomMovie()};
            return View(model);
        }

        [AjaxOnly]
        public IActionResult AjaxRandom()
        {
            return Json(_movieRepository.GetRandomMovie());
        }
    }
}