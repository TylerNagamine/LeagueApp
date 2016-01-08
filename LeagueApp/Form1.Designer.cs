namespace LeagueApp
{
    partial class Form1
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
            this.apikeyBox = new System.Windows.Forms.TextBox();
            this.apiLabel = new System.Windows.Forms.LinkLabel();
            this.masterySectionLabel = new System.Windows.Forms.Label();
            this.summonerNameInput1 = new System.Windows.Forms.TextBox();
            this.summonerNameLabel1 = new System.Windows.Forms.Label();
            this.MatchListBox = new System.Windows.Forms.ListBox();
            this.selectMatchLabel1 = new System.Windows.Forms.Label();
            this.selectFunctionComboBox = new System.Windows.Forms.ComboBox();
            this.masteryListBox = new System.Windows.Forms.ListBox();
            this.masteryLabel = new System.Windows.Forms.Label();
            this.masteryByChampion = new System.Windows.Forms.Panel();
            this.masteryByChampion.SuspendLayout();
            this.SuspendLayout();
            // 
            // apikeyBox
            // 
            this.apikeyBox.Location = new System.Drawing.Point(12, 25);
            this.apikeyBox.Name = "apikeyBox";
            this.apikeyBox.Size = new System.Drawing.Size(221, 20);
            this.apikeyBox.TabIndex = 0;
            this.apikeyBox.Text = "b0991e7f-9626-4bad-85f5-6b0e8ae59a85";
            this.apikeyBox.TextChanged += new System.EventHandler(this.apikeyBox_TextChanged);
            // 
            // apiLabel
            // 
            this.apiLabel.AutoSize = true;
            this.apiLabel.Location = new System.Drawing.Point(12, 6);
            this.apiLabel.Name = "apiLabel";
            this.apiLabel.Size = new System.Drawing.Size(45, 13);
            this.apiLabel.TabIndex = 1;
            this.apiLabel.TabStop = true;
            this.apiLabel.Text = "API Key";
            this.apiLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.apiLabel_LinkClicked);
            // 
            // masterySectionLabel
            // 
            this.masterySectionLabel.AutoSize = true;
            this.masterySectionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.masterySectionLabel.Location = new System.Drawing.Point(3, 4);
            this.masterySectionLabel.Name = "masterySectionLabel";
            this.masterySectionLabel.Size = new System.Drawing.Size(235, 17);
            this.masterySectionLabel.TabIndex = 2;
            this.masterySectionLabel.Text = "Search Mastery Page by Summoner";
            // 
            // summonerNameInput1
            // 
            this.summonerNameInput1.Location = new System.Drawing.Point(3, 40);
            this.summonerNameInput1.Name = "summonerNameInput1";
            this.summonerNameInput1.Size = new System.Drawing.Size(120, 20);
            this.summonerNameInput1.TabIndex = 3;
            this.summonerNameInput1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.summonerNameInput1_KeyDown);
            this.summonerNameInput1.LostFocus += new System.EventHandler(this.summonerNameInput1_LostFocus);
            // 
            // summonerNameLabel1
            // 
            this.summonerNameLabel1.AutoSize = true;
            this.summonerNameLabel1.Location = new System.Drawing.Point(3, 24);
            this.summonerNameLabel1.Name = "summonerNameLabel1";
            this.summonerNameLabel1.Size = new System.Drawing.Size(88, 13);
            this.summonerNameLabel1.TabIndex = 4;
            this.summonerNameLabel1.Text = "Summoner Name";
            // 
            // MatchListBox
            // 
            this.MatchListBox.FormattingEnabled = true;
            this.MatchListBox.Location = new System.Drawing.Point(3, 89);
            this.MatchListBox.Name = "MatchListBox";
            this.MatchListBox.Size = new System.Drawing.Size(120, 95);
            this.MatchListBox.TabIndex = 5;
            this.MatchListBox.SelectedIndexChanged += new System.EventHandler(this.MatchListBox_SelectedIndexChanged);
            // 
            // selectMatchLabel1
            // 
            this.selectMatchLabel1.AutoSize = true;
            this.selectMatchLabel1.Location = new System.Drawing.Point(3, 73);
            this.selectMatchLabel1.Name = "selectMatchLabel1";
            this.selectMatchLabel1.Size = new System.Drawing.Size(78, 13);
            this.selectMatchLabel1.TabIndex = 6;
            this.selectMatchLabel1.Text = "Select a match";
            // 
            // selectFunctionComboBox
            // 
            this.selectFunctionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectFunctionComboBox.FormattingEnabled = true;
            this.selectFunctionComboBox.Location = new System.Drawing.Point(12, 52);
            this.selectFunctionComboBox.Name = "selectFunctionComboBox";
            this.selectFunctionComboBox.Size = new System.Drawing.Size(221, 21);
            this.selectFunctionComboBox.TabIndex = 7;
            this.selectFunctionComboBox.SelectedIndexChanged += new System.EventHandler(this.selectFunctionComboBox_SelectedIndexChanged);
            // 
            // masteryListBox
            // 
            this.masteryListBox.FormattingEnabled = true;
            this.masteryListBox.Location = new System.Drawing.Point(285, 18);
            this.masteryListBox.Name = "masteryListBox";
            this.masteryListBox.Size = new System.Drawing.Size(494, 329);
            this.masteryListBox.TabIndex = 9;
            // 
            // masteryLabel
            // 
            this.masteryLabel.AutoSize = true;
            this.masteryLabel.Location = new System.Drawing.Point(282, 0);
            this.masteryLabel.Name = "masteryLabel";
            this.masteryLabel.Size = new System.Drawing.Size(52, 13);
            this.masteryLabel.TabIndex = 10;
            this.masteryLabel.Text = "Masteries";
            // 
            // masteryByChampion
            // 
            this.masteryByChampion.Controls.Add(this.masteryListBox);
            this.masteryByChampion.Controls.Add(this.masteryLabel);
            this.masteryByChampion.Controls.Add(this.summonerNameInput1);
            this.masteryByChampion.Controls.Add(this.masterySectionLabel);
            this.masteryByChampion.Controls.Add(this.selectMatchLabel1);
            this.masteryByChampion.Controls.Add(this.summonerNameLabel1);
            this.masteryByChampion.Controls.Add(this.MatchListBox);
            this.masteryByChampion.Location = new System.Drawing.Point(15, 79);
            this.masteryByChampion.Name = "masteryByChampion";
            this.masteryByChampion.Size = new System.Drawing.Size(814, 387);
            this.masteryByChampion.TabIndex = 11;
            this.masteryByChampion.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GrayText;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(841, 478);
            this.Controls.Add(this.masteryByChampion);
            this.Controls.Add(this.selectFunctionComboBox);
            this.Controls.Add(this.apiLabel);
            this.Controls.Add(this.apikeyBox);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.Name = "Form1";
            this.Text = "League App";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.masteryByChampion.ResumeLayout(false);
            this.masteryByChampion.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox apikeyBox;
        private System.Windows.Forms.LinkLabel apiLabel;
        private System.Windows.Forms.Label masterySectionLabel;
        private System.Windows.Forms.TextBox summonerNameInput1;
        private System.Windows.Forms.Label summonerNameLabel1;
        private System.Windows.Forms.ListBox MatchListBox;
        private System.Windows.Forms.Label selectMatchLabel1;
        private System.Windows.Forms.ComboBox selectFunctionComboBox;
        private System.Windows.Forms.ListBox masteryListBox;
        private System.Windows.Forms.Label masteryLabel;
        private System.Windows.Forms.Panel masteryByChampion;
    }
}

