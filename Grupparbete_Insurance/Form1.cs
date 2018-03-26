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
using System.Windows.Forms.DataVisualization;

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


            foreach (Customer C in collection)
            {
                chart1.Series["Series"].Points.AddY();
            }

        }

    }
    
}
