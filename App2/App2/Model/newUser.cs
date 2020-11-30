using System;
using System.Collections.Generic;
using System.Text;

namespace App2.Model
{
    class newUser
    {
        public string id { get; set; }
        public string fullname { get; set; }
        public string CC { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
        public string confirm_password { get; set; }

        public newUser()
        {

        }

        public newUser(string id, string fullname, string CC, string phone, string password, string confirm_password)
        {
            this.id = id;
            this.fullname = fullname;
            this.CC = CC;
            this.phone = phone;
            this.password = password;
            this.confirm_password = confirm_password;
        }
    }
}
