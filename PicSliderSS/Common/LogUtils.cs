using System;
using System.IO;
using System.Reflection;

namespace PicSliderSS.Common
{
    class LogUtils
    {
        public static readonly string LogDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) + @"\log";
        public static bool Rotated = false;
        /// <summary>
        /// エラーログファイルのURLを生成する
        /// ディレクトリがなければ生成する
        /// </summary>
        /// <returns></returns>
        public static string LogFile()
        {
            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);
            }
            else
            {
                if (!Rotated)
                {
                    // Rotate(); 過去のログファイルを削除
                    Rotated = true;
                }
            }

            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string url = LogDirectory + @"\" + $"log_{date}.log";
                
            return url;
        }

        /// <summary>
        /// エラーログを出力する
        /// </summary>
        /// <param name="message"></param>
        /// <param name="level">0: レベル低、1: レベル中、2:レベル高</param>
        public static void WriteLog(string message, LogLevel level = LogLevel.Normal)
        {
            #if DEBUG
            // デバッグ時のみ出力
            string dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string fullMessage = $"[{dt}][inf]{message}\r\n";
            File.AppendAllText(LogFile(), fullMessage);
            #endif
        }

        public static void WriteErrorLog(string message)
        {
            string dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string fullMessage = $"[{dt}][err]{message}\r\n";
            File.AppendAllText(LogFile(), fullMessage);
        }

        private static void Rotate()
        {
            var files = Directory.GetFiles(LogDirectory, "*.log");
            foreach (var file in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                var dtStr = fileName.Replace("log_", "");
                var fileDt = DateTime.ParseExact(dtStr, "yyyy-MM-dd", null);
                var diff = DateTime.Now - fileDt;
                if (diff.Days > 30)
                {
                    File.Delete(file);
                }
            }
        }
    }
}
