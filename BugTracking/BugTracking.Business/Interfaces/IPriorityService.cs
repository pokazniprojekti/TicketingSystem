using System.Collections.Generic;
using BugTracking.Models;

namespace BugTracking.Business
{
    public interface IPriorityService
    {
        BugTrackingResponse<Priority> Save(Priority prio);
        List<Priority> GetPriorities();
        Priority DeletePriority(int Id);
        Priority EditPriority(int Id);
        BugTrackingResponse<Priority> SaveEdit(Priority prio);
        BugTrackingResponse<Priority> DeleteConfirmed(int Id);
    }
}
