using System;
using System.Collections.Generic;
using System.Text;

namespace To_Do_CLI.Model
{
    public struct User
    {
        public string Username {  get; }
        public string PasswordHash { get; }

        public User(string username, string password)
        {
            Username = username;
            PasswordHash = password;
        }

        public bool CheckPassword(string password)
        {
            return PasswordHash == password;
        }
        public override string ToString()
        {
            return $"User: {Username}";
        }
    }
}
