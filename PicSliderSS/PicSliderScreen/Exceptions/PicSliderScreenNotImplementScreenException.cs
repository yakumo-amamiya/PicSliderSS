using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PicSliderSS.PicSliderScreen
{
    class PicSliderScreenNotImplementScreenException : PicSliderScreenException
    {
        public PicSliderScreenNotImplementScreenException() : base() { }
        public PicSliderScreenNotImplementScreenException(string message) : base(message) { }
        public PicSliderScreenNotImplementScreenException(string message, Exception innerException) : base(message, innerException) { }
        public PicSliderScreenNotImplementScreenException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
