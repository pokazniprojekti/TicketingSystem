using System.Collections.Generic;
using BugTracking.Models;

namespace BugTracking.Business
{
    interface IStatusService
    {
        BugTrackingResponse<Status> Save(Status prod);
        List<Status> GetStatus();
        Status EditStatus(int Id);
        BugTrackingResponse<Status> SaveEdit(Status prod);
        Status DeleteStatus(int Id);
        BugTrackingResponse<Status> DeleteConfirmed(int Id);
    }
}
