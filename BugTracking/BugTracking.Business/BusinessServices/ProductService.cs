using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using EntityModel = DAL.EntityModel;
using BusinessObjects = BugTracking.Models;
using BugTracking.Business.Validation.Specifications;

namespace BugTracking.Business
{
    public class ProductService : BusinessServiceBase, IProductService
    {
        private static readonly string ClassName = typeof(ProductService).FullName;

        public BusinessObjects.Product DeleteProduct(long Id)
        {
            Logger.TraceMethodStart(ClassName, "DeleteProduct");

            var prod = default(BusinessObjects.Product);

            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {

                    var query = db.Products.Where(c => c.ID == Id && c.Active==true).FirstOrDefault();
                    prod = MapperAllLevels.Map<EntityModel.Product, BusinessObjects.Product>(query);
                    return prod;

                }
            }

            catch (Exception ex)
            {
                Logger.TraceError("Exception: ", ex);
                throw;
            }

            finally
            {
                Logger.TraceMethodEnd(ClassName, "DeleteProduct");
            }

        }


        public BusinessObjects.Product EditProduct(long Id)
        {
            Logger.TraceMethodStart(ClassName, "EditProduct");

            var prod = default(BusinessObjects.Product);

            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {

                    var query = db.Products.Where(c => c.ID == Id && c.Active==true).FirstOrDefault();
                    prod = MapperAllLevels.Map<EntityModel.Product, BusinessObjects.Product>(query);
                    return prod;

                }
            }

            catch (Exception ex)
            {
                Logger.TraceError("Exception ex", ex);
                throw;
            }

            finally
            {
                Logger.TraceMethodEnd(ClassName, "EditProduct");
            }
        }


        public List<BusinessObjects.Product> GetProduct()
        {
            Logger.TraceMethodStart(ClassName, "GetProduct");

            var prod = default(List<BusinessObjects.Product>);

            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {

                    var query = db.Products.Where(c=>c.Active==true).ToList();
                    //Logger.ServiceDebug($"FILTER: [{JsonExtensions.ToJson(query)}].", ClassName);

                    prod = MapperAllLevels.Map<List<EntityModel.Product>, List<BusinessObjects.Product>>(query);
                    return prod;
                }


            }

            catch (Exception ex)
            {
                Logger.TraceError("Exception:", ex);
                throw;
            }

            finally
            {
                Logger.TraceMethodEnd(ClassName, "GetProduct");


            }


        }

        public BugTrackingResponse<BusinessObjects.Product> Save(BusinessObjects.Product prod)
        {
            Logger.TraceMethodStart(ClassName, "Save");

            var response = new BugTrackingResponse<BusinessObjects.Product>();

            using (var db = new EntityModel.BugTrackingEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {

                        var validator = new ProductSpecification();
                        var result = validator.Validate(prod);
                        var failures = result.Errors;

                        response.ValidationResult = result;

                        if (result.IsValid)
                        {
                            prod.Active = true;
                            prod.LastModified = DateTime.Now;
                            var newprod = MapperAllLevels.Map<BusinessObjects.Product, EntityModel.Product>(prod);
                            db.Products.Add(newprod);
                            db.SaveChanges();
                            transaction.Commit();
                        }

                        else
                        {

                            transaction.Rollback();
                            Logger.TraceErrorFormat("Error while Saving {0}", response.ValidationResult.Errors);
                        }

                        return response;
                    }

                    catch (Exception ex)
                    {
                        Logger.TraceError("Exception: ", ex);
                        transaction.Rollback();
                        throw;
                    }

                    finally
                    {
                        Logger.TraceMethodEnd(ClassName, "Save");
                    }

                }
            }
        }

        public BugTrackingResponse<BusinessObjects.Product> SaveEdit(BusinessObjects.Product prod)
        {
            Logger.TraceMethodStart(ClassName, "SaveEdit");

            var response = new BugTrackingResponse<BusinessObjects.Product>();

            using (var db = new EntityModel.BugTrackingEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {

                        var validator = new ProductUpdateSpecification();
                        var result = validator.Validate(prod);
                        var failures = result.Errors;

                        response.ValidationResult = result;

                        if (result.IsValid)
                        {
                            prod.LastModified = DateTime.Now;
                            var newrole = MapperAllLevels.Map<BusinessObjects.Product, EntityModel.Product>(prod);
                            db.Entry(newrole).State = EntityState.Modified;
                            db.SaveChanges();
                            transaction.Commit();
                        }

                        else
                        {

                            transaction.Rollback();
                            Logger.TraceErrorFormat("Error while Saving {0}", response.ValidationResult.Errors);
                        }

                        return response;
                    }

                    catch (Exception ex)
                    {
                        Logger.TraceError("Exception: ", ex);
                        transaction.Rollback();
                        throw;
                    }

                    finally
                    {
                        Logger.TraceMethodEnd(ClassName, "SaveEdit");
                    }

                }
            }
        }


        public BugTrackingResponse<BusinessObjects.Product> DeleteConfirmed(long Id)
        {
            Logger.TraceMethodStart(ClassName, "DeleteProduct");

            var response = new BugTrackingResponse<BusinessObjects.Product>();

            using (var db = new EntityModel.BugTrackingEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {
                        var query = db.Products.Where(c => c.ID == Id && c.Active==true).FirstOrDefault();
                        var prod = MapperAllLevels.Map<EntityModel.Product, BusinessObjects.Product>(query);

                        var validator = new ProductDeleteSpecification();
                        var result = validator.Validate(prod);
                        var failures = result.Errors;

                        response.ValidationResult = result;

                        if (result.IsValid)
                        {

                            query.Active = false;
                            query.LastModified = DateTime.Now;
                            db.SaveChanges();
                            transaction.Commit();

                        }

                        else
                        {

                            transaction.Rollback();
                            Logger.TraceErrorFormat("Error while Saving {0}", response.ValidationResult.Errors);
                        }

                        return response;

                    }

                    catch (Exception ex)
                    {
                        Logger.TraceError("Exception: ", ex);
                        transaction.Rollback();
                        throw;
                    }

                    finally
                    {
                        Logger.TraceMethodEnd(ClassName, "DeleteConfirmed");
                    }

                }
            }
        }

        public List<BusinessObjects.Product> DropDownProduct()
        {
            Logger.TraceMethodStart(ClassName);


            var dropdown = default(List<BusinessObjects.Product>);

            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {

                    var Productquery = db.Products.Where(c=>c.Active==true).ToList();
                    dropdown = MapperAllLevels.Map<List<EntityModel.Product>, List<BusinessObjects.Product>>(Productquery);
                    return dropdown;
                }
            }

            catch (Exception ex)
            {
                Logger.TraceError("Exception: ", ex);
                throw;
            }

            finally
            {
                Logger.TraceMethodEnd(ClassName, "DropDownProduct");
            }

        }



        public BugTrackingResponse<BusinessObjects.Responsible> SaveProductResponsible(BusinessObjects.Responsible productresponsible)
        {
            Logger.TraceMethodStart(ClassName, "SaveProductResponsible");

            var response = new BugTrackingResponse<BusinessObjects.Responsible>();

            using (var db = new EntityModel.BugTrackingEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {

                        var validator = new ResponsibleSpecification();
                        var result = validator.Validate(productresponsible);
                        var failures = result.Errors;

                        response.ValidationResult = result;

                        if (result.IsValid)
                        {
                            productresponsible.Active = true;
                            productresponsible.LastModified = DateTime.Now;
                            var responsible = MapperAllLevels.Map<BusinessObjects.Responsible, EntityModel.Responsible>(productresponsible);
                            db.Responsibles.Add(responsible);
                            db.SaveChanges();
                            transaction.Commit();
                        }

                        else
                        {

                            transaction.Rollback();
                            Logger.TraceErrorFormat("Error while Saving {0}", response.ValidationResult.Errors);
                        }

                        return response;


                    }

                    catch (Exception ex)
                    {
                        Logger.TraceError("Exception: ", ex);
                        transaction.Rollback();
                        throw;
                    }

                    finally
                    {
                        Logger.TraceMethodEnd(ClassName, "SaveProductResponsible");
                    }

                }
            }
        }

        public BugTrackingResponse<BusinessObjects.ProductOrganisation> SaveProductOrganisation(BusinessObjects.ProductOrganisation productorganisation)
        {
            Logger.TraceMethodStart(ClassName, "SaveProductOrganisation");

            var response = new BugTrackingResponse<BusinessObjects.ProductOrganisation>();

            using (var db = new EntityModel.BugTrackingEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {

                        var validator = new ProdOrganizationSpecification();
                        var result = validator.Validate(productorganisation);
                        var failures = result.Errors;

                        response.ValidationResult = result;

                        if (result.IsValid)
                        {
                            productorganisation.Active = true;
                            productorganisation.LastModified = DateTime.Now;
                            var productorg = MapperAllLevels.Map<BusinessObjects.ProductOrganisation, EntityModel.ProductOrganisation>(productorganisation);
                            db.ProductOrganisations.Add(productorg);
                            db.SaveChanges();
                            transaction.Commit();
                        }


                        else
                        {

                            transaction.Rollback();
                            Logger.TraceErrorFormat("Error while Saving {0}", response.ValidationResult.Errors);
                        }

                        return response;


                    }

                    catch (Exception ex)
                    {
                        Logger.TraceError("Exception: ", ex);
                        transaction.Rollback();
                        throw;
                    }

                    finally
                    {
                        Logger.TraceMethodEnd(ClassName, "SaveProductOrganisation");
                    }

                }
            }
        }

        public List<BusinessObjects.Responsible> GetProductResponsibleList()
        {
            Logger.TraceMethodStart(ClassName, "GetProductResponsibleList");

            var prod = default(List<BusinessObjects.Responsible>);


            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {

                    var query = db.Responsibles.Where(c=>c.Active==true)
                         .Include(x => x.Product)
                         .Include(x => x.User).OrderBy(c => c.User.Username).ToList();


                    prod = MapperAllLevels.Map<List<EntityModel.Responsible>, List<BusinessObjects.Responsible>>(query);
                    return prod;
                }
            }
            catch (Exception ex)
            {
                Logger.TraceError("Exception:", ex);
                throw;
            }

            finally
            {
                Logger.TraceMethodEnd(ClassName, "GetProductResponsibleList");
            }


        }

        public List<BusinessObjects.ProductOrganisation> GetProductOrganizationList()
        {
            Logger.TraceMethodStart(ClassName, "GetProductOrganizationList");

            var prod = default(List<BusinessObjects.ProductOrganisation>);


            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {

                    var query = db.ProductOrganisations.Where(c=>c.Active==true)
                         .Include(x => x.Product)
                         .Include(x => x.Organization).ToList();
                    ;

                    prod = MapperAllLevels.Map<List<EntityModel.ProductOrganisation>, List<BusinessObjects.ProductOrganisation>>(query);
                    return prod;
                }
            }
            catch (Exception ex)
            {
                Logger.TraceError("Exception:", ex);
                throw;
            }

            finally
            {
                Logger.TraceMethodEnd(ClassName, "GetProductOrganizationList");
            }
        }


        public BusinessObjects.Responsible EditResponsible(int Id)
        {
            Logger.TraceMethodStart(ClassName, "EditResponsible");

            var resp = default(BusinessObjects.Responsible);

            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {

                    var query = db.Responsibles.Where(c => c.ID == Id && c.Active==true).FirstOrDefault();
                    resp = MapperAllLevels.Map<EntityModel.Responsible, BusinessObjects.Responsible>(query);
                    return resp;

                }
            }

            catch (Exception ex)
            {
                Logger.TraceError("Exception: ", ex);
                throw;
            }

            finally
            {
                Logger.TraceMethodEnd(ClassName, "EditResponsible");
            }
        }


        public BugTrackingResponse<BusinessObjects.Responsible> SaveEditResponsible(BusinessObjects.Responsible responsible)
        {
            Logger.TraceMethodStart(ClassName, "SaveEditResponsible");

            var response = new BugTrackingResponse<BusinessObjects.Responsible>();

            using (var db = new EntityModel.BugTrackingEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {

                        var validator = new ResponsibleUpdateSpecification();
                        var result = validator.Validate(responsible);
                        var failures = result.Errors;

                        response.ValidationResult = result;

                        if (result.IsValid)
                        {
                            responsible.LastModified = DateTime.Now;
                            var resp = MapperAllLevels.Map<BusinessObjects.Responsible, EntityModel.Responsible>(responsible);
                            db.Entry(resp).State = EntityState.Modified;
                            db.SaveChanges();
                            transaction.Commit();
                        }

                        else
                        {

                            transaction.Rollback();
                            Logger.TraceErrorFormat("Error while Saving {0}", response.ValidationResult.Errors);
                        }

                        return response;



                    }

                    catch (Exception ex)
                    {
                        Logger.TraceError("Exception: ", ex);
                        transaction.Rollback();
                        throw;
                    }

                    finally
                    {
                        Logger.TraceMethodEnd(ClassName, "SaveEditResponsible");
                    }

                }
            }
        }


        public BusinessObjects.Responsible DeleteResponsible(int Id)
        {

            Logger.TraceMethodStart(ClassName, "DeleteResponsible");
            var resp = default(BusinessObjects.Responsible);

            try
            {

                using (var db = new EntityModel.BugTrackingEntities())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {

                        var query = db.Responsibles.Where(c => c.ID == Id && c.Active==true).FirstOrDefault();
                        resp = MapperAllLevels.Map<EntityModel.Responsible, BusinessObjects.Responsible>(query);
                        return resp;

                    }
                }

            }

            catch (Exception ex)
            {
                Logger.TraceError("Exception:", ex);
                throw;
            }


            finally
            {
                Logger.TraceMethodEnd(ClassName, "DeleteResponsible");
            }

        }

        public BugTrackingResponse<BusinessObjects.Responsible> DeleteResponsibleConfirmed(int Id)
        {
            Logger.TraceMethodStart(ClassName, "DeleteResponsibleConfirmed");

            var response = new BugTrackingResponse<BusinessObjects.Responsible>();


            using (var db = new EntityModel.BugTrackingEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {
                        var query = db.Responsibles.Where(c => c.ID == Id && c.Active == true).FirstOrDefault();
                        var prod = MapperAllLevels.Map<EntityModel.Responsible, BusinessObjects.Responsible>(query);

                        var validator = new ResponsibleDeleteSpecification();
                        var result = validator.Validate(prod);
                        var failures = result.Errors;

                        response.ValidationResult = result;

                        if (result.IsValid)
                        {
                           
                        
                            query.Active = false;
                            query.LastModified = DateTime.Now;
                            db.SaveChanges();
                            transaction.Commit();

                        }

                        else
                        {

                            transaction.Rollback();
                            Logger.TraceErrorFormat("Error while Saving {0}", response.ValidationResult.Errors);
                        }

                        return response;
                    }

                    catch (Exception ex)
                    {
                        Logger.TraceError("Exception: ", ex);
                        transaction.Rollback();
                        throw;
                    }

                    finally
                    {
                        Logger.TraceMethodEnd(ClassName, "DeleteResponsibleConfirmed");
                    }

                }
            }
        }

        public BusinessObjects.ProductOrganisation EditOrganization(int Id)
        {
            Logger.TraceMethodStart(ClassName, "EditOrganization");

            var prodorg = default(BusinessObjects.ProductOrganisation);

            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {

                    var query = db.ProductOrganisations.Where(c => c.ID == Id && c.Active==true).FirstOrDefault();
                    prodorg = MapperAllLevels.Map<EntityModel.ProductOrganisation, BusinessObjects.ProductOrganisation>(query);
                    return prodorg;

                }
            }

            catch (Exception ex)
            {
                Logger.TraceError("Exception: ", ex);
                throw;
            }

            finally
            {
                Logger.TraceMethodEnd(ClassName, "EditOrganization");
            }
        }

        public BugTrackingResponse<BusinessObjects.ProductOrganisation> SaveEditOrganization(BusinessObjects.ProductOrganisation prodorg)
        {
            Logger.TraceMethodStart(ClassName, "SaveEditOrganization");

            var response = new BugTrackingResponse<BusinessObjects.ProductOrganisation>();

            using (var db = new EntityModel.BugTrackingEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {

                        var validator = new ProdOrganizationUpdateSpecification();
                        var result = validator.Validate(prodorg);
                        var failures = result.Errors;

                        response.ValidationResult = result;

                        if (result.IsValid)
                        {
                            prodorg.LastModified = DateTime.Now;
                            var newrole = MapperAllLevels.Map<BusinessObjects.ProductOrganisation, EntityModel.ProductOrganisation>(prodorg);
                            db.Entry(newrole).State = EntityState.Modified;
                            db.SaveChanges();
                            transaction.Commit();
                        }

                        else
                        {

                            transaction.Rollback();
                            Logger.TraceErrorFormat("Error while Saving {0}", response.ValidationResult.Errors);
                        }

                        return response;


                    }

                    catch (Exception ex)
                    {
                        Logger.TraceError("Exception: ", ex);
                        transaction.Rollback();
                        throw;
                    }

                    finally
                    {
                        Logger.TraceMethodEnd(ClassName, "SaveEditOrganization");
                    }

                }
            }
        }

        public BusinessObjects.ProductOrganisation DeleteOrganization(int Id)
        {
            Logger.TraceMethodStart(ClassName, "DeleteOrganization");
            var org = default(BusinessObjects.ProductOrganisation);


            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {
                    var query = db.ProductOrganisations.Where(c => c.ID == Id && c.Active==true).FirstOrDefault();
                    org = MapperAllLevels.Map<EntityModel.ProductOrganisation, BusinessObjects.ProductOrganisation>(query);
                    return org;
                }
            }

            catch (Exception ex)
            {
                Logger.TraceError("Exception: ", ex);
                throw;
            }

            finally
            {
                Logger.TraceMethodEnd(ClassName, "DeleteOrganization");
            }



        }

        public BugTrackingResponse<BusinessObjects.ProductOrganisation> DeleteOrganizationConfirmed(int Id)
        {
            Logger.TraceMethodStart(ClassName, "DeleteOrganizationConfirmed");

            var response = new BugTrackingResponse<BusinessObjects.ProductOrganisation>();


            using (var db = new EntityModel.BugTrackingEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {
                        var query = db.ProductOrganisations.Where(c => c.ID == Id && c.Active==true).FirstOrDefault();
                        var prodorg = MapperAllLevels.Map<EntityModel.ProductOrganisation, BusinessObjects.ProductOrganisation>(query);

                        var validator = new ProdOrganizationDeleteSpecification();
                        var result = validator.Validate(prodorg);
                        var failures = result.Errors;

                        response.ValidationResult = result;

                        if (result.IsValid)
                        {

                            //db.ProductOrganisations.Remove(query);
                            query.Active = false;
                            query.LastModified = DateTime.Now;
                            db.SaveChanges();
                            transaction.Commit();
                        }

                        else
                        {

                            transaction.Rollback();
                            Logger.TraceErrorFormat("Error while Saving {0}", response.ValidationResult.Errors);
                        }

                        return response;

                    }

                    catch (Exception ex)
                    {
                        Logger.TraceError("Exception: ", ex);
                        transaction.Rollback();
                        throw;
                    }

                    finally
                    {
                        Logger.TraceMethodEnd(ClassName, "DeleteOrganizationConfirmed");
                    }

                }
            }
        }

       
    }
}
