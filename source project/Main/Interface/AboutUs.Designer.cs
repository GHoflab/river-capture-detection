namespace Main.Interface
{
    partial class AboutUs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutUs));
            this.Picture = new System.Windows.Forms.PictureBox();
            this.Coperation = new System.Windows.Forms.Label();
            this.Developer = new System.Windows.Forms.Label();
            this.Teacher = new System.Windows.Forms.Label();
            this.Title = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
            this.SuspendLayout();
            // 
            // Picture
            // 
            this.Picture.Image = ((System.Drawing.Image)(resources.GetObject("Picture.Image")));
            this.Picture.InitialImage = ((System.Drawing.Image)(resources.GetObject("Picture.InitialImage")));
            this.Picture.Location = new System.Drawing.Point(300, 28);
            this.Picture.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Picture.Name = "Picture";
            this.Picture.Size = new System.Drawing.Size(166, 166);
            this.Picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Picture.TabIndex = 14;
            this.Picture.TabStop = false;
            // 
            // Coperation
            // 
            this.Coperation.AutoSize = true;
            this.Coperation.Font = new System.Drawing.Font("华文中宋", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Coperation.Location = new System.Drawing.Point(74, 296);
            this.Coperation.Name = "Coperation";
            this.Coperation.Size = new System.Drawing.Size(619, 27);
            this.Coperation.TabIndex = 13;
            this.Coperation.Text = "Development Institution: Nanjing Normal University";
            // 
            // Developer
            // 
            this.Developer.AutoSize = true;
            this.Developer.Font = new System.Drawing.Font("华文中宋", 12F);
            this.Developer.Location = new System.Drawing.Point(243, 392);
            this.Developer.Name = "Developer";
            this.Developer.Size = new System.Drawing.Size(282, 27);
            this.Developer.TabIndex = 12;
            this.Developer.Text = "Developer: Qi-Yuan Ma";
            // 
            // Teacher
            // 
            this.Teacher.AutoSize = true;
            this.Teacher.Font = new System.Drawing.Font("华文中宋", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Teacher.Location = new System.Drawing.Point(272, 344);
            this.Teacher.Name = "Teacher";
            this.Teacher.Size = new System.Drawing.Size(354, 27);
            this.Teacher.TabIndex = 11;
            this.Teacher.Text = "Mentor: An-Bo Li, Ping Wang";
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Title.Location = new System.Drawing.Point(70, 218);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(626, 42);
            this.Title.TabIndex = 10;
            this.Title.Text = "Automatic Detection of River Capture";
            this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("华文中宋", 12F);
            this.label1.Location = new System.Drawing.Point(39, 440);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(688, 54);
            this.label1.TabIndex = 12;
            this.label1.Text = "Paper: Automatic detection of river capture based on \r\n          planform pattern" +
                " and χ-plot of the stream network";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabel1.Location = new System.Drawing.Point(138, 515);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(562, 24);
            this.linkLabel1.TabIndex = 15;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "https://doi.org/10.1016/j.geomorph.2023.108587";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("华文中宋", 12F);
            this.label2.Location = new System.Drawing.Point(66, 514);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 27);
            this.label2.TabIndex = 12;
            this.label2.Text = "DOI:";
            // 
            // AboutUs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 563);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.Picture);
            this.Controls.Add(this.Coperation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Developer);
            this.Controls.Add(this.Teacher);
            this.Controls.Add(this.Title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutUs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About Us";
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Picture;
        private System.Windows.Forms.Label Coperation;
        private System.Windows.Forms.Label Developer;
        private System.Windows.Forms.Label Teacher;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label2;
    }
}