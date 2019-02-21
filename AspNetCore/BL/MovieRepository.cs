using System;
using System.Collections.Generic;
using System.Linq;
using AspNetCore.BL.Contracts;
using AspNetCore.Models;
using Microsoft.AspNetCore.SignalR;

namespace AspNetCore.BL
{
    public class MovieRepository:IMovieRepository
    {
        public Movie GetRandomMovie()
        {
            Movie movie;
            if ((new System.Random()).Next() % 2 == 0)
            {
                movie = new Movie
                {
                    Id = 348,
                    Name = "Breaking bad"
                };
            }
            else
            {
                movie = new Movie
                {
                    Id = 34,
                    Name = "Silicon valley"
                };
            }

            return movie;
        }

        public List<Movie> GetMovies()
        {
            var random = new System.Random(1);
            return Enumerable.Range(0, 100)
                .Select(i => i + random.Next(0,1000))
                .Select(rnd => new Movie() {Id = rnd, Name = $"Movie {rnd}"}).ToList();
        }
    }
}