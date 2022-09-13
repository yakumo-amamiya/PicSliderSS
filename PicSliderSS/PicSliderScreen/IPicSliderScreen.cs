using PicSliderSS.ImageResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSliderSS.PicSliderScreen
{
    public interface IPicSliderScreen
    {
        event EventHandler PicSlideAllCompleted;
        event EventHandler ScreenRendered;

        void BeginPictureSlide();

        void SetImages(ICollection<ImageResourceData> images);
    }
}
