using BusinessObjects = BugTracking.Models;
using DAL.EntityModel;
using System.Linq;
using FluentValidation;

namespace BugTracking.Business.Validation.Specifications
{
    public class RoleDeleteSpecification : AbstractValidator<BusinessObjects.AspNetRole>
    {
        public RoleDeleteSpecification()
        {
            RuleFor(x => x.Id)
                .Must(CheckRelationship)
                .WithMessage("Cannot delete while active entry exists in another table")
                .Must(CheckExisting).WithMessage("Record not existing");
        }


        public bool CheckRelationship(string Id)
        {
            using (var db = new BugTrackingEntities())
            {

                int count = db.AspNetRoles.Where(f => db.AspNetUserRoles.Where(fb => fb.Active == true).Select(fb => fb.RoleId).Contains(f.Id) && f.Id == Id && f.Active==true).Count();
                return (count == 0);
            }
        }


        public bool CheckExisting(string Id)
        {
            using (var db = new BugTrackingEntities())
            {

                int count = db.AspNetRoles.Where(x => x.Id == Id && x.Active == true).Count();
                return (count != 0);
            }
        }
    }
}
