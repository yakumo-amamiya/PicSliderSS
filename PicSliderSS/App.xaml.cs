using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using PicSliderSS.Common;
using PicSliderSS.Config;

namespace PicSliderSS
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private MainWindow main;
        
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            // 予期しないエラー発生時のハンドリング
            Current.DispatcherUnhandledException += App_OnDispatcherUnhandledException;
            
            // プログラム起動ログ
            LogUtils.WriteLog("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■", LogLevel.High);
            LogUtils.WriteLog("Application Start", LogLevel.High);
            // 引数ログ
            LogUtils.WriteLog("Args -> " + string.Join(" ", e.Args));
            
            // 起動処理
            if (e.Args.Length > 0)
            {
                string mode = e.Args[0].ToLower(CultureInfo.InvariantCulture);

                if (mode.StartsWith("/s"))
                {
                    ShowScreensaver();
                }
                else if (mode.StartsWith("/c"))
                {
                    ShowConfiguration();
                    CommonUtils.Shutdown();
                }
                else if (mode.StartsWith("/p"))
                {
                    // 何もせず終了
                    CommonUtils.Shutdown();
                }
                else
                {
                    // 何もせず終了
                    CommonUtils.Shutdown();
                }
            }
            else
            {
                ShowConfiguration();
                CommonUtils.Shutdown();
            }
        }

        private void ShowScreensaver()
        {
            // 設定値読み込み
            AppConfig.Data = new AppConfig();
            AppConfig.Data.Load();
            
            // 画面を起動
            main = new MainWindow();
            main.Show();
        }

        private void ShowPreview()
        {
            ShowScreensaver();
        }

        private void ShowConfiguration()
        {

            string filePath = AppConfig.GetConfigPath();

            var startInfo = new System.Diagnostics.ProcessStartInfo()
            {
                FileName = filePath,
            };
            var proc = System.Diagnostics.Process.Start(startInfo);
            proc?.WaitForExit();
        }

        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            LogUtils.WriteErrorLog("予期しないエラーが発生しました。");
            var exception = e.Exception;
            while (exception != null)
            {
                LogUtils.WriteErrorLog("Message     -> " + exception.Message);
                LogUtils.WriteErrorLog("Source      -> " + exception.Source);
                LogUtils.WriteErrorLog("StackTrace  ->\r\n" + exception.StackTrace);
                exception = e.Exception.InnerException;
            }
        }
    }
}
