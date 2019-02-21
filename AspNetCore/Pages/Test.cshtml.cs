using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCore.Pages
{
    public class TestModel : PageModel
    {
        [BindProperty] 
        public ContactInfo Contact { get; set; }
        public string Result { get; set; }
        public TestModel()
        {
            if (Contact == null)
            {
                Contact = new ContactInfo();
            }

            Result = string.Empty;
        }
        public IActionResult OnGet()
        {
            Contact.Name = ""; 
            Contact.Email = ""; 
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Result = $"<h1>Dear {Contact.Name},<br/> We have sent email to {Contact.Email}</h1>";
            } 
            return Page();
        }
    }
}