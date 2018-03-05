using System.Collections.Generic;
using BugTracking.Models;

namespace BugTracking.Business
{
    public interface IProductService
    {
        BugTrackingResponse<Product> Save(Product prod);
        List<Product> GetProduct();
        List<Responsible> GetProductResponsibleList();
        List<ProductOrganisation> GetProductOrganizationList();
        Product EditProduct(long Id);
        BugTrackingResponse<Product> SaveEdit(Product prod);
        Product DeleteProduct(long Id);
        BugTrackingResponse<Product> DeleteConfirmed(long Id);
        List<Product> DropDownProduct();
        BugTrackingResponse<Responsible> SaveProductResponsible(Responsible productresponsible);
        BugTrackingResponse<ProductOrganisation> SaveProductOrganisation(ProductOrganisation productorganisation);
        Responsible EditResponsible(int Id);
        BugTrackingResponse<Responsible> SaveEditResponsible(Responsible responsible);
        BugTrackingResponse<Responsible> DeleteResponsibleConfirmed(int Id);
        ProductOrganisation EditOrganization(int Id);
        BugTrackingResponse<ProductOrganisation> SaveEditOrganization(ProductOrganisation prodorg);
        ProductOrganisation DeleteOrganization(int Id);
        BugTrackingResponse<ProductOrganisation> DeleteOrganizationConfirmed(int Id);

    }
}
