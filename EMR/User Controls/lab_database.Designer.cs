namespace EMR.User_Controls
{
    partial class lab_database
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
            searchfield = new Guna.UI2.WinForms.Guna2TextBox();
            searchbtn = new Guna.UI2.WinForms.Guna2Button();
            labdatabasegridview = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)labdatabasegridview).BeginInit();
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
            searchfield.Location = new Point(90, 61);
            searchfield.Name = "searchfield";
            searchfield.PasswordChar = '\0';
            searchfield.PlaceholderText = "";
            searchfield.SelectedText = "";
            searchfield.ShadowDecoration.CustomizableEdges = customizableEdges2;
            searchfield.Size = new Size(272, 29);
            searchfield.TabIndex = 39;
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
            searchbtn.Location = new Point(381, 57);
            searchbtn.Name = "searchbtn";
            searchbtn.ShadowDecoration.CustomizableEdges = customizableEdges4;
            searchbtn.Size = new Size(150, 36);
            searchbtn.TabIndex = 38;
            searchbtn.Text = "Search";
            searchbtn.Click += searchbtn_Click;
            // 
            // labdatabasegridview
            // 
            labdatabasegridview.AllowUserToDeleteRows = false;
            labdatabasegridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            labdatabasegridview.BackgroundColor = Color.White;
            labdatabasegridview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            labdatabasegridview.Location = new Point(90, 113);
            labdatabasegridview.Name = "labdatabasegridview";
            labdatabasegridview.ReadOnly = true;
            labdatabasegridview.RowTemplate.Height = 25;
            labdatabasegridview.Size = new Size(758, 363);
            labdatabasegridview.TabIndex = 37;
            labdatabasegridview.CellClick += labdatabasegridview_CellClick_1;
            // 
            // lab_database
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(searchfield);
            Controls.Add(searchbtn);
            Controls.Add(labdatabasegridview);
            Name = "lab_database";
            Size = new Size(886, 545);
            ((System.ComponentModel.ISupportInitialize)labdatabasegridview).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2TextBox searchfield;
        private Guna.UI2.WinForms.Guna2Button searchbtn;
        private DataGridView labdatabasegridview;
    }
}
