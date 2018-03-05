using System.Collections.Generic;
using BugTracking.Models;

namespace BugTracking.Business
{
    public interface IOrganizationService
    {
        BugTrackingResponse<Organization> Save(Organization org);
        List<Organization> GetOrganizations();
        Organization DeleteOrganization(long Id);
        Organization EditOrganization(long Id);
        BugTrackingResponse<Organization> SaveEdit(Organization org);
        BugTrackingResponse<Organization> DeleteConfirmed(long Id);
        List<Organization> DropDownOrganisation();
    }
}
