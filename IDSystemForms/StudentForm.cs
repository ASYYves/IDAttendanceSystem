using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using IDSystemBusinessLogic;
using IDSystemData;

namespace WinFormsApp1
{

    public partial class StudentForm : Form
    {


        public StudentForm()
        {


            InitializeComponent();

            
            btnEnter.Click += btnEnterClick;
            tbxEnterID.KeyDown += tbxEneterIDKeyDown;


        }




        
        private void btnEnterClick(object sender, EventArgs e)
        {

            string input = tbxEnterID.Text.Trim();

            //exit
            if (input.Equals("ex", StringComparison.OrdinalIgnoreCase))
            {


                Application.Exit();
                return;


            }


            //for admin
            if (input.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {


                clearAllFields();
                using (var adminForm = new AdminForm())
                {


                    adminForm.ShowDialog();


                }


                tbxEnterID.Clear();
                return;


            }


            //id checking
            if (!Checking.checkId(input))
            {


                clearAllFields();
                tbxStudentName.Text = "ID not in System";
                tbxEnterID.Clear();
                return;


            }

            
            Checking.setCurrentID(input);
            displayStudentInfos();
            tbxEnterID.Clear();


        }

        //for Enter key in ID box
        private void tbxEneterIDKeyDown(object sender, KeyEventArgs e)
        {


            if (e.KeyCode == Keys.Enter)
            {


                e.Handled = true;
                btnEnterClick(this, EventArgs.Empty);


            }


        }



        //clears all student fields
        private void clearAllFields()
        {


            tbxStudentName.Clear();
            tbxInOrOut.Clear();
            tbxSchedule.Clear();
            tbxLates.Clear();
            tbxAbsents.Clear();


        }

        //display student details
        private void displayStudentInfos()
        {

            clearAllFields();


            
            string name = Checking.getStudentName();
            tbxStudentName.Text = name;


            //in or out
            string status = Checking.InOrOut();
            tbxInOrOut.Text = status.StartsWith("in", StringComparison.OrdinalIgnoreCase) ? "IN" : "OUT";


            //schedule for today
            string rawSched = Checking.getSchedule();
            bool hasToday = rawSched.Contains(DateTime.Today.DayOfWeek.ToString(), StringComparison.OrdinalIgnoreCase);
            tbxSchedule.Text = (hasToday ? rawSched : "You have no scheduled class today.");

            // Lates & Absents
            var (lates, abs) = Checking.getRecord();
            tbxLates.Text = lates.ToString();
            tbxAbsents.Text = abs.ToString();
        }

        
    }
}
