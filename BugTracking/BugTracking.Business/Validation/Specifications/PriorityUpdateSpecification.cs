using System.Linq;
using FluentValidation;
using DAL.EntityModel;
using BusinessObjects = BugTracking.Models;
namespace BugTracking.Business.Validation.Specifications
{
   public class PriorityUpdateSpecification : AbstractValidator<BusinessObjects.Priority>
    {
        public PriorityUpdateSpecification()
        {
            RuleFor(x => x.Name)
        .Must((x, y) => BeUniqueName(x.Name, x.ID))
        .WithMessage("Priority with that name already exists")
        .NotEmpty().WithMessage("Name is required")
        .Length(1, 100).WithMessage("Maximum Length is 100 characters");


            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                ;
        }

        public bool BeUniqueName(string Name, long ID)
        {
            using (var db = new BugTrackingEntities())
            {

                int count = db.Priorities.Where(x => x.Name == Name && x.Active == true && x.ID != ID).Count();
                return (count == 0);
            }
        }
    }
}
