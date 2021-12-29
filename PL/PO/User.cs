using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    public class User : DependencyObject
    {
        static readonly DependencyProperty IdProperty = DependencyProperty.Register("Id", typeof(int), typeof(User));
        static readonly DependencyProperty UserNameProperty = DependencyProperty.Register("UserName", typeof(string), typeof(User));
        static readonly DependencyProperty PhotoProperty = DependencyProperty.Register("Photo", typeof(string), typeof(User));
        static readonly DependencyProperty EmailProperty = DependencyProperty.Register("Email", typeof(string), typeof(User));
        static readonly DependencyProperty IsManagerProperty = DependencyProperty.Register("IsManager", typeof(bool), typeof(User));
        static readonly DependencyProperty SaltProperty = DependencyProperty.Register("Salt", typeof(int), typeof(User));
        static readonly DependencyProperty HashedPasswordProperty = DependencyProperty.Register("HashedPassword", typeof(string), typeof(User));
        
        public int Id { get => (int)GetValue(IdProperty); set => SetValue(IdProperty, value); }
        public string UserName { get => (string)GetValue(UserNameProperty); set => SetValue(UserNameProperty, value); }
        public string Photo { get => (string)GetValue(PhotoProperty); set => SetValue(PhotoProperty, value); }
        public string Email { get => (string)GetValue(EmailProperty); set => SetValue(EmailProperty, value); }
        public bool IsManager { get => (bool)GetValue(IsManagerProperty); set => SetValue(IsManagerProperty, value); }
        public int Salt { get => (int)GetValue(SaltProperty); set => SetValue(SaltProperty, value); }
        public string HashedPassword { get => (string)GetValue(HashedPasswordProperty); set => SetValue(HashedPasswordProperty, value); }
        public override string ToString()
        {
            return string.Format("Id: {0}, User name: {1}, Email address: {2}", Id, UserName, Email);
        }
    }
}
