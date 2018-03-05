using System.Linq;
using FluentValidation;
using DAL.EntityModel;
using BusinessObjects = BugTracking.Models;

namespace BugTracking.Business.Validation.Specifications
{
    public class PriorityDeleteSpecification : AbstractValidator<BusinessObjects.Priority>
    {
        public PriorityDeleteSpecification()
        {
            RuleFor(x => x.ID)
              .Must(CheckRelationship)
              .WithMessage("Cannot delete while active entry exists in another table")
              .Must(CheckExisting).WithMessage("Record not existing");
        }

        public bool CheckRelationship(byte Id)
        {
            using (var db = new BugTrackingEntities())
            {

                int count = db.Priorities.Where(f => db.Tickets.Where(fb => fb.Active == true).Select(fb => fb.ID).Contains(f.ID) && f.ID == Id && f.Active == true).Count();
                return (count == 0);
            }
        }

        public bool CheckExisting(byte Id)
        {
            using (var db = new BugTrackingEntities())
            {

                int count = db.Priorities.Where(x => x.ID == Id && x.Active == true).Count();
                return (count != 0);
            }
        }
    }
}
