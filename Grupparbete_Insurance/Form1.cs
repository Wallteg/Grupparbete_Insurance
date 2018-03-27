using System;
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



            for (int i = 17; i < 95; i++) // Plockar ut ålder, grupperar och räknar antalet.
            {
                var q = customerList.Where(y => y.age == i && y.carInsurance == 1);
                chart1.Series["Series1"].Points.AddXY(i, q.Count());
            }


            /*
            foreach (Customer c in customerList.Where(y => y.carInsurance == 1))
            {
                chart1.Series["Series1"].Points.AddXY(c.age,c.);
            } 
            */
            chart1.Series["Series1"].ChartType = SeriesChartType.Column;
            chart1.Titles.Add("Age");
            chart1.ChartAreas[0].AxisY.Title = "Balance";
            chart1.ChartAreas[0].AxisX.Title = "Ålder";


            //   chart1.DataManipulator.GroupByAxisLabel("Antal", "age");
        }
    }
}
