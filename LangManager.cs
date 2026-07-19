using System.Reflection;
using System.Text;

namespace tree
{
    /// <summary>
    /// 多语言管理器，从嵌入资源中的 .lang 文件加载键值对
    /// .lang 文件格式：每行 key=value，# 开头为注释，空行忽略
    /// </summary>
    public static class LangManager
    {
        private static readonly Dictionary<string, string> _strings = new();

        /// <summary>
        /// 当前已加载的语言代码（如 "zh_cn"）
        /// </summary>
        public static string CurrentLang { get; private set; } = "";

        /// <summary>
        /// 语言切换时触发
        /// </summary>
        public static event Action? LanguageChanged;

        /// <summary>
        /// 从嵌入资源加载指定语言文件
        /// </summary>
        /// <param name="langCode">语言代码，对应资源名 tree.{langCode}.lang</param>
        public static void Load(string langCode)
        {
            _strings.Clear();
            CurrentLang = langCode;

            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string resourceName = $"tree.{langCode}.lang";

                using Stream? stream = assembly.GetManifestResourceStream(resourceName);
                if (stream == null)
                    return;

                using var reader = new StreamReader(stream, Encoding.UTF8);
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line.Length == 0 || line.StartsWith('#'))
                        continue;

                    int eq = line.IndexOf('=');
                    if (eq < 0)
                        continue;

                    string key = line[..eq].Trim();
                    string value = line[(eq + 1)..].Trim();
                    _strings[key] = value;
                }
            }
            catch
            {
                // 加载失败时回退到空字典，Get 方法会返回 key 本身
            }

            LanguageChanged?.Invoke();
        }

        /// <summary>
        /// 获取翻译字符串
        /// </summary>
        /// <param name="key">语言键</param>
        /// <returns>翻译后的字符串，未找到时返回 key 本身</returns>
        public static string Get(string key)
        {
            return _strings.TryGetValue(key, out string? value) ? value : key;
        }

        /// <summary>
        /// 获取翻译字符串并格式化
        /// </summary>
        /// <param name="key">语言键</param>
        /// <param name="args">格式参数</param>
        public static string Get(string key, params object[] args)
        {
            string fmt = Get(key);
            return args.Length > 0 ? string.Format(fmt, args) : fmt;
        }
    }
}
