using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PicSliderSS.ImageResource;
using PicSliderSS.Common;
using System.Windows.Media.Animation;
using PicSliderSS.Config;
using PicSliderSS.Enum;
using PicSliderSS.SliderImageInfomation;

namespace PicSliderSS.PicSliderWindow
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class PicSliderWindow : System.Windows.Window
    {
        public event EventHandler SlideLoaded;
        private int completed;

        private Point prevCursorPosition;
        public ImageResourceQueue ImageQueue { get; set; }
        private Storyboard[] storyboards;

        public bool Ready { get; set; } = false;

        public DisplayType DisplayType => DisplayUtils.GetDisplayType(this.Height, this.Width);

        public PicSliderWindow()
        {
            InitializeComponent();

            // カーソル位置記憶変数に初期位置を格納
            prevCursorPosition = new Point(-1, -1);

            // 非表示カーソルに指定
            Cursor = Cursors.None;

            // 画像Queueを生成
            ImageQueue = new ImageResourceQueue(AppConfig.Data.TargetFolder);
        }

        /// <summary>
        /// 指定された Grid の要素をすべて削除する
        /// </summary>
        /// <param name="targetGrid"></param>
        public void ClearImage()
        {
            MainContainer.Children.Clear();
            storyboards = null;
            //GC.Collect();
        }

        public void CreateImage()
        {
            var storyboards = new List<Storyboard>();
            var siiList = SliderImageInformationUtils.CreateRandom(this);

            foreach (var sii in siiList)
            {
                var grid = new Canvas();
                grid.ClipToBounds = true;
                grid.Width = sii.Width;
                grid.Height = sii.Height;
                var image = new Image();
                image.Stretch = Stretch.Uniform;
                if (siiList.Length == 1)
                {
                    image.Width = grid.Width;
                    image.Height = grid.Height;
                }
                else
                {
                    var wGridRatio = grid.Width / grid.Height;
                    if (wGridRatio > 1.0)
                    {
                        var wImgRatio = sii.ImageResource.Bitmap.Width / sii.ImageResource.Bitmap.Height;
                        if (wGridRatio > wImgRatio) 
                        {
                            image.Width = grid.Width;
                            grid.VerticalAlignment = VerticalAlignment.Center;
                        }
                        else
                        {
                            image.Height = grid.Height;
                            grid.HorizontalAlignment = HorizontalAlignment.Center;
                        }
                    }
                    else
                    {
                        var hGridRatio = grid.Height / grid.Width;
                        var hImgRatio = sii.ImageResource.Bitmap.Height / sii.ImageResource.Bitmap.Width;
                        if (hGridRatio > hImgRatio) 
                        {
                            image.Height = grid.Height;
                            grid.HorizontalAlignment = HorizontalAlignment.Center;
                        }
                        else
                        {
                            image.Width = grid.Width;
                            grid.VerticalAlignment = VerticalAlignment.Center;
                        }
                    }
                }
                Canvas.SetTop(grid, sii.Top);
                Canvas.SetLeft(grid, sii.Left);
                image.Source = sii.ImageResource.Bitmap;

                var story = StoryboardUtils.SetSlideAnimation(grid, sii.SlideDirection, sii.Start, sii.Middle);
                story.Completed += PicSliderCompletedHandler;
                grid.Children.Add(image);
                MainContainer.Children.Add(grid);
                storyboards.Add(story);
            }
            this.storyboards = storyboards.ToArray();
        }

        /// <summary>
        /// スクリーン読み込み処理
        /// </summary>
        public void LoadImage()
        {
            // エレメントに紐づくスクリーンを削除
            ClearImage();
            CreateImage();
            
            // 読み込み完了処理
            Ready = true;
            SlideLoaded?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// スライド開始処理
        /// </summary>
        public void SlideStart()
        {
            // ステータスを更新
            Ready = false;
            completed = 0;

            // PicSlide を開始
            foreach (var sb in storyboards)
            {
                sb.Begin();
            }
        }
        
        #region 子要素用ハンドラ

        /// <summary>
        /// 各スライド終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void PicSliderCompletedHandler(object sender, EventArgs args)
        {
            completed += 1;
            if (completed == storyboards.Length)
            {
                // 終了数が全ストーリーボードと一致したとき
                PicSlideAllCompletedHandler(this, null);
            }
        }
        /// <summary>
        /// 全スライド終了処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void PicSlideAllCompletedHandler(object sender, EventArgs args)
        {
            LoadImage();
        }
        #endregion

        #region イベントハンドラ

        /// <summary>
        /// レンダリング完了時の処理
        /// アニメーションを開始する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            LoadImage();
        }

        /// <summary>
        /// マウスを動かした時の処理
        /// アプリケーションを終了する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            //Debug.WriteLine("[D]MainWindow MouseMove");

            // 初期ポイントの場合は現在のマウスポイントをセットする
            if (prevCursorPosition == new Point(-1, -1))
            {
                this.prevCursorPosition = Mouse.GetPosition(this);
                return;
            }

            // マウスを動かしたとき、アプリケーションを終了する。
            if (prevCursorPosition != Mouse.GetPosition(this))
            {
#if DEBUG
                return;            
#endif
                LogUtils.WriteLog($"Shutdown by MouseMove");
                CommonUtils.Shutdown();
            }
        }

        /// <summary>
        /// キーボードが押された時の処理
        /// アプリケーションを終了する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
#if DEBUG
            // デバッグ時 ESCAPE 以外のキーが押下された場合は中断する。
            if (e.Key != Key.Escape)
            {
                return;
            }
#endif
            LogUtils.WriteLog($"Shutdown by Keydown -> {e.Key}");
            CommonUtils.Shutdown();
        }

        #endregion
    }
}
