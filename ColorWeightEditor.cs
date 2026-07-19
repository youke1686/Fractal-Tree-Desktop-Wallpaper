namespace tree
{
    public partial class ColorWeightEditor : Form
    {
        public List<ColorWeightEntry> Entries { get; private set; }

        private readonly Color _defaultColor;
        private readonly string _title;
        private readonly TableLayoutPanel _mainTable;
        private readonly Panel _entriesPanel;
        private readonly Button _addBtn;
        private readonly Button _okBtn;
        private readonly Button _cancelBtn;

        public ColorWeightEditor(string title, Color defaultColor, List<ColorWeightEntry>? existing)
        {
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath)!;
            _title = title;
            _defaultColor = defaultColor;
            Entries = existing != null ? existing.Select(e => e.Clone()).ToList() : new();
            if (Entries.Count == 0)
                Entries.Add(new ColorWeightEntry { Color = defaultColor });

            _mainTable = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(10)
            };
            _mainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            _mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 40f));
            _mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 40f));

            _entriesPanel = new Panel { Dock = DockStyle.Fill, AutoScroll = true, BorderStyle = BorderStyle.FixedSingle };
            _mainTable.Controls.Add(_entriesPanel, 0, 0);

            _addBtn = new Button { Text = LangManager.Get("coloreditor.btn.add"), Dock = DockStyle.Top, Height = 30 };
            _addBtn.Click += (_, _) => AddEntry();
            _mainTable.Controls.Add(_addBtn, 0, 1);

            FlowLayoutPanel okCancelPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(0, 5, 0, 0)
            };
            _cancelBtn = new Button { Text = LangManager.Get("coloreditor.btn.cancel"), Size = new Size(75, 30) };
            _cancelBtn.Click += (_, _) => { DialogResult = DialogResult.Cancel; Close(); };
            _okBtn = new Button { Text = LangManager.Get("coloreditor.btn.confirm"), Size = new Size(75, 30) };
            _okBtn.Click += (_, _) => { DialogResult = DialogResult.OK; Close(); };
            okCancelPanel.Controls.Add(_cancelBtn);
            okCancelPanel.Controls.Add(_okBtn);
            _mainTable.Controls.Add(okCancelPanel, 0, 2);

            Controls.Add(_mainTable);

            Text = _title + LangManager.Get("coloreditor.title.suffix");
            ClientSize = new Size(500, 340);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;

            foreach (ColorWeightEntry entry in Entries)
                CreateEntryRow(entry);
        }

        private void CreateEntryRow(ColorWeightEntry entry)
        {
            int y = _entriesPanel.Controls.Count * 38;
            Panel row = new Panel { Height = 36, Width = _entriesPanel.ClientSize.Width - 5, Location = new Point(2, y) };

            Panel colorPanel = new Panel
            {
                BackColor = entry.Color,
                Size = new Size(32, 26),
                Location = new Point(2, 5),
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand
            };
            colorPanel.Click += (_, _) =>
            {
                using ColorDialog cd = new ColorDialog { Color = entry.Color };
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    entry.Color = cd.Color;
                    colorPanel.BackColor = cd.Color;
                }
            };
            row.Controls.Add(colorPanel);

            Button selectBtn = new Button { Text = LangManager.Get("coloreditor.btn.pick"), Size = new Size(44, 26), Location = new Point(38, 5) };
            selectBtn.Click += (_, _) =>
            {
                using ColorDialog cd = new ColorDialog { Color = entry.Color };
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    entry.Color = cd.Color;
                    colorPanel.BackColor = cd.Color;
                }
            };
            row.Controls.Add(selectBtn);

            // 固定标签宽度避免中英文切换时覆盖输入框
            int labelW = 80;
            int xWeightLabel = 88;
            int xWeightNud = xWeightLabel + labelW + 4;
            int xRatioLabel = xWeightNud + 66;
            int xRatioNud = xRatioLabel + labelW + 4;
            int xDeleteBtn = xRatioNud + 66;

            Label weightLabel = new Label
            {
                Text = LangManager.Get("coloreditor.lbl.weight"),
                AutoSize = false,
                Size = new Size(labelW, 20),
                TextAlign = ContentAlignment.MiddleLeft,
                Location = new Point(xWeightLabel, 10)
            };
            row.Controls.Add(weightLabel);

            NumericUpDown weightNud = new NumericUpDown
            {
                Minimum = 1,
                Maximum = 10000,
                DecimalPlaces = 0,
                Value = (decimal)entry.Weight,
                Width = 60,
                Location = new Point(xWeightNud, 6)
            };
            weightNud.ValueChanged += (_, _) => entry.Weight = (float)weightNud.Value;
            row.Controls.Add(weightNud);

            Label ratioLabel = new Label
            {
                Text = LangManager.Get("coloreditor.lbl.width_ratio"),
                AutoSize = false,
                Size = new Size(labelW, 20),
                TextAlign = ContentAlignment.MiddleLeft,
                Location = new Point(xRatioLabel, 10)
            };
            row.Controls.Add(ratioLabel);

            NumericUpDown ratioNud = new NumericUpDown
            {
                Minimum = 0.01m,
                Maximum = 10m,
                DecimalPlaces = 3,
                Increment = 0.01m,
                Value = (decimal)entry.WidthRatio,
                Width = 60,
                Location = new Point(xRatioNud, 6)
            };
            ratioNud.ValueChanged += (_, _) => entry.WidthRatio = (float)ratioNud.Value;
            row.Controls.Add(ratioNud);

            Button deleteBtn = new Button { Text = "✕", Size = new Size(28, 26), Location = new Point(xDeleteBtn, 5) };
            deleteBtn.Click += (_, _) =>
            {
                _entriesPanel.Controls.Remove(row);
                Entries.Remove(entry);
                RepositionRows();
            };
            row.Controls.Add(deleteBtn);

            _entriesPanel.Controls.Add(row);
        }

        private void RepositionRows()
        {
            for (int i = 0; i < _entriesPanel.Controls.Count; i++)
                _entriesPanel.Controls[i].Top = i * 38 + 2;
        }

        private void AddEntry()
        {
            ColorWeightEntry entry = new ColorWeightEntry { Color = _defaultColor };
            Entries.Add(entry);
            CreateEntryRow(entry);
        }
    }
}
