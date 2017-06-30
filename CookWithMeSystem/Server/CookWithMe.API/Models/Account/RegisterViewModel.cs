namespace CookWithMe.API.Models.Account
{
    using CookWithMe.API.Infrastructure;
    using CookWithMeSystem.Common.Constants;
    using CookWithMeSystem.Models;

    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel : IMapFrom<User>
    {
        [Required]
        [StringLength(ValidationConstants.MaxFirstName, ErrorMessage = ValidationConstants.ValidationErrorMessage, MinimumLength = ValidationConstants.MinFirstName)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(ValidationConstants.MaxLastName, ErrorMessage = ValidationConstants.ValidationErrorMessage, MinimumLength = ValidationConstants.MinLastName)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = ValidationConstants.EmailErrorMessage)]
        public string Email { get; set; }

        [Required]
        [StringLength(ValidationConstants.MaxPassword, ErrorMessage = ValidationConstants.ValidationErrorMessage, MinimumLength = ValidationConstants.MinPassword)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(ValidationConstants.PasswordDataValue, ErrorMessage = ValidationConstants.ComparePasswordErrorMessage)]
        public string ConfirmPassword { get; set; }
    }
}