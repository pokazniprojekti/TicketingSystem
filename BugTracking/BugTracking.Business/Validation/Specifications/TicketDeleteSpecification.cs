using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using BusinessObjects = BugTracking.Models;

namespace BugTracking.Business.Validation.Specifications
{
   public class TicketDeleteSpecification : AbstractValidator<BusinessObjects.Ticket>
    {
        public TicketDeleteSpecification()
        {

        }
    }
}
