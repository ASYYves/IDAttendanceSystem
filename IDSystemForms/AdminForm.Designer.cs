namespace WinFormsApp1
{
    partial class AdminForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            cmbStartAmPm = new ComboBox();
            cmbEndAmPm = new ComboBox();
            tbxAddID = new TextBox();
            tbxAddName = new TextBox();
            cmbSchedule = new ComboBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnBack = new Button();
            label7 = new Label();
            cmbScheduleToUpdate = new ComboBox();
            label8 = new Label();
            lbxAllIDs = new ListBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 15F);
            label1.Location = new Point(44, 171);
            label1.Name = "label1";
            label1.Size = new Size(152, 28);
            label1.TabIndex = 0;
            label1.Text = "New Student ID:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 15F);
            label2.Location = new Point(84, 347);
            label2.Name = "label2";
            label2.Size = new Size(104, 28);
            label2.TabIndex = 1;
            label2.Text = "Start Time:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 15F);
            label3.Location = new Point(92, 405);
            label3.Name = "label3";
            label3.Size = new Size(96, 28);
            label3.TabIndex = 2;
            label3.Text = "End Time:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe UI", 15F);
            label4.Location = new Point(54, 285);
            label4.Name = "label4";
            label4.Size = new Size(134, 28);
            label4.TabIndex = 3;
            label4.Text = "Schedule Day:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Segoe UI", 15F);
            label5.Location = new Point(479, 180);
            label5.Name = "label5";
            label5.Size = new Size(125, 28);
            label5.TabIndex = 4;
            label5.Text = "List of all IDs:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Segoe UI", 15F);
            label6.Location = new Point(128, 228);
            label6.Name = "label6";
            label6.Size = new Size(68, 28);
            label6.TabIndex = 5;
            label6.Text = "Name:";
            // 
            // cmbStartAmPm
            // 
            cmbStartAmPm.Font = new Font("Segoe UI", 15F);
            cmbStartAmPm.FormattingEnabled = true;
            cmbStartAmPm.Location = new Point(202, 339);
            cmbStartAmPm.Name = "cmbStartAmPm";
            cmbStartAmPm.Size = new Size(128, 36);
            cmbStartAmPm.TabIndex = 7;
            // 
            // cmbEndAmPm
            // 
            cmbEndAmPm.Font = new Font("Segoe UI", 15F);
            cmbEndAmPm.FormattingEnabled = true;
            cmbEndAmPm.Location = new Point(202, 397);
            cmbEndAmPm.Name = "cmbEndAmPm";
            cmbEndAmPm.Size = new Size(128, 36);
            cmbEndAmPm.TabIndex = 8;
            // 
            // tbxAddID
            // 
            tbxAddID.Font = new Font("Segoe UI", 15F);
            tbxAddID.ForeColor = SystemColors.WindowText;
            tbxAddID.ImeMode = ImeMode.NoControl;
            tbxAddID.Location = new Point(202, 165);
            tbxAddID.Name = "tbxAddID";
            tbxAddID.Size = new Size(241, 34);
            tbxAddID.TabIndex = 9;
            // 
            // tbxAddName
            // 
            tbxAddName.Font = new Font("Segoe UI", 15F);
            tbxAddName.Location = new Point(202, 222);
            tbxAddName.Name = "tbxAddName";
            tbxAddName.Size = new Size(241, 34);
            tbxAddName.TabIndex = 10;
            // 
            // cmbSchedule
            // 
            cmbSchedule.Font = new Font("Segoe UI", 15F);
            cmbSchedule.FormattingEnabled = true;
            cmbSchedule.Location = new Point(202, 277);
            cmbSchedule.Name = "cmbSchedule";
            cmbSchedule.Size = new Size(106, 36);
            cmbSchedule.TabIndex = 15;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(499, 529);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 23);
            btnAdd.TabIndex = 17;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(596, 529);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 18;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(689, 529);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 19;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(678, 638);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(75, 23);
            btnBack.TabIndex = 20;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Segoe UI", 15F);
            label7.Location = new Point(62, 279);
            label7.Name = "label7";
            label7.Size = new Size(0, 28);
            label7.TabIndex = 23;
            // 
            // cmbScheduleToUpdate
            // 
            cmbScheduleToUpdate.Font = new Font("Segoe UI", 15F);
            cmbScheduleToUpdate.FormattingEnabled = true;
            cmbScheduleToUpdate.Location = new Point(294, 481);
            cmbScheduleToUpdate.Name = "cmbScheduleToUpdate";
            cmbScheduleToUpdate.Size = new Size(106, 36);
            cmbScheduleToUpdate.TabIndex = 25;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.Transparent;
            label8.Font = new Font("Segoe UI", 15F);
            label8.Location = new Point(54, 489);
            label8.Name = "label8";
            label8.Size = new Size(229, 28);
            label8.TabIndex = 24;
            label8.Text = "Schedule Day To Update:";
            // 
            // lbxAllIDs
            // 
            lbxAllIDs.FormattingEnabled = true;
            lbxAllIDs.ItemHeight = 15;
            lbxAllIDs.Location = new Point(479, 228);
            lbxAllIDs.Name = "lbxAllIDs";
            lbxAllIDs.Size = new Size(259, 289);
            lbxAllIDs.TabIndex = 26;
            // 
            // AdminForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = IDSystemForms.Properties.Resources.Student_Name__1_;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(787, 673);
            Controls.Add(lbxAllIDs);
            Controls.Add(cmbScheduleToUpdate);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(btnBack);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(cmbSchedule);
            Controls.Add(tbxAddName);
            Controls.Add(tbxAddID);
            Controls.Add(cmbEndAmPm);
            Controls.Add(cmbStartAmPm);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "AdminForm";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Admin";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private ComboBox cmbStartAmPm;
        private ComboBox cmbEndAmPm;
        private TextBox tbxAddID;
        private TextBox tbxAddName;
        private ComboBox cmbSchedule;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnBack;
        private Label label7;
        private ComboBox cmbScheduleToUpdate;
        private Label label8;
        private ListBox lbxAllIDs;
    }

}
