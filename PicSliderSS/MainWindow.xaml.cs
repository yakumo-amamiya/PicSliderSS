using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;

namespace PicSliderSS
{
    public partial class MainWindow : Window
    {
        private List<PicSliderWindow.PicSliderWindow> Windows { get; set; }
        
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
            foreach (var scr in System.Windows.Forms.Screen.AllScreens)
            {
                Debug.WriteLine("[D]" + scr.ToString());
                var picSliderWindow = new PicSliderWindow.PicSliderWindow();
                picSliderWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                picSliderWindow.Top = scr.Bounds.Y;
                picSliderWindow.Left = scr.Bounds.X;
                picSliderWindow.Owner = this;
                picSliderWindow.Show();
                Debug.WriteLine("[D]Show Window -> " + scr.DeviceName);
                Windows.Add(picSliderWindow);
            }
        }
    }
}