using BusinessObjects = BugTracking.Models;
using DAL.EntityModel;
using System.Linq;
using FluentValidation;

namespace BugTracking.Business.Validation.Specifications
{
    public class ResponsibleSpecification : AbstractValidator<BusinessObjects.Responsible>
    {
        public ResponsibleSpecification()
        {
            RuleFor(x => x.ID_Product)
                .Must((x, y) => BeUniqueResp(x.ID_Product, x.ID_User)).WithMessage("User is already assign to that product")
                .NotEmpty().WithMessage("ID_Product is required");


            RuleFor(x => x.ID_User)
                .NotEmpty().WithMessage("ID_User is required");


        }

        public bool BeUniqueResp(long prod, long user)
        {
            using (var db = new BugTrackingEntities())
            {

                int count = db.Responsibles.Where(x => x.ID_Product == prod && x.ID_User == user && x.Active == true).Count();
                return (count == 0);
            }
        }
    }
}

