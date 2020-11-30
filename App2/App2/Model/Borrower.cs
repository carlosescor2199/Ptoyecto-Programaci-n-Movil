using System;
using System.Collections.Generic;
using System.Text;

namespace App2.Model
{
    public class Borrower
    {
        public int id { get; set; }
        public string fullname { get; set; }
        public string CC { get; set; }
        public string phone { get; set; }
        public float amount { get; set; }
        public float percentage { get; set; }
        public int months { get; set; }

        public Borrower()
        {

        }

        public Borrower(int id, string fullname, string CC, string phone, float amount, float percentage, int months)
        {
            this.id = id;
            this.fullname = fullname;
            this.CC = CC;
            this.phone = phone;
            this.amount = amount;
            this.percentage = percentage;
            this.months = months;

        }
    }
}
