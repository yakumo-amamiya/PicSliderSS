using PicSliderSS.Common;
using PicSliderSS.ImageResource;
using PicSliderSS.PicSliderScreen.Massages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PicSliderSS.PicSliderScreen
{
    class PicSliderScreenUtils
    {
        /// <summary>
        /// 画像型種リストより、使用するスクリーン種を選定する
        /// </summary>
        /// <param name="shapeTypes"></param>
        /// <returns></returns>
        // ToDo : 名前を変える必要がある。
        public static string SelectScreenType(ICollection<int> shapeTypes)
        {
            switch (shapeTypes.Count)
            {
                case PicSliderScreenSize.A:
                    return PicSliderScreenTypes.A1;
                case PicSliderScreenSize.B:
                    return PicSliderScreenTypes.B1;
                case PicSliderScreenSize.C:
                    switch ((new System.Random()).Next(2))
                    {
                        case 0:
                            return PicSliderScreenTypes.C1;
                        case 1:
                            return PicSliderScreenTypes.C2;
                        default:
                            throw new PicSliderScreenException(ErrorMessage.SELECT_SCREEN_ERROR_RANDOM);
                    }
                case PicSliderScreenSize.D:
                    return PicSliderScreenTypes.D1;
                default:
                    throw new PicSliderScreenNotImplementScreenException(string.Format(ErrorMessage.NOT_IMPLEMENT_SCREEN, shapeTypes.Count, shapeTypes.ToString()));
            }
        }

        public static UserControl CreateScreen(string screenType)
        {
            return (UserControl)Activator.CreateInstance(Type.GetType("PicSliderSS.PicSliderScreen." + screenType, true));
        }

        public static UserControl CreateScreen(ICollection<int> shapeTypes)
        {
            return (UserControl)CreateScreen(SelectScreenType(shapeTypes));
        }

        public static UserControl CreateLoadedScreen(ICollection<ImageResourceData> images)
        {
            // 画像の型種を抽出する
            List<int> shapeTypes = images.Select(i => { return i.ShapeType; }).ToList();

            // 画像に適合するスクリーンを選定する
            UserControl screen = PicSliderScreenUtils.CreateScreen(shapeTypes);

            // 画像セットする
            ((IPicSliderScreen)screen).SetImages(images);

            return screen;
        }

    }
}
