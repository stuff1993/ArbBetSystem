namespace ArbBetSystem
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeCredentialsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forceLoginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateMeetingListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lvwMeetings = new System.Windows.Forms.ListView();
            this.clmMeeting = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwEvents = new System.Windows.Forms.ListView();
            this.clmEvent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwRunners = new System.Windows.Forms.ListView();
            this.clmName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmJockey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmTrainer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem,
            this.dataToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(750, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeCredentialsToolStripMenuItem,
            this.forceLoginToolStripMenuItem});
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.loginToolStripMenuItem.Text = "Login";
            // 
            // changeCredentialsToolStripMenuItem
            // 
            this.changeCredentialsToolStripMenuItem.Name = "changeCredentialsToolStripMenuItem";
            this.changeCredentialsToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.changeCredentialsToolStripMenuItem.Text = "Change Credentials";
            this.changeCredentialsToolStripMenuItem.Click += new System.EventHandler(this.changeCredentialsToolStripMenuItem_Click);
            // 
            // forceLoginToolStripMenuItem
            // 
            this.forceLoginToolStripMenuItem.Name = "forceLoginToolStripMenuItem";
            this.forceLoginToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.forceLoginToolStripMenuItem.Text = "Force Login";
            this.forceLoginToolStripMenuItem.Click += new System.EventHandler(this.forceLoginToolStripMenuItem_Click);
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.updateMeetingListToolStripMenuItem});
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.dataToolStripMenuItem.Text = "Data";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // updateMeetingListToolStripMenuItem
            // 
            this.updateMeetingListToolStripMenuItem.Name = "updateMeetingListToolStripMenuItem";
            this.updateMeetingListToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.updateMeetingListToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.updateMeetingListToolStripMenuItem.Text = "Update Meeting List";
            this.updateMeetingListToolStripMenuItem.Click += new System.EventHandler(this.updateMeetingListToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Meetings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(362, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Events";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 223);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Runners";
            // 
            // lvwMeetings
            // 
            this.lvwMeetings.CheckBoxes = true;
            this.lvwMeetings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmMeeting});
            this.lvwMeetings.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvwMeetings.HideSelection = false;
            this.lvwMeetings.LabelWrap = false;
            this.lvwMeetings.Location = new System.Drawing.Point(12, 43);
            this.lvwMeetings.MultiSelect = false;
            this.lvwMeetings.Name = "lvwMeetings";
            this.lvwMeetings.Size = new System.Drawing.Size(347, 177);
            this.lvwMeetings.TabIndex = 7;
            this.lvwMeetings.UseCompatibleStateImageBehavior = false;
            this.lvwMeetings.View = System.Windows.Forms.View.Details;
            this.lvwMeetings.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvwMeetings_ItemChecked);
            this.lvwMeetings.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvwMeetings_ItemSelectionChanged);
            // 
            // clmMeeting
            // 
            this.clmMeeting.Text = "Meeting";
            // 
            // lvwEvents
            // 
            this.lvwEvents.CheckBoxes = true;
            this.lvwEvents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmEvent});
            this.lvwEvents.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvwEvents.HideSelection = false;
            this.lvwEvents.LabelWrap = false;
            this.lvwEvents.Location = new System.Drawing.Point(365, 43);
            this.lvwEvents.Name = "lvwEvents";
            this.lvwEvents.Size = new System.Drawing.Size(373, 177);
            this.lvwEvents.TabIndex = 8;
            this.lvwEvents.UseCompatibleStateImageBehavior = false;
            this.lvwEvents.View = System.Windows.Forms.View.Details;
            this.lvwEvents.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvwEvents_ItemChecked);
            this.lvwEvents.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvwEvents_ItemSelectionChanged);
            // 
            // clmEvent
            // 
            this.clmEvent.Text = "Event";
            // 
            // lvwRunners
            // 
            this.lvwRunners.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmNo,
            this.clmName,
            this.clmJockey,
            this.clmTrainer});
            this.lvwRunners.FullRowSelect = true;
            this.lvwRunners.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwRunners.HideSelection = false;
            this.lvwRunners.LabelWrap = false;
            this.lvwRunners.Location = new System.Drawing.Point(12, 239);
            this.lvwRunners.Name = "lvwRunners";
            this.lvwRunners.Size = new System.Drawing.Size(726, 249);
            this.lvwRunners.TabIndex = 9;
            this.lvwRunners.UseCompatibleStateImageBehavior = false;
            this.lvwRunners.View = System.Windows.Forms.View.Details;
            this.lvwRunners.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvwRunners_ItemSelectionChanged);
            // 
            // clmName
            // 
            this.clmName.Text = "Name";
            // 
            // clmNo
            // 
            this.clmNo.DisplayIndex = 1;
            this.clmNo.Text = "No";
            // 
            // clmJockey
            // 
            this.clmJockey.Text = "Jockey";
            // 
            // clmTrainer
            // 
            this.clmTrainer.Text = "Trainer";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 500);
            this.Controls.Add(this.lvwRunners);
            this.Controls.Add(this.lvwEvents);
            this.Controls.Add(this.lvwMeetings);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeCredentialsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateMeetingListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem forceLoginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView lvwMeetings;
        private System.Windows.Forms.ColumnHeader clmMeeting;
        private System.Windows.Forms.ListView lvwEvents;
        private System.Windows.Forms.ColumnHeader clmEvent;
        private System.Windows.Forms.ListView lvwRunners;
        private System.Windows.Forms.ColumnHeader clmName;
        private System.Windows.Forms.ColumnHeader clmNo;
        private System.Windows.Forms.ColumnHeader clmJockey;
        private System.Windows.Forms.ColumnHeader clmTrainer;
    }
}

