using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PicSliderSS.Common
{
    public class PicSliderException : Exception
    {
        public PicSliderException() : base() {
            LogUtils.ErrorLog("エラーメッセージ無し。");
        }
        public PicSliderException(string message) : base(message) {
            LogUtils.ErrorLog(message);
        }
        public PicSliderException(string message, Exception innerException) : base(message, innerException) {
            LogUtils.ErrorLog(message);
            LogUtils.StacktraceLog(innerException.StackTrace);
        }
        public PicSliderException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
