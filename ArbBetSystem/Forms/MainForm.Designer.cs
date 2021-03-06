﻿using System.Collections.Generic;

namespace ArbBetSystem.Forms {
    partial class MainForm {
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
            if (dynOdds != null)
                dynOdds.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeDOLoginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeBFLoginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forceDOLoginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forceBFLoginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateMeetingListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.capOddsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.matchUKVenuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.meetingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.racingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.harnessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.greyhoundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.pickTimeZoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvMeetings = new System.Windows.Forms.DataGridView();
            this.meetCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.meetVenue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.meetCountry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.meetType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvEvents = new System.Windows.Forms.DataGridView();
            this.dgvRunners = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.evtCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.evtName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.evtStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.evtAddDay = new System.Windows.Forms.DataGridViewButtonColumn();
            this.evtSubDay = new System.Windows.Forms.DataGridViewButtonColumn();
            this.evtMatched = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMeetings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEvents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRunners)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem,
            this.dataToolStripMenuItem,
            this.meetingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(990, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeDOLoginToolStripMenuItem,
            this.changeBFLoginToolStripMenuItem,
            this.forceDOLoginToolStripMenuItem,
            this.forceBFLoginToolStripMenuItem});
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.loginToolStripMenuItem.Text = "Login";
            // 
            // changeDOLoginToolStripMenuItem
            // 
            this.changeDOLoginToolStripMenuItem.Name = "changeDOLoginToolStripMenuItem";
            this.changeDOLoginToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.changeDOLoginToolStripMenuItem.Text = "Change DO Login";
            this.changeDOLoginToolStripMenuItem.Click += new System.EventHandler(this.changeDOLoginToolStripMenuItem_Click);
            // 
            // changeBFLoginToolStripMenuItem
            // 
            this.changeBFLoginToolStripMenuItem.Name = "changeBFLoginToolStripMenuItem";
            this.changeBFLoginToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.changeBFLoginToolStripMenuItem.Text = "Change BF Login";
            this.changeBFLoginToolStripMenuItem.Click += new System.EventHandler(this.changeBFLoginToolStripMenuItem_Click);
            // 
            // forceDOLoginToolStripMenuItem
            // 
            this.forceDOLoginToolStripMenuItem.Name = "forceDOLoginToolStripMenuItem";
            this.forceDOLoginToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.forceDOLoginToolStripMenuItem.Text = "Force DO Login";
            this.forceDOLoginToolStripMenuItem.Click += new System.EventHandler(this.forceDOLoginToolStripMenuItem_Click);
            // 
            // forceBFLoginToolStripMenuItem
            // 
            this.forceBFLoginToolStripMenuItem.Name = "forceBFLoginToolStripMenuItem";
            this.forceBFLoginToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.forceBFLoginToolStripMenuItem.Text = "Force BF Login";
            this.forceBFLoginToolStripMenuItem.Click += new System.EventHandler(this.forceBFLoginToolStripMenuItem_Click);
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.updateMeetingListToolStripMenuItem,
            this.capOddsToolStripMenuItem,
            this.matchUKVenuesToolStripMenuItem});
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.dataToolStripMenuItem.Text = "Data";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.startToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Enabled = false;
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // updateMeetingListToolStripMenuItem
            // 
            this.updateMeetingListToolStripMenuItem.Name = "updateMeetingListToolStripMenuItem";
            this.updateMeetingListToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.updateMeetingListToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.updateMeetingListToolStripMenuItem.Text = "Update Meeting List";
            this.updateMeetingListToolStripMenuItem.Click += new System.EventHandler(this.updateMeetingListToolStripMenuItem_Click);
            // 
            // capOddsToolStripMenuItem
            // 
            this.capOddsToolStripMenuItem.CheckOnClick = true;
            this.capOddsToolStripMenuItem.Name = "capOddsToolStripMenuItem";
            this.capOddsToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.capOddsToolStripMenuItem.Text = "Cap Odds";
            this.capOddsToolStripMenuItem.Click += new System.EventHandler(this.capOddsToolStripMenuItem_Click);
            // 
            // matchUKVenuesToolStripMenuItem
            // 
            this.matchUKVenuesToolStripMenuItem.Name = "matchUKVenuesToolStripMenuItem";
            this.matchUKVenuesToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.matchUKVenuesToolStripMenuItem.Text = "Match UK Venues";
            this.matchUKVenuesToolStripMenuItem.Click += new System.EventHandler(this.matchUKVenuesToolStripMenuItem_Click);
            // 
            // meetingsToolStripMenuItem
            // 
            this.meetingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.racingToolStripMenuItem,
            this.harnessToolStripMenuItem,
            this.greyhoundToolStripMenuItem,
            this.toolStripSeparator1,
            this.pickTimeZoneToolStripMenuItem});
            this.meetingsToolStripMenuItem.Name = "meetingsToolStripMenuItem";
            this.meetingsToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.meetingsToolStripMenuItem.Text = "Meetings";
            // 
            // racingToolStripMenuItem
            // 
            this.racingToolStripMenuItem.CheckOnClick = true;
            this.racingToolStripMenuItem.Name = "racingToolStripMenuItem";
            this.racingToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.racingToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.racingToolStripMenuItem.Text = "Racing";
            // 
            // harnessToolStripMenuItem
            // 
            this.harnessToolStripMenuItem.CheckOnClick = true;
            this.harnessToolStripMenuItem.Name = "harnessToolStripMenuItem";
            this.harnessToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.harnessToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.harnessToolStripMenuItem.Text = "Harness";
            // 
            // greyhoundToolStripMenuItem
            // 
            this.greyhoundToolStripMenuItem.CheckOnClick = true;
            this.greyhoundToolStripMenuItem.Name = "greyhoundToolStripMenuItem";
            this.greyhoundToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.greyhoundToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.greyhoundToolStripMenuItem.Text = "Greyhound";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(155, 6);
            // 
            // pickTimeZoneToolStripMenuItem
            // 
            this.pickTimeZoneToolStripMenuItem.Name = "pickTimeZoneToolStripMenuItem";
            this.pickTimeZoneToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.pickTimeZoneToolStripMenuItem.Text = "Pick Time Zone";
            this.pickTimeZoneToolStripMenuItem.Click += new System.EventHandler(this.pickTimeZoneToolStripMenuItem_Click);
            // 
            // dgvMeetings
            // 
            this.dgvMeetings.AllowUserToAddRows = false;
            this.dgvMeetings.AllowUserToDeleteRows = false;
            this.dgvMeetings.AllowUserToOrderColumns = true;
            this.dgvMeetings.AllowUserToResizeRows = false;
            this.dgvMeetings.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvMeetings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMeetings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.meetCheck,
            this.meetVenue,
            this.meetCountry,
            this.meetType});
            this.dgvMeetings.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvMeetings.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvMeetings.Location = new System.Drawing.Point(0, 0);
            this.dgvMeetings.MultiSelect = false;
            this.dgvMeetings.Name = "dgvMeetings";
            this.dgvMeetings.RowHeadersVisible = false;
            this.dgvMeetings.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvMeetings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMeetings.Size = new System.Drawing.Size(966, 177);
            this.dgvMeetings.TabIndex = 10;
            this.dgvMeetings.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMeetings_CellContentClick);
            this.dgvMeetings.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellDoubleClick);
            this.dgvMeetings.SelectionChanged += new System.EventHandler(this.dgvMeetings_SelectionChanged);
            // 
            // meetCheck
            // 
            this.meetCheck.DataPropertyName = "IsChecked";
            this.meetCheck.HeaderText = "Check";
            this.meetCheck.Name = "meetCheck";
            this.meetCheck.Width = 44;
            // 
            // meetVenue
            // 
            this.meetVenue.DataPropertyName = "Venue";
            this.meetVenue.HeaderText = "Venue";
            this.meetVenue.Name = "meetVenue";
            this.meetVenue.Width = 63;
            // 
            // meetCountry
            // 
            this.meetCountry.DataPropertyName = "Country";
            this.meetCountry.HeaderText = "Country";
            this.meetCountry.Name = "meetCountry";
            this.meetCountry.Width = 68;
            // 
            // meetType
            // 
            this.meetType.DataPropertyName = "Type";
            this.meetType.HeaderText = "Type";
            this.meetType.Name = "meetType";
            this.meetType.ReadOnly = true;
            this.meetType.Width = 56;
            // 
            // dgvEvents
            // 
            this.dgvEvents.AllowUserToAddRows = false;
            this.dgvEvents.AllowUserToDeleteRows = false;
            this.dgvEvents.AllowUserToOrderColumns = true;
            this.dgvEvents.AllowUserToResizeRows = false;
            this.dgvEvents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEvents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.evtCheck,
            this.evtName,
            this.evtStartTime,
            this.evtAddDay,
            this.evtSubDay,
            this.evtMatched});
            this.dgvEvents.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvEvents.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvEvents.Location = new System.Drawing.Point(0, 177);
            this.dgvEvents.MultiSelect = false;
            this.dgvEvents.Name = "dgvEvents";
            this.dgvEvents.RowHeadersVisible = false;
            this.dgvEvents.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvEvents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEvents.Size = new System.Drawing.Size(966, 177);
            this.dgvEvents.TabIndex = 11;
            this.dgvEvents.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEvents_CellContentClick);
            this.dgvEvents.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellDoubleClick);
            this.dgvEvents.SelectionChanged += new System.EventHandler(this.dgvEvents_SelectionChanged);
            // 
            // dgvRunners
            // 
            this.dgvRunners.AllowUserToAddRows = false;
            this.dgvRunners.AllowUserToDeleteRows = false;
            this.dgvRunners.AllowUserToOrderColumns = true;
            this.dgvRunners.AllowUserToResizeRows = false;
            this.dgvRunners.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvRunners.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRunners.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRunners.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvRunners.Location = new System.Drawing.Point(0, 354);
            this.dgvRunners.MultiSelect = false;
            this.dgvRunners.Name = "dgvRunners";
            this.dgvRunners.RowHeadersVisible = false;
            this.dgvRunners.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvRunners.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRunners.Size = new System.Drawing.Size(966, 238);
            this.dgvRunners.TabIndex = 12;
            this.dgvRunners.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellDoubleClick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.dgvRunners);
            this.panel1.Controls.Add(this.dgvEvents);
            this.panel1.Controls.Add(this.dgvMeetings);
            this.panel1.Location = new System.Drawing.Point(12, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(966, 592);
            this.panel1.TabIndex = 13;
            // 
            // evtCheck
            // 
            this.evtCheck.DataPropertyName = "Check";
            this.evtCheck.HeaderText = "Check";
            this.evtCheck.Name = "evtCheck";
            this.evtCheck.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.evtCheck.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.evtCheck.Width = 63;
            // 
            // evtName
            // 
            this.evtName.DataPropertyName = "Name";
            this.evtName.HeaderText = "Name";
            this.evtName.Name = "evtName";
            this.evtName.ReadOnly = true;
            this.evtName.Width = 60;
            // 
            // evtStartTime
            // 
            this.evtStartTime.DataPropertyName = "StartTime";
            this.evtStartTime.HeaderText = "Start Time";
            this.evtStartTime.Name = "evtStartTime";
            this.evtStartTime.ReadOnly = true;
            this.evtStartTime.Width = 80;
            // 
            // evtAddDay
            // 
            this.evtAddDay.HeaderText = "Add Day";
            this.evtAddDay.Name = "evtAddDay";
            this.evtAddDay.ReadOnly = true;
            this.evtAddDay.Text = "+";
            this.evtAddDay.Width = 54;
            // 
            // evtSubDay
            // 
            this.evtSubDay.HeaderText = "Sub Day";
            this.evtSubDay.Name = "evtSubDay";
            this.evtSubDay.ReadOnly = true;
            this.evtSubDay.Text = "-";
            this.evtSubDay.Width = 54;
            // 
            // evtMatched
            // 
            this.evtMatched.DataPropertyName = "BFMatched";
            this.evtMatched.HeaderText = "Matched";
            this.evtMatched.Name = "evtMatched";
            this.evtMatched.ReadOnly = true;
            this.evtMatched.Width = 55;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 631);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(800, 670);
            this.Name = "MainForm";
            this.Text = "ArbBetSystem";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMeetings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEvents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRunners)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeDOLoginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateMeetingListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem forceDOLoginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem meetingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem racingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem harnessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem greyhoundToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvMeetings;
        private System.Windows.Forms.DataGridView dgvEvents;
        private System.Windows.Forms.DataGridView dgvRunners;
        private System.Windows.Forms.DataGridViewCheckBoxColumn meetCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn meetVenue;
        private System.Windows.Forms.DataGridViewTextBoxColumn meetCountry;
        private System.Windows.Forms.DataGridViewTextBoxColumn meetType;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem pickTimeZoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem capOddsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeBFLoginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem forceBFLoginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem matchUKVenuesToolStripMenuItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn evtCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn evtName;
        private System.Windows.Forms.DataGridViewTextBoxColumn evtStartTime;
        private System.Windows.Forms.DataGridViewButtonColumn evtAddDay;
        private System.Windows.Forms.DataGridViewButtonColumn evtSubDay;
        private System.Windows.Forms.DataGridViewCheckBoxColumn evtMatched;
    }
}

