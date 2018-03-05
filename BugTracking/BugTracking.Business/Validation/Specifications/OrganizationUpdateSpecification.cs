using System.Linq;
using FluentValidation;
using DAL.EntityModel;
using BusinessObjects = BugTracking.Models;

namespace BugTracking.Business.Validation.Specifications
{
    public class OrganizationUpdateSpecification : AbstractValidator<BusinessObjects.Organization>
    {

        public OrganizationUpdateSpecification()
        {

            RuleFor(x => x.Name)
                .Must((x, y) => BeUniqueName(x.Name, x.ID))
                .WithMessage("Name already exist")
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
                .Must((x, y) => BeUniqueEmail(x.Email, x.ID))
                .WithMessage("Email already exist")
                ;
        }

        public bool BeUniqueName(string Name, long ID)
        {
            using (var db = new BugTrackingEntities())
            {

                int count = db.Organizations.Where(x => x.Name == Name && x.Active && x.ID!=ID).Count();
                return (count == 0);
            }
        }

        public bool BeUniqueEmail(string Email, long ID)
        {
            using (var db = new BugTrackingEntities())
            {

                int count = db.Organizations.Where(x => x.Email == Email && x.Active && x.ID != ID).Count();
                return (count == 0);
            }
        }

    }
}
