using FluentValidation;
using DAL.EntityModel;
using BusinessObjects = BugTracking.Models;
using System.Linq;

namespace BugTracking.Business.Validation.Specifications
{
    public class ProdOrganizationSpecification : AbstractValidator<BusinessObjects.ProductOrganisation>
    {
        public ProdOrganizationSpecification()
        {
            RuleFor(x => x.ID_Product)
             .Must((x, y) => BeUniqueResp(x.ID_Product, x.ID_Organisation)).WithMessage("Product is already assign to that Organization")
             .NotEmpty().WithMessage("ID_Product is required");


            RuleFor(x => x.ID_Organisation)
                .NotEmpty().WithMessage("ID_Organization is required");
        }

        public bool BeUniqueResp(long prod, long org)
        {
            using (var db = new BugTrackingEntities())
            {

                int count = db.ProductOrganisations.Where(x => x.ID_Product == prod && x.ID_Organisation == org && x.Active == true).Count();
                return (count == 0);
            }
        }
    }
}
