using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace DO
{
    public static class PasswordHandler
    {
        private static Random saltGenerator = new Random();
        private static string hashPassword(string passwordWithSalt)
        {
            SHA512 shaM = new SHA512Managed();
            return Convert.ToBase64String(shaM.ComputeHash(Encoding.UTF8.GetBytes(passwordWithSalt)));
        }
        public static int GenerateSalt()
        {
            return saltGenerator.Next();
        }
        public static string GenerateNewPassword(string password, int salt)
        {
            return hashPassword(password + salt);
        }
        public static bool CheckPassword(string entered, string hashed, int salt)
        {
            return hashed == hashPassword(entered + salt);
        }
    }
}
