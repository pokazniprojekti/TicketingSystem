using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTracking.Models;

namespace BugTracking.Business.Interfaces
{
    interface ITicketService
    {
        BugTrackingResponse<Ticket> Save(Ticket ticket);
        List<Ticket> GetTicketList();
        Ticket EditTicket(long Id);
        BugTrackingResponse<Ticket> SaveEdit(Ticket ticket);
        List<Ticket> GetTechnicianTicketList();
        List<Ticket> GetRegularUserList();
        List<Ticket> GetFilteredTicketList(Ticket filter);
        List<Ticket> GetFilteredTechnicianTicketList(Ticket filter);
        List<Ticket> GetFilteredRegularUserTicketList(Ticket filter);
    }
}
