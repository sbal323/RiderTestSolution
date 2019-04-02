using System.Collections.Generic;

namespace AspNetCore.Models
{
    public class CountriesModel:BaseModel
    {
        public CountriesModel(string title) : base(title)
        {
            Codes = new List<string>();
        }

        public List<string> Codes { get; set; }
    }
}