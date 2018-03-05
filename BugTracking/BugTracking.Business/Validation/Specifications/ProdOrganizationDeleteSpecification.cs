using FluentValidation;
using DAL.EntityModel;
using BusinessObjects = BugTracking.Models;
using System.Linq;

namespace BugTracking.Business.Validation.Specifications
{
    public class ProdOrganizationDeleteSpecification : AbstractValidator<BusinessObjects.ProductOrganisation>
    {
        public ProdOrganizationDeleteSpecification()
        {
            RuleFor(x => x.ID)
              .Must(CheckExisting).WithMessage("Record not exist");
        }

        public bool CheckExisting(long ID)
        {
            using (var db = new BugTrackingEntities())
            {

                int count = db.ProductOrganisations.Where(x => x.ID == ID && x.Active == true).Count();
                return (count != 0);
            }
        }
    }
}
