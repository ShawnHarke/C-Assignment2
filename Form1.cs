using System.Numerics;

namespace Reservations
{
    public partial class Form1 : Form
    {
        DateTime CurrentDate = DateTime.Now;  // Initializes the value of CurrentDate to the current date
        DateTime FutureDate = DateTime.Now.AddDays(3); // Initializes the value of FutureDate to
                                                       // 3 days after the current date
        string arrivalDate; // declares a variable arrivalDate as string
        string departDate; // declares a variable departDate as string
        public Form1()
        {
            InitializeComponent();
            arrivalDate = CurrentDate.ToString(@"MM\/dd\/yyyy"); // sets the value of arrivalDate to the current date
                                                                 // in the month/day/year format
            departDate = FutureDate.ToString(@"MM\/dd\/yyyy"); // sets the value of departDate to the current date
                                                               // in the month/day/year format
            txtArrivalDate.Text = arrivalDate; // assigns value of arrivalDate to arrival date textbox
            txtDepartureDate.Text = departDate; // assigns value of departDate to departure date textbox
        }

        private void btnCalculate_Click(object sender, EventArgs e) 
        {
        if (IsValidData())
            {
                DateTime arrive = Convert.ToDateTime(txtArrivalDate.Text); // converts user arrival date to DateTime and
                                                                            // stores the value in a arrive variable
                DateTime depart = Convert.ToDateTime(txtDepartureDate.Text); // converts user departure date to DateTime and
                                                                                // stores the value in a depart variable
                int nightsStayed = (depart.Subtract(arrive)).Days; // calculates the total number of nights stayed
                int nightlyPrice; // initializes a variable called nightlyPrice
                int totalPrice = 0; // initializes a variable called totalPrice with a value of 0
                DateTime NextDate = arrive; // initailizes a variable called NextDate with the value of the arrive variable


                int day = 0; // initializes a variable called day with a value of 0
                while (day < nightsStayed) // while loop that runs as long as the value of day is less than
                                            // the total number of nights stayed
                {
                    string dayOfWeek = NextDate.DayOfWeek.ToString(); // gets the day of the week from the NextDate variable
                    if (dayOfWeek == "Sunday" || dayOfWeek == "Monday" || dayOfWeek == "Tuesday"
                        || dayOfWeek == "Wednesday" || dayOfWeek == "Thursday") // runs if the day of the week is Sunday,
                                                                                // Monday, Tuesday, Wednesday or Thursday
                    {
                        nightlyPrice = 120; // sets the nightlyPrice as $120
                        day++; // increments the value of day by 1
                        NextDate = NextDate.AddDays(1); // increments the NextDate by 1 day
                    }
                    else // runs if the day of the week is Friday or Saturday
                    {
                        nightlyPrice = 150; // sets the nightlyPrice to $150
                        day++; // increments the value of day by 1
                        NextDate = NextDate.AddDays(1); // increments the value of NextDate by 1 day
                    }
                    totalPrice += nightlyPrice; // adds the nightlyPrice to the totalPrice
                }

                int avgPrice = totalPrice / nightsStayed; // calculates the average price based on the totalPrice
                                                            // and nightsStayed and stores it in a variable called avgPrice

                txtNights.Text = nightsStayed.ToString(); // assigns value of nightStayed to the number of nights text box
                txtTotalPrice.Text = totalPrice.ToString(); // assigns value of totalPrice to the total price text box
                txtAvgPrice.Text = avgPrice.ToString(); // assigns value of avgPrice to the avg price per night text box
            }
        }


        public bool IsValidData()
        {
            bool success = true;
            string errorMessage = "";

            // Validate the Arrival date text box
            errorMessage += IsPresent(txtArrivalDate.Text, "Arrival date");

            // Validate the Departure date text box
            errorMessage += IsPresent(txtDepartureDate.Text, "Departure date");
            errorMessage += IsDateTime(txtArrivalDate.Text, "Arrival Date");
            errorMessage += IsDateTime(txtDepartureDate.Text, "Departure Date");

            DateTime minDate = DateTime.Now.Date;
            DateTime maxDate = DateTime.Now.AddYears(5).Date;
            errorMessage += IsWithinDateRange(txtArrivalDate.Text, "Arrival Date", minDate, maxDate);
            errorMessage += IsWithinDateRange(txtDepartureDate.Text, "Departure Date", minDate, maxDate);
            errorMessage += IsLaterDate(txtArrivalDate.Text, "Arrival Date", txtDepartureDate.Text, "Departure Date");

            if (errorMessage != "")
            {
                success = false; 
                MessageBox.Show(errorMessage, "Entry Error");
            }
            return success;
        }

        public string IsPresent(string value, string name)
        {
            string msg = "";
            if (value == "")
            {
                msg = name + " is a required field.\n";
            }
            return msg;
        }

        public string IsDateTime(string value, string name)
        {
            string msg = "";

            DateTime DateInput;
            if (!DateTime.TryParse(value, out DateInput))
            {
                msg = "must enter valid Date";
            }
            return msg;

        }


        public string IsWithinDateRange(string value, string name,
            DateTime min, DateTime max)
        {
            string msg = "";
            DateTime DateInput;
            if (DateTime.TryParse(value, out DateInput))
            {
                if (DateInput < min || DateInput > max) 
                msg = " must be between " + min.ToShortDateString() +
                " and " + max.ToShortDateString();               
            }
            return msg;
        }

        public string IsLaterDate(string earlyValue, string earlyName,
            string laterValue, string laterName)
        {
            string msg = "";
            DateTime earlyDate, laterDate;
            if (DateTime.TryParse(earlyValue, out earlyDate) && DateTime.TryParse(laterValue, out laterDate))
            {
                if (laterDate <= earlyDate)
                {
                    msg = laterName + " must be after " + earlyName;
                }
            }
            return msg;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}