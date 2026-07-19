namespace tree
{
    public partial class main : Form
    {
        /// <summary>
        /// 当前已加载的用户自定义树种配置列表
        /// </summary>
        private readonly List<TreeConfig> _savedConfigs = new();

        /// <summary>
        /// 是否处于编辑模式
        /// </summary>
        private bool _isEditMode;

        /// <summary>
        /// 是否处于删除模式
        /// </summary>
        private bool _isDeleteMode;

        /// <summary>
        /// 硬编码预设树种配置的引用集合，用于保护预设不被编辑/删除
        /// </summary>
        private readonly HashSet<TreeConfig> _presetConfigs = new();

        /// <summary>
        /// 删除按钮的默认背景色
        /// </summary>
        private static readonly Color DeleteNormalColor = Color.FromArgb(180, 150, 120);

        /// <summary>
        /// 删除按钮的激活背景色
        /// </summary>
        private static readonly Color DeleteActiveColor = Color.FromArgb(200, 60, 50);

        /// <summary>
        /// 编辑按钮的默认背景色
        /// </summary>
        private static readonly Color EditNormalColor = Color.FromArgb(180, 150, 120);

        /// <summary>
        /// 编辑按钮的激活背景色
        /// </summary>
        private static readonly Color EditActiveColor = Color.FromArgb(200, 100, 80);

        /// <summary>
        /// 拖拽起始点，用于判断是否触发拖拽
        /// </summary>
        private Point _dragStartPoint;

        /// <summary>
        /// 预设树种名称到 lang key 的映射（用于多语言显示）
        /// </summary>
        private static readonly Dictionary<string, string> PresetKeys = new()
        {
            ["经典分形树"] = "preset.classic",
            ["樱花树"] = "preset.sakura",
            ["绿树"] = "preset.green",
            ["松树"] = "preset.pine",
            ["枫树"] = "preset.maple",
            ["主干"] = "preset.maple.group1",
            ["树冠"] = "preset.maple.group2",
        };

        /// <summary>
        /// 获取树的显示名称（预设名支持多语言，用户自定义名保持原样）
        /// </summary>
        private static string GetDisplayName(TreeConfig config)
        {
            if (PresetKeys.TryGetValue(config.Name, out string? key))
                return LangManager.Get(key);
            return config.Name;
        }

        public main()
        {
            InitializeComponent();
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath)!;
            SetupMenu();
            ApplyLocalization();
            flowLayoutPanel1.Layout += FlowLayoutPanel1_Layout;
            btnClassicTree.Visible = false;
            AddPresetButtons();
            LoadSavedTrees();
            ApplySavedOrder();
            LangManager.LanguageChanged += OnLanguageChanged;
        }

        /// <summary>
        /// 初始化菜单栏事件绑定
        /// </summary>
        private void SetupMenu()
        {
            menuZhCn.Click += LanguageMenuItem_Click;
            menuEnUs.Click += LanguageMenuItem_Click;
            menuJaJp.Click += LanguageMenuItem_Click;
            menuKoKr.Click += LanguageMenuItem_Click;
            menuRuRu.Click += LanguageMenuItem_Click;
            menuFrFr.Click += LanguageMenuItem_Click;
            menuDeDe.Click += LanguageMenuItem_Click;
            menuEsEs.Click += LanguageMenuItem_Click;
        }

        /// <summary>
        /// 语言菜单项点击处理
        /// </summary>
        private void LanguageMenuItem_Click(object? sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item && item.Tag is string lang && lang != LangManager.CurrentLang)
            {
                LangManager.Load(lang);
                TreeConfigStorage.SaveLanguage(lang);
            }
        }

        /// <summary>
        /// 语言切换时的回调
        /// </summary>
        private void OnLanguageChanged()
        {
            ApplyLocalization();
            RefreshTreeButtonTexts();
        }

        /// <summary>
        /// 对当前界面的所有固定文字进行本地化
        /// </summary>
        private void ApplyLocalization()
        {
            languageMenuItem.Text = "🌍" + LangManager.Get("menu.language") + "(Language)";
            menuZhCn.Text = LangManager.Get("menu.lang.zh_cn");
            menuEnUs.Text = LangManager.Get("menu.lang.en_us");
            menuJaJp.Text = LangManager.Get("menu.lang.ja_jp");
            menuKoKr.Text = LangManager.Get("menu.lang.ko_kr");
            menuRuRu.Text = LangManager.Get("menu.lang.ru_ru");
            menuFrFr.Text = LangManager.Get("menu.lang.fr_fr");
            menuDeDe.Text = LangManager.Get("menu.lang.de_de");
            menuEsEs.Text = LangManager.Get("menu.lang.es_es");
            Text = LangManager.Get("main.title");
            label1.Text = LangManager.Get("main.label.title");
            button1.Text = LangManager.Get("main.btn.add");
            button2.Text = _isEditMode ? LangManager.Get("main.btn.edit.done") : LangManager.Get("main.btn.edit");
            button3.Text = _isDeleteMode ? LangManager.Get("main.btn.delete.done") : LangManager.Get("main.btn.delete");
        }

        /// <summary>
        /// 刷新所有树按钮的显示文字（用于语言切换时更新预设名）
        /// </summary>
        private void RefreshTreeButtonTexts()
        {
            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                if (ctrl is Button btn && btn.Tag is TreeConfig config)
                    btn.Text = GetDisplayName(config);
            }
        }

        /// <summary>
        /// 点击"添加"按钮
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            options opt = new options();
            if (opt.ShowDialog() == DialogResult.OK)
            {
                _savedConfigs.Add(opt.Config);
                TreeConfigStorage.SaveAll(_savedConfigs);
                AddTreeButton(opt.Config);
                SavePanelOrder();
            }
        }

        /// <summary>
        /// 点击"编辑"按钮，切换编辑模式
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            if (_isDeleteMode)
                return;

            _isEditMode = !_isEditMode;

            button2.Text = _isEditMode ? LangManager.Get("main.btn.edit.done") : LangManager.Get("main.btn.edit");
            button2.BackColor = _isEditMode ? EditActiveColor : EditNormalColor;

            button1.Enabled = !_isEditMode;
            button3.Enabled = !_isEditMode;

            UpdateTreeButtonsStyle();
        }

        /// <summary>
        /// 点击"删除"按钮，切换删除模式
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            if (_isEditMode)
                return;

            _isDeleteMode = !_isDeleteMode;

            button3.Text = _isDeleteMode ? LangManager.Get("main.btn.delete.done") : LangManager.Get("main.btn.delete");
            button3.BackColor = _isDeleteMode ? DeleteActiveColor : DeleteNormalColor;

            button1.Enabled = !_isDeleteMode;
            button2.Enabled = !_isDeleteMode;

            UpdateTreeButtonsStyle();
        }

        /// <summary>
        /// 点击树种按钮：正常打开树，编辑打开 options，删除移除按钮
        /// </summary>
        private void TreeButton_Click(object sender, EventArgs e)
        {
            if (sender is not Button btn || btn.Tag is not TreeConfig config)
                return;

            if (_isEditMode)
            {
                if (_presetConfigs.Contains(config))
                {
                    MessageBox.Show(
                        LangManager.Get("main.msg.preset_no_edit"),
                        LangManager.Get("main.msg.title"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                options opt = new options(config);
                if (opt.ShowDialog() == DialogResult.OK)
                {
                    config.Name = opt.Config.Name;
                    config.BackgroundColor = opt.Config.BackgroundColor;
                    config.Groups = opt.Config.Groups;

                    btn.Text = GetDisplayName(config);
                    TreeConfigStorage.SaveAll(_savedConfigs);
                }
            }
            else if (_isDeleteMode)
            {
                if (_presetConfigs.Contains(config))
                {
                    MessageBox.Show(
                        LangManager.Get("main.msg.preset_no_delete"),
                        LangManager.Get("main.msg.title"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                _savedConfigs.Remove(config);
                flowLayoutPanel1.Controls.Remove(btn);
                btn.Dispose();
                TreeConfigStorage.SaveAll(_savedConfigs);
                SavePanelOrder();
            }
            else
            {
                OpenTreeWindow(config);
            }
        }

        /// <summary>
        /// 从本地加载已保存的树种配置并创建按钮
        /// </summary>
        private void LoadSavedTrees()
        {
            List<TreeConfig> loaded = TreeConfigStorage.LoadAll();
            _savedConfigs.Clear();
            _savedConfigs.AddRange(loaded);

            foreach (TreeConfig config in _savedConfigs)
            {
                AddTreeButton(config);
            }
        }

        /// <summary>
        /// 添加硬编码预设树种按钮
        /// </summary>
        private void AddPresetButtons()
        {
            foreach (TreeConfig preset in TreeConfig.Presets)
            {
                _presetConfigs.Add(preset);
                AddTreeButton(preset);
            }
        }

        /// <summary>
        /// 在主界面树种列表中添加一个按钮
        /// </summary>
        private void AddTreeButton(TreeConfig config)
        {
            Button btn = new Button();
            btn.Text = GetDisplayName(config);
            btn.AutoSize = false;
            btn.Size = new Size(190, 74);
            btn.Cursor = Cursors.Hand;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Font = new Font("Microsoft YaHei UI", 14F);
            btn.ForeColor = Color.White;
            btn.Margin = new Padding(3, 3, 12, 12);
            btn.Padding = new Padding(30, 18, 30, 18);
            btn.Tag = config;
            btn.UseVisualStyleBackColor = false;
            btn.Click += TreeButton_Click;
            btn.MouseDown += TreeButton_MouseDown;
            btn.MouseMove += TreeButton_MouseMove;
            btn.AllowDrop = true;
            btn.DragEnter += TreeButton_DragEnter;
            btn.DragDrop += TreeButton_DragDrop;

            ApplyTreeButtonStyle(btn);

            flowLayoutPanel1.Controls.Add(btn);
        }

        /// <summary>
        /// 记录拖拽起始点
        /// </summary>
        private void TreeButton_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                _dragStartPoint = e.Location;
        }

        /// <summary>
        /// 超出拖拽阈值后启动拖拽操作
        /// </summary>
        private void TreeButton_MouseMove(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || _isEditMode || _isDeleteMode)
                return;

            if (Math.Abs(e.X - _dragStartPoint.X) < SystemInformation.DragSize.Width &&
                Math.Abs(e.Y - _dragStartPoint.Y) < SystemInformation.DragSize.Height)
                return;

            if (sender is Button btn)
                btn.DoDragDrop(btn, DragDropEffects.Move);
        }

        /// <summary>
        /// 拖入时允许移动
        /// </summary>
        private void TreeButton_DragEnter(object? sender, DragEventArgs e)
        {
            if (e.Data?.GetDataPresent(typeof(Button)) == true)
                e.Effect = DragDropEffects.Move;
        }

        /// <summary>
        /// 放下时重排按钮顺序并保存
        /// </summary>
        private void TreeButton_DragDrop(object? sender, DragEventArgs e)
        {
            if (e.Data?.GetData(typeof(Button)) is not Button draggedBtn || sender is not Button targetBtn)
                return;

            if (draggedBtn == targetBtn)
                return;

            int targetIndex = flowLayoutPanel1.Controls.GetChildIndex(targetBtn);

            // SetChildIndex 在两种情况下都得到正确结果：
            // - dragged 在 target 之后 → 插入到 target 之前（即 targetIndex）
            // - dragged 在 target 之前 → 移除后 target 索引 -1，插入到 targetIndex 即为 target 之后
            flowLayoutPanel1.Controls.SetChildIndex(draggedBtn, targetIndex);

            SyncConfigOrder();
        }

        /// <summary>
        /// 根据面板控件顺序同步 _savedConfigs 并保存
        /// </summary>
        private void SyncConfigOrder()
        {
            _savedConfigs.Clear();
            var allNames = new List<string>();
            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                if (ctrl is Button btn && btn.Tag is TreeConfig config)
                {
                    allNames.Add(config.Name);
                    if (!_presetConfigs.Contains(config))
                        _savedConfigs.Add(config);
                }
            }
            TreeConfigStorage.SaveAll(_savedConfigs);
            TreeConfigStorage.SaveOrder(allNames);
        }

        /// <summary>
        /// 启动时根据 _order.json 中的完整顺序重排面板按钮
        /// </summary>
        private void ApplySavedOrder()
        {
            List<string> order = TreeConfigStorage.LoadOrder();
            if (order.Count == 0)
                return;

            var btnByName = new Dictionary<string, Control>();
            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                if (ctrl is Button btn && btn.Tag is TreeConfig config)
                    btnByName[config.Name] = ctrl;
            }

            int pos = 0;
            foreach (string name in order)
            {
                if (btnByName.TryGetValue(name, out Control? ctrl))
                {
                    flowLayoutPanel1.Controls.SetChildIndex(ctrl, pos);
                    pos++;
                }
            }
        }

        /// <summary>
        /// 按面板当前控件顺序保存排序文件
        /// </summary>
        private void SavePanelOrder()
        {
            var allNames = new List<string>();
            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                if (ctrl is Button btn && btn.Tag is TreeConfig config)
                    allNames.Add(config.Name);
            }
            TreeConfigStorage.SaveOrder(allNames);
        }

        /// <summary>
        /// 根据当前模式给按钮设置颜色
        /// </summary>
        private void ApplyTreeButtonStyle(Button btn)
        {
            if (btn.Tag is TreeConfig config && _presetConfigs.Contains(config))
            {
                btn.BackColor = Color.FromArgb(160, 140, 110);
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(180, 160, 130);
                btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(130, 110, 85);
                return;
            }

            if (_isEditMode)
            {
                btn.BackColor = Color.FromArgb(200, 120, 100);
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 140, 120);
                btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(170, 90, 70);
            }
            else if (_isDeleteMode)
            {
                btn.BackColor = Color.FromArgb(210, 70, 60);
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(230, 90, 80);
                btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(180, 40, 30);
            }
            else
            {
                btn.BackColor = Color.FromArgb(160, 140, 110);
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(180, 160, 130);
                btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(130, 110, 85);
            }
        }

        /// <summary>
        /// 刷新所有动态树种按钮的样式
        /// </summary>
        private void UpdateTreeButtonsStyle()
        {
            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                if (ctrl is Button btn && btn.Tag is TreeConfig)
                {
                    ApplyTreeButtonStyle(btn);
                }
            }
        }

        /// <summary>
        /// 打开一个新的分形树窗口
        /// </summary>
        private void OpenTreeWindow(TreeConfig config)
        {
            Form1 treeForm = new Form1(config);
            treeForm.StartPosition = FormStartPosition.CenterScreen;
            treeForm.Show();
        }

        /// <summary>
        /// 动态调整左 padding 使按钮在面板中居中
        /// </summary>
        private void FlowLayoutPanel1_Layout(object sender, LayoutEventArgs e)
        {
            int panelWidth = flowLayoutPanel1.ClientSize.Width;
            int buttonTotalWidth = 190 + 3 + 12;
            int countPerRow = Math.Max(1, (panelWidth - 40) / buttonTotalWidth);
            int usedWidth = countPerRow * buttonTotalWidth - 12;
            int leftPad = Math.Max(20, (panelWidth - usedWidth) / 2);
            flowLayoutPanel1.Padding = new Padding(leftPad, 10, 20, 10);
        }
    }
}
