using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DO;
using BO;
using User = BO.User;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Web;
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
            try
            {
                SendMail(@"Signup for .DRONE company", $"Hey {userName}! Thank you for signing up to our project! Hope you enjoy.", email);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void RecoverPassword(string userName, int lengthForNewPassword)
        {
            try
            { 
                DO.User user = dalAP.SearchUser(userName);
                string newPassword = CreatePassword(lengthForNewPassword);
                dalAP.DeleteUser(user.Id);
                dalAP.AddUser(user.Id, user.UserName, user.Photo, user.Email, newPassword, user.IsManager);
                SendMail(@"Password recovery for .DRONE company", $"Hey {userName}! Your new password is {newPassword}. Hope you enjoy.", user.Email);

            }
            catch(UserException exception)
            { throw new KeyDoesNotExist($"No user with user name {userName} was found", exception); }
        }
        private void SendMail(string subject, string body, string email)
        {
            MailMessage message = new MailMessage();

            string to = email;
            string from = "dotnetliorahandsarah@gmail.com";
            string pass = "dotnet5782";

            message.To.Add(to);
            //message.To.Add("sarahbednarsh@gmail.com");
            message.From = new MailAddress(from);
            message.Body = body;
            message.Subject = subject;
            message.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, pass);
            try
            {
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                throw new Exception($"email ddn't work: {ex.Message}");
            }
        }
        private string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        //private void SendMailForSignup(string userName, string email)
        //{
        //    MailMessage message = new MailMessage();
           
        //    string to = email;
        //    string from = "dotnetliorahandsarah@gmail.com";
        //    string pass = "dotnet5782";
        //    string subject = $"Hey {userName}! Thank you for signing up to our project! Hope you enjoy.";
        //    string body = @"Signup for .DRONE company";

        //    message.To.Add(to);
        //    //message.To.Add("sarahbednarsh@gmail.com");
        //    message.From = new MailAddress(from);
        //    message.Body = body;
        //    message.Subject = subject;
        //    message.IsBodyHtml = true;

        //    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
        //    smtp.EnableSsl = true;
        //    smtp.Port = 587;
        //    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    smtp.Credentials = new NetworkCredential(from, pass);
        //    try
        //    {
        //        smtp.Send(message);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"email ddn't work: {ex.Message}");
        //    }
        //}
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
        public bool UserInfoCorrect(string userName, string password, bool isManager)
        {
            return dalAP.UserInfoCorrect(userName, password, isManager);
        }
        public User SearchUser(string userName)
        {
            try
            {
                DO.User user = dalAP.SearchUser(userName);
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
