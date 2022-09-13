using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using PicSliderSS.Common;
using PicSliderSS.PicSliderScreen;
using PicSliderSS.PicSliderWindow.Exceptions;

namespace PicSliderSS.ImageResource
{
    class ImageResourceQueue
    {
        private List<string> imageResourceList;
        private Queue<ImageResourceData> imageQueue;
        private Queue<ImageResourceData> LoadedImageQueue;

        public ImageResourceQueue(string directoryPath)
        {
            // ToDo : 高速化の余地がある

            // 画像ファイルのパスを抽出
            this.imageResourceList = ImageResourceUtils.ImageFiles(directoryPath);

            // 未読み込みデータのQueueを生成
            this.imageQueue = new Queue<ImageResourceData>(this.imageResourceList.Select(i => new ImageResourceData(i)));

            this.Randomize();

            this.LoadedImageQueue = new Queue<ImageResourceData>();

            // 画像ファイルをQueueに読み込み
            this.LoadImages(20);
        }

        // 画像ファイルを再読込
        public void Reload()
        {
            this.imageQueue = new Queue<ImageResourceData>(this.imageResourceList.Select(i => new ImageResourceData(i)));
            this.LoadImages(20);
        }

        public ImageResourceData Dequeue()
        {
            //this.LoadedImageQueue.TrimExcess();

            this.LoadImages(1);

            //Task.Run(() =>
            //{
            //    this.LoadImages(1);
            //});

            return this.LoadedImageQueue.Dequeue();
        }

        public void Enqueue(ImageResourceData item)
        {
            this.LoadedImageQueue.Enqueue(item);
        }

        public int Count { get => this.LoadedImageQueue.Count; }

        /// <summary>
        /// Queue をランダム化する
        /// </summary>
        public void Randomize()
        {
            this.imageQueue = new Queue<ImageResourceData>(this.imageQueue.ToArray().OrderBy(i => Guid.NewGuid()));
        }

        /// <summary>
        /// 画像ファイルの読み込みを行う
        /// </summary>
        /// <param name="number">読むこむファイルの件数</param>
        public void LoadImages(int number)
        {
            int counter = 0;
            foreach(var i in Enumerable.Range(1, number))
            {
                ImageResourceData ird = this.imageQueue.Dequeue();

                if (counter >= number)
                {
                    break;
                }

                try
                {
                    ird.LoadImage();
                    this.LoadedImageQueue.Enqueue(ird);
                    counter++;
                }
                catch (ImageResourceException e)
                {
                    LogUtils.ErrorLog(e.Message);
                    LogUtils.StacktraceLog(e.StackTrace);
                }
            }

            return;
        }

        /// <summary>
        /// 全ての画像ファイルの読み込みを行う
        /// </summary>
        public void LoadImages()
        {
            this.LoadImages(this.LoadedImageQueue.Count);
        }


        public List<ImageResourceData> MultiDequeue(int number)
        {
            // 画像を格納するリストを生成する
            List<ImageResourceData> items = new List<ImageResourceData>();

            // 画像を取得する
            if (this.Count == 0)
            {
                // Queueが空の場合は、リロードする
                this.Reload();
            }
            foreach (int i in Enumerable.Range(0, number))
            {
                if (this.Count == 0)
                {
                    // 取得できる場像がない場合は、画像取得を終了する
                    break;
                }
                items.Add(this.Dequeue());
            }

            return items;
        }

        public List<ImageResourceData> MultiDequeue()
        {
            // 取得する画像数を取得する
            // ToDo : 最大数を設定値で変更できるようにする
            int size = (new Random()).Next(PicSliderScreenSize.BIGGEST) + 1;

            return this.MultiDequeue(size);
        }
        
     }
}
