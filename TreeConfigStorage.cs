using System.Text.Json;

namespace tree
{
    /// <summary>
    /// 应用偏好设置文件的结构（prefab.json）
    /// 包含语言选择和树种排序
    /// </summary>
    internal class AppPrefs
    {
        public string Language { get; set; } = "zh_cn";
        public List<string> Order { get; set; } = new();
    }

    /// <summary>
    /// 树种配置的本地存储管理（AppData 目录）
    /// </summary>
    public static class TreeConfigStorage
    {
        private static readonly string DataDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "FractalTree");

        private static readonly string TreesDir = Path.Combine(DataDir, "trees");

        private static readonly string PrefsFile = Path.Combine(DataDir, "prefab.json");

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private static readonly char[] InvalidFileNameChars = Path.GetInvalidFileNameChars();

        /// <summary>
        /// 从本地加载所有已保存的树种配置（每个树存储为独立的 JSON 文件）
        /// 按 prefab.json 中记录的顺序排序，未在排序文件中的树放到末尾
        /// </summary>
        public static List<TreeConfig> LoadAll()
        {
            try
            {
                if (!Directory.Exists(TreesDir))
                    return new List<TreeConfig>();

                // 读取所有树的 JSON 文件
                var configByName = new Dictionary<string, TreeConfig>();
                foreach (string file in Directory.EnumerateFiles(TreesDir, "*.json"))
                {
                    try
                    {
                        string json = File.ReadAllText(file);
                        TreeConfig? config = JsonSerializer.Deserialize<TreeConfig>(json, JsonOptions);
                        if (config != null)
                        {
                            config.EnsureGroups();
                            configByName[config.Name] = config;
                        }
                    }
                    catch
                    {
                        // 单个文件损坏则跳过
                    }
                }

                // 按排序文件排列
                List<string> order = LoadOrder();
                var sorted = new List<TreeConfig>();
                var placed = new HashSet<string>();

                foreach (string name in order)
                {
                    if (configByName.TryGetValue(name, out TreeConfig? config))
                    {
                        sorted.Add(config);
                        placed.Add(name);
                    }
                }

                // 排序文件中不存在的树追加到末尾
                foreach (var kv in configByName)
                {
                    if (!placed.Contains(kv.Key))
                        sorted.Add(kv.Value);
                }

                return sorted;
            }
            catch
            {
                return new List<TreeConfig>();
            }
        }

        /// <summary>
        /// 将所有树种配置保存到本地（每个树独立一个 JSON 文件）
        /// </summary>
        public static void SaveAll(List<TreeConfig> configs)
        {
            try
            {
                if (!Directory.Exists(TreesDir))
                    Directory.CreateDirectory(TreesDir);

                // 清除旧文件
                foreach (string oldFile in Directory.EnumerateFiles(TreesDir, "*.json"))
                {
                    try { File.Delete(oldFile); } catch { }
                }

                // 逐个保存
                foreach (TreeConfig config in configs)
                {
                    string fileName = SanitizeFileName(config.Name) + ".json";
                    string filePath = Path.Combine(TreesDir, fileName);
                    string json = JsonSerializer.Serialize(config, JsonOptions);
                    File.WriteAllText(filePath, json);
                }
            }
            catch
            {
                // 静默处理保存失败
            }
        }

        /// <summary>
        /// 加载偏好设置
        /// </summary>
        private static AppPrefs ReadPrefs()
        {
            try
            {
                if (File.Exists(PrefsFile))
                {
                    string json = File.ReadAllText(PrefsFile);
                    return JsonSerializer.Deserialize<AppPrefs>(json) ?? new AppPrefs();
                }
            }
            catch { }

            // 尝试从旧版 _order.json 迁移
            string legacyFile = Path.Combine(DataDir, "_order.json");
            try
            {
                if (File.Exists(legacyFile))
                {
                    string json = File.ReadAllText(legacyFile);
                    var order = JsonSerializer.Deserialize<List<string>>(json);
                    var prefs = new AppPrefs { Order = order ?? new() };
                    string newJson = JsonSerializer.Serialize(prefs, JsonOptions);
                    File.WriteAllText(PrefsFile, newJson);
                    File.Delete(legacyFile);
                    return prefs;
                }
            }
            catch { }

            return new AppPrefs();
        }

        /// <summary>
        /// 保存偏好设置
        /// </summary>
        private static void WritePrefs(AppPrefs prefs)
        {
            try
            {
                string json = JsonSerializer.Serialize(prefs, JsonOptions);
                File.WriteAllText(PrefsFile, json);
            }
            catch { }
        }

        /// <summary>
        /// 加载语言设置
        /// </summary>
        public static string LoadLanguage()
        {
            return ReadPrefs().Language;
        }

        /// <summary>
        /// 保存语言设置（保留现有排序）
        /// </summary>
        public static void SaveLanguage(string language)
        {
            AppPrefs prefs = ReadPrefs();
            prefs.Language = language;
            WritePrefs(prefs);
        }

        /// <summary>
        /// 加载排序文件，返回树名称列表
        /// </summary>
        public static List<string> LoadOrder()
        {
            return ReadPrefs().Order;
        }

        /// <summary>
        /// 保存排序文件（保留语言设置）
        /// </summary>
        public static void SaveOrder(List<string> names)
        {
            AppPrefs prefs = ReadPrefs();
            prefs.Order = names;
            WritePrefs(prefs);
        }

        private static string SanitizeFileName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return LangManager.Get("options.default_name");

            char[] chars = name.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (Array.IndexOf(InvalidFileNameChars, chars[i]) >= 0)
                    chars[i] = '_';
            }
            return new string(chars).Trim();
        }
    }
}
