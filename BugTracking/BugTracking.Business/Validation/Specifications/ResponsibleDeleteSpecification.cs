using BusinessObjects = BugTracking.Models;
using DAL.EntityModel;
using System.Linq;
using FluentValidation;

namespace BugTracking.Business.Validation.Specifications
{
    public class ResponsibleDeleteSpecification : AbstractValidator<BusinessObjects.Responsible>
    {
        public ResponsibleDeleteSpecification()
        {
            RuleFor(x => x.ID)
               .Must(CheckExisting).WithMessage("Record not exist");
               
        }


        public bool CheckExisting(long ID)
        {
            using (var db = new BugTrackingEntities())
            {

                int count = db.Responsibles.Where(x => x.ID == ID && x.Active == true).Count();
                return (count != 0);
            }
        }
    }

   
}
