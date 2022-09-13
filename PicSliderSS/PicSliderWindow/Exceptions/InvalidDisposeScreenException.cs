using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PicSliderSS.PicSliderWindow.Exceptions
{
    class InvalidDisposeScreenException : PicSliderWindowException
    {
        public InvalidDisposeScreenException() : base() { }
        public InvalidDisposeScreenException(string message) : base(message) { }
        public InvalidDisposeScreenException(string message, Exception innerException) : base(message, innerException) { }
        public InvalidDisposeScreenException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
