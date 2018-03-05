using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BugTracking.Business.Validation.Specifications;
using BusinessObjects = BugTracking.Models;
using EntityModel = DAL.EntityModel;

namespace BugTracking.Business
{
    public class RoleService : BusinessServiceBase, IRoleService
    {
        private static readonly string ClassName = typeof(RoleService).FullName;
        public BugTrackingResponse<BusinessObjects.AspNetRole> Save(BusinessObjects.AspNetRole role)
        {
            Logger.TraceMethodStart(ClassName, "Save");

            var response = new BugTrackingResponse<BusinessObjects.AspNetRole>();


            using (var db = new EntityModel.BugTrackingEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {

                        var validator = new RoleSpecification();
                        var result = validator.Validate(role);
                        var failures = result.Errors;

                        response.ValidationResult = result;

                        if (result.IsValid)
                        {
                          
                            role.Id = Guid.NewGuid().ToString();
                            role.LastModified = DateTime.Now;
                            role.Active = true;
                            var newrole = MapperAllLevels.Map<BusinessObjects.AspNetRole, EntityModel.AspNetRole>(role);
                            db.AspNetRoles.Add(newrole);
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

        public List<BusinessObjects.AspNetRole> GetRoles()
        {
            Logger.TraceMethodStart(ClassName, "GetRoles");
            var roles = default(List<BusinessObjects.AspNetRole>);

            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {

                    var query = db.AspNetRoles.Where(c=>c.Active==true).ToList();
                    //Logger.ServiceDebug($"FILTER: [{JsonExtensions.ToJson(query)}].", ClassName);
                    roles = MapperAllLevels.Map<List<EntityModel.AspNetRole>, List<BusinessObjects.AspNetRole>>(query);
                    return roles;

                }

            }
            catch (Exception ex)
            {
                Logger.TraceError("Exception:", ex);
                throw;
            }

            finally
            {

                Logger.TraceMethodEnd(ClassName, "GetRoles");

            }

        }

        public BusinessObjects.AspNetRole EditRole(string Id)
        {
            Logger.TraceMethodStart(ClassName, "EditRole");

            var role = default(BusinessObjects.AspNetRole);

            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {

                    var query = db.AspNetRoles.Where(c => c.Id == Id && c.Active==true).FirstOrDefault();
                    role = MapperAllLevels.Map<DAL.EntityModel.AspNetRole, BusinessObjects.AspNetRole>(query);
                    return role;

                }
            }

            catch (Exception ex)
            {
                Logger.TraceError("Exception ex", ex);
                throw;
            }

            finally
            {
                Logger.TraceMethodEnd(ClassName, "EditRole");
            }
        }


        public BugTrackingResponse<BusinessObjects.AspNetRole> SaveEdit(BusinessObjects.AspNetRole role)
        {
            Logger.TraceMethodStart(ClassName, "SaveEdit");

            var response = new BugTrackingResponse<BusinessObjects.AspNetRole>();

            using (var db = new EntityModel.BugTrackingEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {

                        var validator = new RoleUpdateSpecification();
                        var result = validator.Validate(role);
                        var failures = result.Errors;

                        response.ValidationResult = result;

                        if (result.IsValid)
                        {
                            role.LastModified = DateTime.Now;
                            var newrole = MapperAllLevels.Map<BusinessObjects.AspNetRole, EntityModel.AspNetRole>(role);
                            db.Entry(newrole).State = EntityState.Modified;
                            newrole.LastModified = DateTime.Now;
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


        public BusinessObjects.AspNetRole DeleteRole(string Id)
        {
            Logger.TraceMethodStart(ClassName, "DeleteRole");

            var role = default(BusinessObjects.AspNetRole);


            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {

                    var query = db.AspNetRoles.Where(c => c.Id == Id && c.Active==true).FirstOrDefault();
                    role = MapperAllLevels.Map<EntityModel.AspNetRole, BusinessObjects.AspNetRole>(query);
                    return role;

                }

            }

            catch (Exception ex)
            {
                Logger.TraceError("Exception: ", ex);
                throw;
            }

            finally
            {
                Logger.TraceMethodEnd(ClassName, "DeleteRole");
            }
        }

        public BugTrackingResponse<BusinessObjects.AspNetRole> DeleteConfirmed(string Id)
        {
            Logger.TraceMethodStart(ClassName, "DeleteRole");

            var response = new BugTrackingResponse<BusinessObjects.AspNetRole>();

            using (var db = new EntityModel.BugTrackingEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {

                        var query = db.AspNetRoles.Where(c => c.Id == Id && c.Active == true).FirstOrDefault();
                        var role = MapperAllLevels.Map<EntityModel.AspNetRole, BusinessObjects.AspNetRole>(query);

                        var validator = new RoleDeleteSpecification();
                        var result = validator.Validate(role);
                        var failures = result.Errors;

                        response.ValidationResult = result;

                        if (result.IsValid)
                        {


                            //db.AspNetRoles.Remove(query);
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

        public List<BusinessObjects.AspNetRole> DropDownRole()
        {
            Logger.TraceMethodStart(ClassName);


            var dropdown = default(List<BusinessObjects.AspNetRole>);

            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {

                    var Rolequery = db.AspNetRoles.Where(c=>c.Active==true).ToList();
                    dropdown = MapperAllLevels.Map<List<EntityModel.AspNetRole>, List<BusinessObjects.AspNetRole>>(Rolequery);
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
                Logger.TraceMethodEnd(ClassName, "DropDownRole");
            }

        }


    }

}


