using PicSliderSS.Config;
using PicSliderSS.Enum;

namespace PicSliderSS.Common
{
    public static class ScreenTypeUtils
    {
        public static ScreenType[] GetTypes(DisplayType dp)
        {
            switch (dp)
            {
                case DisplayType.Square:
                case DisplayType.Wide: 
                    return new[]
                    {
                        ScreenType.A,
                        ScreenType.B,
                        ScreenType.C,
                        ScreenType.D1,
                        ScreenType.D2,
                        ScreenType.E,
                    };
                    break;
                case DisplayType.UltraWide:
                    return new[]
                    {
                        ScreenType.A,
                        ScreenType.B,
                        ScreenType.C,
                        ScreenType.F,
                        ScreenType.G1,
                        ScreenType.G2,
                    };
                    break;
                default:
                    LogUtils.WriteLog("想定されないScreenTypeが指定されています。", LogLevel.Normal);
                    return new[]
                    {
                        ScreenType.A
                    };
            }
        }
    }
}