using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracking.Models
{
    public class RegisterViewModel : IBusinessObject
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(100, ErrorMessage = "Maximum length 100 characters")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(100, ErrorMessage = "Maximum length 100 characters")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Organization")]
        public long OrganizationID { get; set; }

        [Required]
        [Display(Name = "Telephone number")]
        [StringLength(100, ErrorMessage = "Maximum length 50 characters")]
        public string TelephoneNumber { get; set; }


        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string RoleId { get; set; }
    }
}
