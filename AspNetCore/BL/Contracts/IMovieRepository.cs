using System.Collections.Generic;
using AspNetCore.Models;

namespace AspNetCore.BL.Contracts
{
    public interface IMovieRepository
    {
        Movie GetRandomMovie();
        List<Movie> GetMovies();
    }
}