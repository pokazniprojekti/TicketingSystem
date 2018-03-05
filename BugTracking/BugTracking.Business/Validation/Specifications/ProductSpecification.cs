using FluentValidation;
using BusinessObjects = BugTracking.Models;
using DAL.EntityModel;
using System.Linq;

namespace BugTracking.Business.Validation.Specifications
{
    public class ProductSpecification : AbstractValidator<BusinessObjects.Product> 
    {
        public ProductSpecification()
        {

            RuleFor(x => x.Name_Prod)
                .Must(BeUniqueName)
                .WithMessage("Name already exists")
                .NotEmpty().WithMessage("Name is required")
                .Length(1, 100).WithMessage("Maximum Length is 100 characters");


            RuleFor(x => x.Description_Prod)
                .NotEmpty().WithMessage("Description is required")
                .Length(1, 300).WithMessage("Maximum Length is 300 characters");

        }

        public bool BeUniqueName(string Name)
        {
            using (var db = new BugTrackingEntities())
            {

                int count = db.Products.Where(x => x.Name_Prod == Name && x.Active==true).Count();
                return (count == 0);
            }
        }
    }
}
