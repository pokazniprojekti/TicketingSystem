using System.Linq;
using FluentValidation;
using DAL.EntityModel;
using BusinessObjects = BugTracking.Models;

namespace BugTracking.Business.Validation.Specifications
{
    public class PrioritySpecification : AbstractValidator<BusinessObjects.Priority>
    {
        public PrioritySpecification()
        {
            RuleFor(x => x.Name)
           .Must(BeUniqueName)
           .WithMessage("Priority with that name already exists")
           .NotEmpty().WithMessage("Name is required")
           .Length(1, 100).WithMessage("Maximum Length is 100 characters");


            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                ;
        }

        public bool BeUniqueName(string Name)
        {
            using (var db = new BugTrackingEntities())
            {

                int count = db.Priorities.Where(x => x.Name == Name && x.Active == true).Count();
                return (count == 0);
            }
        }
    }
}
