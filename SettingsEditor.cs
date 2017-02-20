using System.Windows.Forms;
using System;
using UCNLUI.Dialogs;
using System.IO.Ports;

namespace RedNODEHost
{
    public partial class SettingsEditor : Form
    {
        #region Properties

        private Button button1;
        private Label label1;
        private ComboBox redNodePortCmb;
        private ComboBox gnssReceiverPortCmb;
        private Label label2;
        private Label label3;
        private NumericUpDown salinityEdit;
        private NumericUpDown pointsToShowEdit;
        private Label label4;
        private NumericUpDown rerrThresholdEdit;
        private Label label5;
        private LinkLabel getSalinityLbl;
        private Button button2;

        public SettingsContainer Value
        {
            get
            {
                SettingsContainer result = new SettingsContainer();
                result.Salinity = Convert.ToInt32(salinityEdit.Value);
                result.NumberOfPointsToShow = Convert.ToInt32(pointsToShowEdit.Value);
                result.RadialErrorThreshold = Convert.ToInt32(rerrThresholdEdit.Value);

                if (redNodePortCmb.SelectedIndex >= 0) result.RedNODEPortName = redNodePortCmb.SelectedItem.ToString();
                if (gnssReceiverPortCmb.SelectedIndex >= 0) result.GNSSPortName = gnssReceiverPortCmb.SelectedItem.ToString();

                return result;
            }
            set
            {
                salinityEdit.Value = Convert.ToDecimal(value.Salinity);
                pointsToShowEdit.Value = value.NumberOfPointsToShow;
                rerrThresholdEdit.Value = value.RadialErrorThreshold;

                var index = redNodePortCmb.Items.IndexOf(value.RedNODEPortName);
                if (index >= 0) redNodePortCmb.SelectedIndex = index;

                index = gnssReceiverPortCmb.Items.IndexOf(value.GNSSPortName);
                if (index >= 0) gnssReceiverPortCmb.SelectedIndex = index;
            }
        }

        public string Title
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }

        #endregion

        #region Constructor

        public SettingsEditor()
        {
            InitializeComponent();

            var portNames = SerialPort.GetPortNames();
            redNodePortCmb.Items.AddRange(portNames);
            
            gnssReceiverPortCmb.Items.AddRange(portNames);
        }

        #endregion

        #region Handlers

        private void calculateBtn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (SoundSpeedDialog sDialog = new SoundSpeedDialog())
            {
                sDialog.Title = "Calculating of speed of sound";
                if (sDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    salinityEdit.Value = Convert.ToDecimal(sDialog.SpeedOfSound);
                }
            }
        }

        
        #endregion             

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.redNodePortCmb = new System.Windows.Forms.ComboBox();
            this.gnssReceiverPortCmb = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.salinityEdit = new System.Windows.Forms.NumericUpDown();
            this.pointsToShowEdit = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.rerrThresholdEdit = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.getSalinityLbl = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.salinityEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pointsToShowEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rerrThresholdEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(242, 275);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 32);
            this.button1.TabIndex = 0;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(377, 275);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 32);
            this.button2.TabIndex = 1;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "RedNODE port";
            // 
            // redNodePortCmb
            // 
            this.redNodePortCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.redNodePortCmb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.redNodePortCmb.FormattingEnabled = true;
            this.redNodePortCmb.Location = new System.Drawing.Point(183, 45);
            this.redNodePortCmb.Name = "redNodePortCmb";
            this.redNodePortCmb.Size = new System.Drawing.Size(175, 24);
            this.redNodePortCmb.TabIndex = 3;
            // 
            // gnssReceiverPortCmb
            // 
            this.gnssReceiverPortCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gnssReceiverPortCmb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gnssReceiverPortCmb.FormattingEnabled = true;
            this.gnssReceiverPortCmb.Location = new System.Drawing.Point(183, 75);
            this.gnssReceiverPortCmb.Name = "gnssReceiverPortCmb";
            this.gnssReceiverPortCmb.Size = new System.Drawing.Size(175, 24);
            this.gnssReceiverPortCmb.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "GNSS receiver port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Salinity, ppm";
            // 
            // salinityEdit
            // 
            this.salinityEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.salinityEdit.DecimalPlaces = 1;
            this.salinityEdit.Location = new System.Drawing.Point(183, 105);
            this.salinityEdit.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.salinityEdit.Name = "salinityEdit";
            this.salinityEdit.Size = new System.Drawing.Size(120, 22);
            this.salinityEdit.TabIndex = 7;
            // 
            // pointsToShowEdit
            // 
            this.pointsToShowEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pointsToShowEdit.Location = new System.Drawing.Point(183, 133);
            this.pointsToShowEdit.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.pointsToShowEdit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pointsToShowEdit.Name = "pointsToShowEdit";
            this.pointsToShowEdit.Size = new System.Drawing.Size(120, 22);
            this.pointsToShowEdit.TabIndex = 9;
            this.pointsToShowEdit.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Points to show";
            // 
            // rerrThresholdEdit
            // 
            this.rerrThresholdEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rerrThresholdEdit.Location = new System.Drawing.Point(183, 161);
            this.rerrThresholdEdit.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.rerrThresholdEdit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.rerrThresholdEdit.Name = "rerrThresholdEdit";
            this.rerrThresholdEdit.Size = new System.Drawing.Size(120, 22);
            this.rerrThresholdEdit.TabIndex = 11;
            this.rerrThresholdEdit.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 163);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(165, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Radial error threshold, m";
            // 
            // getSalinityLbl
            // 
            this.getSalinityLbl.AutoSize = true;
            this.getSalinityLbl.Location = new System.Drawing.Point(319, 107);
            this.getSalinityLbl.Name = "getSalinityLbl";
            this.getSalinityLbl.Size = new System.Drawing.Size(138, 17);
            this.getSalinityLbl.TabIndex = 12;
            this.getSalinityLbl.TabStop = true;
            this.getSalinityLbl.Text = "Get from database...";
            this.getSalinityLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.getSalinityLbl_LinkClicked);
            // 
            // SettingsEditor
            // 
            this.ClientSize = new System.Drawing.Size(478, 319);
            this.Controls.Add(this.getSalinityLbl);
            this.Controls.Add(this.rerrThresholdEdit);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pointsToShowEdit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.salinityEdit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.gnssReceiverPortCmb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.redNodePortCmb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SettingsEditor";
            ((System.ComponentModel.ISupportInitialize)(this.salinityEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pointsToShowEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rerrThresholdEdit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void getSalinityLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (SalinityDialog sDialog = new SalinityDialog())
            {
                if (sDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    salinityEdit.Value = Convert.ToDecimal(sDialog.Salinity);
                }
            }
        }
    }
}
