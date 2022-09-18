using System.Windows.Media;
using PicSliderSS.Common;
using PicSliderSS.Enum;
using PicSliderSS.ImageResource;

namespace PicSliderSS.SliderImageInfomation
{
    public class SliderImageInformation
    {
        public ImageShape[] EnableShapes { get; set; }
        public ImageResourceData ImageResource { get; set; }
        public Stretch Stretch { get; set; }
        public SlideDirection SlideDirection { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Left { get; set; }
        public double Top { get; set; }

        public double Start { get; set; }

        public double Middle { get; set; }
        

    }
}