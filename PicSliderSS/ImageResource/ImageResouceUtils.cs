using System;
using System.IO;
using System.Windows.Media.Imaging;
using System.Linq;
using System.Collections.Generic;
using PicSliderSS.Config;
using PicSliderSS.Enum;


namespace PicSliderSS.ImageResource
{
    class ImageResourceUtils
    {
        /// <summary>
        /// 画像の形を判定するクラスメソッド
        /// </summary>
        /// <param name="bitmap">検査対象の画像</param>
        /// <returns>画像の種類(ImageResourceTypes)</returns>
        public static ImageShape ShapeType(BitmapImage bitmap)
        {
            if (bitmap.Width / bitmap.Height >= 1.2)
            {
                return ImageShape.Landscape;
            }
            else
            {
                if (bitmap.Height / bitmap.Width >= 1.2)
                {
                    return ImageShape.Portrait;
                }
                else
                {
                    return ImageShape.Balance;
                }
            }
        }

        /// <summary>
        /// URLからファイルの形式を返す
        /// </summary>
        /// <param name="url"></param>
        /// <returns>ImageResourceFileType</returns>
        public static int FileType(string url)
        {
            string ext = Path.GetExtension(url).ToLower();

            switch (ext)
            {
                case ".jpeg":
                case ".jpg":
                    return ImageResourceFileTypes.JPEG;
                case ".png":
                    return ImageResourceFileTypes.PNG;
                default:
                    return ImageResourceFileTypes.NO_DATA;
            }
        }

        /// <summary>
        /// 指定したフォルダから画像のパスのリストを取得する
        /// </summary>
        /// <param name="targetDirectory"></param>
        /// <returns></returns>
        public static List<string> ImageFiles(string targetDirectory)
        {
            var opt = AppConfig.Data.Recurse ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            var files = Directory.GetFiles(targetDirectory, "*", opt);
            return files.Where(EnableFilename).ToList();
        }

        /// <summary>
        /// 使用可能ファイル形式か判定する
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool EnableFilename(string filename)
        {
            //string ext = Path.GetExtension(filename).ToLower();

            switch (FileType(filename))
            {
                case ImageResourceFileTypes.JPEG:
                case ImageResourceFileTypes.PNG:
                    return true;
                default:
                    return false;
            }
        }
    }
}
