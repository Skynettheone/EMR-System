namespace EMR.User_Controls
{
    partial class medicalhistory
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
            searchfield = new Guna.UI2.WinForms.Guna2TextBox();
            searchbtn = new Guna.UI2.WinForms.Guna2Button();
            doctorDataGridView = new DataGridView();
            clearbtn = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)doctorDataGridView).BeginInit();
            SuspendLayout();
            // 
            // searchfield
            // 
            searchfield.BorderColor = Color.DodgerBlue;
            searchfield.BorderRadius = 10;
            searchfield.CustomizableEdges = customizableEdges1;
            searchfield.DefaultText = "";
            searchfield.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            searchfield.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            searchfield.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            searchfield.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            searchfield.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            searchfield.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            searchfield.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            searchfield.Location = new Point(78, 83);
            searchfield.Name = "searchfield";
            searchfield.PasswordChar = '\0';
            searchfield.PlaceholderText = "";
            searchfield.SelectedText = "";
            searchfield.ShadowDecoration.CustomizableEdges = customizableEdges2;
            searchfield.Size = new Size(272, 29);
            searchfield.TabIndex = 42;
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
            searchbtn.Location = new Point(369, 79);
            searchbtn.Name = "searchbtn";
            searchbtn.ShadowDecoration.CustomizableEdges = customizableEdges4;
            searchbtn.Size = new Size(150, 36);
            searchbtn.TabIndex = 41;
            searchbtn.Text = "Search";
            searchbtn.Click += searchbtn_Click;
            // 
            // doctorDataGridView
            // 
            doctorDataGridView.AllowUserToDeleteRows = false;
            doctorDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            doctorDataGridView.BackgroundColor = Color.White;
            doctorDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            doctorDataGridView.Location = new Point(78, 135);
            doctorDataGridView.Name = "doctorDataGridView";
            doctorDataGridView.ReadOnly = true;
            doctorDataGridView.RowTemplate.Height = 25;
            doctorDataGridView.Size = new Size(754, 340);
            doctorDataGridView.TabIndex = 40;
            // 
            // clearbtn
            // 
            clearbtn.BackColor = Color.Transparent;
            clearbtn.BorderRadius = 15;
            clearbtn.CustomizableEdges = customizableEdges5;
            clearbtn.DisabledState.BorderColor = Color.DarkGray;
            clearbtn.DisabledState.CustomBorderColor = Color.DarkGray;
            clearbtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            clearbtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            clearbtn.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            clearbtn.ForeColor = Color.White;
            clearbtn.Location = new Point(525, 79);
            clearbtn.Name = "clearbtn";
            clearbtn.ShadowDecoration.CustomizableEdges = customizableEdges6;
            clearbtn.Size = new Size(150, 36);
            clearbtn.TabIndex = 43;
            clearbtn.Text = "Clear";
            clearbtn.Click += clearbtn_Click;
            // 
            // medicalhistory
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(clearbtn);
            Controls.Add(searchfield);
            Controls.Add(searchbtn);
            Controls.Add(doctorDataGridView);
            Name = "medicalhistory";
            Size = new Size(857, 521);
            ((System.ComponentModel.ISupportInitialize)doctorDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2TextBox searchfield;
        private Guna.UI2.WinForms.Guna2Button searchbtn;
        private DataGridView doctorDataGridView;
        private Guna.UI2.WinForms.Guna2Button clearbtn;
    }
}
