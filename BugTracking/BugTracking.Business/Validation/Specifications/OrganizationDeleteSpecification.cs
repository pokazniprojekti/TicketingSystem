using System.Linq;
using DAL.EntityModel;
using FluentValidation;
using BusinessObjects = BugTracking.Models;

namespace BugTracking.Business.Validation.Specifications
{
    public class OrganizationDeleteSpecification: AbstractValidator<BusinessObjects.Organization>
    {
        public OrganizationDeleteSpecification()
        {
            RuleFor(x => x.ID)
                 .Must(CheckRelationship)
                 .WithMessage("Cannot delete while active entry exists in another table")
                 .Must(CheckExisting).WithMessage("Record not existing");


        }

        public bool CheckRelationship(long ID)
        {
            using (var db = new BugTrackingEntities())
            {

                int count = db.Organizations.Where(f => db.ProductOrganisations.Where(fb=>fb.Active==true).Select(fb => fb.ID_Organisation).Contains(f.ID) && f.ID == ID && f.Active).Count();
                int count2= db.Organizations.Where(f => db.Users.Where(fb => fb.Active == true).Select(fb => fb.OrganizationID).Contains(f.ID) && f.ID == ID && f.Active).Count();
                return (count == 0 && count2==0);
            }
        }


        public bool CheckExisting(long ID)
        {
            using (var db = new BugTrackingEntities())
            {

                int count = db.Organizations.Where(x => x.ID == ID && x.Active == true).Count();
                return (count != 0);
            }
        }


    }
}
