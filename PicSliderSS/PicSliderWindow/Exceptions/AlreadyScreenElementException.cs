using PicSliderSS.PicSliderScreen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSliderSS.PicSliderWindow.Exceptions
{
    class AlreadyExistScreenElementException : PicSliderScreenException
    {
        public AlreadyExistScreenElementException() : base() { }
        public AlreadyExistScreenElementException(string message) : base(message) { }

    }
}
