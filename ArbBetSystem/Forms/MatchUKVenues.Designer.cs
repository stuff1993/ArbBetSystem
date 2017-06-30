namespace ArbBetSystem.Forms {
    partial class MatchUKVenues {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.chkShowMatchedDO = new System.Windows.Forms.CheckBox();
            this.lstDoVenues = new System.Windows.Forms.ListBox();
            this.cmbBFVenues = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.chkShowMatchedBF = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkShowMatchedDO
            // 
            this.chkShowMatchedDO.AutoSize = true;
            this.chkShowMatchedDO.Location = new System.Drawing.Point(12, 178);
            this.chkShowMatchedDO.Name = "chkShowMatchedDO";
            this.chkShowMatchedDO.Size = new System.Drawing.Size(98, 17);
            this.chkShowMatchedDO.TabIndex = 0;
            this.chkShowMatchedDO.Text = "Show Matched";
            this.chkShowMatchedDO.UseVisualStyleBackColor = true;
            this.chkShowMatchedDO.CheckedChanged += new System.EventHandler(this.chkShowMatchedDO_CheckedChanged);
            // 
            // lstDoVenues
            // 
            this.lstDoVenues.FormattingEnabled = true;
            this.lstDoVenues.Location = new System.Drawing.Point(12, 12);
            this.lstDoVenues.Name = "lstDoVenues";
            this.lstDoVenues.Size = new System.Drawing.Size(133, 160);
            this.lstDoVenues.TabIndex = 1;
            this.lstDoVenues.SelectedIndexChanged += new System.EventHandler(this.lstDoVenues_SelectedIndexChanged);
            // 
            // cmbBFVenues
            // 
            this.cmbBFVenues.FormattingEnabled = true;
            this.cmbBFVenues.Location = new System.Drawing.Point(151, 12);
            this.cmbBFVenues.Name = "cmbBFVenues";
            this.cmbBFVenues.Size = new System.Drawing.Size(121, 21);
            this.cmbBFVenues.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(12, 201);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(197, 39);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(197, 201);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // chkShowMatchedBF
            // 
            this.chkShowMatchedBF.AutoSize = true;
            this.chkShowMatchedBF.Location = new System.Drawing.Point(174, 178);
            this.chkShowMatchedBF.Name = "chkShowMatchedBF";
            this.chkShowMatchedBF.Size = new System.Drawing.Size(98, 17);
            this.chkShowMatchedBF.TabIndex = 6;
            this.chkShowMatchedBF.Text = "Show Matched";
            this.chkShowMatchedBF.UseVisualStyleBackColor = true;
            this.chkShowMatchedBF.CheckedChanged += new System.EventHandler(this.chkShowMatchedBF_CheckedChanged);
            // 
            // MatchUKVenues
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 234);
            this.Controls.Add(this.chkShowMatchedBF);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cmbBFVenues);
            this.Controls.Add(this.lstDoVenues);
            this.Controls.Add(this.chkShowMatchedDO);
            this.Name = "MatchUKVenues";
            this.Text = "MatchUKVenues";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkShowMatchedDO;
        private System.Windows.Forms.ListBox lstDoVenues;
        private System.Windows.Forms.ComboBox cmbBFVenues;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.CheckBox chkShowMatchedBF;
    }
}