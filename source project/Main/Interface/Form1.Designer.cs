namespace Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.axMapControl2 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openShpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openRasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eagleeyeMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupRegionsOfInterestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.roughIdentificationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateΧplotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oneRiverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allGroupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.χplotIdentificationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oneGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allGroupsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayOneΧplotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayΧplotOfGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.axToolbarControl1.Location = new System.Drawing.Point(0, 32);
            this.axToolbarControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(1149, 28);
            this.axToolbarControl1.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 60);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.axMapControl2);
            this.splitContainer1.Panel1.Controls.Add(this.axTOCControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer1.Panel2.Controls.Add(this.axMapControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1149, 515);
            this.splitContainer1.SplitterDistance = 279;
            this.splitContainer1.TabIndex = 2;
            // 
            // axMapControl2
            // 
            this.axMapControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.axMapControl2.Location = new System.Drawing.Point(0, 349);
            this.axMapControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.axMapControl2.Name = "axMapControl2";
            this.axMapControl2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl2.OcxState")));
            this.axMapControl2.Size = new System.Drawing.Size(279, 166);
            this.axMapControl2.TabIndex = 0;
            this.axMapControl2.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl2_OnMouseDown);
            this.axMapControl2.OnMouseUp += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseUpEventHandler(this.axMapControl2_OnMouseUp);
            this.axMapControl2.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.axMapControl2_OnMouseMove);
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axTOCControl1.Location = new System.Drawing.Point(0, 0);
            this.axTOCControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(279, 515);
            this.axTOCControl1.TabIndex = 1;
            this.axTOCControl1.OnMouseDown += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseDownEventHandler(this.axTOCControl1_OnMouseDown);
            this.axTOCControl1.OnMouseUp += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseUpEventHandler(this.axTOCControl1_OnMouseUp);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 493);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(866, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // axMapControl1
            // 
            this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl1.Location = new System.Drawing.Point(0, 0);
            this.axMapControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(866, 515);
            this.axMapControl1.TabIndex = 1;
            this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            this.axMapControl1.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.axMapControl1_OnMouseMove);
            this.axMapControl1.OnViewRefreshed += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnViewRefreshedEventHandler(this.axMapControl1_OnViewRefreshed);
            this.axMapControl1.OnExtentUpdated += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnExtentUpdatedEventHandler(this.axMapControl1_OnExtentUpdated);
            this.axMapControl1.OnMapReplaced += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMapReplacedEventHandler(this.axMapControl1_OnMapReplaced);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.groupRegionsOfInterestToolStripMenuItem,
            this.roughIdentificationToolStripMenuItem,
            this.generateΧplotToolStripMenuItem,
            this.χplotIdentificationToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1149, 32);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openShpToolStripMenuItem,
            this.openRasterToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(52, 28);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openShpToolStripMenuItem
            // 
            this.openShpToolStripMenuItem.Name = "openShpToolStripMenuItem";
            this.openShpToolStripMenuItem.Size = new System.Drawing.Size(256, 28);
            this.openShpToolStripMenuItem.Text = "Open vector file";
            this.openShpToolStripMenuItem.Click += new System.EventHandler(this.openShpToolStripMenuItem_Click);
            // 
            // openRasterToolStripMenuItem
            // 
            this.openRasterToolStripMenuItem.Name = "openRasterToolStripMenuItem";
            this.openRasterToolStripMenuItem.Size = new System.Drawing.Size(256, 28);
            this.openRasterToolStripMenuItem.Text = "Open raster file";
            this.openRasterToolStripMenuItem.Click += new System.EventHandler(this.openRasterToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(256, 28);
            this.saveToolStripMenuItem.Text = "Save map document";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(253, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(256, 28);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBarToolStripMenuItem,
            this.toolBarToolStripMenuItem,
            this.eagleeyeMapToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(63, 28);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // statusBarToolStripMenuItem
            // 
            this.statusBarToolStripMenuItem.Checked = true;
            this.statusBarToolStripMenuItem.CheckOnClick = true;
            this.statusBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.statusBarToolStripMenuItem.Name = "statusBarToolStripMenuItem";
            this.statusBarToolStripMenuItem.Size = new System.Drawing.Size(206, 28);
            this.statusBarToolStripMenuItem.Text = "Status bar";
            this.statusBarToolStripMenuItem.CheckedChanged += new System.EventHandler(this.statusBarToolStripMenuItem_CheckedChanged);
            // 
            // toolBarToolStripMenuItem
            // 
            this.toolBarToolStripMenuItem.Checked = true;
            this.toolBarToolStripMenuItem.CheckOnClick = true;
            this.toolBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolBarToolStripMenuItem.Name = "toolBarToolStripMenuItem";
            this.toolBarToolStripMenuItem.Size = new System.Drawing.Size(206, 28);
            this.toolBarToolStripMenuItem.Text = "Tool bar";
            this.toolBarToolStripMenuItem.CheckedChanged += new System.EventHandler(this.toolBarToolStripMenuItem_CheckedChanged);
            // 
            // eagleeyeMapToolStripMenuItem
            // 
            this.eagleeyeMapToolStripMenuItem.Checked = true;
            this.eagleeyeMapToolStripMenuItem.CheckOnClick = true;
            this.eagleeyeMapToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.eagleeyeMapToolStripMenuItem.Name = "eagleeyeMapToolStripMenuItem";
            this.eagleeyeMapToolStripMenuItem.Size = new System.Drawing.Size(206, 28);
            this.eagleeyeMapToolStripMenuItem.Text = "Eagle eye map";
            this.eagleeyeMapToolStripMenuItem.CheckedChanged += new System.EventHandler(this.eagleeyeMapToolStripMenuItem_CheckedChanged);
            // 
            // groupRegionsOfInterestToolStripMenuItem
            // 
            this.groupRegionsOfInterestToolStripMenuItem.Name = "groupRegionsOfInterestToolStripMenuItem";
            this.groupRegionsOfInterestToolStripMenuItem.Size = new System.Drawing.Size(256, 28);
            this.groupRegionsOfInterestToolStripMenuItem.Text = "Generate Candiate Regions";
            this.groupRegionsOfInterestToolStripMenuItem.Click += new System.EventHandler(this.groupRegionsOfInterestToolStripMenuItem_Click);
            // 
            // roughIdentificationToolStripMenuItem
            // 
            this.roughIdentificationToolStripMenuItem.Name = "roughIdentificationToolStripMenuItem";
            this.roughIdentificationToolStripMenuItem.Size = new System.Drawing.Size(168, 28);
            this.roughIdentificationToolStripMenuItem.Text = "Rough Detection";
            this.roughIdentificationToolStripMenuItem.Click += new System.EventHandler(this.roughIdentificationToolStripMenuItem_Click);
            // 
            // generateΧplotToolStripMenuItem
            // 
            this.generateΧplotToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oneRiverToolStripMenuItem,
            this.allGroupsToolStripMenuItem});
            this.generateΧplotToolStripMenuItem.Name = "generateΧplotToolStripMenuItem";
            this.generateΧplotToolStripMenuItem.Size = new System.Drawing.Size(159, 28);
            this.generateΧplotToolStripMenuItem.Text = "Generate χ-plot";
            // 
            // oneRiverToolStripMenuItem
            // 
            this.oneRiverToolStripMenuItem.Name = "oneRiverToolStripMenuItem";
            this.oneRiverToolStripMenuItem.Size = new System.Drawing.Size(296, 28);
            this.oneRiverToolStripMenuItem.Text = "Single river";
            this.oneRiverToolStripMenuItem.Click += new System.EventHandler(this.oneRiverToolStripMenuItem_Click);
            // 
            // allGroupsToolStripMenuItem
            // 
            this.allGroupsToolStripMenuItem.Name = "allGroupsToolStripMenuItem";
            this.allGroupsToolStripMenuItem.Size = new System.Drawing.Size(296, 28);
            this.allGroupsToolStripMenuItem.Text = "Target candidate regions";
            this.allGroupsToolStripMenuItem.Click += new System.EventHandler(this.allGroupsToolStripMenuItem_Click);
            // 
            // χplotIdentificationToolStripMenuItem
            // 
            this.χplotIdentificationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oneGroupToolStripMenuItem,
            this.allGroupsToolStripMenuItem1});
            this.χplotIdentificationToolStripMenuItem.Name = "χplotIdentificationToolStripMenuItem";
            this.χplotIdentificationToolStripMenuItem.Size = new System.Drawing.Size(187, 28);
            this.χplotIdentificationToolStripMenuItem.Text = "Accurate Detection";
            // 
            // oneGroupToolStripMenuItem
            // 
            this.oneGroupToolStripMenuItem.Name = "oneGroupToolStripMenuItem";
            this.oneGroupToolStripMenuItem.Size = new System.Drawing.Size(285, 28);
            this.oneGroupToolStripMenuItem.Text = "Single candidate region";
            this.oneGroupToolStripMenuItem.Click += new System.EventHandler(this.oneGroupToolStripMenuItem_Click);
            // 
            // allGroupsToolStripMenuItem1
            // 
            this.allGroupsToolStripMenuItem1.Name = "allGroupsToolStripMenuItem1";
            this.allGroupsToolStripMenuItem1.Size = new System.Drawing.Size(285, 28);
            this.allGroupsToolStripMenuItem1.Text = "All candidate regions";
            this.allGroupsToolStripMenuItem1.Click += new System.EventHandler(this.allGroupsToolStripMenuItem1_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayOneΧplotToolStripMenuItem,
            this.displayΧplotOfGroupToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(67, 28);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // displayOneΧplotToolStripMenuItem
            // 
            this.displayOneΧplotToolStripMenuItem.Name = "displayOneΧplotToolStripMenuItem";
            this.displayOneΧplotToolStripMenuItem.Size = new System.Drawing.Size(259, 28);
            this.displayOneΧplotToolStripMenuItem.Text = "Display single χ-plot";
            this.displayOneΧplotToolStripMenuItem.Click += new System.EventHandler(this.displayOneΧplotToolStripMenuItem_Click);
            // 
            // displayΧplotOfGroupToolStripMenuItem
            // 
            this.displayΧplotOfGroupToolStripMenuItem.Name = "displayΧplotOfGroupToolStripMenuItem";
            this.displayΧplotOfGroupToolStripMenuItem.Size = new System.Drawing.Size(259, 28);
            this.displayΧplotOfGroupToolStripMenuItem.Text = "Display χ-plots";
            this.displayΧplotOfGroupToolStripMenuItem.Click += new System.EventHandler(this.displayΧplotOfGroupToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(92, 28);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(63, 28);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(158, 28);
            this.AboutToolStripMenuItem.Text = "About us";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tableToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(135, 32);
            // 
            // tableToolStripMenuItem
            // 
            this.tableToolStripMenuItem.Name = "tableToolStripMenuItem";
            this.tableToolStripMenuItem.Size = new System.Drawing.Size(134, 28);
            this.tableToolStripMenuItem.Text = "属性表";
            this.tableToolStripMenuItem.Click += new System.EventHandler(this.tableToolStripMenuItem_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(146, 32);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(145, 28);
            this.toolStripMenuItem1.Text = "查看χ图";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1149, 575);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Automatic Detection of River Capture";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statusBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eagleeyeMapToolStripMenuItem;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl2;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private System.Windows.Forms.ToolStripMenuItem openShpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openRasterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem groupRegionsOfInterestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem roughIdentificationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateΧplotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oneRiverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allGroupsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem χplotIdentificationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oneGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allGroupsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayOneΧplotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayΧplotOfGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tableToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}

