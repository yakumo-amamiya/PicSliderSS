using PicSliderSS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PicSliderSS.PicSliderWindow.Exceptions
{
    class PicSliderWindowException : PicSliderException
    {
        public PicSliderWindowException() : base() { }
        public PicSliderWindowException(string message) : base(message) { }
        public PicSliderWindowException(string message, Exception innerException) : base(message, innerException) { }
        public PicSliderWindowException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
