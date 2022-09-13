using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSliderSS.Common
{
    class LogUtils
    {
        public static string userProfileDir = Environment.GetEnvironmentVariable(LogUrls.KEY_USER_PROFILE);
        /// <summary>
        /// エラーログファイルのURLを生成する
        /// ディレクトリがなければ生成する
        /// </summary>
        /// <returns></returns>
        public static string ErrorLogFile()
        {
            string url = LogUtils.userProfileDir + string.Format(LogUrls.ERROR_LOG_FILEPATH, DateTime.Now.Date.ToString("yyyymmdd"));
            string dir = Path.GetDirectoryName(url);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            return url;
        }

        /// <summary>
        /// スタックトレースログファイルのURLを生成する
        /// </summary>
        /// <returns></returns>
        public static string StacktraceLogFile()
        {
            string url = LogUtils.userProfileDir + string.Format(LogUrls.STACKTRACE_LOG_FILEPATH, DateTime.Now.Date.ToString("yyyymmdd"));
            string dir = Path.GetDirectoryName(url);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return url;
        }

        /// <summary>
        /// エラーログを出力する
        /// </summary>
        /// <param name="message"></param>
        public static void ErrorLog(string message)
        {
            System.IO.File.AppendAllText(ErrorLogFile(), message);
        }

        /// <summary>
        /// スタックトレースログを出力する
        /// </summary>
        /// <param name="message"></param>
        public static void StacktraceLog(string message)
        {
            System.IO.File.AppendAllText(StacktraceLogFile(), message);
        }
    }
}
