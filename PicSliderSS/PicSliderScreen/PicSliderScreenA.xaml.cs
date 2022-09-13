using PicSliderSS.Common;
using PicSliderSS.ImageResource;
using PicSliderSS.PicSliderScreen.Massages;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PicSliderSS.PicSliderScreen
{
    /// <summary>
    /// PicSliderScreenA.xaml の相互作用ロジック
    /// </summary>
    public partial class PicSliderScreenA : UserControl, IPicSliderScreen
    {
        public event EventHandler PicSlideAllCompleted;
        public event EventHandler ScreenRendered;

        private Storyboard slideStoryboard;

        public PicSliderScreenA()
        {
            InitializeComponent();

            List<Grid> imageGrids = new List<Grid>();
            imageGrids.Add(this.PicSliderGrid1.PicSliderImageGrid);
            this.slideStoryboard = StoryboardUtils.CreateSlideStoryboard(imageGrids);
            this.slideStoryboard.Completed += PicSliderGrid_Completed;
            this.ScreenRendered += new EventHandler((o, e) =>
            {
                Debug.WriteLine("[D]Screen Render Finish");
            });
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            Debug.WriteLine("[D]OnRender : " + this.ToString());
            Debug.WriteLine("[D]OnRender Visibility : " + this.Visibility);
            base.OnRender(drawingContext);
            if (this.Visibility == Visibility.Visible)
            {
                ScreenRendered(this, EventArgs.Empty);
            }
        }

        public void BeginPictureSlide()
        {
            //this.PicSliderGrid1.SetImage(@"Y:\Picture\FFXIV\ffxiv_20170718_044147.png");
            //this.PicSliderGrid1.PicSlideCompleted += new EventHandler(this.PicSliderGrid_Completed);
            this.slideStoryboard.Begin();
        }

        public void SetImages(ICollection<ImageResourceData> images)
        {
            if (images.Count < PicSliderScreenSize.A)
            {
                // 画像が足りない場合は例外を投げる
                throw new PicSliderScreenException(string.Format(ErrorMessage.NOT_ENOUGH_IMAGE, PicSliderScreenSize.A, images.Count));
            }

            // 画像をセットする
            this.PicSliderGrid1.SetImage(images.ElementAt(0));
        }

        // 全スライド完了時
        public void PicSliderGrid_Completed(object sender, EventArgs e)
        {
            PicSlideAllCompleted(this, EventArgs.Empty);
        }
    }
}
