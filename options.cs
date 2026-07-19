namespace tree
{
    public partial class options : Form
    {
        public TreeConfig Config { get; private set; } = TreeConfig.Default;
        private List<ParameterGroup> _groups = new();
        private int _currentGroupIndex = -1;
        private bool _suppressEvents;
        private float[]? _distributionWeights;
        private float[]? _angleWeights;
        private List<ColorWeightEntry> _leafColors = new();
        private List<ColorWeightEntry> _trunkColors = new();

        public options() : this(null)
        {
        }

        /// <summary>
        /// 创建参数编辑界面，若传入已有配置则预填
        /// </summary>
        public options(TreeConfig existing)
        {
            InitializeComponent();
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath)!;

            // 应用多语言文本
            Text = LangManager.Get("options.title");
            label3.Text = LangManager.Get("options.lbl.group_control");
            lblName.Text = LangManager.Get("options.lbl.name");
            lblLeaf.Text = LangManager.Get("options.lbl.leaf_threshold");
            lblMinBranch.Text = LangManager.Get("options.lbl.min_branch");
            lblTrunkColor.Text = LangManager.Get("options.lbl.trunk_settings");
            btnTrunkSettings.Text = LangManager.Get("options.btn.settings");
            lblLeafColor.Text = LangManager.Get("options.lbl.leaf_settings");
            btnLeafSettings.Text = LangManager.Get("options.btn.settings");
            lblBgColor.Text = LangManager.Get("options.lbl.bg_color");
            btnBackgroundColor.Text = LangManager.Get("options.btn.select");
            label2.Text = LangManager.Get("options.lbl.typical_height");
            lblFallenLeafDispersion.Text = LangManager.Get("options.lbl.fallen_dispersion");
            lblFallenLeafDensity.Text = LangManager.Get("options.lbl.fallen_density");
            lblMaxDepth.Text = LangManager.Get("options.lbl.max_depth");
            lblLeafMaxAngle.Text = LangManager.Get("options.lbl.max_angle");
            lblRatio.Text = LangManager.Get("options.lbl.ratio_range");
            lblAngle.Text = LangManager.Get("options.lbl.angle_range");
            lblInitialLength.Text = LangManager.Get("options.lbl.initial_length");
            btnDistribution.Text = LangManager.Get("options.btn.distribution");
            btnAngleDistribution.Text = LangManager.Get("options.btn.distribution");
            btnConfirm.Text = LangManager.Get("options.btn.confirm");
            btnPreview.Text = LangManager.Get("options.btn.preview");
            btnCancel.Text = LangManager.Get("options.btn.cancel");
            helpBtn.Text = LangManager.Get("options.btn.help");
            openFolder.Text = LangManager.Get("options.btn.open_folder");

            if (existing != null)
            {
                txtName.Text = existing.Name;
                panelBackgroundColor.BackColor = existing.BackgroundColor;
                _groups = existing.Groups.Select(g => g.Clone()).ToList();
            }
            else
            {
                _groups.Add(new ParameterGroup());
            }

            PopulateGroupSelector();
            SelectGroup(0);

            btnConfirm.Click += btnConfirm_Click;
            btnPreview.Click += btnPreview_Click;
            btnCancel.Click += btnCancel_Click;

            btnBackgroundColor.Click += (_, _) => ChooseColor(panelBackgroundColor);
            btnDistribution.Click += BtnDistribution_Click;
            btnAngleDistribution.Click += BtnAngleDistribution_Click;
            btnTrunkSettings.Click += BtnTrunkSettings_Click;
            btnLeafSettings.Click += BtnLeafSettings_Click;

            addBtn.Click += addBtn_Click;
            button1.Click += button1_Click;
            cmbGroupSelector.SelectedIndexChanged += cmbGroupSelector_SelectedIndexChanged;
            cmbGroupSelector.Leave += cmbGroupSelector_Leave;
            cmbGroupSelector.KeyDown += cmbGroupSelector_KeyDown;

            helpBtn.Click += HelpBtn_Click;
            openFolder.Click += OpenFolder_Click;
        }

        private static void OpenFolder_Click(object? sender, EventArgs e)
        {
            string folder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "FractalTree",
                "trees");

            Directory.CreateDirectory(folder);

            var psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = folder,
                UseShellExecute = true
            };
            System.Diagnostics.Process.Start(psi);
        }

        private static void HelpBtn_Click(object? sender, EventArgs e)
        {
            string filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "FractalTree",
                "help.html");

            if (File.Exists(filePath))
            {
                var psi = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                };
                System.Diagnostics.Process.Start(psi);
            }
        }

        /// <summary>
        /// 绑定退出事件：退出时确保当前编辑的名称被提交
        /// </summary>
        private void CommitGroupName()
        {
            if (_currentGroupIndex < 0 || _currentGroupIndex >= _groups.Count)
                return;

            string newName = cmbGroupSelector.Text.Trim();
            if (string.IsNullOrEmpty(newName) || newName == _groups[_currentGroupIndex].Name)
                return;

            _groups[_currentGroupIndex].Name = newName;
            _suppressEvents = true;
            int itemsIndex = cmbGroupSelector.SelectedIndex;
            cmbGroupSelector.Items[_currentGroupIndex] = newName;
            cmbGroupSelector.SelectedIndex = itemsIndex;
            _suppressEvents = false;
        }

        /// <summary>
        /// 刷新 ComboBox 中的参数组名称列表
        /// </summary>
        private void PopulateGroupSelector()
        {
            _suppressEvents = true;
            cmbGroupSelector.Items.Clear();
            foreach (ParameterGroup g in _groups)
            {
                cmbGroupSelector.Items.Add(g.Name);
            }
            _suppressEvents = false;
        }

        /// <summary>
        /// 切换到指定索引的参数组，先保存当前组
        /// </summary>
        private void SelectGroup(int index)
        {
            if (_currentGroupIndex >= 0 && _currentGroupIndex < _groups.Count)
            {
                CommitGroupName();
                SaveCurrentGroup();
            }

            _currentGroupIndex = index;
            _suppressEvents = true;
            cmbGroupSelector.SelectedIndex = index;
            _suppressEvents = false;

            if (index >= 0 && index < _groups.Count)
                LoadGroup(index);

            UpdateDeleteButtonState();
        }

        /// <summary>
        /// 将界面当前值保存到当前参数组
        /// </summary>
        private void SaveCurrentGroup()
        {
            if (_currentGroupIndex < 0 || _currentGroupIndex >= _groups.Count)
                return;

            ParameterGroup g = _groups[_currentGroupIndex];
            g.TypicalHeight = (float)numericUpDown1.Value;
            g.BranchAngleMin = (float)numAngleMin.Value;
            g.BranchAngleMax = (float)numAngleMax.Value;
            g.LengthRatioMin = (float)numRatioMin.Value;
            g.LengthRatioMax = (float)numRatioMax.Value;
            g.LeafThreshold = (float)numLeafThreshold.Value;
            g.MinBranchLength = (float)numMinBranch.Value;
            g.LeafMaxAngle = (float)numLeafMaxAngle.Value;
            g.FallenLeafDensity = (float)numFallenLeafDensity.Value;
            g.FallenLeafDispersion = (float)numFallenLeafDispersion.Value;
            g.MaxDepth = (int)numMaxDepth.Value;
            g.InitialLength = (float)numInitialLength.Value;
            g.LengthRatioWeights = _distributionWeights != null ? (float[])_distributionWeights.Clone() : null;
            g.BranchAngleWeights = _angleWeights != null ? (float[])_angleWeights.Clone() : null;
            g.LeafColors = _leafColors.Select(e => e.Clone()).ToList();
            g.TrunkColors = _trunkColors.Select(e => e.Clone()).ToList();
        }

        /// <summary>
        /// 将指定索引的参数组加载到界面控件
        /// </summary>
        private void LoadGroup(int index)
        {
            ParameterGroup g = _groups[index];
            _suppressEvents = true;
            cmbGroupSelector.Text = g.Name;
            _suppressEvents = false;
            numericUpDown1.Value = (decimal)g.TypicalHeight;
            numInitialLength.Value = (decimal)g.InitialLength;
            numAngleMin.Value = (decimal)g.BranchAngleMin;
            numAngleMax.Value = (decimal)g.BranchAngleMax;
            numRatioMin.Value = (decimal)g.LengthRatioMin;
            numRatioMax.Value = (decimal)g.LengthRatioMax;
            numLeafThreshold.Value = (decimal)g.LeafThreshold;
            numMinBranch.Value = (decimal)g.MinBranchLength;
            numLeafMaxAngle.Value = (decimal)g.LeafMaxAngle;
            numFallenLeafDensity.Value = (decimal)g.FallenLeafDensity;
            numFallenLeafDispersion.Value = (decimal)g.FallenLeafDispersion;
            numMaxDepth.Value = g.MaxDepth;
            _distributionWeights = g.LengthRatioWeights != null ? (float[])g.LengthRatioWeights.Clone() : null;
            _angleWeights = g.BranchAngleWeights != null ? (float[])g.BranchAngleWeights.Clone() : null;
            _leafColors = g.LeafColors.Select(e => e.Clone()).ToList();
            _trunkColors = g.TrunkColors.Select(e => e.Clone()).ToList();
        }

        /// <summary>
        /// 更新删除按钮的启用状态
        /// </summary>
        private void UpdateDeleteButtonState()
        {
            button1.Enabled = _groups.Count > 1;
        }

        /// <summary>
        /// 新增参数组
        /// </summary>
        private void addBtn_Click(object? sender, EventArgs e)
        {
            SaveCurrentGroup();

            ParameterGroup newGroup = _currentGroupIndex >= 0 && _currentGroupIndex < _groups.Count
                ? _groups[_currentGroupIndex].Clone()
                : new ParameterGroup();

            newGroup.Name = LangManager.Get("options.param_group", _groups.Count + 1);
            newGroup.TypicalHeight += 50f;

            _groups.Add(newGroup);
            PopulateGroupSelector();
            SelectGroup(_groups.Count - 1);
        }

        /// <summary>
        /// 删除当前参数组
        /// </summary>
        private void button1_Click(object? sender, EventArgs e)
        {
            if (_groups.Count <= 1)
                return;

            int removedIndex = _currentGroupIndex;
            _groups.RemoveAt(removedIndex);
            PopulateGroupSelector();

            int newIndex = Math.Min(removedIndex, _groups.Count - 1);
            SelectGroup(newIndex);
        }

        /// <summary>
        /// ComboBox 下拉切换参数组
        /// </summary>
        private void cmbGroupSelector_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_suppressEvents)
                return;

            int selected = cmbGroupSelector.SelectedIndex;
            if (selected < 0)
                return;

            if (selected != _currentGroupIndex)
            {
                SaveCurrentGroup();
                _currentGroupIndex = selected;
                LoadGroup(selected);
                UpdateDeleteButtonState();
            }
        }

        /// <summary>
        /// ComboBox 失去焦点时提交名称修改
        /// </summary>
        private void cmbGroupSelector_Leave(object? sender, EventArgs e)
        {
            CommitGroupName();
        }

        /// <summary>
        /// 按回车提交名称修改
        /// </summary>
        private void cmbGroupSelector_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CommitGroupName();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void BtnDistribution_Click(object? sender, EventArgs e)
        {
            float min = (float)numRatioMin.Value;
            float max = (float)numRatioMax.Value;
            using var dialog = new LengthDistributionDialog(LangManager.Get("dialog.length_distribution"), min, max, _distributionWeights);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _distributionWeights = dialog.Weights;
            }
        }

        private void BtnAngleDistribution_Click(object? sender, EventArgs e)
        {
            float min = (float)numAngleMin.Value;
            float max = (float)numAngleMax.Value;
            using var dialog = new LengthDistributionDialog(LangManager.Get("dialog.angle_distribution"), min, max, _angleWeights);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _angleWeights = dialog.Weights;
            }
        }

        private void BtnTrunkSettings_Click(object? sender, EventArgs e)
        {
            using var editor = new ColorWeightEditor(LangManager.Get("dialog.trunk"), Color.FromArgb(139, 90, 43), _trunkColors);
            if (editor.ShowDialog() == DialogResult.OK)
            {
                _trunkColors = editor.Entries;
            }
        }

        private void BtnLeafSettings_Click(object? sender, EventArgs e)
        {
            using var editor = new ColorWeightEditor(LangManager.Get("dialog.leaf"), Color.FromArgb(34, 139, 34), _leafColors);
            if (editor.ShowDialog() == DialogResult.OK)
            {
                _leafColors = editor.Entries;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Config = BuildConfig();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            Form1 treeForm = new Form1(BuildConfig());
            treeForm.StartPosition = FormStartPosition.CenterScreen;
            treeForm.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// 根据当前界面输入构建 TreeConfig（含多参数组）
        /// </summary>
        private TreeConfig BuildConfig()
        {
            CommitGroupName();
            SaveCurrentGroup();

            return new TreeConfig
            {
                Name = string.IsNullOrWhiteSpace(txtName.Text) ? LangManager.Get("options.default_name") : txtName.Text.Trim(),
                BackgroundColor = panelBackgroundColor.BackColor,
                Groups = _groups.Select(g => g.Clone()).ToList()
            };
        }

        /// <summary>
        /// 打开颜色对话框并更新对应预览面板
        /// </summary>
        private static void ChooseColor(Panel preview)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = preview.BackColor;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                preview.BackColor = dialog.Color;
            }
        }
    }
}
