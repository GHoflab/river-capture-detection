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
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
            this.SuspendLayout();
            // 
            // Picture
            // 
            this.Picture.Image = ((System.Drawing.Image)(resources.GetObject("Picture.Image")));
            this.Picture.InitialImage = ((System.Drawing.Image)(resources.GetObject("Picture.InitialImage")));
            this.Picture.Location = new System.Drawing.Point(252, 28);
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
            this.Coperation.Location = new System.Drawing.Point(27, 296);
            this.Coperation.Name = "Coperation";
            this.Coperation.Size = new System.Drawing.Size(619, 27);
            this.Coperation.TabIndex = 13;
            this.Coperation.Text = "Development Institution: Nanjing Normal University";
            // 
            // Developer
            // 
            this.Developer.AutoSize = true;
            this.Developer.Font = new System.Drawing.Font("华文中宋", 12F);
            this.Developer.Location = new System.Drawing.Point(198, 401);
            this.Developer.Name = "Developer";
            this.Developer.Size = new System.Drawing.Size(270, 27);
            this.Developer.TabIndex = 12;
            this.Developer.Text = "Developer: Qiyuan Ma";
            // 
            // Teacher
            // 
            this.Teacher.AutoSize = true;
            this.Teacher.Font = new System.Drawing.Font("华文中宋", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Teacher.Location = new System.Drawing.Point(226, 350);
            this.Teacher.Name = "Teacher";
            this.Teacher.Size = new System.Drawing.Size(354, 27);
            this.Teacher.TabIndex = 11;
            this.Teacher.Text = "Mentor: An-Bo Li, Ping Wang";
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Title.Location = new System.Drawing.Point(23, 218);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(626, 42);
            this.Title.TabIndex = 10;
            this.Title.Text = "Automatic Detection of River Capture";
            this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AboutUs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 461);
            this.Controls.Add(this.Picture);
            this.Controls.Add(this.Coperation);
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
    }
}