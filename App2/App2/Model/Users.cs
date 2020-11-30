using System;
using System.Collections.Generic;
using System.Text;

namespace App2.Model
{
    public class Users
    {
        public string id { get; set; }
        public string fullName { get; set; }
        public string CC { get; set; }
        public string phone { get; set; }
        public string password { get; set; }

        public Users()
        {

        }

        public Users(string id, string fullName, string CC, string phone, string password)
        {
            this.id = id;
            this.fullName = fullName;
            this.CC = CC;
            this.phone = phone;
            this.password = password;
        }
    }
}
