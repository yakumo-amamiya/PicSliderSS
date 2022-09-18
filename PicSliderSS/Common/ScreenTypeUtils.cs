using PicSliderSS.Config;
using PicSliderSS.Enum;

namespace PicSliderSS.Common
{
    public static class ScreenTypeUtils
    {
        public static ScreenType[] GetTypes()
        {
            switch (AppConfig.Data.MaxCount)
            {
                case 1:
                    return new[]
                    {
                        ScreenType.A
                    };
                case 2:
                    return new[]
                    {
                        ScreenType.A,
                        ScreenType.B
                    };
                case 3:
                    return new[]
                    {
                        ScreenType.A,
                        ScreenType.B,
                        ScreenType.C,
                        ScreenType.D1,
                        ScreenType.D2,
                    };
                case 4:
                    return new[]
                    {
                        ScreenType.A,
                        ScreenType.B,
                        ScreenType.C,
                        ScreenType.D1,
                        ScreenType.D2,
                        ScreenType.E,
                    };
                default:
                    return new[]
                    {
                        ScreenType.A,
                    };
            }
        }
    }
}