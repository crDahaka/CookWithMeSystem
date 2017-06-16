namespace CookWithMe.API.Models.Account
{
    using CookWithMe.API.Infrastructure;
    using CookWithMeSystem.Common.Constants;
    using CookWithMeSystem.Models;

    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel : IMapFrom<User>
    {
        [Required]
        [StringLength(ValidationConstants.MaxFirstName)]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(ValidationConstants.MaxLastName)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = ValidationConstants.MinPassword)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}