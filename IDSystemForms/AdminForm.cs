using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using IDSystemBusinessLogic;
using IDSystemData;

namespace WinFormsApp1
{

    public partial class AdminForm : Form
    {


        public AdminForm()
        {


            InitializeComponent();
            buttonEvents();
            addDaysToCMB();
            refreshLBX();


            lbxAllIDs.SelectedIndexChanged += lbxListSelectedStudent;
        }



        private void buttonEvents()
        {
            btnAdd.Click += btnAddOnClick;
            btnUpdate.Click += btnUpdateOnClick;
            btnDelete.Click += tnDeleteOnClick;
            btnBack.Click += (_, __) => Close();
        }



        private void addItemsToCMB()
        {


            var days = new[] {
                "Monday","Tuesday","Wednesday", "Thursday","Friday","Saturday","Sunday"
            };


            cmbSchedule.Items.AddRange(days);
            

        }



        private void addDaysToCMB()
        {
            

            var days = new[] {
                "Monday","Tuesday","Wednesday", "Thursday","Friday","Saturday","Sunday"
            };


            cmbSchedule.Items.AddRange(days);
            

            //7AM to 8PM in 30-min increments
            var times = new List<string>();
            DateTime time = DateTime.Today.AddHours(7);
            while (time.TimeOfDay <= TimeSpan.FromHours(20))
            {


                times.Add(time.ToString("h:mm tt"));
                time = time.AddMinutes(30);


            }


            cmbStartAmPm.Items.AddRange(times.ToArray());
            cmbEndAmPm.Items.AddRange(times.ToArray());


        }



        private void refreshLBX()
        {
            lbxAllIDs.Items.Clear();
            foreach (var (id, name) in Checking.AdminListAll())
                lbxAllIDs.Items.Add($"{id} : {name}");
        }



        private void btnAddOnClick(object sender, EventArgs e)
        {


            string id = tbxAddID.Text.Trim();
            string name = tbxAddName.Text.Trim();


            if (id == "" || name == "")
            {


                MessageBox.Show("ID and Name are required.");
                return;


            }


            if (cmbSchedule.SelectedItem is null || cmbStartAmPm.SelectedItem is null || cmbEndAmPm.SelectedItem is null)
            {


                MessageBox.Show("Pick day, start and end times.");
                return;


            }

            var sched = new List<string> {$"{cmbSchedule.Text}: {cmbStartAmPm.Text} - {cmbEndAmPm.Text}"};


            bool ifAddisOK = Checking.adminAdd(id, name, sched);
            MessageBox.Show(ifAddisOK ? "Student added." : "Add failed (duplicate ID?).");


            if (ifAddisOK) refreshLBX();


            setFeildsToBlank();


        }



        private void btnUpdateOnClick(object sender, EventArgs e)
        {


            if (lbxAllIDs.SelectedItem is null)
            {


                MessageBox.Show("Select a student first.");
                return;


            }


            if (cmbScheduleToUpdate.SelectedItem is null || cmbStartAmPm.SelectedItem is null || cmbEndAmPm.SelectedItem is null)
            {


                MessageBox.Show("Pick a day, start and end time.");
                return;


            }


            //extract id on list
            string id = lbxAllIDs.SelectedItem.ToString().Split(':')[0].Trim();


            //build new schedule list for just this one entry
            string day = cmbScheduleToUpdate.Text;
            var newSched = new List<string> {$"{day}: {cmbStartAmPm.Text} - {cmbEndAmPm.Text}"};


            bool ifUpadateIsOK = Checking.adminUpdate(id, newSched);
            MessageBox.Show(ifUpadateIsOK ? "Schedule updated." : "Update failed.");

        
        }



        private void tnDeleteOnClick(object sender, EventArgs e)
        {


            if (lbxAllIDs.SelectedItem is null)
            {


                MessageBox.Show("Select a student to delete.");
                return;


            }


            var sel = lbxAllIDs.SelectedItem.ToString();
            var id = sel.Split(':')[0].Trim();


            bool ifDeleteIsOK = Checking.adminDelete(id);
            MessageBox.Show(ifDeleteIsOK  ? "Student deleted."  : "Delete failed.");

            if (ifDeleteIsOK) refreshLBX();


        }



        private void setFeildsToBlank()
        {


            tbxAddID.Clear();
            tbxAddName.Clear();
            cmbSchedule.SelectedIndex = -1;
            cmbStartAmPm.SelectedIndex = -1;
            cmbEndAmPm.SelectedIndex = -1;


        }



        private void lbxListSelectedStudent(object sender, EventArgs e)
        {


            cmbScheduleToUpdate.Items.Clear();
            if (lbxAllIDs.SelectedItem is null) return;


            //parse out the student ID from "ID : Name"
            string id = lbxAllIDs.SelectedItem.ToString().Split(':')[0].Trim();


            
            Checking.setCurrentID(id);


            //gwt schedule for this student
            string getsched = Checking.getSchedule();


            //extract each entry, ignore blank lines or headers
            var sched = getsched.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(lines => lines.Trim()).Where(l => !string.IsNullOrWhiteSpace(l)).Where(l => !l.StartsWith("---")).ToArray();


            //pull the day from each "Day: time - time"
            foreach (var line in sched)
            {


                var day = line.Split(':', 2)[0].Trim();
                if (!cmbScheduleToUpdate.Items.Contains(day))
                    cmbScheduleToUpdate.Items.Add(day);


            }

            //clear any previous selection
            cmbScheduleToUpdate.SelectedIndex = -1;


        }

        
    }


}
