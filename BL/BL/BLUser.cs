using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DO;
using BO;
using User = BO.User;

namespace BL
{
    internal partial class BL
    {
        public void AddUser(int id, string userName, string photo, string email, string password, bool isManager)
        {
            try
            {
                dalAP.AddUser(id, userName, photo, email, password, isManager);
            }
            catch (UserException exception)
            {
                throw new KeyAlreadyExists("There is alreday a user with the same ID or username", exception);
            }
        }
        public void DeleteUser(int id)
        {
            try
            {
                dalAP.DeleteUser(id);
            }
            catch (UserException exception)
            {
                throw new KeyDoesNotExist("There is no user with the specified ID", exception);
            }
        }
        public User SearchUser(string userName, string password, bool isManager)
        {
            try
            {
                DO.User user = dalAP.SearchUser(userName, password, isManager);
                return new User()
                {
                    Id = user.Id,
                    Email = user.Email,
                    HashedPassword = user.HashedPassword,
                    IsManager = user.IsManager,
                    Photo = user.Photo,
                    Salt = user.Salt,
                    UserName = user.UserName
                };
            }
            catch(UserException excpetion)
            {
                throw new KeyDoesNotExist("Cannot find requested user", excpetion);
            }
        }
    }
}
