using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;
namespace Dal
{
    partial class DalObject
    {
        public void AddUser(int id, string userName, string photo, string email, string password, bool isManager)
        {
            if (DataSource.Users.Exists(x => x.Id == id || x.UserName == userName))
                throw new UserException("User with the same id or username already exists");
            //if (!File.Exists(photo))
               // photo = GetDefaultPhoto();
            int salt = PasswordHandler.GenerateSalt();
            User tempUser = new User()
            {
                Id = id,
                UserName = userName,
                Photo = photo,
                Email = email,
                Salt = salt,
                HashedPassword = PasswordHandler.GenerateNewPassword(password, salt),
                IsManager = isManager
            };
            DataSource.Users.Add(tempUser);
        }
        public void DeleteUser(int id)
        {
            if (!DataSource.Users.Exists(x => x.Id == id))
                throw new UserException("no such user");
            DataSource.Users.RemoveAll(x => x.Id == id);

        }
        public User SearchUser(string userName)
        {
            if (!DataSource.Users.Exists(x => x.UserName == userName))
                throw new UserException("Cannot find user");
            return DataSource.Users.Find(x => x.UserName == userName);
        }
        public bool UserInfoCorrect(string userName, string password, bool isManager)
        {
            return DataSource.Users.Exists(x => x.UserName == userName && x.IsManager == isManager && PasswordHandler.CheckPassword(password, x.HashedPassword, x.Salt));

        }
        public string GetDefaultPhoto()
        {
            return DataSource.Config.defaultPhoto;
        }
    }
}
