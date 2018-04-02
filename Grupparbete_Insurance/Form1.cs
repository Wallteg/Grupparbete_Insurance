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

            //Skapar en koppling till Databasen "Insurance" som finns på SQL server.
            conn.ConnectionString = "Data Source=DESKTOP-0KMEDJA\\SQL2017;Initial Catalog=Insurance;Integrated Security=True";

            //Skapar en kundlista där vi sedan kan spara alla kunduppgifter om våra kunder.
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
                int credit; //    1 true 0 false
                int balance;
                int homeInsurance; //      1 true 0 false
                int carLoan;  //       1 true 0 false
                string communication;
                int lastContactDay;
                string lastContactMonth;
                int noOfContacts;
                int daysPassed;
                int prevAttemts;
                string outcome;
                TimeSpan callStart;
                TimeSpan callEnd;
                int carInsurance; //  1 true 0 false


                while (myReader.Read())
                {

                    //inhämtning av värden från SQL server till våra olika variabler. 
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

                    //Koppling till vår klass "Customer", varje kund blir ett objekt med dessa olika egenskaperna.
                    Customer customer = new Customer(id, age, job, marital, education, credit, balance,
                        homeInsurance, carLoan, communication, lastContactDay, lastContactMonth, noOfContacts,
                        daysPassed, prevAttemts, outcome, callStart, callEnd, carInsurance);

                    // Lägg till det i customerList som skapades inledningsvis på denna sida.
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

            //För vår visualisering vill vi kunna särskilja personer som har försäkring och dem som inte har försäkring
            // Vi har olika intressepunkter och olika områden vi vill undersöka, såsom, Ålder, Utbildning, Civilstatus, Jobb, osv.
            var groups = customerList.Where(c => c.carInsurance == 1).GroupBy(c => c.age).OrderBy(g => g.Key).ToList();
            var groups1 = customerList.Where(c => c.carInsurance == 0).GroupBy(cn => cn.age).OrderBy(gn => gn.Key).ToList();
            var edu = customerList.Where(c => c.carInsurance == 1).GroupBy(c => c.education).OrderBy(ed => ed.Key).ToList();
            var edu1 = customerList.Where(c => c.carInsurance == 0).GroupBy(c => c.education).OrderBy(ed1 => ed1.Key).ToList();
            var Marital = customerList.Where(c => c.carInsurance == 1).GroupBy(c => c.marital).OrderBy(m => m.Key).ToList();
            var Marital1 = customerList.Where(c => c.carInsurance == 0).GroupBy(c => c.marital).OrderBy(m => m.Key).ToList();
            var jobs = customerList.Where(c => c.carInsurance == 1).GroupBy(c => c.job).OrderBy(j => j.Key).ToList();
            var jobs1 = customerList.Where(c => c.carInsurance == 0).GroupBy(c => c.job).OrderBy(j => j.Key).ToList();
            var home = customerList.Where(c => c.carInsurance == 1).GroupBy(c => c.homeInsurance).OrderBy(h => h.Key).ToList();
            var home1 = customerList.Where(c => c.carInsurance == 0).GroupBy(c => c.homeInsurance).OrderBy(h => h.Key).ToList();

            //Countern sätts i decimal för att undvika konvertering till decimal från int i uträkningar
            decimal counter = customerList.Count(); // Beräknar antalet rader för att kunna få ut procent

            //vi vill kunna se åldergrupper på våra kunder för att kunna veta vart vi ska lägga största fokus,
            // För att kunna göra det använder vi denna för att kunna lägga ihop ett samlat resultat för dem som är inom samma åldersspann.
            int countUppTill25 = groups.Where(g => g.Key <= 25).Sum(g => g.Count());
            int count26Till32 = groups.Where(g => g.Key >= 26 && g.Key <= 32).Sum(g => g.Count());
            int count33Till40 = groups.Where(g => g.Key >= 33 && g.Key <= 40).Sum(g => g.Count());
            int count41Till50 = groups.Where(g => g.Key >= 41 && g.Key <= 50).Sum(g => g.Count());
            int count51Till65 = groups.Where(g => g.Key >= 51 && g.Key <= 65).Sum(g => g.Count());
            int count66OchAldre = groups.Where(g => g.Key >= 66).Sum(g => g.Count());

            //Denna gör samma sak, men räknar ut dem som SAKNAR försäkring, den ovan får ut resultatet för personer som är försäkrade.
            int countUppTill25n = groups1.Where(gn => gn.Key <= 25).Sum(gn => gn.Count());
            int count26Till32n = groups1.Where(gn => gn.Key >= 26 && gn.Key <= 32).Sum(gn => gn.Count());
            int count33Till40n = groups1.Where(gn => gn.Key >= 33 && gn.Key <= 40).Sum(gn => gn.Count());
            int count41Till50n = groups1.Where(gn => gn.Key >= 41 && gn.Key <= 50).Sum(gn => gn.Count());
            int count51Till65n = groups1.Where(gn => gn.Key >= 51 && gn.Key <= 65).Sum(gn => gn.Count());
            int count66OchAldren = groups1.Where(gn => gn.Key >= 66).Sum(gn => gn.Count());

            //Samma princip fast här tar vi reda på antalet personer som har en viss utbildningsgrad.
            int primary = edu.Where(ed => ed.Key == "primary").Sum(ed => ed.Count());
            int secondary = edu.Where(ed => ed.Key == "secondary").Sum(ed => ed.Count());
            int tertiary = edu.Where(ed => ed.Key == "tertiary").Sum(ed => ed.Count());
            int NA = edu.Where(ed => ed.Key == "NA").Sum(ed => ed.Count());

            int primaryn = edu1.Where(ed => ed.Key == "primary").Sum(ed => ed.Count());
            int secondaryn = edu1.Where(ed => ed.Key == "secondary").Sum(ed => ed.Count());
            int tertiaryn = edu1.Where(ed => ed.Key == "tertiary").Sum(ed => ed.Count());
            int NAn = edu1.Where(ed => ed.Key == "NA").Sum(ed => ed.Count());

            //Civilstatus
            int married = Marital.Where(m => m.Key == "married").Sum(m => m.Count());
            int single = Marital.Where(m => m.Key == "single").Sum(m => m.Count());
            int divorced = Marital.Where(m => m.Key == "divorced").Sum(m => m.Count());

            int marriedn = Marital1.Where(m => m.Key == "married").Sum(m => m.Count());
            int singlen = Marital1.Where(m => m.Key == "single").Sum(m => m.Count());
            int divorcedn = Marital1.Where(m => m.Key == "divorced").Sum(m => m.Count());

            //Yrke
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

            int managementn = jobs1.Where(j => j.Key == "management").Sum(j => j.Count());
            int techniciann = jobs1.Where(j => j.Key == "technician").Sum(j => j.Count());
            int bluecollarn = jobs1.Where(j => j.Key == "blue-collar").Sum(j => j.Count());
            int adminn = jobs1.Where(j => j.Key == "admin.").Sum(j => j.Count());
            int retiredn = jobs1.Where(j => j.Key == "retired").Sum(j => j.Count());
            int servicesn = jobs1.Where(j => j.Key == "services").Sum(j => j.Count());
            int selfemployedn = jobs1.Where(j => j.Key == "self-employed").Sum(j => j.Count());
            int studentn = jobs1.Where(j => j.Key == "student").Sum(j => j.Count());
            int unemployedn = jobs1.Where(j => j.Key == "unemployed").Sum(j => j.Count());
            int entrepreneurn = jobs1.Where(j => j.Key == "entrepreneur").Sum(j => j.Count());
            int housemaidn = jobs.Where(j => j.Key == "housemaid").Sum(j => j.Count());

            // Vi ville bland annat kunna se om det var vanligt att samla sina försäkringar på samma ställe inom banken
            // Denna del hjälper oss att få fram antalet kunder som har båda försäkringarna, endast bil, endast hem eller saknar försäkring inom banken
            int homecar = home.Where(h => h.Key == 1).Sum(h => h.Count());
            int noHomeCar = home.Where(h => h.Key == 0).Sum(h => h.Count());

            int homeNoCar = home1.Where(h => h.Key == 1).Sum(h => h.Count());
            int noHomeNoCar = home1.Where(h => h.Key == 0).Sum(h => h.Count());


            //Vi gör en serie som vi kallar för ageIns (ålder Försäkrade), den ska läggas till i seriens Age Insured
            //raderna under så lägger vi till ett X-värde bestående av åldersspannet, därefter lägger vi till ett värde i Procent.
            //Alla resultat visas med 2 decimaler
            Series ageIns = chart1.Series["Age Insured"]; // Hämtar serien från diagramet
            ageIns.Points.AddXY("0 - 25", decimal.Round((countUppTill25 / counter) * 100, 2));
            ageIns.Points.AddXY("26 - 32", decimal.Round((count26Till32 / counter) * 100, 2));
            ageIns.Points.AddXY("33 - 40", decimal.Round((count33Till40 / counter) * 100, 2));
            ageIns.Points.AddXY("41 - 50", decimal.Round(count41Till50 / counter * 100, 2));
            ageIns.Points.AddXY("51 - 65", decimal.Round((count51Till65 / counter) * 100, 2));
            ageIns.Points.AddXY("66+", decimal.Round((count66OchAldre / counter) * 100, 2));
            ageIns.Enabled = false; // Vi gör grafen ej synlig som default, för att med hjälp av knappar senare kunna visa önskad chart.

            // Serie för ålderspann fast för kunder som saknar försäkring.
            Series ageNoIns = chart1.Series["Age Uninsured"]; // Hämtar serien från diagramet
            ageNoIns.Points.AddXY("0 - 25", decimal.Round((countUppTill25n / counter) * 100, 2));
            ageNoIns.Points.AddXY("26 - 32", decimal.Round((count26Till32n / counter) * 100, 2));
            ageNoIns.Points.AddXY("33 - 40", decimal.Round((count33Till40n / counter) * 100, 2));
            ageNoIns.Points.AddXY("41 - 50", decimal.Round((count41Till50n / counter) * 100, 2));
            ageNoIns.Points.AddXY("51 - 65", decimal.Round((count51Till65n / counter) * 100, 2));
            ageNoIns.Points.AddXY("66+", decimal.Round((count66OchAldren / counter) * 100, 2));
            ageNoIns.Enabled = false; // Gör grafen ej synlig vid uppstart.

            //Skapar en serie för utbildning
            Series educationIns = chart1.Series["Education Insured"]; // Hämtar serien från diagramet
            educationIns.Enabled = false;

            educationIns.Points.AddXY("NA", decimal.Round((NA / counter) * 100, 2));
            educationIns.Points.AddXY("primary", decimal.Round((primary / counter) * 100, 2));
            educationIns.Points.AddXY("secondary", decimal.Round((secondary / counter) * 100, 2));
            educationIns.Points.AddXY("tertiary", decimal.Round((tertiary / counter) * 100, 2));


            Series educationNoIns = chart1.Series["Education Uninsured"]; // Hämtar serien från diagramet
            educationNoIns.Enabled = false;

            educationNoIns.Points.AddXY("NA", decimal.Round((NAn / counter) * 100, 2));
            educationNoIns.Points.AddXY("primary", decimal.Round((primaryn / counter) * 100, 2));
            educationNoIns.Points.AddXY("secondary", decimal.Round((secondaryn / counter) * 100, 2));
            educationNoIns.Points.AddXY("tertiary", decimal.Round((tertiaryn / counter) * 100, 2));


            Series maritalIns = chart1.Series["Marital Status Insured"]; // Hämtar serien från diagramet (dom som har försäkring)
            maritalIns.Enabled = false;

            maritalIns.Points.AddXY("married", decimal.Round((married / counter) * 100, 2));
            maritalIns.Points.AddXY("single", decimal.Round((single / counter) * 100, 2));
            maritalIns.Points.AddXY("divorced", decimal.Round((divorced / counter) * 100, 2));

            //Dom som inte har försäkring
            Series maritalNoIns = chart1.Series["Marital Status Uninsured"]; // Hämtar serien från diagramet
            maritalNoIns.Enabled = false;

            maritalNoIns.Points.AddXY("Married", decimal.Round((marriedn / counter) * 100, 2));
            maritalNoIns.Points.AddXY("Single", decimal.Round((singlen / counter) * 100, 2));
            maritalNoIns.Points.AddXY("Divorced", decimal.Round((divorcedn / counter) * 100, 2));


            Series occupationIns = chart1.Series["Occupation Insured"]; // Hämtar serien från diagramet
            occupationIns.Points.AddXY("management", decimal.Round((management / counter) * 100, 2));
            occupationIns.Points.AddXY("technician", decimal.Round((technician / counter) * 100, 2));
            occupationIns.Points.AddXY("bluecollar", decimal.Round((bluecollar / counter) * 100, 2));
            occupationIns.Points.AddXY("admin", decimal.Round((admin / counter) * 100, 2));
            occupationIns.Points.AddXY("retired", decimal.Round((retired / counter) * 100, 2));
            occupationIns.Points.AddXY("services", decimal.Round((services / counter) * 100, 2));
            occupationIns.Points.AddXY("selfemployed", decimal.Round((selfemployed / counter) * 100, 2));
            occupationIns.Points.AddXY("student", decimal.Round((student / counter) * 100, 2));
            occupationIns.Points.AddXY("unemployed", decimal.Round((unemployed / counter) * 100, 2));
            occupationIns.Points.AddXY("entrepreneur", decimal.Round((entrepreneur / counter) * 100, 2));
            occupationIns.Points.AddXY("housemaid", decimal.Round((housemaid / counter) * 100, 2));

            occupationIns.Enabled = false;

            Series occupationNoIns = chart1.Series["Occupation Uninsured"]; // Hämtar serien från diagramet
            occupationNoIns.Points.AddXY("management", decimal.Round((managementn / counter) * 100, 2));
            occupationNoIns.Points.AddXY("technician", decimal.Round((techniciann / counter) * 100, 2));
            occupationNoIns.Points.AddXY("bluecollar", decimal.Round((bluecollarn / counter) * 100, 2));
            occupationNoIns.Points.AddXY("admin", decimal.Round((adminn / counter) * 100, 2));
            occupationNoIns.Points.AddXY("retired", decimal.Round((retiredn / counter) * 100, 2));
            occupationNoIns.Points.AddXY("services", decimal.Round((servicesn / counter) * 100, 2));
            occupationNoIns.Points.AddXY("selfemployed", decimal.Round((selfemployedn / counter) * 100, 2));
            occupationNoIns.Points.AddXY("student", decimal.Round((studentn / counter) * 100, 2));
            occupationNoIns.Points.AddXY("unemployed", decimal.Round((unemployedn / counter) * 100, 2));
            occupationNoIns.Points.AddXY("entrepreneur", decimal.Round((entrepreneurn / counter) * 100, 2));
            occupationNoIns.Points.AddXY("housemaid", decimal.Round((housemaidn / counter) * 100, 2));

            occupationNoIns.Enabled = false;

            Series insurance = chart1.Series["Insurance"];
            insurance.Points.AddXY("Both", decimal.Round((homecar / counter) * 100, 2));
            insurance.Points.AddXY("Car", decimal.Round((noHomeCar / counter) * 100, 2));
            insurance.Points.AddXY("Home", decimal.Round((homeNoCar / counter) * 100, 2));
            insurance.Points.AddXY("No Insurance", decimal.Round((noHomeNoCar / counter) * 100, 2));
            insurance.Enabled = false;

            //Designinställningar
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            WindowState = FormWindowState.Maximized;

            button1.BackColor = System.Drawing.Color.DeepSkyBlue;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderColor = Color.DeepSkyBlue;
            button1.FlatAppearance.BorderSize = 1;
            button1.ForeColor = Color.White;
            button1.Font = new Font("Microsoft Sans Serif", 12f);


            button2.BackColor = System.Drawing.Color.OliveDrab;
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderColor = Color.OliveDrab;
            button2.FlatAppearance.BorderSize = 1;
            button2.ForeColor = Color.White;
            button2.Font = new Font("Microsoft Sans Serif", 12f);

            button3.BackColor = System.Drawing.Color.Orange;
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderColor = Color.Orange;
            button3.FlatAppearance.BorderSize = 1;
            button3.ForeColor = Color.White;
            button3.Font = new Font("Microsoft Sans Serif", 12f);

            button4.BackColor = System.Drawing.Color.DarkViolet;
            button4.FlatStyle = FlatStyle.Flat;
            button4.FlatAppearance.BorderColor = Color.DarkViolet;
            button4.FlatAppearance.BorderSize = 1;
            button4.ForeColor = Color.White;
            button4.Font = new Font("Microsoft Sans Serif", 12f);

            button5.BackColor = System.Drawing.Color.PaleVioletRed;
            button5.FlatStyle = FlatStyle.Flat;
            button5.FlatAppearance.BorderColor = Color.PaleVioletRed;
            button5.FlatAppearance.BorderSize = 1;
            button5.ForeColor = Color.White;
            button5.Font = new Font("Microsoft Sans Serif", 12f);

        }

        //Metod för att först tömma graferna på innehåll

        private void tomGraf()
        {
            foreach (var series in chart1.Series)
            {
                series.Enabled = false;
            }

            chart1.Titles.Clear();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            {

                //Knapp med click funktion, den första är för att få upp åldersstatistiken på vår chart.
                Series ageIns = chart1.Series["Age Insured"];
                Series ageNoIns = chart1.Series["Age Uninsured"];



                //Då vi tidigare i koden satt dessa conditions till false så kommer dem till en början alltid inledas i ett läge där
                //Grafen är tom. genom att klicka på knapparna kommer man sedan få fram önskad information.
                if (ageIns.Enabled == false && ageNoIns.Enabled == false)
                {
                    tomGraf();
                    //Inget visas
                    ageIns.Enabled = true; // ändrar till true så att nästa gång man trycker på knappen ska den komma in i nästa steg.

                    //Inställningar för hur grafen ska se ut, såsom chart type, titel, namn på x/y axel, tjocklek på visaren och höjd på y axeln.
                    chart1.Series["Age Insured"].ChartType = SeriesChartType.Line;
                    chart1.Series["Age Uninsured"].ChartType = SeriesChartType.Line;
                    chart1.Titles.Add("Variation of age with or without an insurance");
                    chart1.ChartAreas[0].AxisY.Title = "Amount of Persons";
                    chart1.ChartAreas[0].AxisX.Title = "Age";
                    chart1.Series["Age Insured"].BorderWidth = 5;
                    chart1.Series["Age Uninsured"].BorderWidth = 5;
                    chart1.ChartAreas[0].AxisY.Maximum = 25;
                    chart1.ChartAreas[0].AxisY.Minimum = 0;
                    chart1.Series["Age Insured"].Color = Color.DeepSkyBlue;
                    chart1.Series["Age Uninsured"].Color = Color.Gray;
                    chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                    chart1.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;


                }
                else if (ageIns.Enabled == true && ageNoIns.Enabled == false)
                {
                    // Nu visas endast de som har försäkring i dem olika åldrarna.
                    ageIns.Enabled = false;
                    ageNoIns.Enabled = true;

                }
                else if (ageIns.Enabled == false && ageNoIns.Enabled == true)
                {

                    // Nu visas endast de som inte är försäkrade.
                    ageNoIns.Enabled = true;
                    ageIns.Enabled = true;

                }
                else if (ageIns.Enabled == true && ageNoIns.Enabled == true)
                {
                    tomGraf();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            // samma sak som förra knappen fast nytt område. (Utbildningsgrad).
            Series educationIns = chart1.Series["Education Insured"];
            Series educationNoIns = chart1.Series["Education Uninsured"];

            if (educationIns.Enabled == false && educationNoIns.Enabled == false)
            {
                tomGraf();
                //Börjar vara tom, sen får man ny visning för varje knapptryck
                educationIns.Enabled = true;

                chart1.Titles.Add("Education");
                chart1.ChartAreas[0].AxisY.Title = "Amount in %";
                chart1.ChartAreas[0].AxisX.Title = "Education level";
                chart1.Series["Education Insured"].ChartType = SeriesChartType.Column;
                chart1.Series["Education Uninsured"].ChartType = SeriesChartType.Column;
                chart1.ChartAreas[0].AxisY.Maximum = 35;
                chart1.ChartAreas[0].AxisY.Minimum = 0;
                chart1.Series["Education Insured"].Color = Color.OliveDrab;
                chart1.Series["Education Uninsured"].Color = Color.Gray;


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
                tomGraf();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            {

                Series occupationIns = chart1.Series["Occupation Insured"];
                Series occupationNoIns = chart1.Series["Occupation Uninsured"];


                if (occupationIns.Enabled == false && occupationNoIns.Enabled == false)
                {
                    tomGraf();
                    occupationIns.Enabled = true;

                    chart1.Titles.Add("Occupation");
                    chart1.ChartAreas[0].AxisY.Title = "Amount in %";
                    chart1.ChartAreas[0].AxisX.Title = "Occupation";
                    chart1.Series["Occupation Insured"].ChartType = SeriesChartType.Column;
                    chart1.Series["Occupation Uninsured"].ChartType = SeriesChartType.Column;
                    chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                    chart1.ChartAreas[0].AxisY.Maximum = 15;
                    chart1.ChartAreas[0].AxisY.Minimum = 0;
                    chart1.Series["Occupation Insured"].Color = Color.Orange;
                    chart1.Series["Occupation Uninsured"].Color = Color.Gray;
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
                    tomGraf();
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
                    tomGraf();
                    martialIns.Enabled = true;

                    chart1.Titles.Add("Martial Status");


                    chart1.ChartAreas[0].AxisY.Title = "Amount in %";
                    chart1.ChartAreas[0].AxisX.Title = "Marital Status";

                    chart1.ChartAreas[0].AxisY.Maximum = 40;
                    chart1.ChartAreas[0].AxisY.Minimum = 0;

                    chart1.Series["Marital Status Insured"].Color = Color.DarkViolet;
                    chart1.Series["Marital Status Uninsured"].Color = Color.Gray;


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
                    tomGraf();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            {

                Series insurance = chart1.Series["Insurance"];


                if (insurance.Enabled == false)
                {
                    tomGraf();
                    insurance.Enabled = true;

                    chart1.Titles.Add("Insurance types");
                    chart1.ChartAreas[0].AxisY.Title = "Amount in %";
                    chart1.ChartAreas[0].AxisX.Title = "Insurances";
                    chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1; // Då vi hade många olika yrken behövde vi denna rad för att den 
                    //Skulle skriva ut "yrkestiteln" vid varje x punkt.
                    chart1.Series["Insurance"].Color = Color.PaleVioletRed;


                    chart1.ChartAreas[0].AxisY.Maximum = 40; // Visa diagrammet upp till 40% på Y axeln.
                    chart1.ChartAreas[0].AxisY.Minimum = 0; // Börja på 0.
                }
                else if (insurance.Enabled == true)
                {
                    tomGraf();
                }
            }
        }
    }
}