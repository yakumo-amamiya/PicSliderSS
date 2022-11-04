using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using PicSliderSS.Common;
using PicSliderSS.Config;
using PicSliderSS.Enum;
using MessageBox = System.Windows.Forms.MessageBox;

namespace PicSliderSS
{
    public partial class MainWindow : Window
    {
        public StartUpMode Mode { get; set; }
        public static List<PicSliderWindow.PicSliderWindow> Windows;
        
        public MainWindow(StartUpMode mode)
        {
            InitializeComponent();
            Mode = mode;
            MainContainer.DataContext = AppConfig.Data;
            Windows = new List<PicSliderWindow.PicSliderWindow>();
        }

        /// <summary>
        /// メインウインドウ読み込み後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            switch (Mode)
            {
                case StartUpMode.Normal:
                    // 何もしない
                    break;
                case StartUpMode.Config:
                    ExeButton.IsEnabled = false;
                    break;
                case StartUpMode.ScreenSaver:
                    InitializeSlideWindow();
                    break;
                default:
                    LogUtils.WriteLog($"想定しない StartUpMode が選択されました。処理を終了します。StartUpMode = {Mode}", LogLevel.Normal);
                    CommonUtils.Shutdown();
                    break;
            }
        }

        /// <summary>
        /// スライド用ウインドウを初期化する。
        /// </summary>
        private void InitializeSlideWindow()
        {
            Hide();
            
            LogUtils.WriteLog($"ScreenCount -> {Screen.AllScreens.Length}");
            foreach (var scr in System.Windows.Forms.Screen.AllScreens)
            {
                Debug.WriteLine("[D]" + scr.ToString());
                var picSliderWindow = new PicSliderWindow.PicSliderWindow();
                picSliderWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                picSliderWindow.Top = scr.Bounds.Y;
                picSliderWindow.Left = scr.Bounds.X;
                picSliderWindow.Width = scr.Bounds.Width;
                picSliderWindow.Height = scr.Bounds.Height;
                picSliderWindow.Owner = this;
                picSliderWindow.Show();
                picSliderWindow.SlideLoaded += PicSliderWindow_OnSlideLoaded;
                Windows.Add(picSliderWindow);
            }
        }

        private void PicSliderWindow_OnSlideLoaded(object sender, EventArgs e)
        {
            int count = 0;
            foreach (var w in Windows)
            {
                if (w.Ready == true)
                {
                    count += 1;
                }
            }

            if (count == Windows.Count)
            {
                foreach (var w in Windows)
                {
                    w.SlideStart();
                }
            }
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            AppConfig.Data.Save();
            MessageBox.Show("正常に保存できました。");
            LogUtils.WriteLog("設定ファイルを保存しました。", LogLevel.Low);
        }

        private void ExeButton_OnClick(object sender, RoutedEventArgs e)
        {
            InitializeSlideWindow();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            CommonUtils.Shutdown();
        }

        private void PickUpButton_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.Description = "対象にするフォルダを選択してください。";
            dialog.ShowNewFolderButton = false;
            var result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                AppConfig.Data.TargetFolder = dialog.SelectedPath;
            }
            dialog.Dispose();
        }
    }
}