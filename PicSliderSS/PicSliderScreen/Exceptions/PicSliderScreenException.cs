using PicSliderSS.Common;
using PicSliderSS.PicSliderScreen.Massages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PicSliderSS.PicSliderScreen
{
    class PicSliderScreenException : PicSliderException
    {
        public PicSliderScreenException() : base() { }
        public PicSliderScreenException(string message) : base(message) { }
        public PicSliderScreenException(int number, List<int> shapeTypes) : base()
        {
            string message = string.Format(ErrorMessage.NOT_IMPLEMENT_SCREEN, number, shapeTypes.ToString());
        }
        public PicSliderScreenException(string message, Exception innerException) : base(message, innerException) { }
        public PicSliderScreenException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
