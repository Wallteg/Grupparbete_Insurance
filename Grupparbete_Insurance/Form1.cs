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



            /*
            for (int i = 17; i <= 90; i++) // Plockar ut ålder, grupperar och räknar antalet.
            {
                var ins = customerList.Where(y => y.age == i && y.carInsurance == 1);
                chart1.Series["Age Insured"].Points.AddXY(i, ins.Count());

                var noIns = customerList.Where(y => y.age == i && y.carInsurance == 0);
                chart1.Series["Age Uninsured"].Points.AddXY(i, noIns.Count());
            }

            Series ageIns = chart1.Series["Age Insured"];
            ageIns.Enabled = false;
            Series ageNoIns = chart1.Series["Age Uninsured"];
            ageNoIns.Enabled = false */


            var groups = customerList.Where(c => c.carInsurance == 1).GroupBy(c => c.age).OrderBy(g => g.Key).ToList();
            var groups1 = customerList.Where(c => c.carInsurance == 0).GroupBy(c1 => c1.age).OrderBy(g1 => g1.Key).ToList();
            var edu = customerList.Where(c => c.carInsurance == 1).GroupBy(c => c.education).OrderBy(ed => ed.Key).ToList();
            var edu1 = customerList.Where(c => c.carInsurance == 0).GroupBy(c => c.education).OrderBy(ed1 => ed1.Key).ToList();
            var Marital = customerList.Where(c => c.carInsurance == 1).GroupBy(c => c.marital).OrderBy(m => m.Key).ToList();
            var Marital1 = customerList.Where(c => c.carInsurance == 0).GroupBy(c => c.marital).OrderBy(m => m.Key).ToList();
            var jobs = customerList.Where(c => c.carInsurance == 1).GroupBy(c => c.job).OrderBy(j => j.Key).ToList();
            var jobs1 = customerList.Where(c => c.carInsurance == 0).GroupBy(c => c.job).OrderBy(j => j.Key).ToList();

            int totalCount = groups.Sum(g => g.Count());
            int totalCount1 = groups1.Sum(g1 => g1.Count());
            int totalCount2 = edu.Sum(ed => ed.Count());
            int totalCount3 = edu1.Sum(ed1 => ed1.Count());
            int totalCount4 = Marital.Sum(m => m.Count());
            int totalCount5 = Marital1.Sum(m1 => m1.Count());
            int totalCount7 = jobs.Sum(j => j.Count());
            int totalCount8 = jobs1.Sum(j1 => j1.Count());

            int countUppTill25 = groups.Where(g => g.Key <= 25).Sum(g => g.Count());
            int count26Till32 = groups.Where(g => g.Key >= 26 && g.Key <= 32).Sum(g => g.Count());
            int count33Till40 = groups.Where(g => g.Key >= 33 && g.Key <= 40).Sum(g => g.Count());
            int count41Till50 = groups.Where(g => g.Key >= 41 && g.Key <= 50).Sum(g => g.Count());
            int count51Till65 = groups.Where(g => g.Key >= 51 && g.Key <= 65).Sum(g => g.Count());
            int count66OchAldre = groups.Where(g => g.Key >= 66).Sum(g => g.Count());
            int NA = edu.Where(ed => ed.Key == "NA").Sum(ed => ed.Count());

            int primary = edu.Where(ed => ed.Key == "primary").Sum(ed => ed.Count());
            int secondary = edu.Where(ed => ed.Key == "secondary").Sum(ed => ed.Count());
            int tertiary = edu.Where(ed => ed.Key == "tertiary").Sum(ed => ed.Count());

            int married = Marital.Where(m => m.Key == "married").Sum(m => m.Count());
            int single = Marital.Where(m => m.Key == "single").Sum(m => m.Count());
            int divorced = Marital.Where(m => m.Key == "divorced").Sum(m => m.Count());


            int management = jobs.Where(j => j.Key == "management").Sum(j => j.Count());
            int technician = jobs.Where(j => j.Key == "technician").Sum(j => j.Count());
            int bluecollar = jobs.Where(j => j.Key == "blue-collar").Sum(j => j.Count());
            int admin = jobs.Where(j => j.Key == "admin.").Sum(j => j.Count());
            int retired = jobs.Where(j => j.Key == "retired").Sum(j => j.Count());
            int services = jobs.Where(j => j.Key == "services").Sum(j => j.Count());
            int selfemployed = jobs.Where(j => j.Key == "self-employed").Sum(j => j.Count());
            int student = jobs.Where(j => j.Key == "student").Sum(j => j.Count());
            int unemployed = jobs.Where(j => j.Key == "unemployed").Sum(j => j.Count());
            int entrepreneur = jobs.Where(j => j.Key == "entrepreneur").Sum(j => j.Count());
            int housemaid = jobs.Where(j => j.Key == "housemaid").Sum(j => j.Count());

            Series ageIns = chart1.Series["Age Insured"]; // Hämtar serien från diagramet
            ageIns.Points.AddXY("0 - 25", ((decimal)countUppTill25 / totalCount) * 100);
            ageIns.Points.AddXY("26 - 32", ((decimal)count26Till32 / totalCount) * 100);
            ageIns.Points.AddXY("33 - 40", ((decimal)count33Till40 / totalCount) * 100);
            ageIns.Points.AddXY("41 - 50", ((decimal)count41Till50 / totalCount) * 100);
            ageIns.Points.AddXY("51 - 65", ((decimal)count51Till65 / totalCount) * 100);
            ageIns.Points.AddXY("66+", ((decimal)count66OchAldre / totalCount) * 100);
            ageIns.Enabled = false;


            Series ageNoIns = chart1.Series["Age Uninsured"]; // Hämtar serien från diagramet
            ageNoIns.Points.AddXY("0 - 25", ((decimal)countUppTill25 / totalCount1) * 100);
            ageNoIns.Points.AddXY("26 - 32", ((decimal)count26Till32 / totalCount1) * 100);
            ageNoIns.Points.AddXY("33 - 40", ((decimal)count33Till40 / totalCount1) * 100);
            ageNoIns.Points.AddXY("41 - 50", ((decimal)count41Till50 / totalCount1) * 100);
            ageNoIns.Points.AddXY("51 - 65", ((decimal)count51Till65 / totalCount1) * 100);
            ageNoIns.Points.AddXY("66+", ((decimal)count66OchAldre / totalCount1) * 100);
            ageNoIns.Enabled = false;







            Series educationIns = chart1.Series["Education Insured"]; // Hämtar serien från diagramet
            educationIns.Enabled = false;

            educationIns.Points.AddXY("NA", ((decimal)NA / totalCount2) * 100);
            educationIns.Points.AddXY("primary", ((decimal)primary / totalCount2) * 100);
            educationIns.Points.AddXY("secondary", ((decimal)secondary / totalCount2) * 100);
            educationIns.Points.AddXY("tertiary", ((decimal)tertiary / totalCount2) * 100);


            Series educationNoIns = chart1.Series["Education Uninsured"]; // Hämtar serien från diagramet
            educationNoIns.Enabled = false;

            educationNoIns.Points.AddXY("NA", ((decimal)NA / totalCount3) * 100);
            educationNoIns.Points.AddXY("primary", ((decimal)primary / totalCount3) * 100);
            educationNoIns.Points.AddXY("secondary", ((decimal)secondary / totalCount3) * 100);
            educationNoIns.Points.AddXY("tertiary", ((decimal)tertiary / totalCount3) * 100);

            //MARIAL SOFIA


            Series maritalIns = chart1.Series["Marital Status Insured"]; // Hämtar serien från diagramet (dom som har försäkring)
            maritalIns.Enabled = false;

            maritalIns.Points.AddXY("married", ((decimal)married / totalCount4) * 100);
            maritalIns.Points.AddXY("single", ((decimal)single / totalCount4) * 100);
            maritalIns.Points.AddXY("divorced", ((decimal)divorced / totalCount4) * 100);
            //Dom som inte har försäkring
            Series maritalNoIns = chart1.Series["Marital Status Uninsured"]; // Hämtar serien från diagramet
            maritalNoIns.Enabled = false;

            maritalNoIns.Points.AddXY("Married", ((decimal)married / totalCount5) * 100);
            maritalNoIns.Points.AddXY("Single", ((decimal)single / totalCount5) * 100);
            maritalNoIns.Points.AddXY("Divorced", ((decimal)divorced / totalCount5) * 100);







            Series occupationIns = chart1.Series["Occupation Insured"]; // Hämtar serien från diagramet
            occupationIns.Points.AddXY("management", ((decimal)management / totalCount7) * 100);
            occupationIns.Points.AddXY("technician", ((decimal)technician / totalCount7) * 100);
            occupationIns.Points.AddXY("bluecollar", ((decimal)bluecollar / totalCount7) * 100);
            occupationIns.Points.AddXY("admin", ((decimal)admin / totalCount7) * 100);
            occupationIns.Points.AddXY("retired", ((decimal)retired / totalCount7) * 100);
            occupationIns.Points.AddXY("services", ((decimal)services / totalCount7) * 100);
            occupationIns.Points.AddXY("selfemployed", ((decimal)selfemployed / totalCount7) * 100);
            occupationIns.Points.AddXY("student", ((decimal)student / totalCount7) * 100);
            occupationIns.Points.AddXY("unemployed", ((decimal)unemployed / totalCount7) * 100);
            occupationIns.Points.AddXY("entrepreneur", ((decimal)entrepreneur / totalCount7) * 100);
            occupationIns.Points.AddXY("housemaid", ((decimal)housemaid / totalCount7) * 100);

            occupationIns.Enabled = false; 

            Series occupationNoIns = chart1.Series["Occupation Uninsured"]; // Hämtar serien från diagramet
            occupationNoIns.Points.AddXY("management", ((decimal)management / totalCount8) * 100);
            occupationNoIns.Points.AddXY("technician", ((decimal)technician / totalCount8) * 100);
            occupationNoIns.Points.AddXY("bluecollar", ((decimal)bluecollar / totalCount8) * 100);
            occupationNoIns.Points.AddXY("admin", ((decimal)admin / totalCount8) * 100);
            occupationNoIns.Points.AddXY("retired", ((decimal)retired / totalCount8) * 100);
            occupationNoIns.Points.AddXY("services", ((decimal)services / totalCount8) * 100);
            occupationNoIns.Points.AddXY("selfemployed", ((decimal)selfemployed / totalCount8) * 100);
            occupationNoIns.Points.AddXY("student", ((decimal)student / totalCount8) * 100);
            occupationNoIns.Points.AddXY("unemployed", ((decimal)unemployed / totalCount8) * 100);
            occupationNoIns.Points.AddXY("entrepreneur", ((decimal)entrepreneur / totalCount8) * 100);
            occupationNoIns.Points.AddXY("housemaid", ((decimal)housemaid / totalCount8) * 100);

            occupationNoIns.Enabled = false;


            


            /*        var groups = customerLis0t.Where(c => c.carInsurance == 1).GroupBy(c => c.age).OrderBy(g => g.Key).ToList();

                int totalCount = groups.Sum(g => g.Count());

                int countUppTill25 = groups.Where(g => g.Key <= 25).Sum(g => g.Count());
                int count26Till32 = groups.Where(g => g.Key >= 26 && g.Key <= 32).Sum(g => g.Count());
                int count33Till40 = groups.Where(g => g.Key >= 33 && g.Key <= 40).Sum(g => g.Count());
                int count41Till50 = groups.Where(g => g.Key >= 41 && g.Key <= 50).Sum(g => g.Count());
                int count51Till65 = groups.Where(g => g.Key >= 51 && g.Key <= 65).Sum(g => g.Count());
                int count66OchAldre = groups.Where(g => g.Key >= 66).Sum(g => g.Count());

                Series serice1 = chart2.Series["Series1"]; // Hämtar serien från diagramet
                serice1.Points.AddXY("0 - 25", ((decimal) countUppTill25 / totalCount) * 100);
                    serice1.Points.AddXY("26 - 32", ((decimal) count26Till32 / totalCount) * 100);
                    serice1.Points.AddXY("33 - 40", ((decimal) count33Till40 / totalCount) * 100);
                    serice1.Points.AddXY("41 - 50", ((decimal) count41Till50 / totalCount) * 100);
                    serice1.Points.AddXY("51 - 65", ((decimal) count51Till65 / totalCount) * 100);
                    serice1.Points.AddXY("66+", ((decimal) count66OchAldre / totalCount) * 100);

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

            */

            // CALLES DEL FÖR ATT FÅ FRAM JOB / PROCENT
            /*
            var jobs = customerList.Where(c => c.carInsurance == 1).GroupBy(c => c.job).OrderBy(j => j.Key).ToList();

            int totalCounts = jobs.Sum(j => j.Count());

            int management = jobs.Where(j => j.Key == "management").Sum(j => j.Count());
            int technician = jobs.Where(j => j.Key == "technician").Sum(j => j.Count());
            int bluecollar = jobs.Where(j => j.Key == "blue-collar").Sum(j => j.Count());
            int admin = jobs.Where(j => j.Key == "admin.").Sum(j => j.Count());
            int retired = jobs.Where(j => j.Key == "retired").Sum(j => j.Count());
            int services = jobs.Where(j => j.Key == "services").Sum(j => j.Count());
            int selfemployed = jobs.Where(j => j.Key == "self-employed").Sum(j => j.Count());
            int student = jobs.Where(j => j.Key == "student").Sum(j => j.Count());
            int unemployed = jobs.Where(j => j.Key == "unemployed").Sum(j => j.Count());
            int entrepreneur = jobs.Where(j => j.Key == "entrepreneur").Sum(j => j.Count());
            int housemaid = jobs.Where(j => j.Key == "housemaid").Sum(j => j.Count());



            Series series1 = chart1.Series["Occupation Insured"]; // Hämtar serien från diagramet
            ageIns.Points.AddXY("management", ((decimal)management / totalCounts) * 100);
            ageIns.Points.AddXY("technician", ((decimal)technician / totalCounts) * 100);
            ageIns.Points.AddXY("bluecollar", ((decimal)bluecollar / totalCounts) * 100);
            ageIns.Points.AddXY("admin", ((decimal)admin / totalCounts) * 100);
            ageIns.Points.AddXY("retired", ((decimal)retired / totalCounts) * 100);
            ageIns.Points.AddXY("services", ((decimal)services / totalCounts) * 100);
            ageIns.Points.AddXY("selfemployed", ((decimal)selfemployed / totalCounts) * 100);
            ageIns.Points.AddXY("student", ((decimal)student / totalCounts) * 100);
            ageIns.Points.AddXY("unemployed", ((decimal)unemployed / totalCounts) * 100);
            ageIns.Points.AddXY("entrepreneur", ((decimal)entrepreneur / totalCounts) * 100);
            ageIns.Points.AddXY("housemaid", ((decimal)housemaid / totalCounts) * 100);




            // SOFIAS DEL OM MARTIAL

            var Marital = customerList.Where(c => c.carInsurance == 1).GroupBy(c => c.marital).OrderBy(m => m.Key).ToList();
            int totalCount1 = Marital.Sum(m => m.Count());

            int maried = Marital.Where(m => m.Key == "married").Sum(m => m.Count());
            int single = Marital.Where(m => m.Key == "single").Sum(m => m.Count());
            int divorced = Marital.Where(m => m.Key == "divorced").Sum(m => m.Count());

            Series serice1 = chart1.Series["Marital Status Insured"]; // Hämtar serien från diagramet
            serice1.Points.AddXY("married", ((decimal)maried / totalCount1) * 100);
            serice1.Points.AddXY("single", ((decimal)single / totalCount1) * 100);
            serice1.Points.AddXY("divorced", ((decimal)divorced / totalCount1) * 100);




            // Marcus Lösning för chart med procent, baserat på Utbildning.
            var edu = customerList.Where(c => c.carInsurance == 1).GroupBy(c => c.education).OrderBy(ed => ed.Key).ToList();
            var edu1 = customerList.Where(c => c.carInsurance == 0).GroupBy(c => c.education).OrderBy(ed1 => ed1.Key).ToList();
            int totalCount2 = edu.Sum(ed => ed.Count());
            int totalCount3 = edu1.Sum(ed1 => ed1.Count());

            int NA = edu.Where(ed => ed.Key == "NA").Sum(ed => ed.Count());
            int primary = edu.Where(ed => ed.Key == "primary").Sum(ed => ed.Count());
            int secondary = edu.Where(ed => ed.Key == "secondary").Sum(ed => ed.Count());
            int tertiary = edu.Where(ed => ed.Key == "tertiary").Sum(ed => ed.Count());

            Series educationIns = chart1.Series["Education Insured"]; // Hämtar serien från diagramet
            educationIns.Enabled = false;

            educationIns.Points.AddXY("NA", ((decimal)NA / totalCount2) * 100);
            educationIns.Points.AddXY("primary", ((decimal)primary / totalCount2) * 100);
            educationIns.Points.AddXY("secondary", ((decimal)secondary / totalCount2) * 100);
            educationIns.Points.AddXY("tertiary", ((decimal)tertiary / totalCount2) * 100);

            Series educationNoIns = chart1.Series["Education Uninsured"]; // Hämtar serien från diagramet
            educationNoIns.Enabled = false;

            educationNoIns.Points.AddXY("NA", ((decimal)NA / totalCount3) * 100);
            educationNoIns.Points.AddXY("primary", ((decimal)primary / totalCount3) * 100);
            educationNoIns.Points.AddXY("secondary", ((decimal)secondary / totalCount3) * 100);
            educationNoIns.Points.AddXY("tertiary", ((decimal)tertiary / totalCount3) * 100);
            */

        }


        private void button1_Click(object sender, EventArgs e)
        {
            {
                Series ageIns = chart1.Series["Age Insured"];
                Series ageNoIns = chart1.Series["Age Uninsured"];

                if (ageIns.Enabled == false && ageNoIns.Enabled == false)
                {
                    ageIns.Enabled = true;

                    chart1.Series["Age Insured"].ChartType = SeriesChartType.Line;
                    chart1.Series["Age Uninsured"].ChartType = SeriesChartType.Line;
                    chart1.Titles.Add("Spridning av kundersålder och koppling till om dem har eller inte har försäkring");
                    chart1.ChartAreas[0].AxisY.Title = "Antal";
                    chart1.ChartAreas[0].AxisX.Title = "Ålder";
                    chart1.Series["Age Insured"].BorderWidth = 5;
                    chart1.Series["Age Uninsured"].BorderWidth = 5;
                    //chart1.Series["Age Insured"].IsValueShownAsLabel = true;
                    //chart1.ChartAreas[0].AxisX.LabelStyle.Format = "#.#";

                }
                else if (ageIns.Enabled == true && ageNoIns.Enabled == false)
                {
                    ageIns.Enabled = false;
                    ageNoIns.Enabled = true;
                    
                }
                else if (ageIns.Enabled == false && ageNoIns.Enabled == true)
                {
                    ageNoIns.Enabled = true;
                    ageIns.Enabled = true;
                    
                }
                else if (ageIns.Enabled == true && ageNoIns.Enabled == true)
                {
                    ageIns.Enabled = false;
                    ageNoIns.Enabled = false;
                    
                    chart1.Titles.Clear();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            {

                Series educationIns = chart1.Series["Education Insured"];
                Series educationNoIns = chart1.Series["Education Uninsured"];


                if (educationIns.Enabled == false && educationNoIns.Enabled == false)
                {
                    educationIns.Enabled = true;

                    chart1.Titles.Add("Education");
                    chart1.ChartAreas[0].AxisY.Title = "Amount in Percentage";
                    chart1.ChartAreas[0].AxisX.Title = "Education level";
                    chart1.Series["Education Insured"].ChartType = SeriesChartType.Column;
                    chart1.Series["Education Uninsured"].ChartType = SeriesChartType.Column;

                }
                else if (educationIns.Enabled == true && educationNoIns.Enabled == false)
                {
                    educationIns.Enabled = false;
                    educationNoIns.Enabled = true;
                                    }
                else if (educationIns.Enabled == false && educationNoIns.Enabled == true)
                {
                    educationIns.Enabled = true;
                    educationNoIns.Enabled = true;
                    
                }
                else if (educationIns.Enabled == true && educationNoIns.Enabled == true)
                {
                    educationIns.Enabled = false;
                    educationNoIns.Enabled = false;
                    chart1.Titles.Clear();
                }


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            {

                Series occupationIns = chart1.Series["Occupation Insured"];
                Series occupationNoIns = chart1.Series["Occupation Uninsured"];


                if (occupationIns.Enabled == false && occupationNoIns.Enabled == false)
                {
                    occupationIns.Enabled = true;

                    chart1.Titles.Add("Occupation");
                    chart1.ChartAreas[0].AxisY.Title = "Amount in Percentage";
                    chart1.ChartAreas[0].AxisX.Title = "Occupation";
                    chart1.Series["Occupation Insured"].ChartType = SeriesChartType.Column;
                    chart1.Series["Occupation Uninsured"].ChartType = SeriesChartType.Column;
                    chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                    
                }
                else if (occupationIns.Enabled == true && occupationNoIns.Enabled == false)
                {
                    occupationIns.Enabled = false;
                    occupationNoIns.Enabled = true;
                }
                else if (occupationIns.Enabled == false && occupationNoIns.Enabled == true)
                {
                    occupationNoIns.Enabled = true;
                    occupationIns.Enabled = true;
                    
                }
                else if (occupationIns.Enabled == true && occupationNoIns.Enabled == true)
                {
                    occupationIns.Enabled = false;
                    occupationNoIns.Enabled = false;

                    chart1.Titles.Clear();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            {

                Series martialIns = chart1.Series["Marital Status Insured"];
                Series maritalNoIns = chart1.Series["Marital Status Uninsured"];


                if (martialIns.Enabled == false && maritalNoIns.Enabled == false)
                {
                    martialIns.Enabled = true;

                    chart1.Titles.Add("Martial Status");
                    chart1.ChartAreas[0].AxisY.Title = "Amount in Percentage";
                    chart1.ChartAreas[0].AxisX.Title = "Marital Status";
                }
                else if (martialIns.Enabled == true && maritalNoIns.Enabled == false)
                {
                    martialIns.Enabled = false;
                    maritalNoIns.Enabled = true;
                }
                else if (martialIns.Enabled == false && maritalNoIns.Enabled == true)
                {
                    maritalNoIns.Enabled = true;
                    martialIns.Enabled = true;
                    
                }
                else if (martialIns.Enabled == true && maritalNoIns.Enabled == true)
                {
                    martialIns.Enabled = false;
                    maritalNoIns.Enabled = false;

                    chart1.Titles.Clear();
                }
            }
        }
    }

}

