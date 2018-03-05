using BusinessObjects = BugTracking.Models;
using DAL.EntityModel;
using System.Linq;
using FluentValidation;


namespace BugTracking.Business.Validation.Specifications
{
    public class StatusDeleteSpecification : AbstractValidator<BusinessObjects.Status>
    {
        public StatusDeleteSpecification()
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

                int count = db.Status.Where(f => db.Tickets.Where(fb => fb.Active == true).Select(fb => fb.ID).Contains(f.ID) && f.ID == Id && f.Active == true).Count();
                return (count == 0);
            }
        }

        public bool CheckExisting(byte Id)
        {
            using (var db = new BugTrackingEntities())
            {

                int count = db.Status.Where(x => x.ID == Id && x.Active == true).Count();
                return (count != 0);
            }
        }
    }

  
}
