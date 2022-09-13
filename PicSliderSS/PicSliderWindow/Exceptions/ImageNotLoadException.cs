using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PicSliderSS.PicSliderWindow.Exceptions
{
    class ImageNotLoadException : PicSliderWindowException
    {
        public ImageNotLoadException() : base() { }
        public ImageNotLoadException(string message) : base(message) { }
        public ImageNotLoadException(string message, Exception innerException) : base(message, innerException) { }
        public ImageNotLoadException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
