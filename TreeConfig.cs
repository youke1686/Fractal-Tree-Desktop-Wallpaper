using System.Text.Json;

namespace tree
{
    public class ColorWeightEntry
    {
        public float Weight { get; set; } = 1f;

        public float WidthRatio { get; set; } = 0.2f;

        [System.Text.Json.Serialization.JsonIgnore]
        public Color Color { get; set; } = Color.Green;

        public int ColorArgb
        {
            get => Color.ToArgb();
            set => Color = Color.FromArgb(value);
        }

        public ColorWeightEntry Clone()
        {
            return new ColorWeightEntry
            {
                Weight = Weight,
                WidthRatio = WidthRatio,
                Color = Color
            };
        }
    }

    /// <summary>
    /// 单组参数配置，包含完整的分形树绘制参数 + 参数生效典型高度
    /// </summary>
    public class ParameterGroup
    {
        /// <summary>
        /// 参数组名称
        /// </summary>
        public string Name { get; set; } = "默认参数组";

        /// <summary>
        /// 参数生效典型高度（距离树根的像素值）
        /// </summary>
        public float TypicalHeight { get; set; } = 0f;

        public float BranchAngleMin { get; set; } = 5f;
        public float BranchAngleMax { get; set; } = 30f;

        /// <summary>
        /// 分叉角度概率分布权重（8 个区间，对应 BranchAngleMin ~ BranchAngleMax）
        /// </summary>
        public float[]? BranchAngleWeights { get; set; }

        public float LengthRatioMin { get; set; } = 0.6f;
        public float LengthRatioMax { get; set; } = 0.9f;

        /// <summary>
        /// 长度衰减概率分布权重（8 个区间，对应 LengthRatioMin ~ LengthRatioMax）
        /// </summary>
        public float[]? LengthRatioWeights { get; set; }

        public float MinBranchLength { get; set; } = 6f;
        public float InitialLength { get; set; } = 100f;

        public float LeafThreshold { get; set; } = 15f;

        /// <summary>
        /// 枝条最大倾角（度）
        /// </summary>
        public float LeafMaxAngle { get; set; } = 100f;

        /// <summary>
        /// 分支最大深度
        /// </summary>
        public int MaxDepth { get; set; } = 10;

        /// <summary>
        /// 落叶密度（0~1）
        /// </summary>
        public float FallenLeafDensity { get; set; } = 0.1f;

        /// <summary>
        /// 落叶分散程度（像素）
        /// </summary>
        public float FallenLeafDispersion { get; set; } = 100f;

        private List<ColorWeightEntry> _leafColors = new() { new ColorWeightEntry { Color = Color.FromArgb(34, 139, 34) } };
        private List<ColorWeightEntry> _trunkColors = new() { new ColorWeightEntry { Color = Color.FromArgb(139, 90, 43) } };

        public List<ColorWeightEntry> LeafColors
        {
            get => _leafColors;
            set => _leafColors = value ?? new() { new ColorWeightEntry { Color = Color.FromArgb(34, 139, 34) } };
        }

        public List<ColorWeightEntry> TrunkColors
        {
            get => _trunkColors;
            set => _trunkColors = value ?? new() { new ColorWeightEntry { Color = Color.FromArgb(139, 90, 43) } };
        }

        /// <summary>
        /// 创建本参数组的深拷贝
        /// </summary>
        public ParameterGroup Clone()
        {
            return new ParameterGroup
            {
                Name = Name,
                TypicalHeight = TypicalHeight,
                BranchAngleMin = BranchAngleMin,
                BranchAngleMax = BranchAngleMax,
                BranchAngleWeights = BranchAngleWeights != null ? (float[])BranchAngleWeights.Clone() : null,
                LengthRatioMin = LengthRatioMin,
                LengthRatioMax = LengthRatioMax,
                LengthRatioWeights = LengthRatioWeights != null ? (float[])LengthRatioWeights.Clone() : null,
                MinBranchLength = MinBranchLength,
                InitialLength = InitialLength,
                LeafThreshold = LeafThreshold,
                LeafMaxAngle = LeafMaxAngle,
                MaxDepth = MaxDepth,
                FallenLeafDensity = FallenLeafDensity,
                FallenLeafDispersion = FallenLeafDispersion,
                LeafColors = LeafColors.Select(e => e.Clone()).ToList(),
                TrunkColors = TrunkColors.Select(e => e.Clone()).ToList()
            };
        }
    }

    /// <summary>
    /// 分形树全局配置（名称、背景色、参数组列表）
    /// </summary>
    public class TreeConfig
    {
        /// <summary>
        /// 树种名称
        /// </summary>
        public string Name { get; set; } = "未命名";

        /// <summary>
        /// 多参数组（按高度分级），永远不为空
        /// </summary>
        public List<ParameterGroup> Groups { get; set; } = new() { new ParameterGroup() };

        [System.Text.Json.Serialization.JsonIgnore]
        public Color BackgroundColor { get; set; } = Color.FromArgb(245, 245, 240);

        public int BackgroundColorArgb
        {
            get => BackgroundColor.ToArgb();
            set => BackgroundColor = Color.FromArgb(value);
        }

        /// <summary>
        /// 确保 Groups 不为空
        /// </summary>
        public void EnsureGroups()
        {
            if (Groups == null || Groups.Count == 0)
                Groups = new List<ParameterGroup> { new ParameterGroup() };
        }

        public static TreeConfig Default => new()
        {
            Name = "经典分形树",
            BackgroundColor = Color.FromArgb(-1),
            Groups = new List<ParameterGroup>
            {
                new ParameterGroup
                {
                    Name = "经典分形树",
                    BranchAngleMin = 30f,
                    BranchAngleMax = 30f,
                    LengthRatioMin = 0.8f,
                    LengthRatioMax = 0.8f,
                    MinBranchLength = 6f,
                    InitialLength = 100f,
                    LeafThreshold = 15f,
                    LeafMaxAngle = 180f,
                    MaxDepth = 30,
                    FallenLeafDensity = 0.01f,
                    FallenLeafDispersion = 50f,
                    LeafColors = new()
                    {
                        new() { Color = Color.FromArgb(-14513374), WidthRatio = 0.2f },
                        new() { Color = Color.FromArgb(-13454286), WidthRatio = 0.2f }
                    },
                    TrunkColors = new()
                    {
                        new() { Color = Color.FromArgb(-7644629), WidthRatio = 0.1f }
                    }
                }
            }
        };

        public static TreeConfig Sakura => new()
        {
            Name = "樱花树",
            BackgroundColor = Color.FromArgb(-923682),
            Groups = new List<ParameterGroup>
            {
                new ParameterGroup
                {
                    Name = "樱花树",
                    BranchAngleMin = 15f,
                    BranchAngleMax = 20f,
                    LengthRatioMin = 0.7f,
                    LengthRatioMax = 0.9f,
                    MinBranchLength = 7f,
                    InitialLength = 120f,
                    LeafThreshold = 10f,
                    LeafMaxAngle = 100f,
                    MaxDepth = 20,
                    FallenLeafDensity = 0.02f,
                    FallenLeafDispersion = 50f,
                    LeafColors = new()
                    {
                        new() { Color = Color.FromArgb(-676435), WidthRatio = 0.1f },
                        new() { Color = Color.FromArgb(-134414), WidthRatio = 0.1f }
                    },
                    TrunkColors = new()
                    {
                        new() { Color = Color.FromArgb(-7644629), WidthRatio = 0.1f }
                    }
                }
            }
        };

        public static TreeConfig GreenTree => new()
        {
            Name = "绿树",
            BackgroundColor = Color.FromArgb(-3151923),
            Groups = new List<ParameterGroup>
            {
                new ParameterGroup
                {
                    Name = "绿树",
                    BranchAngleMin = 5f,
                    BranchAngleMax = 30f,
                    LengthRatioMin = 0.6f,
                    LengthRatioMax = 0.94f,
                    LengthRatioWeights = new float[] { 0.84063745f, 0.02f, 0.02f, 0.02f, 0.02f, 0.02f, 0.69721115f, 1f },
                    MinBranchLength = 6f,
                    InitialLength = 80f,
                    LeafThreshold = 15f,
                    LeafMaxAngle = 90f,
                    MaxDepth = 30,
                    FallenLeafDensity = 0.003f,
                    FallenLeafDispersion = 50f,
                    LeafColors = new()
                    {
                        new() { Color = Color.FromArgb(-14513374), WidthRatio = 0.2f },
                        new() { Color = Color.FromArgb(-13454286), WidthRatio = 0.2f }
                    },
                    TrunkColors = new()
                    {
                        new() { Color = Color.FromArgb(-7644629), WidthRatio = 0.1f }
                    }
                }
            }
        };

        public static TreeConfig Pine => new()
        {
            Name = "松树",
            BackgroundColor = Color.FromArgb(-2431825),
            Groups = new List<ParameterGroup>
            {
                new ParameterGroup
                {
                    Name = "松树",
                    BranchAngleMin = 0f,
                    BranchAngleMax = 90f,
                    BranchAngleWeights = new float[] { 1f, 0.02f, 0.02f, 0.02f, 0.02f, 0.02f, 0.02f, 0.21912348f },
                    LengthRatioMin = 0.7f,
                    LengthRatioMax = 0.94f,
                    LengthRatioWeights = new float[] { 1f, 0.35059762f, 0.3944223f, 0.3984064f, 0.374502f, 0.38247013f, 1f, 1f },
                    MinBranchLength = 6f,
                    InitialLength = 100f,
                    LeafThreshold = 15f,
                    LeafMaxAngle = 120f,
                    MaxDepth = 30,
                    FallenLeafDensity = 0.001f,
                    FallenLeafDispersion = 50f,
                    LeafColors = new()
                    {
                        new() { Color = Color.FromArgb(-14513374), WidthRatio = 0.2f },
                        new() { Color = Color.FromArgb(-16748288), WidthRatio = 0.2f }
                    },
                    TrunkColors = new()
                    {
                        new() { Color = Color.FromArgb(-7644629), WidthRatio = 0.1f }
                    }
                }
            }
        };

        public static TreeConfig Maple => new()
        {
            Name = "枫树",
            BackgroundColor = Color.FromArgb(-136997),
            Groups = new List<ParameterGroup>
            {
                new ParameterGroup
                {
                    Name = "主干",
                    TypicalHeight = 0f,
                    BranchAngleMin = 0f,
                    BranchAngleMax = 10f,
                    BranchAngleWeights = new float[] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f },
                    LengthRatioMin = 0.8f,
                    LengthRatioMax = 0.95f,
                    LengthRatioWeights = new float[] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f },
                    MinBranchLength = 6f,
                    InitialLength = 100f,
                    LeafThreshold = 10f,
                    LeafMaxAngle = 100f,
                    MaxDepth = 20,
                    FallenLeafDensity = 0.02f,
                    FallenLeafDispersion = 50f,
                    LeafColors = new()
                    {
                        new() { Color = Color.FromArgb(-1462438), WidthRatio = 1f },
                        new() { Color = Color.FromArgb(-3254505), WidthRatio = 1f }
                    },
                    TrunkColors = new()
                    {
                        new() { Color = Color.FromArgb(-7644629), WidthRatio = 0.1f }
                    }
                },
                new ParameterGroup
                {
                    Name = "树冠",
                    TypicalHeight = 180f,
                    BranchAngleMin = 0f,
                    BranchAngleMax = 60f,
                    BranchAngleWeights = new float[] { 1f, 0.02f, 0.027888417f, 0.11155379f, 0.21115535f, 0.31474102f, 0.5697211f, 1f },
                    LengthRatioMin = 0.3f,
                    LengthRatioMax = 0.95f,
                    LengthRatioWeights = new float[] { 0.02f, 0.02f, 0.02f, 0.02f, 0.565737f, 0.250996f, 0.27091634f, 1f },
                    MinBranchLength = 6f,
                    InitialLength = 100f,
                    LeafThreshold = 10f,
                    LeafMaxAngle = 100f,
                    MaxDepth = 20,
                    FallenLeafDensity = 0.02f,
                    FallenLeafDispersion = 50f,
                    LeafColors = new()
                    {
                        new() { Color = Color.FromArgb(-1462438), WidthRatio = 1f },
                        new() { Color = Color.FromArgb(-3254505), WidthRatio = 1f }
                    },
                    TrunkColors = new()
                    {
                        new() { Color = Color.FromArgb(-7644629), WidthRatio = 0.1f }
                    }
                }
            }
        };

        public static List<TreeConfig> Presets => new()
        {
            Default,
            Sakura,
            GreenTree,
            Pine,
            Maple
        };
    }
}
