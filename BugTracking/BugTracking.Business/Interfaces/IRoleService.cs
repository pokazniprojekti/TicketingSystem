using System.Collections.Generic;
using BugTracking.Models;

namespace BugTracking.Business
{
    public interface IRoleService
    {
        BugTrackingResponse<AspNetRole> Save(AspNetRole role);
        List<AspNetRole> GetRoles();
        AspNetRole EditRole(string Id);
        BugTrackingResponse<AspNetRole> SaveEdit(AspNetRole role);
        AspNetRole DeleteRole(string Id);
        BugTrackingResponse<AspNetRole> DeleteConfirmed(string Id);
        List<AspNetRole> DropDownRole();
    }


}
