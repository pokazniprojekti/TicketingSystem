using FluentValidation;
using System.Linq;
using DAL.EntityModel;
using BusinessObjects = BugTracking.Models;

namespace BugTracking.Business.Validation.Specifications
{
    public class OrganizationSpecification : AbstractValidator<BusinessObjects.Organization>
    {
        public OrganizationSpecification()
        {


            RuleFor(x => x.Name)
                .Must(BeUniqueName)
                .WithMessage("Name already exists")
                .NotEmpty().WithMessage("Name is required")
                .Length(1, 100).WithMessage("Maximum Length is 100 characters");
                

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Location is required")
                .Length(1, 100).WithMessage("Maximum Length is 100 characters");


            RuleFor(x => x.TelephoneNumber)
              .NotEmpty().WithMessage("Telephone Number is required")
              .Length(1, 50).WithMessage("Maximum Length is 50 characters");


            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .Length(1, 50).WithMessage("Maximum Length is 50 characters")
                .EmailAddress().WithMessage("Not valid format for email address")
                .Must(BeUniqueEmail)
                .WithMessage("Email already exists")
                ; 

        }

        public bool BeUniqueName(string Name)
        {
            using (var db = new BugTrackingEntities())
            {

                int count = db.Organizations.Where(x => x.Name == Name && x.Active).Count();
                return (count == 0);
            }
        }

        public bool BeUniqueEmail(string Email)
        {
            using (var db = new BugTrackingEntities())
            {

                int count = db.Organizations.Where(x => x.Email == Email && x.Active).Count();
                return (count == 0);
            }
        }
    }
}
