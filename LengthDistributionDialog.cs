using System.Drawing.Drawing2D;

namespace tree
{
    public class LengthDistributionDialog : Form
    {
        private const int BUCKET_COUNT = 8;
        private readonly float[] _weights;
        private readonly float _minVal;
        private readonly float _maxVal;

        private readonly Panel _chartPanel;
        private readonly Button _btnReset;
        private readonly Button _btnOk;
        private readonly Button _btnCancel;

        public float[] Weights => _weights;

        public LengthDistributionDialog(string title, float minVal, float maxVal, float[]? existingWeights)
        {
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath)!;
            _minVal = minVal;
            _maxVal = maxVal;
            _weights = existingWeights != null && existingWeights.Length == BUCKET_COUNT
                ? (float[])existingWeights.Clone()
                : Enumerable.Repeat(1f, BUCKET_COUNT).ToArray();

            Text = title;
            Size = new Size(520, 420);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.FromArgb(250, 248, 240);

            _chartPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(245, 242, 235),
                Padding = new Padding(50, 30, 30, 50)
            };
            _chartPanel.Paint += ChartPanel_Paint;
            _chartPanel.MouseDown += ChartPanel_MouseDown;

            _btnReset = new Button
            {
                Text = LangManager.Get("distribution.btn.reset"),
                AutoSize = true,
                Padding = new Padding(16, 6, 16, 6),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderColor = Color.FromArgb(180, 160, 140) },
                BackColor = Color.FromArgb(240, 235, 225),
                Cursor = Cursors.Hand
            };
            _btnReset.Click += (_, _) => ResetWeights();

            _btnOk = new Button
            {
                Text = LangManager.Get("distribution.btn.confirm"),
                AutoSize = true,
                Padding = new Padding(24, 6, 24, 6),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                BackColor = Color.FromArgb(130, 160, 110),
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            _btnOk.Click += (_, _) => { DialogResult = DialogResult.OK; Close(); };

            _btnCancel = new Button
            {
                Text = LangManager.Get("distribution.btn.cancel"),
                AutoSize = true,
                Padding = new Padding(24, 6, 24, 6),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                BackColor = Color.FromArgb(200, 180, 160),
                ForeColor = Color.FromArgb(80, 50, 30),
                Cursor = Cursors.Hand
            };
            _btnCancel.Click += (_, _) => { DialogResult = DialogResult.Cancel; Close(); };

            var bottomPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                Padding = new Padding(20, 2, 20, 8),
                FlowDirection = FlowDirection.RightToLeft,
                BackColor = Color.FromArgb(250, 248, 240)
            };
            bottomPanel.Controls.Add(_btnCancel);
            bottomPanel.Controls.Add(_btnOk);
            bottomPanel.Controls.Add(_btnReset);

            Controls.Add(_chartPanel);
            Controls.Add(bottomPanel);
        }

        private void ResetWeights()
        {
            for (int i = 0; i < BUCKET_COUNT; i++)
                _weights[i] = 1f;
            _chartPanel.Invalidate();
        }

        private void ChartPanel_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle bounds = _chartPanel.ClientRectangle;
            int left = bounds.Left + _chartPanel.Padding.Left;
            int right = bounds.Right - _chartPanel.Padding.Right;
            int top = bounds.Top + _chartPanel.Padding.Top;
            int bottom = bounds.Bottom - _chartPanel.Padding.Bottom;

            int chartW = right - left;
            int chartH = bottom - top;

            float barSpacing = chartW / (float)BUCKET_COUNT;
            float barWidth = barSpacing * 0.7f;

            using Font axisFont = new Font("Microsoft YaHei UI", 8F);
            using Font bucketFont = new Font("Microsoft YaHei UI", 7.5F);
            using Pen gridPen = new Pen(Color.FromArgb(210, 205, 195), 1);
            using Pen axisPen = new Pen(Color.FromArgb(120, 100, 80), 1.5f);
            using SolidBrush barBrush = new SolidBrush(Color.FromArgb(140, 170, 120));
            using SolidBrush textBrush = new SolidBrush(Color.FromArgb(80, 60, 40));

            for (int i = 0; i <= 5; i++)
            {
                float y = top + chartH * (1f - i / 5f);
                g.DrawLine(gridPen, left, y, right, y);
                string label = (i / 5f).ToString("0.0");
                g.DrawString(label, axisFont, textBrush, 2, y - 7);
            }

            for (int i = 0; i < BUCKET_COUNT; i++)
            {
                float barH = _weights[i] * chartH;
                float x = left + i * barSpacing + (barSpacing - barWidth) / 2f;
                float y = bottom - barH;

                RectangleF barRect = new RectangleF(x, y, barWidth, barH);
                g.FillRectangle(barBrush, barRect);
                g.DrawRectangle(gridPen, x, y, barWidth, barH);

                float val = _minVal + (i + 0.5f) / BUCKET_COUNT * (_maxVal - _minVal);
                string label = val.ToString("0.00");
                float tw = g.MeasureString(label, bucketFont).Width;
                g.DrawString(label, bucketFont, textBrush, x + (barWidth - tw) / 2f, bottom + 4);
            }

            g.DrawLine(axisPen, left, bottom, right, bottom);
            g.DrawLine(axisPen, left, top, left, bottom);

            using Font titleFont = new Font("Microsoft YaHei UI", 9F);
            g.DrawString(LangManager.Get("distribution.hint"), titleFont, textBrush,
                left, top - 20);
        }

        private int HitTestBucket(Point pt)
        {
            Rectangle bounds = _chartPanel.ClientRectangle;
            int left = bounds.Left + _chartPanel.Padding.Left;
            int right = bounds.Right - _chartPanel.Padding.Right;
            int top = bounds.Top + _chartPanel.Padding.Top;
            int bottom = bounds.Bottom - _chartPanel.Padding.Bottom;

            if (pt.X < left || pt.X > right || pt.Y < top || pt.Y > bottom)
                return -1;

            int chartW = right - left;
            float barSpacing = chartW / (float)BUCKET_COUNT;
            int idx = (int)((pt.X - left) / barSpacing);
            return Math.Clamp(idx, 0, BUCKET_COUNT - 1);
        }

        private void ChartPanel_MouseDown(object? sender, MouseEventArgs e)
        {
            int idx = HitTestBucket(e.Location);
            if (idx >= 0)
                SetWeightFromY(idx, e.Y);
        }

        private void SetWeightFromY(int idx, int mouseY)
        {
            Rectangle bounds = _chartPanel.ClientRectangle;
            int top = bounds.Top + _chartPanel.Padding.Top;
            int bottom = bounds.Bottom - _chartPanel.Padding.Bottom;
            int chartH = bottom - top;

            float ratio = 1f - Math.Clamp((mouseY - top) / (float)chartH, 0f, 1f);
            ratio = Math.Max(ratio, 0.02f);
            _weights[idx] = ratio;
            _chartPanel.Invalidate();
        }
    }
}
