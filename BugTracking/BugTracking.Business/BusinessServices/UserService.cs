using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using BusinessObjects = BugTracking.Models;
using EntityModel = DAL.EntityModel;
using BugTracking.Business.Validation.Specifications;
using Newtonsoft.Json;

namespace BugTracking.Business
{
    public class UserService : BusinessServiceBase, IUserService
    {
        private static readonly string ClassName = typeof(UserService).FullName;

        public BusinessObjects.User DeleteUser(int Id)
        {
            Logger.TraceMethodStart(ClassName, "DeleteUser");
            var user = default(BusinessObjects.User);

            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {


                    var query = db.Users.Where(c => c.ID == Id && c.Active==true).FirstOrDefault();
                    user = MapperAllLevels.Map<EntityModel.User, BusinessObjects.User>(query);
                    return user;
                }
            }

            catch (Exception ex)
            {
                Logger.TraceError("Exception: ", ex);
                throw;
            }

            finally
            {
                Logger.TraceMethodEnd(ClassName, "DeleteUser");
            }

        }

        public void DeleteUserConfirmed(int Id)
        {
            Logger.TraceMethodStart(ClassName, "DeleteUserConfirmed");



            using (var db = new EntityModel.BugTrackingEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {

                        var query = db.Users.Where(c => c.ID == Id && c.Active == true)
                        .Include(x => x.AspNetUser)
                        .FirstOrDefault();

                        var user = MapperAllLevels.Map<EntityModel.User, BusinessObjects.User>(query);
                        //db.Users.Remove(query);
                        query.Active = false;
                        query.LastModified = DateTime.Now;
                        query.AspNetUser.Active = false;
                       
                        db.SaveChanges();

                        transaction.Commit();
                    }

                    catch (Exception ex)
                    {
                        Logger.TraceError("Exception: ", ex);
                        transaction.Rollback();
                    }

                    finally
                    {
                        Logger.TraceMethodEnd(ClassName, "DeleteUserConfirmed");
                    }

                }
            }
        }

       

        public List<BusinessObjects.User> DropDownUser()
        {
            Logger.TraceMethodStart(ClassName);


            var dropdown = default(List<BusinessObjects.User>);


            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {

                    var Userquery = db.Users.Select(c => c).Where(d=>d.Active).ToList();
                    dropdown = MapperAllLevels.Map<List<EntityModel.User>, List<BusinessObjects.User>>(Userquery);
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
                Logger.TraceMethodEnd(ClassName, "DropDownUser");
            }

        }

        public BusinessObjects.User EditUser(int Id)
        {
            Logger.TraceMethodStart(ClassName, "EditUser");

            var user = default(BusinessObjects.User);

            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {

                    var query = db.Users.Where(c => c.ID == Id && c.Active).FirstOrDefault();
                    user = MapperAllLevels.Map<EntityModel.User, BusinessObjects.User>(query);
                    return user;

                }
            }

            catch (Exception ex)
            {
                Logger.TraceError("Exception: ", ex);
                throw;
            }

            finally
            {
                Logger.TraceMethodEnd(ClassName, "EditUser");
            }
        }


        public List<BusinessObjects.AspNetUser> GetRoleList()
        {

            Logger.TraceMethodStart(ClassName, "GetRoleList");
            var roles = default(List<BusinessObjects.AspNetUser>);

            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {
                    
                    var query = db.AspNetUsers.Where(c=>c.Active==true)
                          .Include(x => x.Users)
                          .Include(x=>x.AspNetUserRoles)
                              
                        .ToList();
                        
                   

                    /*var query=from c in db.AspNetUsers
                    join cr in db.Users on c.Id equals cr.UserId
                    join r in db.AspNetUserRoles on c.Id equals r.UserId
                    join k in db.AspNetRoles on r.RoleId equals k.Id
                    where c.Active == true
                    orderby c.UserName
                    select new { FirstName = cr.FirstName, LastName = cr.LastName, Name = k.Name, Description = k.Description };

                    var q = query.ToList();*/

                    roles = MapperAllLevels.Map<List<EntityModel.AspNetUser>, List<BusinessObjects.AspNetUser>>(query);

                    string json = JsonConvert.SerializeObject(roles, Formatting.Indented,
                    new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });

                    Logger.LogDebug(json);
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

                Logger.TraceMethodEnd(ClassName, "GetRoleList");

            }


        }

        public List<BusinessObjects.User> GetUserList()
        {
            Logger.TraceMethodStart(ClassName, "GetUserList");
            var users = default(List<BusinessObjects.User>);

            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {

                    var query = db.Users.Where(c=>c.Active)
                        .ToList();

                    users = MapperAllLevels.Map<List<EntityModel.User>, List<BusinessObjects.User>>(query);
                    return users;

                }

            }
            catch (Exception ex)
            {
                Logger.TraceError("Exception:", ex);
                throw;
            }

            finally
            {

                Logger.TraceMethodEnd(ClassName, "GetUserList");

            }

        }



        public List<BusinessObjects.User> GetInactiveUserList()
        {
            Logger.TraceMethodStart(ClassName, "GetInactiveUserList");
            var users = default(List<BusinessObjects.User>);

            try
            {
                using (var db = new EntityModel.BugTrackingEntities())
                {

                    var query = db.Users.Where(c => c.Active==false)
                        .ToList();

                    users = MapperAllLevels.Map<List<EntityModel.User>, List<BusinessObjects.User>>(query);
                    return users;

                }

            }
            catch (Exception ex)
            {
                Logger.TraceError("Exception:", ex);
                throw;
            }

            finally
            {

                Logger.TraceMethodEnd(ClassName, "GetInactiveUserList");

            }

        }


       

        public void SaveEditUser(BusinessObjects.User User)
        {
            Logger.TraceMethodStart(ClassName, "SaveEditUser");



            using (var db = new EntityModel.BugTrackingEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {
                        User.LastModified = DateTime.Now;
                        var resp = MapperAllLevels.Map<BusinessObjects.User, EntityModel.User>(User);
                        db.Entry(resp).State = EntityState.Modified;
                        db.SaveChanges();
                        transaction.Commit();

                    }

                    catch (Exception ex)
                    {
                        Logger.TraceError("Exception: ", ex);
                        transaction.Rollback();
                    }

                    finally
                    {
                        Logger.TraceMethodEnd(ClassName, "SaveEditUser");
                    }

                }
            }
        }

        public void SaveUserRole(BusinessObjects.AspNetUserRole UserRole)
        {
            Logger.TraceMethodStart(ClassName, "Save");



            using (var db = new EntityModel.BugTrackingEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {
                        UserRole.LastModified = DateTime.Now;
                        UserRole.Active = true;
                        var newUserRole = MapperAllLevels.Map<BusinessObjects.AspNetUserRole, EntityModel.AspNetUserRole>(UserRole);
                        db.AspNetUserRoles.Add(newUserRole);
                        db.SaveChanges();
                        transaction.Commit();
                    }

                    catch (Exception ex)
                    {
                        Logger.TraceError("Exception: ", ex);
                        transaction.Rollback();
                    }

                    finally
                    {
                        Logger.TraceMethodEnd(ClassName, "Save");
                    }

                }
            }
        }



        public List<BusinessObjects.AspNetUser> SelectAll()
        {
            Logger.TraceMethodStart(ClassName);

            var boList = default(List<BusinessObjects.AspNetUser>);

            try
            {

                using (var db = new EntityModel.BugTrackingEntities())
                {
                    var query = db.AspNetUsers.Where(x=>x.Active==true).ToList();
                    boList = MapperAllLevels.Map<List<EntityModel.AspNetUser>, List<BusinessObjects.AspNetUser>>(query);
                    return boList;
                }


            }
            catch (Exception ex)
            {
                Logger.TraceError("Exception: ", ex);
                throw;
            }

            finally
            {
                Logger.TraceMethodEnd(ClassName);
            }
        }


        public BugTrackingResponse<BusinessObjects.RegisterViewModel> SaveUser(BusinessObjects.RegisterViewModel Register)
        {
            Logger.TraceMethodStart(ClassName, "SaveUser");
            var response = new BugTrackingResponse<BusinessObjects.RegisterViewModel>();


            using (var db = new EntityModel.BugTrackingEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {
                        var validator = new RegisterSpecification();
                        var result = validator.Validate(Register);
                        var failures = result.Errors;

                        response.ValidationResult = result;

                        if (result.IsValid)
                        {

                            var usernamedata = db.AspNetUsers.Where(c => c.UserName == Register.Email && c.Active==true).SingleOrDefault();
                            var user = MapperAllLevels.Map<BusinessObjects.RegisterViewModel, EntityModel.User>(Register);
                            

                            user.Active = true;
                            user.LastModified = DateTime.Now;
                            user.UserId = usernamedata.Id;
                            user.Email = usernamedata.Email;
                            user.Username = usernamedata.UserName;
                            user.FirstName = Register.FirstName;
                            user.LastName = Register.LastName;
                            user.OrganizationID = Register.OrganizationID;
                            user.TelephoneNumber = Register.TelephoneNumber;

                            var userrole = MapperAllLevels.Map<BusinessObjects.RegisterViewModel, EntityModel.AspNetUserRole>(Register);
                            userrole.RoleId = Register.RoleId;
                            userrole.UserId = usernamedata.Id;
                            userrole.Active = true;
                            userrole.LastModified = DateTime.Now;

                            db.AspNetUserRoles.Add(userrole);
                            db.Users.Add(user);

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
                        Logger.TraceMethodEnd(ClassName, "SaveAddInfo");
                    }

                }
            }
        }


        public void ActivateExistingUser(int Id)
        {
            Logger.TraceMethodStart(ClassName, "ActivateUser");



            using (var db = new EntityModel.BugTrackingEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {

                        var query = db.Users.Where(c => c.ID == Id && c.Active==false)
                            .Include(x=>x.AspNetUser)
                            .SingleOrDefault()
                            ;

                        query.Active = true;
                        query.AspNetUser.Active = true;

                       // var user = MapperAllLevels.Map<EntityModel.User, BusinessObjects.User>(query);

                        //db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                        transaction.Commit();

                    }

                    catch (Exception ex)
                    {
                        Logger.TraceError("Exception: ", ex);
                        transaction.Rollback();
                    }

                    finally
                    {
                        Logger.TraceMethodEnd(ClassName, "SaveAddInfo");
                    }

                }
            }
        }
    }
}
