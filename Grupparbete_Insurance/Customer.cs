using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupparbete_Insurance
{
    class Customer
    {
        
        public int Id { get; set; }
        public int age { get; set; }
        public string job { get; set; }
        public string marital { get; set; }
        public string education { get; set; }
        public int credit { get; set; }
        public int balance { get; set; }
        public int homeInsurance { get; set; }
        public int carLoan { get; set; }
        public string communication { get; set; }
        public int lastContactDay { get; set; }
        public string lastContactMonth { get; set; }
        public int noOfContacts { get; set; }
        public int daysPassed { get; set; }
        public int prevAttemts { get; set; }
        public string outcome { get; set; }
        public TimeSpan callStart { get; set; }
        public TimeSpan callEnd { get; set; }
        public int carInsurance { get; set; }

        

        public Customer(
            int id, 
            int age, 
            string job, 
            string marital, 
            string education, 
            int credit, 
            int balance, 
            int homeInsurance, 
            int carLoan, 
            string communication, 
            int lastContactDay, 
            string lastContactMonth, 
            int noOfContacts, 
            int daysPassed, 
            int prevAttemts, 
            string outcome, 
            TimeSpan callStart, 
            TimeSpan callEnd, 
            int carInsurance
            )
        {
            Id = id;
            this.age = age;
            this.job = job;
            this.marital = marital;
            this.education = education;
            this.credit = credit;
            this.balance = balance;
            this.homeInsurance = homeInsurance;
            this.carLoan = carLoan;
            this.communication = communication;
            this.lastContactDay = lastContactDay;
            this.lastContactMonth = lastContactMonth;
            this.noOfContacts = noOfContacts;
            this.daysPassed = daysPassed;
            this.prevAttemts = prevAttemts;
            this.outcome = outcome;
            this.callStart = callStart;
            this.callEnd = callEnd;
            this.carInsurance = carInsurance;
        }
    }
}
