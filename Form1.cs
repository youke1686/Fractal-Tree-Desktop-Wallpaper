using System.Drawing.Drawing2D;

namespace tree
{
    public partial class Form1 : Form
    {
        private readonly TreeConfig _config;

        private int rootSeed = Environment.TickCount;
        private Random _random;

        /// <summary>
        /// 某高度上的混合参数结果
        /// </summary>
        private struct ResolvedParams
        {
            public float MinBranchLength;
            public float LeafThreshold;
            public List<ColorWeightEntry> LeafColors;
            public List<ColorWeightEntry> TrunkColors;
            public float FallenLeafDensity;
            public float FallenLeafDispersion;
            public int MaxDepth;
            public float BranchAngleMin;
            public float BranchAngleMax;
            public float[]? BranchAngleWeights;
            public float LengthRatioMin;
            public float LengthRatioMax;
            public float[]? LengthRatioWeights;
            public float LeafMaxAngle;
            public float InitialLength;
        }

        private struct BranchSegment
        {
            public float StartX, StartY;
            public float Angle;
            public float Length;
            public int Depth;
        }

        private struct FallenLeaf
        {
            public float X, Y;
            public float Angle;
            public float Length;
            public float PenWidth;
            public Color Color;
        }

        /// <summary>
        /// 使用指定配置创建分形树窗口
        /// </summary>
        public Form1(TreeConfig config)
        {
            _config = config;
            InitializeComponent();
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath)!;

            BackColor = config.BackgroundColor;
            Text = LangManager.Get("form1.title");
            btnSetWallpaper.Text = LangManager.Get("form1.btn.set_wallpaper");
            btnRefresh.Text = LangManager.Get("form1.btn.refresh");
            btnSaveImage.Text = LangManager.Get("form1.btn.save_image");
            btnSetWallpaper.Click += BtnSetWallpaper_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnSaveImage.Click += BtnSaveImage_Click;

            ResizeRedraw = true;
        }

        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            rootSeed = Environment.TickCount;
            Invalidate();
        }

        /// <summary>
        /// 将当前分形树保存为 PNG 图片到桌面
        /// </summary>
        private void BtnSaveImage_Click(object? sender, EventArgs e)
        {
            btnSetWallpaper.Visible = false;
            btnRefresh.Visible = false;
            btnSaveImage.Visible = false;
            Invalidate();
            Update();

            int w = ClientSize.Width * 2;
            int h = ClientSize.Height * 2;
            string fileName = $"{_config.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, fileName);

            using (var bmp = new Bitmap(w, h))
            using (var g = Graphics.FromImage(bmp))
            {
                g.ScaleTransform(2f, 2f);
                DrawTree(g, ClientSize.Width, ClientSize.Height);
                bmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
            }

            btnSetWallpaper.Visible = true;
            btnRefresh.Visible = true;
            btnSaveImage.Visible = true;
            Invalidate();

            MessageBox.Show(
                LangManager.Get("form1.msg.save_success_prefix") + fileName,
                LangManager.Get("form1.msg.save_success.title"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnSetWallpaper_Click(object? sender, EventArgs e)
        {
            btnSetWallpaper.Visible = false;
            btnRefresh.Visible = false;
            btnSaveImage.Visible = false;

            var bounds = Screen.PrimaryScreen!.Bounds;

            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            Location = bounds.Location;
            Size = bounds.Size;

            Invalidate();
            Update();

            int w = bounds.Width * 2;
            int h = bounds.Height * 2;
            string tempPath = Path.Combine(Path.GetTempPath(), "tree_wallpaper.bmp");
            using (var bmp = new Bitmap(w, h))
            using (var g = Graphics.FromImage(bmp))
            {
                g.ScaleTransform(2f, 2f);
                DrawTree(g, bounds.Width, bounds.Height);
                bmp.Save(tempPath, System.Drawing.Imaging.ImageFormat.Bmp);
            }

            DesktopWallpaper.SetFromFile(tempPath);

            Close();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawTree(e.Graphics, ClientSize.Width, ClientSize.Height);
        }

        /// <summary>
        /// 广度优先绘制分形树；若有多参数组则按枝条高度反距离加权混合
        /// </summary>
        private void DrawTree(Graphics g, float width, float height)
        {
            _random = new(rootSeed);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(_config.BackgroundColor);

            float groundY = height * 0.9f;
            List<FallenLeaf> fallenLeaves = new List<FallenLeaf>();
            Random leafRandom = new Random(rootSeed ^ 0x5EEDA1);
            float scale = height / 1080f;

            Queue<BranchSegment> queue = new Queue<BranchSegment>();

            ResolvedParams rootParams = ResolveParams(0f);
            queue.Enqueue(new BranchSegment
            {
                StartX = width / 2f,
                StartY = groundY,
                Angle = 0f,
                Length = rootParams.InitialLength * scale,
                Depth = 0
            });

            while (queue.Count > 0)
            {
                BranchSegment seg = queue.Dequeue();

                float branchHeight = groundY - seg.StartY;
                ResolvedParams rp = ResolveParams(branchHeight / scale);

                if (seg.Length < rp.MinBranchLength * scale)
                    continue;

                float radian = seg.Angle * MathF.PI / 180f;
                float endX = seg.StartX + seg.Length * MathF.Sin(radian);
                float endY = seg.StartY - seg.Length * MathF.Cos(radian);

                if (endX < 0 || endX > width || endY < 0 || endY > height)
                    continue;

                bool isLeaf = seg.Length < rp.LeafThreshold * scale;
                    Color branchColor;
                    float penWidth;

                    if (isLeaf)
                    {
                        ColorWeightEntry leafEntry = PickWeightedEntry(rp.LeafColors, _random);
                        branchColor = leafEntry.Color;
                        penWidth = seg.Length * leafEntry.WidthRatio;
                    }
                    else
                    {
                        ColorWeightEntry trunkEntry = PickWeightedEntry(rp.TrunkColors, _random);
                        branchColor = trunkEntry.Color;
                        penWidth = seg.Length * trunkEntry.WidthRatio;
                    }

                    using Pen pen = new(branchColor, penWidth);
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                g.DrawLine(pen, seg.StartX, seg.StartY, endX, endY);

                if (isLeaf && (float)leafRandom.NextDouble() < rp.FallenLeafDensity)
                {
                    float xOff = RandomRange(leafRandom, -rp.FallenLeafDispersion, rp.FallenLeafDispersion);
                    float yOff = RandomRange(leafRandom, rp.FallenLeafDispersion * 0.5f, 0) + RandomRange(leafRandom, rp.FallenLeafDispersion * 0.5f, 0);
                    fallenLeaves.Add(new FallenLeaf
                    {
                        X = endX + xOff,
                        Y = groundY + yOff,
                        Angle = seg.Angle,
                        Length = seg.Length,
                        PenWidth = penWidth,
                        Color = branchColor
                    });
                }

                if (seg.Depth < rp.MaxDepth)
                {
                    float leftDelta = SampleBranchAngle(rp);
                    float rightDelta = SampleBranchAngle(rp);

                    float leftNewAngle = seg.Angle + leftDelta;
                    float rightNewAngle = seg.Angle - rightDelta;

                    float leftLength = seg.Length * SampleLengthRatio(rp);
                    float rightLength = seg.Length * SampleLengthRatio(rp);

                    if (MathF.Abs(leftNewAngle) <= rp.LeafMaxAngle)
                    {
                        queue.Enqueue(new BranchSegment
                        {
                            StartX = endX,
                            StartY = endY,
                            Angle = leftNewAngle,
                            Length = leftLength,
                            Depth = seg.Depth + 1
                        });
                    }
                    if (MathF.Abs(rightNewAngle) <= rp.LeafMaxAngle)
                    {
                        queue.Enqueue(new BranchSegment
                        {
                            StartX = endX,
                            StartY = endY,
                            Angle = rightNewAngle,
                            Length = rightLength,
                            Depth = seg.Depth + 1
                        });
                    }
                }
            }

            foreach (var fl in fallenLeaves)
            {
                float rad = fl.Angle * MathF.PI / 180f;
                float endXf = fl.X + fl.Length * MathF.Sin(rad);
                float endYf = fl.Y - fl.Length * MathF.Cos(rad);
                using Pen pen = new Pen(fl.Color, fl.PenWidth);
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                g.DrawLine(pen, fl.X, fl.Y, endXf, endYf);
            }
        }

        /// <summary>
        /// 按枝条高度对所有参数组做反距离加权平均，返回混合后的参数
        /// </summary>
        private ResolvedParams ResolveParams(float branchHeight)
        {
            List<ParameterGroup> groups = _config.Groups;

            if (groups.Count == 1)
                return FromGroup(groups[0]);

            float[] w = new float[groups.Count];
            float totalW = 0f;
            const float k = 1f;
            for (int i = 0; i < groups.Count; i++)
            {
                float dist = MathF.Abs(branchHeight - groups[i].TypicalHeight);
                if (dist < float.Epsilon)
                    w[i] = 128f;
                else
                    w[i] = Math.Min(k / dist, 128f);
                totalW += w[i];
            }

            for (int i = 0; i < groups.Count; i++)
                w[i] /= totalW;

            return BlendGroups(groups, w);
        }

        /// <summary>
        /// 将单个参数组转为 ResolvedParams
        /// </summary>
        private static ResolvedParams FromGroup(ParameterGroup g)
        {
            return new ResolvedParams
            {
                MinBranchLength = g.MinBranchLength,
                LeafThreshold = g.LeafThreshold,
                LeafColors = g.LeafColors,
                TrunkColors = g.TrunkColors,
                FallenLeafDensity = g.FallenLeafDensity,
                FallenLeafDispersion = g.FallenLeafDispersion,
                MaxDepth = g.MaxDepth,
                BranchAngleMin = g.BranchAngleMin,
                BranchAngleMax = g.BranchAngleMax,
                BranchAngleWeights = g.BranchAngleWeights,
                LengthRatioMin = g.LengthRatioMin,
                LengthRatioMax = g.LengthRatioMax,
                LengthRatioWeights = g.LengthRatioWeights,
                LeafMaxAngle = g.LeafMaxAngle,
                InitialLength = g.InitialLength
            };
        }

        /// <summary>
        /// 反距离加权混合多个参数组
        /// </summary>
        private static ResolvedParams BlendGroups(List<ParameterGroup> groups, float[] w)
        {
            ResolvedParams rp = default;

            rp.MinBranchLength = WeightedSum(groups, w, g => g.MinBranchLength);
            rp.LeafThreshold = WeightedSum(groups, w, g => g.LeafThreshold);
            rp.FallenLeafDensity = WeightedSum(groups, w, g => g.FallenLeafDensity);
            rp.FallenLeafDispersion = WeightedSum(groups, w, g => g.FallenLeafDispersion);
            rp.MaxDepth = (int)MathF.Round(WeightedSum(groups, w, g => g.MaxDepth));
            rp.BranchAngleMin = WeightedSum(groups, w, g => g.BranchAngleMin);
            rp.BranchAngleMax = WeightedSum(groups, w, g => g.BranchAngleMax);
            rp.LengthRatioMin = WeightedSum(groups, w, g => g.LengthRatioMin);
            rp.LengthRatioMax = WeightedSum(groups, w, g => g.LengthRatioMax);
            rp.LeafMaxAngle = WeightedSum(groups, w, g => g.LeafMaxAngle);
            rp.InitialLength = WeightedSum(groups, w, g => g.InitialLength);

            int bestIdx = 0;
            float bestW = w[0];
            for (int i = 1; i < w.Length; i++)
            {
                if (w[i] > bestW) { bestIdx = i; bestW = w[i]; }
            }
            rp.LeafColors = groups[bestIdx].LeafColors;
            rp.TrunkColors = groups[bestIdx].TrunkColors;

            rp.BranchAngleWeights = BlendWeightArrays(groups, w, g => g.BranchAngleWeights);
            rp.LengthRatioWeights = BlendWeightArrays(groups, w, g => g.LengthRatioWeights);

            return rp;
        }

        /// <summary>
        /// 普通参数加权求和
        /// </summary>
        private static float WeightedSum(List<ParameterGroup> groups, float[] w, Func<ParameterGroup, float> selector)
        {
            float sum = 0f;
            for (int i = 0; i < groups.Count; i++)
                sum += w[i] * selector(groups[i]);
            return sum;
        }

        /// <summary>
        /// 按权重随机选择颜色条目
        private static ColorWeightEntry PickWeightedEntry(List<ColorWeightEntry> entries, Random random)
        {
            float total = 0f;
            foreach (var e in entries)
                total += MathF.Max(e.Weight, 0f);

            if (total <= 0f)
                return entries[0];

            float r = (float)random.NextDouble() * total;
            float cum = 0f;
            foreach (var e in entries)
            {
                cum += MathF.Max(e.Weight, 0f);
                if (r <= cum)
                    return e;
            }
            return entries[^1];
        }

        /// <summary>
        /// 分布权重数组逐元素加权平均
        /// </summary>
        private static float[]? BlendWeightArrays(List<ParameterGroup> groups, float[] w, Func<ParameterGroup, float[]?> selector)
        {
            bool any = false;
            foreach (var g in groups)
            {
                if (selector(g) != null) { any = true; break; }
            }
            if (!any) return null;

            int maxLen = 0;
            foreach (var g in groups)
            {
                var arr = selector(g);
                if (arr != null && arr.Length > maxLen) maxLen = arr.Length;
            }

            float[] result = new float[maxLen];
            for (int elem = 0; elem < maxLen; elem++)
            {
                float num = 0f, denom = 0f;
                for (int i = 0; i < groups.Count; i++)
                {
                    var arr = selector(groups[i]);
                    if (arr != null && elem < arr.Length)
                    {
                        num += w[i] * arr[elem];
                        denom += w[i];
                    }
                }
                result[elem] = denom > 0f ? num / denom : 1f;
            }
            return result;
        }

        private static byte ClampByte(float v)
        {
            return (byte)Math.Clamp(v, 0f, 255f);
        }

        private static float RandomRange(Random random, float min, float max)
        {
            return min + (float)random.NextDouble() * (max - min);
        }

        /// <summary>
        /// 根据混合后的概率分布采样长度衰减比例
        /// </summary>
        private float SampleLengthRatio(ResolvedParams p)
        {
            float[]? weights = p.LengthRatioWeights;
            if (weights == null || weights.Length == 0)
                return RandomRange(_random, p.LengthRatioMin, p.LengthRatioMax);

            float total = 0f;
            foreach (var v in weights)
                total += v;
            if (total <= 0f)
                return RandomRange(_random, p.LengthRatioMin, p.LengthRatioMax);

            float r = (float)_random.NextDouble() * total;
            float cum = 0f;
            int idx = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                cum += weights[i];
                if (r <= cum) { idx = i; break; }
            }

            float bucketSize = (p.LengthRatioMax - p.LengthRatioMin) / weights.Length;
            float bMin = p.LengthRatioMin + idx * bucketSize;
            return RandomRange(_random, bMin, bMin + bucketSize);
        }

        /// <summary>
        /// 根据混合后的概率分布采样分叉角度
        /// </summary>
        private float SampleBranchAngle(ResolvedParams p)
        {
            float[]? weights = p.BranchAngleWeights;
            if (weights == null || weights.Length == 0)
                return RandomRange(_random, p.BranchAngleMin, p.BranchAngleMax);

            float total = 0f;
            foreach (var v in weights)
                total += v;
            if (total <= 0f)
                return RandomRange(_random, p.BranchAngleMin, p.BranchAngleMax);

            float r = (float)_random.NextDouble() * total;
            float cum = 0f;
            int idx = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                cum += weights[i];
                if (r <= cum) { idx = i; break; }
            }

            float bucketSize = (p.BranchAngleMax - p.BranchAngleMin) / weights.Length;
            float bMin = p.BranchAngleMin + idx * bucketSize;
            return RandomRange(_random, bMin, bMin + bucketSize);
        }
    }
}
