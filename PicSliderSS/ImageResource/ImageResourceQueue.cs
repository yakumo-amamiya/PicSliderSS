using System;
using System.Collections.Generic;
using System.Linq;
using PicSliderSS.Enum;

namespace PicSliderSS.ImageResource
{
    public class ImageResourceQueue
    {
        private List<string> imageResourceList;
        private Queue<string> imageQueue;

        public ImageResourceQueue(string directoryPath)
        {
            // ToDo : 高速化の余地がある

            // 画像ファイルのパスを抽出
            imageResourceList = ImageResourceUtils.ImageFiles(directoryPath);

            // 未読み込みデータのQueueを生成
            imageQueue = new Queue<string>(this.imageResourceList.Select(i => i));

            Randomize();
        }

        // 画像ファイルを再読込
        public void Reload()
        {
            this.imageQueue = new Queue<string>(this.imageResourceList.Select(i => i));
            Randomize();
        }

        public ImageResourceData Dequeue(ImageShape[] shapes, int dWidth, int dHeight)
        {
            var loop = false;
            while (true)
            {
                if (imageQueue.Count == 0)
                {
                    if (loop)
                    {
                        // 2週目に入った場合は1データ目を返す。
                        Reload();
                        return new ImageResourceData(imageQueue.Dequeue(), dWidth, dHeight);
                    }
                    Reload();
                    loop = true;
                }
                
                var ird = new ImageResourceData(imageQueue.Dequeue(), dWidth, dHeight);
                if (shapes.Contains(ird.ShapeType))
                {
                    return ird;
                }
            }
        }

        public void Enqueue(ImageResourceData item)
        {
            imageQueue.Enqueue(item.Url);
        }

        public int Count { get => this.imageQueue.Count; }

        /// <summary>
        /// Queue をランダム化する
        /// </summary>
        public void Randomize()
        {
            this.imageQueue = new Queue<string>(this.imageQueue.ToArray().OrderBy(i => Guid.NewGuid()));
        }
    }
}
