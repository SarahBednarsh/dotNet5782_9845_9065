using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Photo { get; set; }
        public string Email { get; set; }
        public bool IsManager { get; set; }
        public int Salt { get; set; }
        public string HashedPassword { get; set; }
        public override string ToString()
        {
            return string.Format("Id: {0}, User name: {1}, Email address: {2}", Id, UserName, Email);
        }
    }
}
