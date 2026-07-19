namespace tree
{
    partial class main
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            label1 = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            btnClassicTree = new Button();
            tableLayoutPanel2 = new TableLayoutPanel();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            menuStrip1 = new MenuStrip();
            languageMenuItem = new ToolStripMenuItem();
            menuZhCn = new ToolStripMenuItem();
            menuEnUs = new ToolStripMenuItem();
            menuJaJp = new ToolStripMenuItem();
            menuKoKr = new ToolStripMenuItem();
            menuRuRu = new ToolStripMenuItem();
            menuFrFr = new ToolStripMenuItem();
            menuDeDe = new ToolStripMenuItem();
            menuEsEs = new ToolStripMenuItem();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { languageMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(721, 24);
            menuStrip1.TabIndex = 0;
            // 
            // languageMenuItem
            // 
            languageMenuItem.DropDownItems.AddRange(new ToolStripItem[] { menuZhCn, menuEnUs, menuJaJp, menuKoKr, menuRuRu, menuFrFr, menuDeDe, menuEsEs });
            languageMenuItem.Name = "languageMenuItem";
            languageMenuItem.Size = new Size(65, 20);
            languageMenuItem.Text = "Language";
            // 
            // menuZhCn
            // 
            menuZhCn.Name = "menuZhCn";
            menuZhCn.Size = new Size(100, 22);
            menuZhCn.Tag = "zh_cn";
            menuZhCn.Text = "中文";
            // 
            // menuEnUs
            // 
            menuEnUs.Name = "menuEnUs";
            menuEnUs.Size = new Size(100, 22);
            menuEnUs.Tag = "en_us";
            menuEnUs.Text = "English";
            // 
            // menuJaJp
            // 
            menuJaJp.Name = "menuJaJp";
            menuJaJp.Size = new Size(100, 22);
            menuJaJp.Tag = "ja_jp";
            menuJaJp.Text = "日本語";
            // 
            // menuKoKr
            // 
            menuKoKr.Name = "menuKoKr";
            menuKoKr.Size = new Size(100, 22);
            menuKoKr.Tag = "ko_kr";
            menuKoKr.Text = "한국어";
            // 
            // menuRuRu
            // 
            menuRuRu.Name = "menuRuRu";
            menuRuRu.Size = new Size(100, 22);
            menuRuRu.Tag = "ru_ru";
            menuRuRu.Text = "Русский";
            // 
            // menuFrFr
            // 
            menuFrFr.Name = "menuFrFr";
            menuFrFr.Size = new Size(100, 22);
            menuFrFr.Tag = "fr_fr";
            menuFrFr.Text = "Français";
            // 
            // menuDeDe
            // 
            menuDeDe.Name = "menuDeDe";
            menuDeDe.Size = new Size(100, 22);
            menuDeDe.Tag = "de_de";
            menuDeDe.Text = "Deutsch";
            // 
            // menuEsEs
            // 
            menuEsEs.Name = "menuEsEs";
            menuEsEs.Size = new Size(100, 22);
            menuEsEs.Tag = "es_es";
            menuEsEs.Text = "Español";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.FromArgb(250, 248, 240);
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayoutPanel1.Size = new Size(721, 520);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Microsoft YaHei UI", 22F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(100, 60, 40);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(715, 130);
            label1.TabIndex = 1;
            label1.Text = "分形树桌面壁纸";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.BackColor = Color.Transparent;
            flowLayoutPanel1.Controls.Add(btnClassicTree);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(3, 133);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(20, 10, 20, 10);
            flowLayoutPanel1.Size = new Size(715, 306);
            flowLayoutPanel1.TabIndex = 2;
            // 
            // btnClassicTree
            // 
            btnClassicTree.AutoSize = true;
            btnClassicTree.BackColor = Color.FromArgb(130, 160, 110);
            btnClassicTree.Cursor = Cursors.Hand;
            btnClassicTree.FlatAppearance.BorderSize = 0;
            btnClassicTree.FlatAppearance.MouseDownBackColor = Color.FromArgb(105, 135, 85);
            btnClassicTree.FlatAppearance.MouseOverBackColor = Color.FromArgb(150, 180, 130);
            btnClassicTree.FlatStyle = FlatStyle.Flat;
            btnClassicTree.Font = new Font("Microsoft YaHei UI", 14F);
            btnClassicTree.ForeColor = Color.White;
            btnClassicTree.Location = new Point(23, 13);
            btnClassicTree.Margin = new Padding(3, 3, 12, 12);
            btnClassicTree.Name = "btnClassicTree";
            btnClassicTree.Padding = new Padding(30, 18, 30, 18);
            btnClassicTree.Size = new Size(190, 74);
            btnClassicTree.TabIndex = 0;
            btnClassicTree.Tag = "classic";
            btnClassicTree.Text = "经典分形树";
            btnClassicTree.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = Color.FromArgb(238, 232, 218);
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            tableLayoutPanel2.Controls.Add(button1, 0, 0);
            tableLayoutPanel2.Controls.Add(button2, 1, 0);
            tableLayoutPanel2.Controls.Add(button3, 2, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 445);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.Padding = new Padding(30, 10, 30, 10);
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(715, 72);
            tableLayoutPanel2.TabIndex = 3;
            // 
            // button1
            // 
            button1.AutoSize = true;
            button1.BackColor = Color.FromArgb(160, 180, 140);
            button1.Cursor = Cursors.Hand;
            button1.Dock = DockStyle.Fill;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.FromArgb(130, 150, 110);
            button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(180, 200, 160);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Microsoft YaHei UI", 12F);
            button1.ForeColor = Color.White;
            button1.Location = new Point(33, 13);
            button1.Name = "button1";
            button1.Size = new Size(212, 46);
            button1.TabIndex = 3;
            button1.Text = "添加";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.AutoSize = true;
            button2.BackColor = Color.FromArgb(180, 150, 120);
            button2.Cursor = Cursors.Hand;
            button2.Dock = DockStyle.Fill;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.MouseDownBackColor = Color.FromArgb(150, 120, 95);
            button2.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 170, 140);
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Microsoft YaHei UI", 12F);
            button2.ForeColor = Color.White;
            button2.Location = new Point(251, 13);
            button2.Name = "button2";
            button2.Size = new Size(212, 46);
            button2.TabIndex = 4;
            button2.Text = "编辑";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.AutoSize = true;
            button3.BackColor = Color.FromArgb(180, 150, 120);
            button3.Cursor = Cursors.Hand;
            button3.Dock = DockStyle.Fill;
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatAppearance.MouseDownBackColor = Color.FromArgb(150, 120, 95);
            button3.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 170, 140);
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Microsoft YaHei UI", 12F);
            button3.ForeColor = Color.White;
            button3.Location = new Point(469, 13);
            button3.Name = "button3";
            button3.Size = new Size(213, 46);
            button3.TabIndex = 5;
            button3.Text = "删除";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // main
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(250, 248, 240);
            ClientSize = new Size(721, 520);
            Controls.Add(menuStrip1);
            Controls.Add(tableLayoutPanel1);
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(480, 400);
            Name = "main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "分形树";
            tableLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
        }
        private TableLayoutPanel tableLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button btnClassicTree;
        private Label label1;
        private Button button1;
        private TableLayoutPanel tableLayoutPanel2;
        private Button button2;
        private Button button3;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem languageMenuItem;
        private ToolStripMenuItem menuZhCn;
        private ToolStripMenuItem menuEnUs;
        private ToolStripMenuItem menuJaJp;
        private ToolStripMenuItem menuKoKr;
        private ToolStripMenuItem menuRuRu;
        private ToolStripMenuItem menuFrFr;
        private ToolStripMenuItem menuDeDe;
        private ToolStripMenuItem menuEsEs;
    }
}
