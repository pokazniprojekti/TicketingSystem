using System.Collections.Generic;
using BugTracking.Models;

namespace BugTracking.Business
{
    public interface IUserService
    {
        List<AspNetUser> SelectAll();
        List<User> DropDownUser();
        void SaveUserRole(AspNetUserRole UserRole);
        List<User> GetUserList();
        List<AspNetUser> GetRoleList();
        User EditUser(int Id);
        User DeleteUser(int Id);
        void DeleteUserConfirmed(int Id);
        List<User> GetInactiveUserList();
        void ActivateExistingUser(int Id);


    }
}
