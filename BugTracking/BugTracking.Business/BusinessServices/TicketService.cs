using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using BugTracking.Business.Interfaces;
using BugTracking.Business.Validation.Specifications;
using BugTracking.Models;
using BusinessObjects = BugTracking.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace BugTracking.Business.BusinessServices
{
    public class TicketService : BusinessServiceBase, ITicketService
    {
        private static readonly string ClassName = typeof(TicketService).FullName;
        public BugTrackingResponse<Ticket> Save(Ticket ticket)
        {
            Logger.TraceMethodStart(ClassName, "SaveTicket");

            var response = new BugTrackingResponse<BusinessObjects.Ticket>();

            using (var db = new DAL.EntityModel.BugTrackingEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {
                        var UserID = System.Web.HttpContext.Current.User.Identity.GetUserId();
                        ticket.UserID = UserID;
                        var validator = new TicketSaveSpecificaion();
                        var result = validator.Validate(ticket);
                        var failures = result.Errors;

                        response.ValidationResult = result;

                        if (result.IsValid)
                        {
                            ticket.Active = true;
                            ticket.DateCreated = DateTime.Now;
                            ticket.LastModified = DateTime.Now;

                            var _ticket = MapperAllLevels.Map<BusinessObjects.Ticket, DAL.EntityModel.Ticket>(ticket);
                            db.Tickets.Add(_ticket);
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
                        Logger.TraceMethodEnd(ClassName, "SaveTicket");
                    }

                }
            }
        }
        public List<BusinessObjects.Ticket> GetTicketList()
        {
            Logger.TraceMethodStart(ClassName, "GetTicketList");

            var ticket = default(List<BusinessObjects.Ticket>);


            try
            {
                using (var db = new DAL.EntityModel.BugTrackingEntities())
                {

                    var query = db.Tickets.Where(c => c.Active == true)
                         .Include(x=>x.Status)
                         .Include(x=>x.Priority)
                         .Include(x=>x.Category)
                         .Include(x=>x.User)
                         .Include(x => x.Product)
                         .Include(x => x.Organization).ToList();
                    ;

                    ticket = MapperAllLevels.Map<List<DAL.EntityModel.Ticket>, List<BusinessObjects.Ticket>>(query);
                    return ticket;
                }
            }
            catch (Exception ex)
            {
                Logger.TraceError("Exception:", ex);
                throw;
            }

            finally
            {
                Logger.TraceMethodEnd(ClassName, "GetTicketList");
            }
        }


        public List<BusinessObjects.Ticket> GetFilteredTicketList(BusinessObjects.Ticket filter)
        {
            Logger.TraceMethodStart(ClassName, "GetFilteredTicketList");

            var ticket = default(List<BusinessObjects.Ticket>);


            try
            {
                using (var db = new DAL.EntityModel.BugTrackingEntities())
                {
                    var query = db.Tickets.Where(c => c.Active == true);
                    if (filter.ProductID.ToString() != "0") query = query.Where(c => c.ProductID == filter.ProductID);
                    if (filter.CategoryID.ToString() != "0") query = query.Where(c => c.CategoryID == filter.CategoryID);
                    if (filter.StatusID.ToString() != "0") query = query.Where(c => c.StatusID == filter.StatusID);
                    if (filter.OrganizationID.ToString() != "0") query = query.Where(c => c.OrganizationID == filter.OrganizationID);
                    if (filter.PriorityID.ToString() != "0") query = query.Where(c => c.PriorityID == filter.PriorityID);
                    query = query.Include(x => x.Status)
                         .Include(x => x.Priority)
                         .Include(x => x.Category)
                         .Include(x => x.User)
                         .Include(x => x.Product)
                         .Include(x => x.Organization);
                    ;
                    ;

                    ticket = MapperAllLevels.Map<List<DAL.EntityModel.Ticket>, List<BusinessObjects.Ticket>>(query.ToList());
                    return ticket;
                }
            }
            catch (Exception ex)
            {
                Logger.TraceError("Exception:", ex);
                throw;
            }

            finally
            {
                Logger.TraceMethodEnd(ClassName, "GetFilteredTicketList");
            }
        }

        public List<BusinessObjects.Ticket> GetFilteredTechnicianTicketList(BusinessObjects.Ticket filter)
        {
            Logger.TraceMethodStart(ClassName, "GetFilteredTechnicianTicketList");

            var ticket = default(List<BusinessObjects.Ticket>);


            try
            {
                using (var db = new DAL.EntityModel.BugTrackingEntities())
                {
                    var UserID = System.Web.HttpContext.Current.User.Identity.GetUserId();
                    var Assigned = db.Users.Where(c => c.UserId == UserID).Select(c => c.ID).First();
                    var query = db.Tickets.Where(c => c.Active == true && c.AssigneeID == Assigned);
                    if (filter.ProductID.ToString() != "0") query = query.Where(c => c.ProductID == filter.ProductID);
                    if (filter.CategoryID.ToString() != "0") query = query.Where(c => c.CategoryID == filter.CategoryID);
                    if (filter.StatusID.ToString() != "0") query = query.Where(c => c.StatusID == filter.StatusID);
                    if (filter.OrganizationID.ToString() != "0") query = query.Where(c => c.OrganizationID == filter.OrganizationID);
                    if (filter.PriorityID.ToString() != "0") query = query.Where(c => c.PriorityID == filter.PriorityID);
                    query = query.Include(x => x.Status)
                         .Include(x => x.Priority)
                         .Include(x => x.Category)
                         .Include(x => x.User)
                         .Include(x => x.Product)
                         .Include(x => x.Organization);
                    ;
                    ;

                    ticket = MapperAllLevels.Map<List<DAL.EntityModel.Ticket>, List<BusinessObjects.Ticket>>(query.ToList());
                    return ticket;
                }
            }
            catch (Exception ex)
            {
                Logger.TraceError("Exception:", ex);
                throw;
            }

            finally
            {
                Logger.TraceMethodEnd(ClassName, "GetFilteredTechnicianTicketList");
            }
        }

        public List<BusinessObjects.Ticket> GetFilteredRegularUserTicketList(BusinessObjects.Ticket filter)
        {
            Logger.TraceMethodStart(ClassName, "GetFilteredRegularUserTicketList");

            var ticket = default(List<BusinessObjects.Ticket>);


            try
            {
                using (var db = new DAL.EntityModel.BugTrackingEntities())
                {
                    var UserID = System.Web.HttpContext.Current.User.Identity.GetUserId();
                    var query = db.Tickets.Where(c => c.Active == true && c.UserID == UserID);
                    if (filter.ProductID.ToString() != "0") query = query.Where(c => c.ProductID == filter.ProductID);
                    if (filter.CategoryID.ToString() != "0") query = query.Where(c => c.CategoryID == filter.CategoryID);
                    if (filter.StatusID.ToString() != "0") query = query.Where(c => c.StatusID == filter.StatusID);
                    if (filter.OrganizationID.ToString() != "0") query = query.Where(c => c.OrganizationID == filter.OrganizationID);
                    if (filter.PriorityID.ToString() != "0") query = query.Where(c => c.PriorityID == filter.PriorityID);
                    query = query.Include(x => x.Status)
                         .Include(x => x.Priority)
                         .Include(x => x.Category)
                         .Include(x => x.User)
                         .Include(x => x.Product)
                         .Include(x => x.Organization);
                    ;
                    ;

                    ticket = MapperAllLevels.Map<List<DAL.EntityModel.Ticket>, List<BusinessObjects.Ticket>>(query.ToList());
                    return ticket;
                }
            }
            catch (Exception ex)
            {
                Logger.TraceError("Exception:", ex);
                throw;
            }

            finally
            {
                Logger.TraceMethodEnd(ClassName, "GetFilteredRegularUserTicketList");
            }
        }
        public List<BusinessObjects.Ticket> GetTechnicianTicketList()
        {
            Logger.TraceMethodStart(ClassName, "GetTechnicianTicketList");

            var ticket = default(List<BusinessObjects.Ticket>);


            try
            {
                using (var db = new DAL.EntityModel.BugTrackingEntities())
                {
                    var UserID = System.Web.HttpContext.Current.User.Identity.GetUserId();
                    var Assigned = db.Users.Where(c => c.UserId == UserID).Select(c => c.ID).First();
                    var query = db.Tickets.Where(c => c.Active == true && c.AssigneeID==Assigned)
                         .Include(x => x.Status)
                         .Include(x => x.Priority)
                         .Include(x => x.Category)
                         .Include(x => x.User)
                         .Include(x => x.Product)
                         .Include(x => x.Organization)
                         .ToList();
                    ;

                    ticket = MapperAllLevels.Map<List<DAL.EntityModel.Ticket>, List<BusinessObjects.Ticket>>(query);
                    return ticket;
                }
            }
            catch (Exception ex)
            {
                Logger.TraceError("Exception:", ex);
                throw;
            }

            finally
            {
                Logger.TraceMethodEnd(ClassName, "GetTechnicianTicketList");
            }
        }

        public List<BusinessObjects.Ticket> GetRegularUserList()
        {
            Logger.TraceMethodStart(ClassName, "GetRegularUserList");

            var ticket = default(List<BusinessObjects.Ticket>);


            try
            {
                using (var db = new DAL.EntityModel.BugTrackingEntities())
                {
                    var UserID = System.Web.HttpContext.Current.User.Identity.GetUserId();
                    //var Assigned = db.Users.Where(c => c.UserId == UserID).Select(c => c.ID).First();
                    var query = db.Tickets.Where(c => c.Active == true && c.UserID == UserID)
                         .Include(x => x.Status)
                         .Include(x => x.Priority)
                         .Include(x => x.Category)
                         .Include(x => x.User)
                         .Include(x => x.Product)
                         .Include(x => x.Organization)
                         .ToList();
                    ;

                    ticket = MapperAllLevels.Map<List<DAL.EntityModel.Ticket>, List<BusinessObjects.Ticket>>(query);
                    return ticket;
                }
            }
            catch (Exception ex)
            {
                Logger.TraceError("Exception:", ex);
                throw;
            }

            finally
            {
                Logger.TraceMethodEnd(ClassName, "GetRegularUserList");
            }
        }

        public BusinessObjects.Ticket EditTicket(long Id)
        {
            Logger.TraceMethodStart(ClassName, "EditTicket");

            var ticket = default(BusinessObjects.Ticket);

            try
            {
                using (var db = new DAL.EntityModel.BugTrackingEntities())
                {

                    var query = db.Tickets.Where(c => c.ID == Id && c.Active == true).FirstOrDefault();
                    ticket = MapperAllLevels.Map<DAL.EntityModel.Ticket, BusinessObjects.Ticket>(query);
                    return ticket;

                }
            }

            catch (Exception ex)
            {
                Logger.TraceError("Exception ex", ex);
                throw;
            }

            finally
            {
                Logger.TraceMethodEnd(ClassName, "EditTicket");
            }
        }

        public BugTrackingResponse<BusinessObjects.Ticket> SaveEdit(BusinessObjects.Ticket ticket)
        {
            Logger.TraceMethodStart(ClassName, "SaveEdit");

            var response = new BugTrackingResponse<BusinessObjects.Ticket>();

            using (var db = new DAL.EntityModel.BugTrackingEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {
                        //var UserID = System.Web.HttpContext.Current.User.Identity.GetUserId();
                        //ticket.UserID = UserID;
                        var validator = new TicketSaveSpecificaion();
                        var result = validator.Validate(ticket);
                        var failures = result.Errors;

                        response.ValidationResult = result;

                        if (result.IsValid)
                        {
                            ticket.LastModified = DateTime.Now;
                            var newticket = MapperAllLevels.Map<BusinessObjects.Ticket, DAL.EntityModel.Ticket>(ticket);
                            db.Entry(newticket).State = EntityState.Modified;
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

        public BusinessObjects.Ticket DeleteTicket(long Id)
        {
            Logger.TraceMethodStart(ClassName, "DeleteTicket");

            var ticket = default(BusinessObjects.Ticket);

            try
            {
                using (var db = new DAL.EntityModel.BugTrackingEntities())
                {

                    var query = db.Tickets.Where(c => c.ID == Id && c.Active == true).FirstOrDefault();
                    ticket = MapperAllLevels.Map<DAL.EntityModel.Ticket, BusinessObjects.Ticket>(query);
                    return ticket;

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

        public BugTrackingResponse<BusinessObjects.Ticket> DeleteTicketConfirmed(long Id)
        {
            Logger.TraceMethodStart(ClassName, "DeleteTicket");

            var response = new BugTrackingResponse<BusinessObjects.Ticket>();

            using (var db = new DAL.EntityModel.BugTrackingEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {

                    try
                    {
                        var query = db.Tickets.Where(c => c.ID == Id && c.Active == true).FirstOrDefault();
                        var ticket = MapperAllLevels.Map<DAL.EntityModel.Ticket, BusinessObjects.Ticket>(query);

                        var validator = new TicketDeleteSpecification();
                        var result = validator.Validate(ticket);
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
                        Logger.TraceMethodEnd(ClassName, "DeleteTicketConfirmed");
                    }

                }
            }
        }
    }
}
