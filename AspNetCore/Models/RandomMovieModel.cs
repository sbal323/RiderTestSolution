using System.Collections.Generic;

namespace AspNetCore.Models
{
    public class RandomMovieModel: BaseModel
    {
        public Movie RandomMovie { get; set; }
        public RandomMovieModel(string title) : base(title)
        {
        }
    }
}