using BusinessObjects = BugTracking.Models;
using DAL.EntityModel;
using System.Linq;
using FluentValidation;


namespace BugTracking.Business.Validation.Specifications
{
   public class StatusSpecification : AbstractValidator<BusinessObjects.Status>
    {
        public StatusSpecification()
        {
            RuleFor(x => x.Name)
            .Must(BeUniqueName)
            .WithMessage("Status with that name already exists")
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

                int count = db.Status.Where(x => x.Name == Name && x.Active == true).Count();
                return (count == 0);
            }
        }
    }
}
