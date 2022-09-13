using PicSliderSS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PicSliderSS.ImageResource
{
    [Serializable()]
    public class ImageResourceException : PicSliderException
    {
        public ImageResourceException() : base() { }
        public ImageResourceException(string message) : base(message) { }
        public ImageResourceException(string message, Exception innerException) : base(message, innerException) { }
        public ImageResourceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
