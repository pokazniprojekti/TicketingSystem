using System.Linq;
using FluentValidation;
using BusinessObjects = BugTracking.Models;
using DAL.EntityModel;

namespace BugTracking.Business.Validation.Specifications
{
    public class ProductDeleteSpecification : AbstractValidator<BusinessObjects.Product>
    {
        public ProductDeleteSpecification()
        {
            RuleFor(x => x.ID)
                .Must(CheckRelationship)
                .WithMessage("Cannot delete while active entry exists in another table")
                .Must(CheckExisting).WithMessage("Record not existing");
            ;
        }

        public bool CheckRelationship(long ID)
        {
            using (var db = new BugTrackingEntities())
            {

                int count = db.Products.Where(f => db.ProductOrganisations.Where(fb => fb.Active == true).Select(fb => fb.ID_Organisation).Contains(f.ID) && f.ID == ID && f.Active==true).Count();
                int count2 = db.Products.Where(f => db.Responsibles.Where(fb => fb.Active == true).Select(fb => fb.ID_Product).Contains(f.ID) && f.ID == ID && f.Active==true).Count();
                return (count == 0 && count2 == 0);
            }
        }

        public bool CheckExisting(long ID)
        {
            using (var db = new BugTrackingEntities())
            {

                int count = db.Products.Where(x => x.ID == ID && x.Active == true).Count();
                return (count != 0);
            }
        }
    }
}
