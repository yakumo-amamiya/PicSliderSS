﻿using PicSliderSS.ImageResource;
using System;
using System.Collections.Generic;
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

namespace PicSliderSS.PicSliderGrid
{
    /// <summary>
    /// PicSliderGridLeft.xaml の相互作用ロジック
    /// </summary>
    public partial class PicSliderGridLeft : UserControl, IPicSliderGrid
    {
        public event EventHandler PicSlideCompleted;

        public PicSliderGridLeft()
        {
            InitializeComponent();
        }

        public AnimationTimeline GetShowAnimation()
        {
            return Common.StoryboardUtils.CreateShowAnimation(this.PicSliderImageGrid);
        }

        public AnimationTimeline GetWaitAnimation()
        {
            return Common.StoryboardUtils.CreateWaitAnimation(this.PicSliderImageGrid);
        }

        public AnimationTimeline GetHiddenAnimation()
        {
            return Common.StoryboardUtils.CreateHiddenAnimation(this.PicSliderImageGrid);
        }

        /// <summary>
        /// 画像をurl指定でセットする
        /// </summary>
        /// <param name="url"></param>
        public void SetImage(string url)
        {            
            this.PicSliderImage.Source = new BitmapImage(new Uri(url));
        }

        /// <summary>
        /// 画像をセットする
        /// </summary>
        /// <param name="iData"></param>
        public void SetImage(ImageResourceData iData)
        {
            this.PicSliderImage.Source = iData.Bitmap;
        }
    }
}
