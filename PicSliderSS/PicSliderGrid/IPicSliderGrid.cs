using PicSliderSS.ImageResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace PicSliderSS.PicSliderGrid
{
    interface IPicSliderGrid
    {
        /// <summary>
        /// 画像の表示アニメーションを開始する
        /// </summary>
        void SetImage(ImageResourceData iData);
        AnimationTimeline GetShowAnimation();
        AnimationTimeline GetWaitAnimation();
        AnimationTimeline GetHiddenAnimation();
    }
}
