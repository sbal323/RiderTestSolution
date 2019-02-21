using System.Collections.Generic;

namespace AspNetCore.Models
{
    public class MoviesModel: BaseModel
    {
        public List<Movie> Movies { get; set; }
        public MoviesModel(string title) : base(title)
        {
        }
    }
}