﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Forms.DataVisualization.Charting;

namespace Grupparbete_Insurance
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Connection med Sql server, databas Insurance.
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = "Data Source=DESKTOP-0KMEDJA\\SQL2017;Initial Catalog=Insurance;Integrated Security=True";

            List<Customer> customerList = new List<Customer>();

            try
            {
                conn.Open(); // öppna kopplingen

                //Vi vill komma åt tabellen Car_Insurance som innehåller vår data.
                SqlCommand myQuery = new SqlCommand("SELECT * FROM Car_Insurance;", connection: conn);

                SqlDataReader myReader = myQuery.ExecuteReader();



                //Vi inleder med att deklarera våra olika variabler som vi har att tillgå från SQL server
                int id;
                int age;
                string job;
                string marital;
                string education;
                int credit; // bool?    1 true 0 false
                int balance;
                int homeInsurance; // bool?     1 true 0 false
                int carLoan;  // bool?      1 true 0 false
                string communication;
                int lastContactDay;
                string lastContactMonth;
                int noOfContacts;
                int daysPassed;
                int prevAttemts;
                string outcome;
                TimeSpan callStart;
                TimeSpan callEnd;
                int carInsurance; // bool?   1 true 0 false


                while (myReader.Read())
                {

                    id = (int)myReader["id"];
                    age = (int)myReader["age"];
                    job = myReader["job"].ToString();
                    marital = myReader["marital"].ToString();
                    education = myReader["education"].ToString();
                    credit = (int)myReader["default"];
                    balance = (int)myReader["balance"];
                    homeInsurance = (int)myReader["hhinsurance"];
                    carLoan = (int)myReader["carloan"];
                    communication = myReader["communication"].ToString();
                    lastContactDay = (int)myReader["lastcontactday"];
                    lastContactMonth = myReader["lastcontactmonth"].ToString();
                    noOfContacts = (int)myReader["noofcontacts"];
                    daysPassed = (int)myReader["dayspassed"];
                    prevAttemts = (int)myReader["prevattempts"];
                    outcome = myReader["outcome"].ToString();
                    callStart = (TimeSpan)myReader["callstart"];
                    callEnd = (TimeSpan)myReader["callend"];
                    carInsurance = (int)myReader["carinsurance"];


                    Customer customer = new Customer(id, age, job, marital, education, credit, balance,
                        homeInsurance, carLoan, communication, lastContactDay, lastContactMonth, noOfContacts,
                        daysPassed, prevAttemts, outcome, callStart, callEnd, carInsurance);


                    customerList.Add(customer);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }



            /*var query = from r in customerList
                        group r by r.age - (r.age % 10) into r
                        select new
                        {
                            Range = r.Key,
                            Count = r.Count()
                        }; */
            /*
foreach(var x in Query)

{

}

// var groupedCustomerList = customerList.GroupBy(y => y.age).Select(age => age.ToList()).ToList();

// denna grupperar samt sorterar alla åldrar
var hejMomina = from Customer in customerList
                group Customer by Customer.age into newGroup
                orderby newGroup.Key
                select newGroup;

/*
 Foreach (var nameGroup in hejMomina)
{

    foreach (var Customer in nameGroup)
    {
        chart1.Series["Series1"].Points.AddXY(Customer.age, Customer.balance);
    }
}
*/



            //   chart1.DataManipulator.GroupByAxisLabel("Antal", "age");
            for (int i = 17; i <= 90; i++) // Plockar ut ålder, grupperar och räknar antalet.
            {
                var ins = customerList.Where(y => y.age == i && y.carInsurance == 1);
                //var byage = customerList.GroupBy(y => y.age);
                chart1.Series["Bilförsäkring"].Points.AddXY(i, ins.Count());

                var noIns = customerList.Where(y => y.age == i && y.carInsurance == 0);
                chart1.Series["Utan försäkring"].Points.AddXY(i, noIns.Count());
            }



            /*   foreach (Customer c in customerList)
               {
                   var q = customerList.Where(y => y.carInsurance == 1);
                   chart1.Series["Series1"].Points.AddXY(c.education, q.Count());

               };
            */

            /*
            foreach (Customer c in customerList.Where(y => y.carInsurance == 1))
            {
                chart1.Series["Series1"].Points.AddXY(c.age,c.);
            } 
            */
            chart1.Series["Utan försäkring"].ChartType = SeriesChartType.Line;
            chart1.Series["Bilförsäkring"].ChartType = SeriesChartType.Line;
            chart1.Titles.Add("Spridning av kundersålder och koppling till om dem har eller inte har försäkring");
            chart1.ChartAreas[0].AxisY.Title = "Antal";
            chart1.ChartAreas[0].AxisX.Title = "Ålder";
            chart1.Series["Utan försäkring"].BorderWidth = 5;
            chart1.Series["Bilförsäkring"].BorderWidth = 5;




            /*
                        foreach (var Customer in nameGroup)
                        {
                            chart1.Series["Series1"].Points.AddXY(Customer.age, Customer.balance);
                        }

            */
            var groups = customerList.Where(c => c.carInsurance == 1).GroupBy(c => c.age).OrderBy(g => g.Key).ToList();

            int totalCount = groups.Sum(g => g.Count());

            int countUppTill25 = groups.Where(g => g.Key <= 25).Sum(g => g.Count());
            int count26Till32 = groups.Where(g => g.Key >= 26 && g.Key <= 32).Sum(g => g.Count());
            int count33Till40 = groups.Where(g => g.Key >= 33 && g.Key <= 40).Sum(g => g.Count());
            int count41Till50 = groups.Where(g => g.Key >= 41 && g.Key <= 50).Sum(g => g.Count());
            int count51Till65 = groups.Where(g => g.Key >= 51 && g.Key <= 65).Sum(g => g.Count());
            int count66OchAldre = groups.Where(g => g.Key >= 66).Sum(g => g.Count());

            Series serice1 = chart2.Series["Series1"]; // Hämtar serien från diagramet
            serice1.Points.AddXY("0 - 25", ((decimal)countUppTill25 / totalCount) * 100);
            serice1.Points.AddXY("26 - 32", ((decimal)count26Till32 / totalCount) * 100);
            serice1.Points.AddXY("33 - 40", ((decimal)count33Till40 / totalCount) * 100);
            serice1.Points.AddXY("41 - 50", ((decimal)count41Till50 / totalCount) * 100);
            serice1.Points.AddXY("51 - 65", ((decimal)count51Till65 / totalCount) * 100);
            serice1.Points.AddXY("66+", ((decimal)count66OchAldre / totalCount) * 100);

            chart2.Series["Series1"].ChartType = SeriesChartType.RangeColumn;
            chart2.Titles.Add("Procentuell");
            chart2.ChartAreas[0].AxisY.Title = "Antal i procent";
            chart2.ChartAreas[0].AxisX.Title = "Ålder";
            chart2.Series["Series1"].BorderWidth = 5;


            //Series serice1 = chart1.Series["Series1"]; // Hämtar serien från diagramet
            //serice1.Points.AddXY("0 - 25", countUppTill25);
            //serice1.Points.AddXY("26 - 32", count26Till32);
            //serice1.Points.AddXY("33 - 40", count33Till40);
            //serice1.Points.AddXY("41 - 50", count41Till50);
            //serice1.Points.AddXY("51 - 65", count51Till65);
            //serice1.Points.AddXY("66+", count66OchAldre);
        }
    }
}
