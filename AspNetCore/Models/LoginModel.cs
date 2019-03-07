using AspNetCore.Models.Input;

namespace AspNetCore.Models
{
    public class LoginModel:BaseModel
    {
        public LoginModel(string title) : base(title)
        {
        }
        public LoginInputModel InputModel { get; set; } = new LoginInputModel();
    }
}