using BusinessObjects = BugTracking.Models;
using DAL.EntityModel;
using System.Linq;
using FluentValidation;

namespace BugTracking.Business.Validation.Specifications
{
    public class RoleSpecification : AbstractValidator<BusinessObjects.AspNetRole>
    {
        public RoleSpecification()
        {
            RuleFor(x => x.Name)
             .Must(BeUniqueName)
             .WithMessage("Role with that name already exists")
             .NotEmpty().WithMessage("Name is required")
             .Length(1, 256).WithMessage("Maximum Length is 256 characters");


            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                ;
        }

        public bool BeUniqueName(string Name)
        {
            using (var db = new BugTrackingEntities())
            {

                int count = db.AspNetRoles.Where(x => x.Name == Name && x.Active==true).Count();
                return (count == 0);
            }
        }
    }
}
