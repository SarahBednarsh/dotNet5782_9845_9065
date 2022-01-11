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
using System.Runtime.CompilerServices;
using System.IO;

namespace BL
{
    internal partial class BL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddUser(int id, string userName, string photo, string email, string password, bool isManager)
        {
            string photoPath = "tried";
            try
            {         
                //photoPath = Directory.GetCurrentDirectory() + @"\CustomerPhotos\" + id + @".jpg";
                photoPath = @"CustomerPhotos\" + id + @".jpg";

                (File.Create(photoPath)).Close();
                //    (File.Create(photoPath)).Close();

                System.IO.File.Copy(photo, photoPath.Remove(1), true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Console.WriteLine($"success maybe! path is: {photoPath}");
            try
            {
                MailMessage message = new MailMessage();
                message.To.Add(email);
                lock (dalAP)
                {
                    dalAP.AddUser(id, userName, photo, email, password, isManager);
                }
            }
            catch (UserException exception)
            {
                throw new KeyAlreadyExists("There is already a user with the same ID or username", exception);
            }
            catch (Exception ex)
            {
                throw new SendMailException($"email didn't work: {ex.Message}");
            }
            try
            {
                SendMail(@"Signup for .DRONE company", $"Hey {userName}! Thank you for signing up to our project! Hope you enjoy.", email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RecoverPassword(string userName, int lengthForNewPassword)
        {
            try
            {
                lock (dalAP)
                {
                    DO.User user = dalAP.SearchUser(userName);
                    string newPassword = CreatePassword(lengthForNewPassword);
                    dalAP.DeleteUser(user.Id);
                    dalAP.AddUser(user.Id, user.UserName, user.Photo, user.Email, newPassword, user.IsManager);
                    SendMail(@"Password recovery for .DRONE company", $"Hey {userName}! Your new password is {newPassword}. Hope you enjoy.", user.Email);
                }
            }
            catch (UserException exception)
            { throw new KeyDoesNotExist($"No user with user name {userName} was found", exception); }
        }
        private void SendMail(string subject, string body, string email)
        {
            MailMessage message = new MailMessage();

            string to = email;
            string from = "dotnetliorahandsarah@gmail.com";
            string pass = "dotnet5782";
            try
            {
                message.To.Add(to);
            }
            catch (Exception ex)
            {
                throw new SendMailException($"email didn't work: {ex.Message}");
            }
            message.To.Add("sarahbednarsh@gmail.com");
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
            catch (SmtpException ex)
            {
                throw new Exception($"email didn't work-smtp: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"email didn't work: {ex.Message}");
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
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteUser(int id)
        {
            try
            {
                lock (dalAP)
                {
                    dalAP.DeleteUser(id);
                }
            }
            catch (UserException exception)
            {
                throw new KeyDoesNotExist("There is no user with the specified ID", exception);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool UserInfoCorrect(string userName, string password, bool isManager)
        {
            lock (dalAP)
            {
                return dalAP.UserInfoCorrect(userName, password, isManager);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public User SearchUser(string userName)
        {
            try
            {
                lock (dalAP)
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
            }
            catch (UserException excpetion)
            {
                throw new KeyDoesNotExist("Cannot find requested user", excpetion);
            }
        }
    }
}
