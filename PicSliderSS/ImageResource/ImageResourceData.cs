using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using PicSliderSS.Enum;

namespace PicSliderSS.ImageResource
{
    public class ImageResourceData
    {
        private string url;
        private bool loaded;
        private int fileType;
        private ImageShape shapeType;
        private BitmapImage bitmap;

        public ImageResourceData(string url, int dWidth, int dHeight)
        {
            this.url = url;
            this.loaded = true;
            LoadImage(dWidth, dHeight);
            this.fileType = ImageResourceUtils.FileType(this.Url);
        }

        public string Url { get => url; }
        public bool Loaded { get => loaded; }
        public int FileType { get => fileType; }
        public ImageShape ShapeType { get => shapeType; }
        public BitmapImage Bitmap { get => bitmap; }

        public void LoadImage(int dWidth, int dHeight)
        {
            var bytes = File.ReadAllBytes(url);
            var stream = new MemoryStream(bytes);
            this.bitmap = new BitmapImage();
            this.bitmap.BeginInit();
            // 長い方の解像度を採用する。
            if (dWidth > dHeight)
            {
                this.Bitmap.DecodePixelWidth = dWidth;
            }
            else
            {
                this.Bitmap.DecodePixelHeight = dHeight;
            }
            this.bitmap.StreamSource = stream;
            this.bitmap.EndInit();
            this.bitmap.Freeze();
            this.shapeType = ImageResourceUtils.ShapeType(Bitmap);
        }
    }
}
