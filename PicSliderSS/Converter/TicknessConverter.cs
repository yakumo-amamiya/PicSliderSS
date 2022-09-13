using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PicSliderSS.Converter
{
    class TicknessConverter : IValueConverter
    {
        public object Convert(object value, Type type, object param, CultureInfo cul)
        {
            switch (param.ToString())
            {
                case "Left":
                    return string.Format("{0},0,0,0", value.ToString());
                case "Top":
                    return string.Format("0,{0},0,0", value.ToString());
                case "Right":
                    return string.Format("0,0,{0},0", value.ToString());
                case "Bottom":
                    return string.Format("0,0,0,{0}", value.ToString());
                default:
                    return "Right";
            }
        }
        public object ConvertBack(object value, Type type, object param, CultureInfo cul)
        {
            return "";
        }
    }
}
