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
                SendMailForSignup(userName, email);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private void SendMailForSignup(string userName, string email)
        {
            MailMessage message = new MailMessage();
           
            string to = email;
            string from = "dotnetliorahandsarah@gmail.com";
            string pass = "dotnet5782";
            string subject = $"Hey {userName}! Thank you for signing up to our project! Hope you enjoy.";
            string body = @"Signup for .DRONE company";

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
                //DialogResult code = MessageBox.Show("Email Sent Successfully", "Email Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //if (code == DialogResult.OK)
                //{
                //    txtmail.Clear();
                //    txtsub.Clear();
                //    txtmess.Clear();
                //}

            }
            catch (Exception ex)
            {
                throw new Exception($"email ddn't work: {ex.Message}");
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
