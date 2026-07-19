namespace tree
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private Button btnSetWallpaper;
        private Button btnRefresh;
        private Button btnSaveImage;

        private void InitializeComponent()
        {
            btnSetWallpaper = new Button();
            btnRefresh = new Button();
            btnSaveImage = new Button();
            components = new System.ComponentModel.Container();
            SuspendLayout();

            btnSetWallpaper.Location = new Point(12, 560);
            btnSetWallpaper.Name = "btnSetWallpaper";
            btnSetWallpaper.Size = new Size(120, 28);
            btnSetWallpaper.TabIndex = 0;
            btnSetWallpaper.Text = "设为桌面壁纸";
            btnSetWallpaper.UseVisualStyleBackColor = true;
            btnSetWallpaper.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

            btnRefresh.Location = new Point(138, 560);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(75, 28);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "刷新";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

            btnSaveImage.Location = new Point(219, 560);
            btnSaveImage.Name = "btnSaveImage";
            btnSaveImage.Size = new Size(120, 28);
            btnSaveImage.TabIndex = 2;
            btnSaveImage.Text = "保存为图片";
            btnSaveImage.UseVisualStyleBackColor = true;
            btnSaveImage.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 600);
            Controls.Add(btnSetWallpaper);
            Controls.Add(btnRefresh);
            Controls.Add(btnSaveImage);
            Text = "分形树";
            DoubleBuffered = true;
            ResumeLayout(false);
        }

        #endregion
    }
}
