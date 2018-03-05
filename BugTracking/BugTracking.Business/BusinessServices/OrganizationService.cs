using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BugTracking.Business.Validation.Specifications;
using EntityModel = DAL.EntityModel;
using BusinessObjects = BugTracking.Models;
using FluentValidation;

namespace BugTracking.Business
{
    public class OrganizationService : BusinessServiceBase, IOrganizationService
    {
        private static readonly string ClassName = typeof(OrganizationService).FullName;
        public BugTrackingResponse<BusinessObjects.Organization> Save(BusinessObjects.Organization org)
        {
            Logger.TraceMethodStart(ClassName, "Save");

            var response = new BugTrackingResponse<BusinessObjects.Organization>();

            using (var db = new EntityModel.BugTrackingEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {

                        var validator = new OrganizationSpecification();
                        var result = validator.Validate(org);
                        var failures = result.Errors;

                        response.ValidationResult = result;

                        if (result.IsValid)
                        {
                            org.LastModified = DateTime.Now;
                            org.Active = true;
                            var neworg = MapperAllLevels.Map<BusinessObjects.Organization, EntityModel.Organization>(org);
                            db.Organizations.Add(neworg);
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


        public List<BusinessObjects.Organization> GetOrganizations()
        {

            Logger.TraceMethodStart(ClassName, "GetOrganizations");
            var organizations = default(List<BusinessObjects.Organization>);

            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {

                    var query = db.Organizations.Where(c=>c.Active).ToList();

                    organizations = MapperAllLevels.Map<List<EntityModel.Organization>, List<BusinessObjects.Organization>>(query);
                    return organizations;
                }

            }
            catch (Exception ex)
            {
                Logger.TraceError("Exception", ex);

                throw;
            }

            finally
            {
                Logger.TraceMethodEnd(ClassName, "GetOrganizations");
            }


        }

        public BusinessObjects.Organization EditOrganization(long Id)
        {
            Logger.TraceMethodStart(ClassName, "EditOrganization");
            var org = default(BusinessObjects.Organization);
           

            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {
                    var query = db.Organizations.Where(c => c.ID == Id && c.Active).FirstOrDefault();
                    org = MapperAllLevels.Map<EntityModel.Organization, BusinessObjects.Organization>(query);
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
                Logger.TraceMethodEnd(ClassName, "EditOrganization");
            }
        }




        public BugTrackingResponse<BusinessObjects.Organization> SaveEdit(BusinessObjects.Organization org)
        {
            Logger.TraceMethodStart(ClassName, "SaveEdit");

            var response = new BugTrackingResponse<BusinessObjects.Organization>();

            using (var db = new EntityModel.BugTrackingEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {

                        var validator = new OrganizationUpdateSpecification();
                        var result = validator.Validate(org);
                        var failures = result.Errors;

                        response.ValidationResult = result;

                        if (result.IsValid)
                        {

                            org.LastModified = DateTime.Now;
                            var newrole = MapperAllLevels.Map<BusinessObjects.Organization, EntityModel.Organization>(org);
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


        public BusinessObjects.Organization DeleteOrganization(long Id)
        {
            Logger.TraceMethodStart(ClassName, "DeleteOrganization");
            var org = default(BusinessObjects.Organization);

            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {

                    var query = db.Organizations.Where(c => c.ID == Id && c.Active).FirstOrDefault();
                    org = MapperAllLevels.Map<EntityModel.Organization, BusinessObjects.Organization>(query);
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

        public BugTrackingResponse<BusinessObjects.Organization> DeleteConfirmed(long Id)
        {
            Logger.TraceMethodStart(ClassName, "DeleteOrganization");

            var response = new BugTrackingResponse<BusinessObjects.Organization>();

            using (var db = new EntityModel.BugTrackingEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {
                        var query = db.Organizations.Where(c => c.ID == Id && c.Active).FirstOrDefault();
                        var orgdelete = MapperAllLevels.Map<EntityModel.Organization, BusinessObjects.Organization>(query);

                        var validator = new OrganizationDeleteSpecification();
                        var result = validator.Validate(orgdelete);
                        var failures = result.Errors;

                        response.ValidationResult = result;

                        if (result.IsValid)
                        {

                            query.Active = false;
                            query.LastModified = DateTime.Now;
                            //db.Organizations.Remove(query);
                            db.SaveChanges();
                            transaction.Commit();
                        }

                        else
                        {
                            transaction.Rollback();
                            Logger.TraceErrorFormat("Error while DeleteOrganization {0}", response.ValidationResult.Errors);
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
                        Logger.TraceMethodEnd(ClassName, "DeleteOrganization");
                    }

                }
            }
        }

        public List<BusinessObjects.Organization> DropDownOrganisation()
        {
            Logger.TraceMethodStart(ClassName, "DropDownOrganisation");


            var dropdown = default(List<BusinessObjects.Organization>);

            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {

                    var Organisationquery = db.Organizations.Where(c=>c.Active).ToList();
                    dropdown = MapperAllLevels.Map<List<EntityModel.Organization>, List<BusinessObjects.Organization>>(Organisationquery);
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
                Logger.TraceMethodEnd(ClassName, "DropDownOrganisation");
            }

        }


    }

}

