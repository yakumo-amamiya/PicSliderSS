using System.IO;
using System.Reflection;
using System.Xml;
using PicSliderSS.Common;

namespace PicSliderSS.Config
{
    public class AppConfig
    {
        public static AppConfig Data;

        public string TargetFolder { get; set; }
        public bool Recurse { get; set; }
        public int MaxCount { get; set; }

        public void Load()
        {
            string configPath = GetConfigPath();
            LogUtils.WriteLog($"Load Config");
            LogUtils.WriteLog($"File         -> {configPath}");
            var doc = new XmlDocument();
            doc.Load(configPath);
            TargetFolder = GetValue(doc, nameof(TargetFolder));
            Recurse = bool.Parse(GetValue(doc, nameof(Recurse)));
            MaxCount = int.Parse(GetValue(doc, nameof(MaxCount)));
            LogUtils.WriteLog($"TargetFolder -> {TargetFolder}");
            LogUtils.WriteLog($"Recurse      -> {Recurse}");
            LogUtils.WriteLog($"MaxCount     -> {MaxCount}");
        }

        public static string GetConfigPath()
        {
            string exePath = Assembly.GetExecutingAssembly().Location;
            string exeDirPath = Path.GetDirectoryName(exePath);
            string configPath = exeDirPath + @"\" + "config.xml";
            return configPath;
        }
        
        private static string GetValue(XmlDocument doc, string key)
        {
            return doc.SelectSingleNode($"configuration/appSettings/{key}")?.InnerText;
        }
    }
}