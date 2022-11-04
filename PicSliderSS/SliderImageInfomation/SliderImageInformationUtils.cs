using System;
using System.Collections.Generic;
using System.Windows.Media;
using PicSliderSS.Common;
using PicSliderSS.Enum;
using PicSliderSS.ImageResource;

namespace PicSliderSS.SliderImageInfomation
{
    public static class SliderImageInformationUtils
    {
        public const double Margin = 3.0;

        public static SliderImageInformation[] CreateRandom(PicSliderWindow.PicSliderWindow window)
        {
            var array = ScreenTypeUtils.GetTypes(window.DisplayType);

            return Create(CommonUtils.PickRandom(array), window);
        }
        public static SliderImageInformation[] Create(ScreenType type, PicSliderWindow.PicSliderWindow window)
        {
            switch (type)
            {
                case ScreenType.A:
                    return CreateA(window);
                case ScreenType.B:
                    return CreateB(window);
                case ScreenType.C:
                    return CreateC(window);
                case ScreenType.D1:
                    return CreateD1(window);
                case ScreenType.D2:
                    return CreateD2(window);
                case ScreenType.E:
                    return CreateE(window);
                case ScreenType.F:
                    return CreateF(window);
                case ScreenType.G1:
                    return CreateG1(window);
                case ScreenType.G2:
                    return CreateG2(window);
            }

            return null;
        }
        /// <summary>
        /// 1枚
        /// ┌───────┐
        /// │       │
        /// └───────┘
        /// </summary>
        /// <param name="window"></param>
        private static SliderImageInformation[] CreateA(PicSliderWindow.PicSliderWindow window)
        {
            var siiList = new List<SliderImageInformation>();
            var sii = new SliderImageInformation();
            var directs = new[]
            {
                SlideDirection.BottomToTop,
                SlideDirection.LeftToRight,
                SlideDirection.RightToLeft,
                SlideDirection.TopToBottom,
            };
            sii.SlideDirection = CommonUtils.PickRandom(directs);
            sii.Height = window.Height;
            sii.Width = window.Width;
            SetStartPosition(sii.SlideDirection, sii.Width, sii.Height, 0, 0, sii, window.Width, window.Height);
            sii.EnableShapes = new[] { ImageShape.Landscape, ImageShape.Portrait, ImageShape.Balance };
            sii.Stretch = Stretch.Uniform;
            sii.ImageResource = window.ImageQueue.Dequeue(sii.EnableShapes, (int)sii.Width, (int)sii.Height);
            siiList.Add(sii);
            
            return siiList.ToArray();
        }

        /// <summary>
        /// 横並び2枚
        /// ┌───┬───┐
        /// │   │   │
        /// └───┴───┘
        /// </summary>
        /// <param name="window"></param>
        private static SliderImageInformation[] CreateB(PicSliderWindow.PicSliderWindow window)
        {
            var siiList = new List<SliderImageInformation>();
            double width = window.Width / 2 - Margin;
            SlideDirection d1;
            SlideDirection d2;
            switch (CommonUtils.GetRandom(5))
            {
                case 0:
                    d1 = SlideDirection.TopToBottom;
                    d2 = SlideDirection.BottomToTop;
                    break;
                case 1:
                    d1 = SlideDirection.TopToBottom;
                    d2 = SlideDirection.RightToLeft;
                    break;
                case 2:
                    d1 = SlideDirection.BottomToTop;
                    d2 = SlideDirection.TopToBottom;
                    break;
                case 3:
                    d1 = SlideDirection.BottomToTop;
                    d2 = SlideDirection.RightToLeft;
                    break;
                default:
                    d1 = SlideDirection.LeftToRight;
                    d2 = SlideDirection.RightToLeft;
                    break;
            }
            siiList.Add(CreateImageInfo(width, window.Height, 0, 0, d1, window.ImageQueue, window));
            siiList.Add(CreateImageInfo(width, window.Height, window.Width - width, 0, d2, window.ImageQueue, window));
            return siiList.ToArray();
        }
        
        /// <summary>
        /// 横並び3枚
        /// ┌──┬──┬──┐
        /// │  │  │  │
        /// └──┴──┴──┘
        /// </summary>
        /// <param name="window"></param>
        private static SliderImageInformation[] CreateC(PicSliderWindow.PicSliderWindow window)
        {
            var siiList = new List<SliderImageInformation>();
            var width = window.Width / 3 - Margin * 2;
            SlideDirection d1;
            SlideDirection d2;
            SlideDirection d3;
            switch (CommonUtils.GetRandom(2))
            {
                case 0:
                    d1 = SlideDirection.TopToBottom;
                    d2 = SlideDirection.BottomToTop;
                    d3 = SlideDirection.TopToBottom;
                    break;
                default:
                    d1 = SlideDirection.BottomToTop;
                    d2 = SlideDirection.TopToBottom;
                    d3 = SlideDirection.BottomToTop;
                    break;
            }
            siiList.Add(CreateImageInfo(width, window.Height, 0, 0, d1, window.ImageQueue, window));
            siiList.Add(CreateImageInfo(width, window.Height, width + Margin, 0, d2, window.ImageQueue, window));
            siiList.Add(CreateImageInfo(width, window.Height, width * 2 + Margin * 2, 0, d3, window.ImageQueue, window));
            return siiList.ToArray();
        }

        /// <summary>
        /// 左1枚 + 右2枚
        /// ┌───┬───┐
        /// │   ├───┤
        /// └───┴───┘
        /// </summary>
        /// <param name="window"></param>
        private static SliderImageInformation[] CreateD1(PicSliderWindow.PicSliderWindow window)
        {
            var siiList = new List<SliderImageInformation>();
            var width = window.Width / 2 - Margin;
            var heightL = window.Height;
            var heightR = window.Height / 2 - Margin;
            SlideDirection d1;
            SlideDirection d2;
            SlideDirection d3;
            switch (CommonUtils.GetRandom(4))
            {
                case 0:
                    d1 = SlideDirection.TopToBottom;
                    d2 = SlideDirection.RightToLeft;
                    d3 = SlideDirection.BottomToTop;
                    break;
                case 1:
                    d1 = SlideDirection.LeftToRight;
                    d2 = SlideDirection.TopToBottom;
                    d3 = SlideDirection.BottomToTop;
                    break;
                case 2:
                    d1 = SlideDirection.LeftToRight;
                    d2 = SlideDirection.RightToLeft;
                    d3 = SlideDirection.BottomToTop;
                    break;
                default:
                    d1 = SlideDirection.BottomToTop;
                    d2 = SlideDirection.TopToBottom;
                    d3 = SlideDirection.RightToLeft;
                    break;
            }
            siiList.Add(CreateImageInfo(width, heightL, 0, 0, d1, window.ImageQueue, window));
            siiList.Add(CreateImageInfo(width, heightR, width + Margin, 0, d2, window.ImageQueue, window));
            siiList.Add(CreateImageInfo(width, heightR, width + Margin, heightR + Margin, d3, window.ImageQueue, window));
            return siiList.ToArray();
        }

        /// <summary>
        /// 左2枚 + 右1枚
        /// ┌───┬───┐
        /// ├───┤   │
        /// └───┴───┘
        /// </summary>
        /// <param name="window"></param>
        private static SliderImageInformation[] CreateD2(PicSliderWindow.PicSliderWindow window)
        {
            var siiList = new List<SliderImageInformation>();
            var width = window.Width / 2 - Margin;
            var heightL = window.Height / 2 - Margin;
            var heightR = window.Height;
            SlideDirection d1;
            SlideDirection d2;
            SlideDirection d3;
            switch (CommonUtils.GetRandom(4))
            {
                case 0:
                    d3 = SlideDirection.TopToBottom;
                    d1 = SlideDirection.LeftToRight;
                    d2 = SlideDirection.BottomToTop;
                    break;
                case 1:
                    d3 = SlideDirection.RightToLeft;
                    d1 = SlideDirection.TopToBottom;
                    d2 = SlideDirection.BottomToTop;
                    break;
                case 2:
                    d3 = SlideDirection.RightToLeft;
                    d1 = SlideDirection.LeftToRight;
                    d2 = SlideDirection.BottomToTop;
                    break;
                default:
                    d3 = SlideDirection.BottomToTop;
                    d1 = SlideDirection.TopToBottom;
                    d2 = SlideDirection.LeftToRight;
                    break;
            }
            siiList.Add(CreateImageInfo(width, heightL, 0, 0, d1, window.ImageQueue, window));
            siiList.Add(CreateImageInfo(width, heightL, 0, heightL + Margin, d2, window.ImageQueue, window));
            siiList.Add(CreateImageInfo(width, heightR, width + Margin, 0, d3, window.ImageQueue, window));
            return siiList.ToArray();
        }

        /// <summary>
        /// 4枚
        /// ┌───┬───┐
        /// ├───┼───┤
        /// └───┴───┘
        /// </summary>
        /// <param name="window"></param>
        private static SliderImageInformation[] CreateE(PicSliderWindow.PicSliderWindow window)
        {
            var siiList = new List<SliderImageInformation>();
            var width = window.Width / 2 - Margin;
            var height = window.Height / 2 - Margin;
            var d1 = SlideDirection.TopToBottom;
            var d2 = SlideDirection.LeftToRight;
            var d3 = SlideDirection.RightToLeft;
            var d4 = SlideDirection.BottomToTop;
            siiList.Add(CreateImageInfo(width, height, 0, 0, d1, window.ImageQueue, window));
            siiList.Add(CreateImageInfo(width, height, 0, height + Margin, d2, window.ImageQueue, window));
            siiList.Add(CreateImageInfo(width, height, width + Margin, 0, d3, window.ImageQueue, window));
            siiList.Add(CreateImageInfo(width, height, width + Margin, height + Margin, d4, window.ImageQueue, window));
            return siiList.ToArray();
        }
        
        /// <summary>
        /// 横並び4枚（ウルトラワイド向け）
        /// ┌──┬──┬──┬──┐
        /// │  │  │  │  │
        /// └──┴──┴──┴──┘
        /// </summary>
        /// <param name="window"></param>
        private static SliderImageInformation[] CreateF(PicSliderWindow.PicSliderWindow window)
        {
            var siiList = new List<SliderImageInformation>();
            var width = window.Width / 4 - Margin * 3;
            SlideDirection d1;
            SlideDirection d2;
            SlideDirection d3;
            SlideDirection d4;
            switch (CommonUtils.GetRandom(2))
            {
                case 0:
                    d1 = SlideDirection.TopToBottom;
                    d2 = SlideDirection.BottomToTop;
                    d3 = SlideDirection.TopToBottom;
                    d4 = SlideDirection.BottomToTop;
                    break;
                default:
                    d1 = SlideDirection.BottomToTop;
                    d2 = SlideDirection.TopToBottom;
                    d3 = SlideDirection.BottomToTop;
                    d4 = SlideDirection.TopToBottom;
                    break;
            }
            siiList.Add(CreateImageInfo(width, window.Height, 0, 0, d1, window.ImageQueue, window));
            siiList.Add(CreateImageInfo(width, window.Height, width + Margin, 0, d2, window.ImageQueue, window));
            siiList.Add(CreateImageInfo(width, window.Height, width * 2 + Margin * 2, 0, d3, window.ImageQueue, window));
            siiList.Add(CreateImageInfo(width, window.Height, width * 3 + Margin * 3, 0, d4, window.ImageQueue, window));
            return siiList.ToArray();
        }

        /// <summary>
        /// 左2枚（縦） + 右2枚（横）（ウルトラワイド向け）
        /// ┌───┬───┬───┐
        /// │   │   ├───┤
        /// └───┴───┴───┘
        /// </summary>
        /// <param name="window"></param>
        private static SliderImageInformation[] CreateG1(PicSliderWindow.PicSliderWindow window)
        {
            var siiList = new List<SliderImageInformation>();
            var width = window.Width / 3 - Margin * 2;
            var heightL = window.Height;
            var heightR = window.Height / 2 - Margin;
            SlideDirection d1;
            SlideDirection d2;
            SlideDirection d3;
            SlideDirection d4;
            switch (CommonUtils.GetRandom(2))
            {
                case 0:
                    d1 = SlideDirection.BottomToTop;
                    d2 = SlideDirection.TopToBottom;
                    break;
                default:
                    d1 = SlideDirection.TopToBottom;
                    d2 = SlideDirection.BottomToTop;
                    break;
            }
            switch (CommonUtils.GetRandom(3))
            {
                case 0:
                    d3 = SlideDirection.RightToLeft;
                    d4 = SlideDirection.BottomToTop;
                    break;
                case 1:
                    d3 = SlideDirection.TopToBottom;
                    d4 = SlideDirection.BottomToTop;
                    break;
                default:
                    d3 = SlideDirection.TopToBottom;
                    d4 = SlideDirection.RightToLeft;
                    break;
            }
            siiList.Add(CreateImageInfo(width, heightL, 0, 0, d1, window.ImageQueue, window));
            siiList.Add(CreateImageInfo(width, heightL, width + Margin, 0, d2, window.ImageQueue, window));
            siiList.Add(CreateImageInfo(width, heightR, width * 2 + Margin * 2, 0, d3, window.ImageQueue, window));
            siiList.Add(CreateImageInfo(width, heightR, width * 2 + Margin * 2, heightR + Margin, d4, window.ImageQueue, window));
            return siiList.ToArray();
        }

        /// <summary>
        /// 左2枚（横） + 右1枚（縦）（ウルトラワイド向け）
        /// ┌───┬───┬───┐
        /// ├───┤   │   │
        /// └───┴───┴───┘
        /// </summary>
        /// <param name="window"></param>
        private static SliderImageInformation[] CreateG2(PicSliderWindow.PicSliderWindow window)
        {
            var siiList = new List<SliderImageInformation>();
            var width = window.Width / 3 - Margin * 2;
            var heightL = window.Height / 2 - Margin;
            var heightR = window.Height;
            SlideDirection d1;
            SlideDirection d2;
            SlideDirection d3;
            SlideDirection d4;
            switch (CommonUtils.GetRandom(2))
            {
                case 0:
                    d3 = SlideDirection.BottomToTop;
                    d4 = SlideDirection.TopToBottom;
                    break;
                default:
                    d3 = SlideDirection.TopToBottom;
                    d4 = SlideDirection.BottomToTop;
                    break;
            }
            switch (CommonUtils.GetRandom(3))
            {
                case 0:
                    d1 = SlideDirection.TopToBottom;
                    d2 = SlideDirection.LeftToRight;
                    break;
                case 1:
                    d1 = SlideDirection.TopToBottom;
                    d2 = SlideDirection.BottomToTop;
                    break;
                default:
                    d1 = SlideDirection.LeftToRight;
                    d2 = SlideDirection.BottomToTop;
                    break;
            }
            siiList.Add(CreateImageInfo(width, heightL, 0, 0, d1, window.ImageQueue, window));
            siiList.Add(CreateImageInfo(width, heightL, 0, heightL + Margin, d2, window.ImageQueue, window));
            siiList.Add(CreateImageInfo(width, heightR, width + Margin, 0, d3, window.ImageQueue, window));
            siiList.Add(CreateImageInfo(width, heightR, width * 2 + Margin * 2, 0, d4, window.ImageQueue, window));
            return siiList.ToArray();
        }

        private static SliderImageInformation CreateImageInfo(double width, double height, double left, double top, SlideDirection direction, ImageResourceQueue queue, PicSliderWindow.PicSliderWindow window)
        {
            var sii = new SliderImageInformation();
            sii.SlideDirection = direction;
            sii.Height = height;
            sii.Width = width;
            SetStartPosition(sii.SlideDirection, sii.Width, sii.Height, left, top, sii, window.Width, window.Height);
            sii.EnableShapes = GetEnableShapes(sii.Width, sii.Height);
            sii.Stretch = Stretch.UniformToFill;
            sii.ImageResource = queue.Dequeue(sii.EnableShapes, (int)sii.Width, (int)sii.Height);

            return sii;
        }

        private static void SetStartPosition(SlideDirection direction, double width, double height, double left, double top, SliderImageInformation target, double wWidth, double wHeight)
        {
            switch (direction)
            {
                case SlideDirection.BottomToTop:
                    target.Top = wHeight;
                    target.Left = left;
                    target.Start = target.Top;
                    target.Middle = top;
                    break;
                case SlideDirection.LeftToRight:
                    target.Top = top;
                    target.Left = -width;
                    target.Start = target.Left;
                    target.Middle = 0.0;
                    break;
                case SlideDirection.RightToLeft:
                    target.Top = top;
                    target.Left = wWidth;
                    target.Start = target.Left;
                    target.Middle = left;
                    break;
                case SlideDirection.TopToBottom:
                    target.Top = -height;
                    target.Left = left;
                    target.Start = target.Top;
                    target.Middle = 0.0;
                    break;
            }
        }
        public static ImageShape[] GetEnableShapes(double width, double height)
        {
            if (width / height > 1.2)
            {
                return new [] { ImageShape.Landscape };
            }

            if (height / width > 1.2)
            {
                return new [] { ImageShape.Portrait };
            }

            return new [] { ImageShape.Landscape, ImageShape.Portrait, ImageShape.Balance};
        }
    }
}