using BusinessObjects = BugTracking.Models;
using DAL.EntityModel;
using System.Linq;
using FluentValidation;

namespace BugTracking.Business.Validation.Specifications
{
    public class ProductUpdateSpecification : AbstractValidator<BusinessObjects.Product>
    {
        public ProductUpdateSpecification()
        {
            RuleFor(x => x.Name_Prod)
              .Must((x, y) => BeUniqueName(x.Name_Prod, x.ID))
              .WithMessage("Name already exists")
              .NotEmpty().WithMessage("Name is required")
              .Length(1, 100).WithMessage("Maximum Length is 100 characters");


            RuleFor(x => x.Description_Prod)
                .NotEmpty().WithMessage("Description is required")
                .Length(1, 300).WithMessage("Maximum Length is 300 characters");


        }

        public bool BeUniqueName(string Name, long ID)
        {
            using (var db = new BugTrackingEntities())
            {

                int count = db.Products.Where(x => x.Name_Prod == Name && x.Active == true && x.ID!=ID).Count();
                return (count == 0);
            }
        }
    }
}
