namespace EMR.User_Controls
{
    partial class p_appointment
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            datePickersearch = new Guna.UI2.WinForms.Guna2DateTimePicker();
            searchbtn = new Guna.UI2.WinForms.Guna2Button();
            appointmentrecorddatagridview = new DataGridView();
            appointmentcount = new Label();
            Clearbtn = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)appointmentrecorddatagridview).BeginInit();
            SuspendLayout();
            // 
            // datePickersearch
            // 
            datePickersearch.BackColor = Color.Transparent;
            datePickersearch.BorderRadius = 15;
            datePickersearch.Checked = true;
            datePickersearch.CustomizableEdges = customizableEdges1;
            datePickersearch.FillColor = Color.White;
            datePickersearch.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            datePickersearch.Format = DateTimePickerFormat.Short;
            datePickersearch.Location = new Point(120, 84);
            datePickersearch.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            datePickersearch.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            datePickersearch.Name = "datePickersearch";
            datePickersearch.ShadowDecoration.CustomizableEdges = customizableEdges2;
            datePickersearch.Size = new Size(200, 36);
            datePickersearch.TabIndex = 0;
            datePickersearch.Value = new DateTime(2023, 8, 30, 9, 1, 50, 651);
            // 
            // searchbtn
            // 
            searchbtn.BackColor = Color.Transparent;
            searchbtn.BorderRadius = 15;
            searchbtn.CustomizableEdges = customizableEdges3;
            searchbtn.DisabledState.BorderColor = Color.DarkGray;
            searchbtn.DisabledState.CustomBorderColor = Color.DarkGray;
            searchbtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            searchbtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            searchbtn.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            searchbtn.ForeColor = Color.White;
            searchbtn.Location = new Point(326, 84);
            searchbtn.Name = "searchbtn";
            searchbtn.ShadowDecoration.CustomizableEdges = customizableEdges4;
            searchbtn.Size = new Size(150, 36);
            searchbtn.TabIndex = 38;
            searchbtn.Text = "Search";
            searchbtn.Click += searchbtn_Click;
            // 
            // appointmentrecorddatagridview
            // 
            appointmentrecorddatagridview.AllowUserToDeleteRows = false;
            appointmentrecorddatagridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            appointmentrecorddatagridview.BackgroundColor = Color.White;
            appointmentrecorddatagridview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            appointmentrecorddatagridview.Location = new Point(120, 135);
            appointmentrecorddatagridview.Name = "appointmentrecorddatagridview";
            appointmentrecorddatagridview.ReadOnly = true;
            appointmentrecorddatagridview.RowTemplate.Height = 25;
            appointmentrecorddatagridview.Size = new Size(707, 305);
            appointmentrecorddatagridview.TabIndex = 37;
            // 
            // appointmentcount
            // 
            appointmentcount.AutoSize = true;
            appointmentcount.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            appointmentcount.Location = new Point(503, 30);
            appointmentcount.Name = "appointmentcount";
            appointmentcount.Size = new Size(129, 20);
            appointmentcount.TabIndex = 39;
            appointmentcount.Text = "Appoinmentcount";
            // 
            // Clearbtn
            // 
            Clearbtn.BackColor = Color.Transparent;
            Clearbtn.BorderRadius = 15;
            Clearbtn.CustomizableEdges = customizableEdges5;
            Clearbtn.DisabledState.BorderColor = Color.DarkGray;
            Clearbtn.DisabledState.CustomBorderColor = Color.DarkGray;
            Clearbtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            Clearbtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            Clearbtn.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            Clearbtn.ForeColor = Color.White;
            Clearbtn.Location = new Point(482, 84);
            Clearbtn.Name = "Clearbtn";
            Clearbtn.ShadowDecoration.CustomizableEdges = customizableEdges6;
            Clearbtn.Size = new Size(150, 36);
            Clearbtn.TabIndex = 40;
            Clearbtn.Text = "Clear";
            Clearbtn.Click += Clearbtn_Click;
            // 
            // p_appointment
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(Clearbtn);
            Controls.Add(appointmentcount);
            Controls.Add(searchbtn);
            Controls.Add(appointmentrecorddatagridview);
            Controls.Add(datePickersearch);
            Name = "p_appointment";
            Size = new Size(864, 493);
            Load += p_appointment_Load;
            ((System.ComponentModel.ISupportInitialize)appointmentrecorddatagridview).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2DateTimePicker datePickersearch;
        private Guna.UI2.WinForms.Guna2Button searchbtn;
        private DataGridView appointmentrecorddatagridview;
        private Label appointmentcount;
        private Guna.UI2.WinForms.Guna2Button Clearbtn;
    }
}
