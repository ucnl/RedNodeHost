namespace RedNODEHost
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.connectionBtn = new System.Windows.Forms.ToolStripButton();
            this.settingsBtn = new System.Windows.Forms.ToolStripButton();
            this.dataBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.saveTrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTemperatureVsDepthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSoundSpeedVsDepthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.parseFromLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parseFromAFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoBtn = new System.Windows.Forms.ToolStripButton();
            this.gnssConnectBtn = new System.Windows.Forms.ToolStripButton();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.mainSplit = new System.Windows.Forms.SplitContainer();
            this.sysStateTable = new System.Windows.Forms.TableLayoutPanel();
            this.serialNumberLbl = new System.Windows.Forms.Label();
            this.serialContextMnu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pressureRatingLbl = new System.Windows.Forms.Label();
            this.acCoreInfoLbl = new System.Windows.Forms.Label();
            this.systemInfoLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.depthLbl = new System.Windows.Forms.Label();
            this.latLbl = new System.Windows.Forms.Label();
            this.lonLbl = new System.Windows.Forms.Label();
            this.rerrLbl = new System.Windows.Forms.Label();
            this.pressureLbl = new System.Windows.Forms.Label();
            this.base1Lbl = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.base2Lbl = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.base3Lbl = new System.Windows.Forms.Label();
            this.base4Lbl = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.soundSpeedLbl = new System.Windows.Forms.Label();
            this.temperatureLbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.salinityLbl = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.gravityAccLbl = new System.Windows.Forms.Label();
            this.waterDensityLbl = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.gnssLonLbl = new System.Windows.Forms.Label();
            this.gnssLatLbl = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.lblgnssDiffLbl = new System.Windows.Forms.Label();
            this.sysStateToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.zeroDptAdjustBtn = new System.Windows.Forms.ToolStripButton();
            this.geoPlot = new RedNODEHost.GeoPlot();
            this.plotStatusStrip = new System.Windows.Forms.StatusStrip();
            this.plotToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.markPointBtn = new System.Windows.Forms.ToolStripButton();
            this.plotClearBtn = new System.Windows.Forms.ToolStripButton();
            this.clearMPBtn = new System.Windows.Forms.ToolStripButton();
            this.clearGNSSBtn = new System.Windows.Forms.ToolStripButton();
            this.mainToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplit)).BeginInit();
            this.mainSplit.Panel1.SuspendLayout();
            this.mainSplit.Panel2.SuspendLayout();
            this.mainSplit.SuspendLayout();
            this.sysStateTable.SuspendLayout();
            this.serialContextMnu.SuspendLayout();
            this.sysStateToolStrip.SuspendLayout();
            this.plotToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionBtn,
            this.settingsBtn,
            this.dataBtn,
            this.toolsBtn,
            this.infoBtn,
            this.gnssConnectBtn});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(1161, 32);
            this.mainToolStrip.TabIndex = 0;
            this.mainToolStrip.Text = "toolStrip1";
            // 
            // connectionBtn
            // 
            this.connectionBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.connectionBtn.Font = new System.Drawing.Font("Segoe UI", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.connectionBtn.Image = ((System.Drawing.Image)(resources.GetObject("connectionBtn.Image")));
            this.connectionBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.connectionBtn.Name = "connectionBtn";
            this.connectionBtn.Size = new System.Drawing.Size(90, 29);
            this.connectionBtn.Text = "Connect";
            this.connectionBtn.Click += new System.EventHandler(this.connectionBtn_Click);
            // 
            // settingsBtn
            // 
            this.settingsBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.settingsBtn.Font = new System.Drawing.Font("Segoe UI", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.settingsBtn.Image = ((System.Drawing.Image)(resources.GetObject("settingsBtn.Image")));
            this.settingsBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsBtn.Name = "settingsBtn";
            this.settingsBtn.Size = new System.Drawing.Size(88, 29);
            this.settingsBtn.Text = "Settings";
            this.settingsBtn.Click += new System.EventHandler(this.settingsBtn_Click);
            // 
            // dataBtn
            // 
            this.dataBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.dataBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveTrackToolStripMenuItem,
            this.saveTemperatureVsDepthToolStripMenuItem,
            this.saveSoundSpeedVsDepthToolStripMenuItem});
            this.dataBtn.Font = new System.Drawing.Font("Segoe UI", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataBtn.Image = ((System.Drawing.Image)(resources.GetObject("dataBtn.Image")));
            this.dataBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dataBtn.Name = "dataBtn";
            this.dataBtn.Size = new System.Drawing.Size(66, 29);
            this.dataBtn.Text = "Data";
            // 
            // saveTrackToolStripMenuItem
            // 
            this.saveTrackToolStripMenuItem.Name = "saveTrackToolStripMenuItem";
            this.saveTrackToolStripMenuItem.Size = new System.Drawing.Size(307, 30);
            this.saveTrackToolStripMenuItem.Text = "Save track";
            this.saveTrackToolStripMenuItem.Click += new System.EventHandler(this.saveTrackToolStripMenuItem_Click);
            // 
            // saveTemperatureVsDepthToolStripMenuItem
            // 
            this.saveTemperatureVsDepthToolStripMenuItem.Name = "saveTemperatureVsDepthToolStripMenuItem";
            this.saveTemperatureVsDepthToolStripMenuItem.Size = new System.Drawing.Size(307, 30);
            this.saveTemperatureVsDepthToolStripMenuItem.Text = "Save temperature profile";
            this.saveTemperatureVsDepthToolStripMenuItem.Click += new System.EventHandler(this.saveTemperatureVsDepthToolStripMenuItem_Click);
            // 
            // saveSoundSpeedVsDepthToolStripMenuItem
            // 
            this.saveSoundSpeedVsDepthToolStripMenuItem.Name = "saveSoundSpeedVsDepthToolStripMenuItem";
            this.saveSoundSpeedVsDepthToolStripMenuItem.Size = new System.Drawing.Size(307, 30);
            this.saveSoundSpeedVsDepthToolStripMenuItem.Text = "Save sound speed profile";
            this.saveSoundSpeedVsDepthToolStripMenuItem.Click += new System.EventHandler(this.saveSoundSpeedVsDepthToolStripMenuItem_Click);
            // 
            // toolsBtn
            // 
            this.toolsBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolsBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parseFromLogToolStripMenuItem,
            this.parseFromAFileToolStripMenuItem});
            this.toolsBtn.Font = new System.Drawing.Font("Segoe UI", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolsBtn.Image = ((System.Drawing.Image)(resources.GetObject("toolsBtn.Image")));
            this.toolsBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolsBtn.Name = "toolsBtn";
            this.toolsBtn.Size = new System.Drawing.Size(71, 29);
            this.toolsBtn.Text = "Tools";
            // 
            // parseFromLogToolStripMenuItem
            // 
            this.parseFromLogToolStripMenuItem.Name = "parseFromLogToolStripMenuItem";
            this.parseFromLogToolStripMenuItem.Size = new System.Drawing.Size(243, 30);
            this.parseFromLogToolStripMenuItem.Text = "Parse from log";
            this.parseFromLogToolStripMenuItem.Click += new System.EventHandler(this.parseFromLogToolStripMenuItem_Click);
            // 
            // parseFromAFileToolStripMenuItem
            // 
            this.parseFromAFileToolStripMenuItem.Name = "parseFromAFileToolStripMenuItem";
            this.parseFromAFileToolStripMenuItem.Size = new System.Drawing.Size(243, 30);
            this.parseFromAFileToolStripMenuItem.Text = "Parse from a file...";
            this.parseFromAFileToolStripMenuItem.Click += new System.EventHandler(this.parseFromAFileToolStripMenuItem_Click);
            // 
            // infoBtn
            // 
            this.infoBtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.infoBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.infoBtn.Font = new System.Drawing.Font("Segoe UI", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.infoBtn.Image = ((System.Drawing.Image)(resources.GetObject("infoBtn.Image")));
            this.infoBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.infoBtn.Name = "infoBtn";
            this.infoBtn.Size = new System.Drawing.Size(53, 29);
            this.infoBtn.Text = "Info";
            this.infoBtn.Click += new System.EventHandler(this.infoBtn_Click);
            // 
            // gnssConnectBtn
            // 
            this.gnssConnectBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.gnssConnectBtn.Font = new System.Drawing.Font("Segoe UI", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gnssConnectBtn.Image = ((System.Drawing.Image)(resources.GetObject("gnssConnectBtn.Image")));
            this.gnssConnectBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.gnssConnectBtn.Name = "gnssConnectBtn";
            this.gnssConnectBtn.Size = new System.Drawing.Size(67, 29);
            this.gnssConnectBtn.Text = "GNSS";
            this.gnssConnectBtn.Click += new System.EventHandler(this.gnssConnectBtn_Click);
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 705);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(1161, 22);
            this.mainStatusStrip.TabIndex = 1;
            this.mainStatusStrip.Text = "statusStrip1";
            // 
            // mainSplit
            // 
            this.mainSplit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplit.Location = new System.Drawing.Point(0, 32);
            this.mainSplit.Name = "mainSplit";
            // 
            // mainSplit.Panel1
            // 
            this.mainSplit.Panel1.Controls.Add(this.sysStateTable);
            this.mainSplit.Panel1.Controls.Add(this.sysStateToolStrip);
            // 
            // mainSplit.Panel2
            // 
            this.mainSplit.Panel2.Controls.Add(this.geoPlot);
            this.mainSplit.Panel2.Controls.Add(this.plotStatusStrip);
            this.mainSplit.Panel2.Controls.Add(this.plotToolStrip);
            this.mainSplit.Size = new System.Drawing.Size(1161, 673);
            this.mainSplit.SplitterDistance = 543;
            this.mainSplit.TabIndex = 2;
            // 
            // sysStateTable
            // 
            this.sysStateTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sysStateTable.ColumnCount = 2;
            this.sysStateTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.sysStateTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.sysStateTable.Controls.Add(this.serialNumberLbl, 1, 3);
            this.sysStateTable.Controls.Add(this.pressureRatingLbl, 1, 2);
            this.sysStateTable.Controls.Add(this.acCoreInfoLbl, 1, 1);
            this.sysStateTable.Controls.Add(this.systemInfoLbl, 1, 0);
            this.sysStateTable.Controls.Add(this.label1, 0, 0);
            this.sysStateTable.Controls.Add(this.label2, 0, 1);
            this.sysStateTable.Controls.Add(this.label7, 0, 2);
            this.sysStateTable.Controls.Add(this.label5, 0, 3);
            this.sysStateTable.Controls.Add(this.depthLbl, 1, 5);
            this.sysStateTable.Controls.Add(this.latLbl, 1, 6);
            this.sysStateTable.Controls.Add(this.lonLbl, 1, 7);
            this.sysStateTable.Controls.Add(this.rerrLbl, 1, 8);
            this.sysStateTable.Controls.Add(this.pressureLbl, 1, 9);
            this.sysStateTable.Controls.Add(this.base1Lbl, 1, 12);
            this.sysStateTable.Controls.Add(this.label24, 0, 11);
            this.sysStateTable.Controls.Add(this.label25, 0, 12);
            this.sysStateTable.Controls.Add(this.label19, 0, 5);
            this.sysStateTable.Controls.Add(this.label21, 0, 6);
            this.sysStateTable.Controls.Add(this.label22, 0, 7);
            this.sysStateTable.Controls.Add(this.label18, 0, 9);
            this.sysStateTable.Controls.Add(this.label20, 0, 10);
            this.sysStateTable.Controls.Add(this.label23, 0, 8);
            this.sysStateTable.Controls.Add(this.label27, 0, 13);
            this.sysStateTable.Controls.Add(this.base2Lbl, 1, 13);
            this.sysStateTable.Controls.Add(this.label28, 0, 14);
            this.sysStateTable.Controls.Add(this.label29, 0, 15);
            this.sysStateTable.Controls.Add(this.base3Lbl, 1, 14);
            this.sysStateTable.Controls.Add(this.base4Lbl, 1, 15);
            this.sysStateTable.Controls.Add(this.label9, 0, 4);
            this.sysStateTable.Controls.Add(this.soundSpeedLbl, 1, 11);
            this.sysStateTable.Controls.Add(this.temperatureLbl, 1, 10);
            this.sysStateTable.Controls.Add(this.label3, 0, 16);
            this.sysStateTable.Controls.Add(this.salinityLbl, 1, 16);
            this.sysStateTable.Controls.Add(this.label4, 0, 17);
            this.sysStateTable.Controls.Add(this.label6, 0, 18);
            this.sysStateTable.Controls.Add(this.gravityAccLbl, 1, 18);
            this.sysStateTable.Controls.Add(this.waterDensityLbl, 1, 17);
            this.sysStateTable.Controls.Add(this.label34, 0, 21);
            this.sysStateTable.Controls.Add(this.label16, 0, 19);
            this.sysStateTable.Controls.Add(this.label32, 0, 20);
            this.sysStateTable.Controls.Add(this.gnssLonLbl, 1, 21);
            this.sysStateTable.Controls.Add(this.gnssLatLbl, 1, 20);
            this.sysStateTable.Controls.Add(this.label39, 0, 22);
            this.sysStateTable.Controls.Add(this.lblgnssDiffLbl, 1, 22);
            this.sysStateTable.Location = new System.Drawing.Point(12, 31);
            this.sysStateTable.Name = "sysStateTable";
            this.sysStateTable.RowCount = 24;
            this.sysStateTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.sysStateTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.sysStateTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.sysStateTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.sysStateTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.sysStateTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.sysStateTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.sysStateTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.sysStateTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.sysStateTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.sysStateTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.sysStateTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.sysStateTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.sysStateTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.sysStateTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.sysStateTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.sysStateTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.sysStateTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.sysStateTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.sysStateTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.sysStateTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.sysStateTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.sysStateTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.sysStateTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.sysStateTable.Size = new System.Drawing.Size(526, 637);
            this.sysStateTable.TabIndex = 1;
            // 
            // serialNumberLbl
            // 
            this.serialNumberLbl.AutoSize = true;
            this.serialNumberLbl.ContextMenuStrip = this.serialContextMnu;
            this.serialNumberLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.serialNumberLbl.Location = new System.Drawing.Point(175, 81);
            this.serialNumberLbl.Margin = new System.Windows.Forms.Padding(3);
            this.serialNumberLbl.Name = "serialNumberLbl";
            this.serialNumberLbl.Size = new System.Drawing.Size(37, 20);
            this.serialNumberLbl.TabIndex = 7;
            this.serialNumberLbl.Text = "- - -";
            // 
            // serialContextMnu
            // 
            this.serialContextMnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem});
            this.serialContextMnu.Name = "serialContextMnu";
            this.serialContextMnu.Size = new System.Drawing.Size(113, 28);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(112, 24);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pressureRatingLbl
            // 
            this.pressureRatingLbl.AutoSize = true;
            this.pressureRatingLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pressureRatingLbl.Location = new System.Drawing.Point(175, 55);
            this.pressureRatingLbl.Margin = new System.Windows.Forms.Padding(3);
            this.pressureRatingLbl.Name = "pressureRatingLbl";
            this.pressureRatingLbl.Size = new System.Drawing.Size(37, 20);
            this.pressureRatingLbl.TabIndex = 5;
            this.pressureRatingLbl.Text = "- - -";
            // 
            // acCoreInfoLbl
            // 
            this.acCoreInfoLbl.AutoSize = true;
            this.acCoreInfoLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.acCoreInfoLbl.Location = new System.Drawing.Point(175, 29);
            this.acCoreInfoLbl.Margin = new System.Windows.Forms.Padding(3);
            this.acCoreInfoLbl.Name = "acCoreInfoLbl";
            this.acCoreInfoLbl.Size = new System.Drawing.Size(37, 20);
            this.acCoreInfoLbl.TabIndex = 3;
            this.acCoreInfoLbl.Text = "- - -";
            // 
            // systemInfoLbl
            // 
            this.systemInfoLbl.AutoSize = true;
            this.systemInfoLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.systemInfoLbl.Location = new System.Drawing.Point(175, 3);
            this.systemInfoLbl.Margin = new System.Windows.Forms.Padding(3);
            this.systemInfoLbl.Name = "systemInfoLbl";
            this.systemInfoLbl.Size = new System.Drawing.Size(37, 20);
            this.systemInfoLbl.TabIndex = 2;
            this.systemInfoLbl.Text = "- - -";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "System";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(3, 29);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "AC Core";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(3, 55);
            this.label7.Margin = new System.Windows.Forms.Padding(3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(157, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "Pressure rating, bar";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(3, 81);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Serial number";
            // 
            // depthLbl
            // 
            this.depthLbl.AutoSize = true;
            this.depthLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.depthLbl.Location = new System.Drawing.Point(175, 130);
            this.depthLbl.Margin = new System.Windows.Forms.Padding(3);
            this.depthLbl.Name = "depthLbl";
            this.depthLbl.Size = new System.Drawing.Size(37, 20);
            this.depthLbl.TabIndex = 9;
            this.depthLbl.Text = "- - -";
            // 
            // latLbl
            // 
            this.latLbl.AutoSize = true;
            this.latLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.latLbl.Location = new System.Drawing.Point(175, 156);
            this.latLbl.Margin = new System.Windows.Forms.Padding(3);
            this.latLbl.Name = "latLbl";
            this.latLbl.Size = new System.Drawing.Size(37, 20);
            this.latLbl.TabIndex = 10;
            this.latLbl.Text = "- - -";
            // 
            // lonLbl
            // 
            this.lonLbl.AutoSize = true;
            this.lonLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lonLbl.Location = new System.Drawing.Point(175, 182);
            this.lonLbl.Margin = new System.Windows.Forms.Padding(3);
            this.lonLbl.Name = "lonLbl";
            this.lonLbl.Size = new System.Drawing.Size(37, 20);
            this.lonLbl.TabIndex = 11;
            this.lonLbl.Text = "- - -";
            // 
            // rerrLbl
            // 
            this.rerrLbl.AutoSize = true;
            this.rerrLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rerrLbl.Location = new System.Drawing.Point(175, 208);
            this.rerrLbl.Margin = new System.Windows.Forms.Padding(3);
            this.rerrLbl.Name = "rerrLbl";
            this.rerrLbl.Size = new System.Drawing.Size(37, 20);
            this.rerrLbl.TabIndex = 12;
            this.rerrLbl.Text = "- - -";
            // 
            // pressureLbl
            // 
            this.pressureLbl.AutoSize = true;
            this.pressureLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pressureLbl.Location = new System.Drawing.Point(175, 234);
            this.pressureLbl.Margin = new System.Windows.Forms.Padding(3);
            this.pressureLbl.Name = "pressureLbl";
            this.pressureLbl.Size = new System.Drawing.Size(37, 20);
            this.pressureLbl.TabIndex = 13;
            this.pressureLbl.Text = "- - -";
            // 
            // base1Lbl
            // 
            this.base1Lbl.AutoSize = true;
            this.base1Lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.base1Lbl.Location = new System.Drawing.Point(175, 312);
            this.base1Lbl.Margin = new System.Windows.Forms.Padding(3);
            this.base1Lbl.Name = "base1Lbl";
            this.base1Lbl.Size = new System.Drawing.Size(37, 20);
            this.base1Lbl.TabIndex = 16;
            this.base1Lbl.Text = "- - -";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label24.Location = new System.Drawing.Point(3, 286);
            this.label24.Margin = new System.Windows.Forms.Padding(3);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(143, 20);
            this.label24.TabIndex = 23;
            this.label24.Text = "Sound speed, m/s";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label25.Location = new System.Drawing.Point(3, 312);
            this.label25.Margin = new System.Windows.Forms.Padding(3);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(107, 20);
            this.label25.TabIndex = 24;
            this.label25.Text = "RedBASE #1";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label19.Location = new System.Drawing.Point(3, 130);
            this.label19.Margin = new System.Windows.Forms.Padding(3);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(77, 20);
            this.label19.TabIndex = 18;
            this.label19.Text = "Depth, m";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label21.Location = new System.Drawing.Point(3, 156);
            this.label21.Margin = new System.Windows.Forms.Padding(3);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(69, 20);
            this.label21.TabIndex = 20;
            this.label21.Text = "Latitude";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label22.Location = new System.Drawing.Point(3, 182);
            this.label22.Margin = new System.Windows.Forms.Padding(3);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(82, 20);
            this.label22.TabIndex = 21;
            this.label22.Text = "Longitude";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label18.Location = new System.Drawing.Point(3, 234);
            this.label18.Margin = new System.Windows.Forms.Padding(3);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(127, 20);
            this.label18.TabIndex = 17;
            this.label18.Text = "Pressure, mBar";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label20.Location = new System.Drawing.Point(3, 260);
            this.label20.Margin = new System.Windows.Forms.Padding(3);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(125, 20);
            this.label20.TabIndex = 19;
            this.label20.Text = "Temperature, C";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label23.Location = new System.Drawing.Point(3, 208);
            this.label23.Margin = new System.Windows.Forms.Padding(3);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(120, 20);
            this.label23.TabIndex = 22;
            this.label23.Text = "Radial error, m";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label27.Location = new System.Drawing.Point(3, 338);
            this.label27.Margin = new System.Windows.Forms.Padding(3);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(107, 20);
            this.label27.TabIndex = 26;
            this.label27.Text = "RedBASE #2";
            // 
            // base2Lbl
            // 
            this.base2Lbl.AutoSize = true;
            this.base2Lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.base2Lbl.Location = new System.Drawing.Point(175, 338);
            this.base2Lbl.Margin = new System.Windows.Forms.Padding(3);
            this.base2Lbl.Name = "base2Lbl";
            this.base2Lbl.Size = new System.Drawing.Size(37, 20);
            this.base2Lbl.TabIndex = 25;
            this.base2Lbl.Text = "- - -";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label28.Location = new System.Drawing.Point(3, 364);
            this.label28.Margin = new System.Windows.Forms.Padding(3);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(107, 20);
            this.label28.TabIndex = 27;
            this.label28.Text = "RedBASE #3";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label29.Location = new System.Drawing.Point(3, 390);
            this.label29.Margin = new System.Windows.Forms.Padding(3);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(107, 20);
            this.label29.TabIndex = 28;
            this.label29.Text = "RedBASE #4";
            // 
            // base3Lbl
            // 
            this.base3Lbl.AutoSize = true;
            this.base3Lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.base3Lbl.Location = new System.Drawing.Point(175, 364);
            this.base3Lbl.Margin = new System.Windows.Forms.Padding(3);
            this.base3Lbl.Name = "base3Lbl";
            this.base3Lbl.Size = new System.Drawing.Size(37, 20);
            this.base3Lbl.TabIndex = 29;
            this.base3Lbl.Text = "- - -";
            // 
            // base4Lbl
            // 
            this.base4Lbl.AutoSize = true;
            this.base4Lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.base4Lbl.Location = new System.Drawing.Point(175, 390);
            this.base4Lbl.Margin = new System.Windows.Forms.Padding(3);
            this.base4Lbl.Name = "base4Lbl";
            this.base4Lbl.Size = new System.Drawing.Size(37, 20);
            this.base4Lbl.TabIndex = 30;
            this.base4Lbl.Text = "- - -";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(3, 107);
            this.label9.Margin = new System.Windows.Forms.Padding(3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 17);
            this.label9.TabIndex = 8;
            // 
            // soundSpeedLbl
            // 
            this.soundSpeedLbl.AutoSize = true;
            this.soundSpeedLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.soundSpeedLbl.Location = new System.Drawing.Point(175, 286);
            this.soundSpeedLbl.Margin = new System.Windows.Forms.Padding(3);
            this.soundSpeedLbl.Name = "soundSpeedLbl";
            this.soundSpeedLbl.Size = new System.Drawing.Size(37, 20);
            this.soundSpeedLbl.TabIndex = 40;
            this.soundSpeedLbl.Text = "- - -";
            // 
            // temperatureLbl
            // 
            this.temperatureLbl.AutoSize = true;
            this.temperatureLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.temperatureLbl.Location = new System.Drawing.Point(175, 260);
            this.temperatureLbl.Margin = new System.Windows.Forms.Padding(3);
            this.temperatureLbl.Name = "temperatureLbl";
            this.temperatureLbl.Size = new System.Drawing.Size(37, 20);
            this.temperatureLbl.TabIndex = 14;
            this.temperatureLbl.Text = "- - -";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(3, 416);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 20);
            this.label3.TabIndex = 41;
            this.label3.Text = "Salinity, ppm";
            // 
            // salinityLbl
            // 
            this.salinityLbl.AutoSize = true;
            this.salinityLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.salinityLbl.Location = new System.Drawing.Point(175, 416);
            this.salinityLbl.Margin = new System.Windows.Forms.Padding(3);
            this.salinityLbl.Name = "salinityLbl";
            this.salinityLbl.Size = new System.Drawing.Size(37, 20);
            this.salinityLbl.TabIndex = 42;
            this.salinityLbl.Text = "- - -";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(3, 442);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(166, 20);
            this.label4.TabIndex = 43;
            this.label4.Text = "Water density, kg/m3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(3, 468);
            this.label6.Margin = new System.Windows.Forms.Padding(3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(140, 20);
            this.label6.TabIndex = 44;
            this.label6.Text = "Gravity acc, m/s2";
            // 
            // gravityAccLbl
            // 
            this.gravityAccLbl.AutoSize = true;
            this.gravityAccLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gravityAccLbl.Location = new System.Drawing.Point(175, 468);
            this.gravityAccLbl.Margin = new System.Windows.Forms.Padding(3);
            this.gravityAccLbl.Name = "gravityAccLbl";
            this.gravityAccLbl.Size = new System.Drawing.Size(37, 20);
            this.gravityAccLbl.TabIndex = 45;
            this.gravityAccLbl.Text = "- - -";
            // 
            // waterDensityLbl
            // 
            this.waterDensityLbl.AutoSize = true;
            this.waterDensityLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.waterDensityLbl.Location = new System.Drawing.Point(175, 442);
            this.waterDensityLbl.Margin = new System.Windows.Forms.Padding(3);
            this.waterDensityLbl.Name = "waterDensityLbl";
            this.waterDensityLbl.Size = new System.Drawing.Size(37, 20);
            this.waterDensityLbl.TabIndex = 46;
            this.waterDensityLbl.Text = "- - -";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label34.Location = new System.Drawing.Point(3, 543);
            this.label34.Margin = new System.Windows.Forms.Padding(3);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(134, 20);
            this.label34.TabIndex = 34;
            this.label34.Text = "GNSS Longitude";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label16.Location = new System.Drawing.Point(3, 494);
            this.label16.Margin = new System.Windows.Forms.Padding(3);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(0, 17);
            this.label16.TabIndex = 31;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label32.Location = new System.Drawing.Point(3, 517);
            this.label32.Margin = new System.Windows.Forms.Padding(3);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(121, 20);
            this.label32.TabIndex = 32;
            this.label32.Text = "GNSS Latitude";
            // 
            // gnssLonLbl
            // 
            this.gnssLonLbl.AutoSize = true;
            this.gnssLonLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gnssLonLbl.Location = new System.Drawing.Point(175, 543);
            this.gnssLonLbl.Margin = new System.Windows.Forms.Padding(3);
            this.gnssLonLbl.Name = "gnssLonLbl";
            this.gnssLonLbl.Size = new System.Drawing.Size(37, 20);
            this.gnssLonLbl.TabIndex = 35;
            this.gnssLonLbl.Text = "- - -";
            // 
            // gnssLatLbl
            // 
            this.gnssLatLbl.AutoSize = true;
            this.gnssLatLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gnssLatLbl.Location = new System.Drawing.Point(175, 517);
            this.gnssLatLbl.Margin = new System.Windows.Forms.Padding(3);
            this.gnssLatLbl.Name = "gnssLatLbl";
            this.gnssLatLbl.Size = new System.Drawing.Size(37, 20);
            this.gnssLatLbl.TabIndex = 33;
            this.gnssLatLbl.Text = "- - -";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label39.Location = new System.Drawing.Point(3, 569);
            this.label39.Margin = new System.Windows.Forms.Padding(3);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(110, 20);
            this.label39.TabIndex = 39;
            this.label39.Text = "Difference, m";
            // 
            // lblgnssDiffLbl
            // 
            this.lblgnssDiffLbl.AutoSize = true;
            this.lblgnssDiffLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblgnssDiffLbl.Location = new System.Drawing.Point(175, 569);
            this.lblgnssDiffLbl.Margin = new System.Windows.Forms.Padding(3);
            this.lblgnssDiffLbl.Name = "lblgnssDiffLbl";
            this.lblgnssDiffLbl.Size = new System.Drawing.Size(37, 20);
            this.lblgnssDiffLbl.TabIndex = 37;
            this.lblgnssDiffLbl.Text = "- - -";
            // 
            // sysStateToolStrip
            // 
            this.sysStateToolStrip.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sysStateToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.toolStripSeparator1,
            this.zeroDptAdjustBtn});
            this.sysStateToolStrip.Location = new System.Drawing.Point(0, 0);
            this.sysStateToolStrip.Name = "sysStateToolStrip";
            this.sysStateToolStrip.Size = new System.Drawing.Size(541, 32);
            this.sysStateToolStrip.TabIndex = 0;
            this.sysStateToolStrip.Text = "toolStrip1";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(115, 29);
            this.toolStripLabel2.Text = "System state";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 32);
            // 
            // zeroDptAdjustBtn
            // 
            this.zeroDptAdjustBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.zeroDptAdjustBtn.Enabled = false;
            this.zeroDptAdjustBtn.Font = new System.Drawing.Font("Segoe UI", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.zeroDptAdjustBtn.Image = ((System.Drawing.Image)(resources.GetObject("zeroDptAdjustBtn.Image")));
            this.zeroDptAdjustBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zeroDptAdjustBtn.Name = "zeroDptAdjustBtn";
            this.zeroDptAdjustBtn.Size = new System.Drawing.Size(173, 29);
            this.zeroDptAdjustBtn.Text = "Depth zero adjust";
            this.zeroDptAdjustBtn.Click += new System.EventHandler(this.zeroDptAdjustBtn_Click);
            // 
            // geoPlot
            // 
            this.geoPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.geoPlot.Location = new System.Drawing.Point(0, 32);
            this.geoPlot.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.geoPlot.Name = "geoPlot";
            this.geoPlot.Size = new System.Drawing.Size(612, 617);
            this.geoPlot.TabIndex = 2;
            // 
            // plotStatusStrip
            // 
            this.plotStatusStrip.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.plotStatusStrip.Location = new System.Drawing.Point(0, 649);
            this.plotStatusStrip.Name = "plotStatusStrip";
            this.plotStatusStrip.Size = new System.Drawing.Size(612, 22);
            this.plotStatusStrip.TabIndex = 1;
            this.plotStatusStrip.Text = "statusStrip1";
            // 
            // plotToolStrip
            // 
            this.plotToolStrip.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.plotToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripSeparator2,
            this.markPointBtn,
            this.plotClearBtn,
            this.clearMPBtn,
            this.clearGNSSBtn});
            this.plotToolStrip.Location = new System.Drawing.Point(0, 0);
            this.plotToolStrip.Name = "plotToolStrip";
            this.plotToolStrip.Size = new System.Drawing.Size(612, 32);
            this.plotToolStrip.TabIndex = 0;
            this.plotToolStrip.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(89, 29);
            this.toolStripLabel1.Text = "Geo view";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 32);
            // 
            // markPointBtn
            // 
            this.markPointBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.markPointBtn.Font = new System.Drawing.Font("Segoe UI", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.markPointBtn.Image = ((System.Drawing.Image)(resources.GetObject("markPointBtn.Image")));
            this.markPointBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.markPointBtn.Name = "markPointBtn";
            this.markPointBtn.Size = new System.Drawing.Size(116, 29);
            this.markPointBtn.Text = "Mark point";
            this.markPointBtn.Click += new System.EventHandler(this.markPointBtn_Click);
            // 
            // plotClearBtn
            // 
            this.plotClearBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.plotClearBtn.Font = new System.Drawing.Font("Segoe UI", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.plotClearBtn.Image = ((System.Drawing.Image)(resources.GetObject("plotClearBtn.Image")));
            this.plotClearBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.plotClearBtn.Name = "plotClearBtn";
            this.plotClearBtn.Size = new System.Drawing.Size(86, 29);
            this.plotClearBtn.Text = "Clear all";
            this.plotClearBtn.Click += new System.EventHandler(this.plotClearBtn_Click);
            // 
            // clearMPBtn
            // 
            this.clearMPBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.clearMPBtn.Font = new System.Drawing.Font("Segoe UI", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clearMPBtn.Image = ((System.Drawing.Image)(resources.GetObject("clearMPBtn.Image")));
            this.clearMPBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearMPBtn.Name = "clearMPBtn";
            this.clearMPBtn.Size = new System.Drawing.Size(134, 29);
            this.clearMPBtn.Text = "Clear marked";
            this.clearMPBtn.Click += new System.EventHandler(this.clearMPBtn_Click);
            // 
            // clearGNSSBtn
            // 
            this.clearGNSSBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.clearGNSSBtn.Font = new System.Drawing.Font("Segoe UI", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clearGNSSBtn.Image = ((System.Drawing.Image)(resources.GetObject("clearGNSSBtn.Image")));
            this.clearGNSSBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearGNSSBtn.Name = "clearGNSSBtn";
            this.clearGNSSBtn.Size = new System.Drawing.Size(117, 29);
            this.clearGNSSBtn.Text = "Clear GNSS";
            this.clearGNSSBtn.Click += new System.EventHandler(this.clearGNSSBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 727);
            this.Controls.Add(this.mainSplit);
            this.Controls.Add(this.mainStatusStrip);
            this.Controls.Add(this.mainToolStrip);
            this.DoubleBuffered = true;
            this.Name = "MainForm";
            this.Text = "RedNODE Host";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.mainSplit.Panel1.ResumeLayout(false);
            this.mainSplit.Panel1.PerformLayout();
            this.mainSplit.Panel2.ResumeLayout(false);
            this.mainSplit.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplit)).EndInit();
            this.mainSplit.ResumeLayout(false);
            this.sysStateTable.ResumeLayout(false);
            this.sysStateTable.PerformLayout();
            this.serialContextMnu.ResumeLayout(false);
            this.sysStateToolStrip.ResumeLayout(false);
            this.sysStateToolStrip.PerformLayout();
            this.plotToolStrip.ResumeLayout(false);
            this.plotToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.ToolStripButton connectionBtn;
        private System.Windows.Forms.ToolStripButton settingsBtn;
        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStripDropDownButton dataBtn;
        private System.Windows.Forms.ToolStripDropDownButton toolsBtn;
        private System.Windows.Forms.ToolStripButton infoBtn;
        private System.Windows.Forms.SplitContainer mainSplit;
        private System.Windows.Forms.TableLayoutPanel sysStateTable;
        private System.Windows.Forms.Label pressureRatingLbl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label acCoreInfoLbl;
        private System.Windows.Forms.Label systemInfoLbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStrip sysStateToolStrip;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.StatusStrip plotStatusStrip;
        private System.Windows.Forms.ToolStrip plotToolStrip;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Label serialNumberLbl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label depthLbl;
        private System.Windows.Forms.Label latLbl;
        private System.Windows.Forms.Label lonLbl;
        private System.Windows.Forms.Label rerrLbl;
        private System.Windows.Forms.Label pressureLbl;
        private System.Windows.Forms.Label temperatureLbl;
        private System.Windows.Forms.Label base1Lbl;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label base2Lbl;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label base3Lbl;
        private System.Windows.Forms.Label base4Lbl;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label gnssLatLbl;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label gnssLonLbl;
        private System.Windows.Forms.Label lblgnssDiffLbl;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.ToolStripButton plotClearBtn;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label soundSpeedLbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label salinityLbl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label gravityAccLbl;
        private System.Windows.Forms.Label waterDensityLbl;
        private System.Windows.Forms.ContextMenuStrip serialContextMnu;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private GeoPlot geoPlot;
        private System.Windows.Forms.ToolStripButton zeroDptAdjustBtn;
        private System.Windows.Forms.ToolStripButton markPointBtn;
        private System.Windows.Forms.ToolStripMenuItem saveTrackToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton gnssConnectBtn;
        private System.Windows.Forms.ToolStripMenuItem saveTemperatureVsDepthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSoundSpeedVsDepthToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton clearMPBtn;
        private System.Windows.Forms.ToolStripButton clearGNSSBtn;
        private System.Windows.Forms.ToolStripMenuItem parseFromLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parseFromAFileToolStripMenuItem;
    }
}

