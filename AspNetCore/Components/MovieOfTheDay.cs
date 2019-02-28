using System.Threading.Tasks;
using AspNetCore.BL.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Components
{
    public class MovieOfTheDay : ViewComponent
    {
        private readonly IMovieRepository _movieRepository;

        public MovieOfTheDay(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(_movieRepository.GetRandomMovie());
        }
    }
}