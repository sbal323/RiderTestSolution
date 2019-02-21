using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AspNetCore.Pages
{
    public class ContactInfo
    {
        [Required]
        [DisplayName("Your Name")]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}