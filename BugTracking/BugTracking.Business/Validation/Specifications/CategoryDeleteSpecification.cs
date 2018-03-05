using System.Linq;
using DAL.EntityModel;
using FluentValidation;
using BusinessObjects = BugTracking.Models;

namespace BugTracking.Business.Validation.Specifications
{
   public class CategoryDeleteSpecification : AbstractValidator<BusinessObjects.Category>
    {
        public CategoryDeleteSpecification()
        {
            RuleFor(x => x.ID)
             .Must(CheckRelationship)
             .WithMessage("Cannot delete while active entry exists in another table")
             .Must(CheckExisting).WithMessage("Record not existing");
        }

        public bool CheckRelationship(short Id)
        {
            using (var db = new BugTrackingEntities())
            {

                int count = db.Categories.Where(f => db.Tickets.Where(fb => fb.Active == true).Select(fb => fb.ID).Contains(f.ID) && f.ID == Id && f.Active == true).Count();
                return (count == 0);
            }
        }

        public bool CheckExisting(short Id)
        {
            using (var db = new BugTrackingEntities())
            {

                int count = db.Categories.Where(x => x.ID == Id && x.Active == true).Count();
                return (count != 0);
            }
        }
    }
}
