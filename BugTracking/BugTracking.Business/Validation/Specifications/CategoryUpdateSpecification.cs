using System.Linq;
using DAL.EntityModel;
using FluentValidation;
using BusinessObjects = BugTracking.Models;

namespace BugTracking.Business.Validation.Specifications
{
    public class CategoryUpdateSpecification : AbstractValidator<BusinessObjects.Category>
    {
        public CategoryUpdateSpecification()
        {
            RuleFor(x => x.Name)
      .Must((x, y, z) => BeUniqueName(x.Name, x.Section, x.ID)).WithMessage("Category with that name is already assign to that Section")
      .NotEmpty().WithMessage("Name is required")
      .Length(1, 100).WithMessage("Maximum Length is 100 characters");


            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                ;

            RuleFor(x => x.Section)
               .NotEmpty().WithMessage("Section is required")
               ;
        }

        public bool BeUniqueName(string Name, string Section, long ID)
        {
            using (var db = new BugTrackingEntities())
            {

                int count = db.Categories.Where(x => x.Name == Name && x.Section==Section && x.Active == true && x.ID != ID).Count();
                return (count == 0);
            }
        }
    }
}
