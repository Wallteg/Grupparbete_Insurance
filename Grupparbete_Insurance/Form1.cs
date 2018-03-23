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
            
            conn.ConnectionString = "Data Source = DESKTOP - 0KMEDJA\\SQL2017; Initial Catalog = Insurance; Integrated Security = True";

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
                bool credit;
                int balance;
                bool homeInsurance;
                bool carLoan;
                string communication;
                int lastContactDay;
                string lastContactMonth;
                int noOfContacts;
                int daysPassed;
                int prevAttemts;
                string outcome;
                TimeSpan callStart;
                TimeSpan callEnd;
                bool carInsurance;

                while (myReader.Read())
                {
                    id = (int)myReader["id"];
                    age = (int)myReader["age"];
                    job = myReader["job"].ToString();
                    marital = myReader["martial"].ToString();
                    education = myReader["education"].ToString();
                    if ((int)myReader["default"] == 1)
                    {
                        credit = true;
                    }
                    else
                    {
                        credit = false;
                    }
                    balance = (int)myReader["balance"];
                    if ((int)myReader["HHInsurance"] == 1)
                    {
                        homeInsurance = true;
                    }
                    else
                    {
                        homeInsurance = false;
                    }


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
        }

    }
    
}
