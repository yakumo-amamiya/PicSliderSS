using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PicSliderSS.ImageResource
{
    public class ImageResourceData
    {
        private string url;
        private bool loaded;
        private int fileType;
        private int shapeType;
        private BitmapImage bitmap;
        private Exception error;

        public ImageResourceData()
        {
            this.url = null;
            this.loaded = false;
            this.fileType = ImageResourceFileTypes.NO_DATA;
            this.shapeType = ImageResourceShapeTypes.NO_SHAPE;
            this.bitmap = null;
        }

        public ImageResourceData(string url)
        {
            this.url = url;
            this.loaded = false;
            this.fileType = ImageResourceUtils.FileType(this.Url);
            this.shapeType = ImageResourceShapeTypes.NO_SHAPE;
            this.bitmap = null;
        }

        public ImageResourceData(BitmapImage bitmap)
        {
            this.bitmap = bitmap;
            this.url = bitmap.BaseUri.LocalPath;
            this.loaded = true;
            this.fileType = ImageResourceUtils.FileType(this.url);
            this.shapeType = ImageResourceUtils.ShapeType(this.Bitmap);
        }

        public string Url { get => url; }
        public bool Loaded { get => loaded; }
        public int FileType { get => fileType; }
        public int ShapeType { get => shapeType; }
        public BitmapImage Bitmap { get => bitmap; }
        public Exception Error { get => error; }

        public void LoadImage()
        {
            try
            {
                this.bitmap = new BitmapImage();
                this.bitmap.BeginInit();
                this.bitmap.DecodePixelWidth = 1920;
                this.bitmap.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
                this.bitmap.UriSource = new Uri(this.url);
                //this.bitmap = new BitmapImage(new Uri(this.url));
                this.bitmap.EndInit();
                this.bitmap.Freeze();
                this.shapeType = ImageResourceUtils.ShapeType(this.Bitmap);
                Debug.WriteLine(string.Format("ファイルを読み込みました。url:{0}", this.Url));
            }
            catch (System.ArgumentNullException e)
            {
                this.error = e;
                throw new ImageResourceException(string.Format(ImageResourceMessages.EX_ARGUMENT_NULL_EXCEPTION, e.ParamName), e);
            }
            catch (FileNotFoundException e)
            {
                this.error = e;
                throw new ImageResourceException(string.Format(ImageResourceMessages.EX_FILE_NOT_FOUND, e.FileName), e);
            }
        }
    }
}
