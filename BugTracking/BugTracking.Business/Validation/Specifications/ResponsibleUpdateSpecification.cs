using BusinessObjects = BugTracking.Models;
using DAL.EntityModel;
using System.Linq;
using FluentValidation;

namespace BugTracking.Business.Validation.Specifications
{
    public class ResponsibleUpdateSpecification : AbstractValidator<BusinessObjects.Responsible>
    {
        public ResponsibleUpdateSpecification()
        {
            RuleFor(x => x.ID_Product)
               .Must((x, y, z) => BeUniqueResp(x.ID_Product, x.ID_User, x.ID)).WithMessage("Duplicate Assign")
               .NotEmpty().WithMessage("ID_Product is required");


            RuleFor(x => x.ID_User)
                .NotEmpty().WithMessage("ID_User is required");


        }

        public bool BeUniqueResp(long prod, long user, long ID)
        {
            using (var db = new BugTrackingEntities())
            {

                int count = db.Responsibles.Where(x => x.ID_Product == prod && x.ID_User==user && x.Active == true && x.ID != ID).Count();
                return (count == 0);
            }
        }
    }
}
