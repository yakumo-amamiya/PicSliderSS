using PicSliderSS.PicSliderWindow.Exceptions;
using PicSliderSS.PicSliderScreen;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PicSliderSS.ImageResource;
using PicSliderSS.PicSliderWindow.Messages;
using PicSliderSS.Common;
using System.Threading;
using System.Windows.Media.Animation;

namespace PicSliderSS.PicSliderWindow
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class PicSliderWindow : System.Windows.Window
    {
        private Point prevCursorPosition;
        private UserControl activeScreen;
        private Grid screenGrid;
        private Dictionary<string, UserControl> screens;
        private ImageResourceQueue iQueue;

        public PicSliderWindow()
        {
            Debug.WriteLine("[D]MainWindow Constractor");

            InitializeComponent();

            // カーソル位置記憶変数に初期位置を格納
            prevCursorPosition = new Point(-1, -1);

            // 非表示カーソルに指定
            this.Cursor = Cursors.None;

            // 画像Queueを生成
            // this.iQueue = new ImageResourceQueue(@"C:\Users\yakum\source\repos\PicSliderSS\PicSliderSS\static\D");
            this.iQueue = new ImageResourceQueue(@"D:\Dropbox\Pictures\Illust");

            // スクリーンディクショナリを生成
            this.screens = new Dictionary<string, UserControl>();
            this.screens[ScreenKeys.SCREEN1] = null;
            this.screens[ScreenKeys.SCREEN2] = null;

            // 画像を取得する
            List<ImageResourceData> items = this.iQueue.MultiDequeue(1);

            // 画像に適合するスクリーンを生成する
            this.screens[ScreenKeys.SCREEN1] = PicSliderScreenUtils.CreateLoadedScreen(items);
            this.activeScreen = this.screens[ScreenKeys.SCREEN1];
            this.screenGrid = this.PicSlider1;

            // スクリーンを UI に追加
            this.AddScreenElement(this.PicSlider1, this.activeScreen);

        }


        #region プロパティ
        public Grid HiddenScreenGrid
        {
            get
            {
                if (this.screenGrid == this.PicSlider1)
                {
                    return this.PicSlider2;
                }
                else
                {
                    return this.PicSlider1;
                }
            }
        }

        public Grid ActiveScreenGrid
        {
            get
            {
                return this.screenGrid;
            }
        }

        public UserControl HiddenScreen
        {
            get
            {
                if (this.activeScreen == this.screens[ScreenKeys.SCREEN1])
                {
                    return this.screens[ScreenKeys.SCREEN2];
                }
                else
                {
                    return this.screens[ScreenKeys.SCREEN1];
                }
            }
        }

        public string HiddenScreenKey
        {
            get
            {
                foreach(string key in this.screens.Keys)
                {
                    if (this.screens[key] == this.HiddenScreen)
                    {
                        return key;
                    }
                }

                return null;
            }
        }

        #endregion

        #region 子要素用ハンドラ

        public void PicSlideAllCompletedHandler(object sender, EventArgs args)
        {
            Debug.WriteLine("[D]PicSlide is all Completed!!");

            ((IPicSliderScreen)this.HiddenScreen).ScreenRendered += this.ScreenRenderedHandler;

            // 表示を切り替え
            this.SwitchAcrive();

        }

        public void ScreenRenderedHandler(object sender, EventArgs args)
        {
            Debug.WriteLine("[D]Screen Rendered : " + sender.ToString());

            // PicSlide を開始
            ((IPicSliderScreen)this.activeScreen).PicSlideAllCompleted += new EventHandler(PicSlideAllCompletedHandler);
            ((IPicSliderScreen)this.activeScreen).BeginPictureSlide();

            // 次 Screen を準備
            this.UpdateHiddenPicSlider();

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
            Debug.WriteLine("[D]MainWindow contentRendered");

            ((IPicSliderScreen)this.activeScreen).PicSlideAllCompleted += new EventHandler(PicSlideAllCompletedHandler);
            ((IPicSliderScreen)this.activeScreen).BeginPictureSlide();

            // 次 Screen を準備
            this.UpdateHiddenPicSlider();
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
                Application.Current.Shutdown();
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
            Application.Current.Shutdown();
        }

        #endregion

        /// <summary>
        /// アクティブスクリーンを切り替える
        /// </summary>
        public void SwitchAcrive()
        {
            Debug.WriteLine("[D]Switch Screen");
            if (this.activeScreen == this.screens[ScreenKeys.SCREEN1])
            {
                this.PicSlider1.Visibility = Visibility.Collapsed;
                this.activeScreen = this.screens[ScreenKeys.SCREEN2];
                this.screenGrid = this.PicSlider2;
                this.PicSlider2.Visibility = Visibility.Visible;
            }
            else if (this.activeScreen == this.screens[ScreenKeys.SCREEN2])
            {
                this.PicSlider2.Visibility = Visibility.Collapsed;
                this.activeScreen = this.screens[ScreenKeys.SCREEN1];
                this.screenGrid = this.PicSlider1;
                this.PicSlider1.Visibility = Visibility.Visible;
            }
            else
            {
                throw new InvalidSwitchScreenException(ErrorMessage.SELECTED_INVALID_SCREEN);
            }
        }

        /// <summary>
        /// PicSliderGridを更新させる処理
        /// </summary>
        public void UpdateHiddenPicSlider()
        {
            Debug.WriteLine("[D]Update Hidden PicSlider");

            Grid picSliderGrid = this.HiddenScreenGrid;

            // エレメントに紐づくスクリーンを削除
            this.ClearScreenElement(picSliderGrid);

            // 不要スクリーンを削除
            this.screens[HiddenScreenKey] = null;

            // 画像を取得する
            List<ImageResourceData> items = this.iQueue.MultiDequeue();

            // 画像に適合するスクリーンを生成する
            UserControl newScreen = PicSliderScreenUtils.CreateLoadedScreen(items);

            // 不要スクリーンをセット
            this.screens[HiddenScreenKey] = newScreen;

            // スクリーンを画面に追加
            this.AddScreenElement(picSliderGrid, newScreen);
        }

        /// <summary>
        /// 指定された Grid の要素をすべて削除する
        /// </summary>
        /// <param name="targetGrid"></param>
        public void ClearScreenElement(Grid targetGrid)
        {
            if (this.activeScreen != null && targetGrid.Children.Contains(this.activeScreen))
            {
                throw new PicSliderException("Acrive化されているスクリーンは削除できません。");
            }

            targetGrid.Children.Clear();
            System.GC.Collect();
        }

        /// <summary>
        /// 指定された Grid に Key で渡されたスクリーンを加える
        /// </summary>
        /// <param name="targetGrid"></param>
        /// <param name="screenKey"></param>
        public void AddScreenElement(Grid targetGrid, string screenKey)
        {
            this.AddScreenElement(targetGrid, this.screens[screenKey]);
        }

        /// <summary>
        /// 指定された Grid に 渡されたスクリーンを加える
        /// </summary>
        /// <param name="targetGrid"></param>
        /// <param name="screen"></param>
        public void AddScreenElement(Grid targetGrid, UserControl screen)
        {
            // セット済みのGridか判定
            if (targetGrid.Children.Contains(screen))
            {
                throw new AlreadyExistScreenElementException("すでにセット済みの Grid です。");
            }

            // 要素をセット
            targetGrid.Children.Add(screen);
        }
    }
}
