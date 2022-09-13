using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PicSliderSS.Converter
{
    class DoubleConverter : IValueConverter
    {
        public object Convert(object value, Type type, object param, CultureInfo cul)
        {
            return (int.Parse(value.ToString()) * 2).ToString();
        }
        public object ConvertBack(object value, Type type, object param, CultureInfo cul)
        {
            return (int.Parse(value.ToString()) / 2).ToString();
        }
    }
}
