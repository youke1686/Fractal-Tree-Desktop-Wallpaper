using System.Reflection;

namespace tree
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            string lang = TreeConfigStorage.LoadLanguage();
            if (string.IsNullOrEmpty(lang))
                lang = "zh_cn";
            LangManager.Load(lang);

            ExtractHelpFile();

            Application.Run(new main());
        }

        private static void ExtractHelpFile()
        {
            string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FractalTree");
            string filePath = Path.Combine(dir, "help.html");

            if (File.Exists(filePath))
                return;

            Directory.CreateDirectory(dir);

            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream("tree.help.html");
            if (stream == null)
                return;

            using var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            stream.CopyTo(fs);
        }
    }
}