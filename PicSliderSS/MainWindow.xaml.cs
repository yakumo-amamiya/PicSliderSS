using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using PicSliderSS.Common;

namespace PicSliderSS
{
    public partial class MainWindow : Window
    {
        public static List<PicSliderWindow.PicSliderWindow> Windows;
        
        public MainWindow()
        {
            InitializeComponent();
            
            Windows = new List<PicSliderWindow.PicSliderWindow>();
        }
        
        /// <summary>
        /// メインウインドウ読み込み後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
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
    }
}