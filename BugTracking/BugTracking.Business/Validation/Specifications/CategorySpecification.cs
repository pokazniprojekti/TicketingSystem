using System.Linq;
using DAL.EntityModel;
using FluentValidation;
using BusinessObjects = BugTracking.Models;

namespace BugTracking.Business.Validation.Specifications
{
    public class CategorySpecification : AbstractValidator<BusinessObjects.Category>
    {
        public CategorySpecification()
        {
            RuleFor(x => x.Name)
         .Must((x, y) => BeUniqueName(x.Name, x.Section)).WithMessage("Category is already assign to that Section")
         .WithMessage("Category with that name already exists")
         .NotEmpty().WithMessage("Name is required")
         .Length(1, 100).WithMessage("Maximum Length is 100 characters");


            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                ;

            RuleFor(x => x.Section)
               .NotEmpty().WithMessage("Section is required")
               ;
        }

        public bool BeUniqueName(string Name, string Section)
        {
            using (var db = new BugTrackingEntities())
            {

                int count = db.Categories.Where(x => x.Name == Name && x.Section==Section && x.Active == true).Count();
                return (count == 0);
            }
        }
    }
}
