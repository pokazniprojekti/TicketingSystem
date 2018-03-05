using BusinessObjects = BugTracking.Models;
using DAL.EntityModel;
using System.Linq;
using FluentValidation;

namespace BugTracking.Business.Validation.Specifications
{
   public class RegisterSpecification : AbstractValidator<BusinessObjects.RegisterViewModel>
    {

    }
}
