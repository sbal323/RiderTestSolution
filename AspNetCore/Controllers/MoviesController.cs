using System;
using AspNetCore.BL.Contracts;
using AspNetCore.Configuration;
using AspNetCore.Filters;
using AspNetCore.Models;
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

        public IActionResult Index()
        {
            _logger.LogCritical("Inside index method");
            var model = new MoviesModel("All Movies") {Movies = _movieRepository.GetMovies()};
            return View(model);
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