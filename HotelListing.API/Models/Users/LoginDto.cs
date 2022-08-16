using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.Users
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Password must be complex and between {2} and {1} characters."
            , MinimumLength = 6)]
        public string Password { get; set; }
    }
}
