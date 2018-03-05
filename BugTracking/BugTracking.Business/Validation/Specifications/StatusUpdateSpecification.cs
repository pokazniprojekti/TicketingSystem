using BusinessObjects = BugTracking.Models;
using DAL.EntityModel;
using System.Linq;
using FluentValidation;


namespace BugTracking.Business.Validation.Specifications
{
    public class StatusUpdateSpecification : AbstractValidator<BusinessObjects.Status>
    {
        public StatusUpdateSpecification()
        {
           RuleFor(x => x.Name)
          .Must((x, y) => BeUniqueName(x.Name, x.ID))
          .WithMessage("Status with that name already exists")
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

                int count = db.Status.Where(x => x.Name == Name && x.Active == true && x.ID != ID).Count();
                return (count == 0);
            }
        }
    }
}
