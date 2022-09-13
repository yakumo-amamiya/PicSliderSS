using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PicSliderSS.PicSliderWindow.Exceptions
{
    class InvalidSwitchScreenException : PicSliderWindowException
    {
        public InvalidSwitchScreenException() : base() { }
        public InvalidSwitchScreenException(string message) : base(message) { }
        public InvalidSwitchScreenException(string message, Exception innerException) : base(message, innerException) { }
        public InvalidSwitchScreenException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
