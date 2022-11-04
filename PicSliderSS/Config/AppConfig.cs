using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using PicSliderSS.Common;

namespace PicSliderSS.Config
{
    public class AppConfig : INotifyPropertyChanged
    {
        public static AppConfig Data;

        private string _targetFolder;

        public string TargetFolder
        {
            get => _targetFolder;
            set => SetField(ref _targetFolder, value);
        }

        public bool Recurse { get; set; }
        public int MaxCount { get; set; }

        public void Load()
        {
            string configPath = GetConfigPath();
            LogUtils.WriteLog($"Load Config");
            LogUtils.WriteLog($"File         -> {configPath}");

            if (!File.Exists(configPath))
            {
                // ファイルがなければ生成する(既定はマイピクチャ）
                string defaultPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                string content = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" +
                                 "<configuration>" +
                                 "<appSettings>" +
                                $"<TargetFolder>{defaultPath}</TargetFolder>" +
                                 "<Recurse>true</Recurse>" +
                                 "</appSettings>" +
                                 "</configuration>";
                File.WriteAllText(configPath, content, Encoding.UTF8);
            }
            var doc = new XmlDocument();
            doc.Load(configPath);
            TargetFolder = GetValue(doc, nameof(TargetFolder));
            Recurse = bool.Parse(GetValue(doc, nameof(Recurse)));
            LogUtils.WriteLog($"TargetFolder -> {TargetFolder}");
            LogUtils.WriteLog($"Recurse      -> {Recurse}");
            LogUtils.WriteLog($"MaxCount     -> {MaxCount}");
        }

        public void Save()
        {
            string configPath = GetConfigPath();
            var doc = new XmlDocument();
            doc.Load(configPath);
            SetValue(doc, nameof(TargetFolder), TargetFolder);
            SetValue(doc, nameof(Recurse), Recurse.ToString());
            doc.Save(configPath);
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

        private static void SetValue(XmlDocument doc, string key, string value)
        {
            var el = doc.SelectSingleNode($"configuration/appSettings/{key}");
            el.InnerText = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}