using PicSliderSS.Common;
using PicSliderSS.ImageResource;
using PicSliderSS.PicSliderGrid;
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
    /// PicSliderScreenB1.xaml の相互作用ロジック
    /// </summary>
    public partial class PicSliderScreenC2 : UserControl, IPicSliderScreen
    {
        public event EventHandler PicSlideAllCompleted;
        public event EventHandler ScreenRendered;

        private Storyboard slideStoryboard;

        public PicSliderScreenC2()
        {
            InitializeComponent();
            List<Grid> imageGrids = new List<Grid>();
            imageGrids.Add(this.PicSliderGrid1.PicSliderImageGrid);
            imageGrids.Add(this.PicSliderGrid2.PicSliderImageGrid);
            imageGrids.Add(this.PicSliderGrid3.PicSliderImageGrid);
            this.slideStoryboard = StoryboardUtils.CreateSlideStoryboard(imageGrids);
            this.slideStoryboard.Completed += PicSliderGrid_Completed;
            this.ScreenRendered += new EventHandler((o, e) => {
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
            this.slideStoryboard.Begin();
        }

        public void SetImages(ICollection<ImageResourceData> images)
        {
            if (images.Count < PicSliderScreenSize.C)
            {
                // 画像が足りない場合は例外を投げる
                throw new PicSliderScreenException(string.Format(ErrorMessage.NOT_ENOUGH_IMAGE, PicSliderScreenSize.C, images.Count));
            }

            // 縦長画像抽出する
            var vImages = images.Where(i => {
                return i.ShapeType == ImageResourceShapeTypes.RECTANGLE_VERTICAL;
            });

            if (vImages.Count() == 0)
            {
                // 縦長な画像がなければ、全リストの最後をセットする
                this.PicSliderGrid1.SetImage(images.Last());
            }
            else
            {
                // 縦長な画像があればセットして、全リストから削除する
                this.PicSliderGrid1.SetImage(vImages.First());
                images.Remove(vImages.First());
            }
            
            // 残りの画像をセットする
            this.PicSliderGrid2.SetImage(images.ElementAt(0));
            this.PicSliderGrid3.SetImage(images.ElementAt(1));
        }

        // 全スライド完了時
        public void PicSliderGrid_Completed(object sender, EventArgs e)
        {
            Debug.WriteLine("[D]PicSlideCompleted");
            PicSlideAllCompleted(this, EventArgs.Empty);
        }

    }
}
