namespace AspNetCore.Models
{
    public class AboutModel:BaseModel
    {
        public AboutModel(string title) : base(title)
        {
        }

        public EmailData Email { get; set; }
    }
}