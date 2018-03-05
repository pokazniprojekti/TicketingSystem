using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using BusinessObjects = BugTracking.Models;

namespace BugTracking.Business.Validation.Specifications
{
    public class TicketSaveSpecificaion : AbstractValidator<BusinessObjects.Ticket>
    {
        public TicketSaveSpecificaion()
        {
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Subject cannot be empty!");
            RuleFor(x => x.Body).NotEmpty().WithMessage("Body cannot be empty!");
            RuleFor(x => x.StatusID).NotEmpty().WithMessage("Status cannot be empty!");
            RuleFor(x => x.CategoryID).NotEmpty().WithMessage("Category cannot be empty!");
           //RuleFor(x => x.DateCreated).NotEmpty().WithMessage("Date Created cannot be empty!");
           // RuleFor(x => x.Active).NotEmpty().WithMessage("Active cannot be empty!");
            RuleFor(x => x.OrganizationID).NotEmpty().WithMessage("Organization cannot be empty!");
            RuleFor(x => x.PriorityID).NotEmpty().WithMessage("Priority cannot be empty!");
            RuleFor(x => x.ProductID).NotEmpty().WithMessage("Product cannot be empty!");
            RuleFor(x => x.UserID).NotEmpty().WithMessage("User cannot be empty!");
        }
    }
}
