namespace WinFormsApp1
{
    partial class StudentForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            tbxEnterID = new TextBox();
            tbxStudentName = new TextBox();
            tbxInOrOut = new TextBox();
            tbxLates = new TextBox();
            tbxAbsents = new TextBox();
            btnEnter = new Button();
            tbxSchedule = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(189, 285);
            label1.Name = "label1";
            label1.Size = new Size(68, 21);
            label1.TabIndex = 0;
            label1.Text = "Enter ID:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(183, 330);
            label2.Name = "label2";
            label2.Size = new Size(74, 21);
            label2.TabIndex = 1;
            label2.Text = "Welcome";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(161, 390);
            label3.Name = "label3";
            label3.Size = new Size(96, 21);
            label3.TabIndex = 2;
            label3.Text = "You are now";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(189, 452);
            label4.Name = "label4";
            label4.Size = new Size(68, 21);
            label4.TabIndex = 3;
            label4.Text = "Schdule:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(565, 364);
            label5.Name = "label5";
            label5.Size = new Size(49, 21);
            label5.TabIndex = 4;
            label5.Text = "Lates:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Segoe UI", 12F);
            label6.Location = new Point(546, 428);
            label6.Name = "label6";
            label6.Size = new Size(68, 21);
            label6.TabIndex = 5;
            label6.Text = "Absents:";
            // 
            // tbxEnterID
            // 
            tbxEnterID.Font = new Font("Segoe UI", 12F);
            tbxEnterID.Location = new Point(263, 277);
            tbxEnterID.Name = "tbxEnterID";
            tbxEnterID.Size = new Size(199, 29);
            tbxEnterID.TabIndex = 6;
            // 
            // tbxStudentName
            // 
            tbxStudentName.Font = new Font("Segoe UI", 12F);
            tbxStudentName.Location = new Point(263, 322);
            tbxStudentName.Name = "tbxStudentName";
            tbxStudentName.Size = new Size(199, 29);
            tbxStudentName.TabIndex = 7;
            // 
            // tbxInOrOut
            // 
            tbxInOrOut.Font = new Font("Segoe UI", 12F);
            tbxInOrOut.Location = new Point(263, 382);
            tbxInOrOut.Name = "tbxInOrOut";
            tbxInOrOut.Size = new Size(199, 29);
            tbxInOrOut.TabIndex = 8;
            // 
            // tbxLates
            // 
            tbxLates.Font = new Font("Segoe UI", 12F);
            tbxLates.Location = new Point(620, 356);
            tbxLates.Name = "tbxLates";
            tbxLates.Size = new Size(90, 29);
            tbxLates.TabIndex = 9;
            // 
            // tbxAbsents
            // 
            tbxAbsents.Font = new Font("Segoe UI", 12F);
            tbxAbsents.Location = new Point(620, 420);
            tbxAbsents.Name = "tbxAbsents";
            tbxAbsents.Size = new Size(90, 29);
            tbxAbsents.TabIndex = 10;
            // 
            // btnEnter
            // 
            btnEnter.Font = new Font("Segoe UI", 12F);
            btnEnter.Location = new Point(507, 277);
            btnEnter.Name = "btnEnter";
            btnEnter.Size = new Size(93, 29);
            btnEnter.TabIndex = 11;
            btnEnter.Text = "Enter";
            btnEnter.UseVisualStyleBackColor = true;
            // 
            // tbxSchedule
            // 
            tbxSchedule.Location = new Point(263, 452);
            tbxSchedule.Multiline = true;
            tbxSchedule.Name = "tbxSchedule";
            tbxSchedule.Size = new Size(250, 196);
            tbxSchedule.TabIndex = 12;
            // 
            // StudentForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            BackgroundImage = IDSystemForms.Properties.Resources.Student_Name;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(787, 673);
            Controls.Add(tbxSchedule);
            Controls.Add(btnEnter);
            Controls.Add(tbxAbsents);
            Controls.Add(tbxLates);
            Controls.Add(tbxInOrOut);
            Controls.Add(tbxStudentName);
            Controls.Add(tbxEnterID);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "StudentForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "StudentForm";
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
        private TextBox tbxEnterID;
        private TextBox tbxStudentName;
        private TextBox tbxInOrOut;
        private TextBox tbxLates;
        private TextBox tbxAbsents;
        private Button btnEnter;
        private TextBox tbxSchedule;
    
}
}