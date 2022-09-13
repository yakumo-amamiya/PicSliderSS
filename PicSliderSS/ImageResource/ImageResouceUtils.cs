using System;
using System.IO;
using System.Windows.Media.Imaging;
using System.Linq;
using System.Collections.Generic;


namespace PicSliderSS.ImageResource
{
    class ImageResourceUtils
    {
        /// <summary>
        /// 画像の形を判定するクラスメソッド
        /// </summary>
        /// <param name="bitmap">検査対象の画像</param>
        /// <returns>画像の種類(ImageResourceTypes)</returns>
        public static int ShapeType(BitmapImage bitmap)
        {
            if (bitmap.Width / bitmap.Height < 1.5)
            {
                return ImageResourceShapeTypes.SQUARE;
            }
            if (bitmap.Height / bitmap.Width < 1.5)
            {
                return ImageResourceShapeTypes.SQUARE;
            }

            if (bitmap.Height > bitmap.Width)
            {
                // 縦に長い場合
                return ImageResourceShapeTypes.RECTANGLE_VERTICAL;
            }
            else
            {
                // 横に長い場合
                return ImageResourceShapeTypes.RECTANGLE_SIDE;
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
            var files = Directory.GetFiles(targetDirectory, "*", SearchOption.AllDirectories);
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

            switch (ImageResourceUtils.FileType(filename))
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
