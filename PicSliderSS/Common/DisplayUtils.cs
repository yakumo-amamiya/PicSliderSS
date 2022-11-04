using PicSliderSS.Enum;

namespace PicSliderSS.Common
{
    public static class DisplayUtils
    {
        /// <summary>
        /// モニタタイプの判定。
        /// 参考値：
        /// 1920 * 1080	=	1.777777778
        /// 1920 * 1200	=	1.6
        /// 2560 * 1080	=	2.37037037
        /// </summary>
        /// <param name="width">モニタの幅</param>
        /// <param name="height">モニタの高さ</param>
        public static DisplayType GetDisplayType(double width, double height)
        {
            double big = width > height ? width : height;
            double sml = width > height ? height : width;
            double rate = big / sml;

            if (rate < 1.6)
            {
                // 現時点ではワイド扱い。今後 Squareとすることを検討。
                return DisplayType.Wide;
            }

            if (rate < 2.1)
            {
                return DisplayType.Wide;
            }
            
            // 縦置きのウルトラワイドは未検討。
            return DisplayType.UltraWide;
        }
    }
}