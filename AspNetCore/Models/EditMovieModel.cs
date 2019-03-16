using AspNetCore.Models.Input;

namespace AspNetCore.Models
{
    
    public class EditMovieModel:BaseModel
    {
        public EditMovieModel(string title) : base(title)
        {
            
        }

        public EditMovieModel():base("")
        {
            
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}