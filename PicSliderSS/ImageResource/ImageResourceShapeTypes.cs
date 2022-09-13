using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSliderSS.ImageResource
{
    class ImageResourceShapeTypes
    {
        /// <summary>
        /// 不正な形
        /// </summary>
        public const int NO_SHAPE = 0;

        /// <summary>
        /// 正方形
        /// </summary>
        public const int SQUARE = 1;

        /// <summary>
        /// 縦長な長方形
        /// </summary>
        public const int RECTANGLE_VERTICAL = 2;

        /// <summary>
        /// 横長な長方形
        /// </summary>
        public const int RECTANGLE_SIDE = 3;

        public const double THRESHOLD_SQUARE = 0.2;

        public const double THRESHOLD_RECTANGLE = 0.0;
    }
}
