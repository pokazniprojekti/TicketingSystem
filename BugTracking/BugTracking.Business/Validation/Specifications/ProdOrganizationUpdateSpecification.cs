using FluentValidation;
using DAL.EntityModel;
using BusinessObjects = BugTracking.Models;
using System.Linq;

namespace BugTracking.Business.Validation.Specifications
{
    public class ProdOrganizationUpdateSpecification : AbstractValidator<BusinessObjects.ProductOrganisation>
    {
        public ProdOrganizationUpdateSpecification()
        {
            RuleFor(x => x.ID_Product)
              .Must((x, y, z) => BeUniqueResp(x.ID_Product, x.ID_Organisation, x.ID)).WithMessage("Duplicate Assign")
              .NotEmpty().WithMessage("ID_Product is required");


            RuleFor(x => x.ID_Organisation)
                .NotEmpty().WithMessage("ID_User is required");

        }

        public bool BeUniqueResp(long prod, long org, long ID)
        {
            using (var db = new BugTrackingEntities())
            {

                int count = db.ProductOrganisations.Where(x => x.ID_Product == prod && x.ID_Organisation == org && x.Active == true && x.ID != ID).Count();
                return (count == 0);
            }
        }
    }
}
