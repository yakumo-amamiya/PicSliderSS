using System;
using System.Windows;

namespace PicSliderSS.Common
{
    public static class CommonUtils
    {
        private static readonly Random _random = new Random();
        public static T PickRandom<T>(T[] array)
        {
            return array[_random.Next(array.Length)];
        }

        public static int GetRandom(int n)
        {
            return _random.Next(n);
        }

        public static void Shutdown()
        {
            LogUtils.WriteLog("Application Shutdown", LogLevel.High);
            Application.Current.Shutdown();
        }
    }
}