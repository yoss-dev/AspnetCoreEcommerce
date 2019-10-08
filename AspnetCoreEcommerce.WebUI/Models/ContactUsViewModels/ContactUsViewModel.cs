using System.ComponentModel.DataAnnotations;

namespace AspnetCoreEcommerce.WebUI.Models.ContactUsViewModels
{
    public class ContactUsViewModel
    {
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Message { get; set; }

    }
}
