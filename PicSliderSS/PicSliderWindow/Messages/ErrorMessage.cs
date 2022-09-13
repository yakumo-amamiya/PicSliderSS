using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSliderSS.PicSliderWindow.Messages
{
    class ErrorMessage
    {
        public const string SELECTED_INVALID_SCREEN = "不正なスクリーンが選択されました。";
        public const string INVALID_DISPOSE_SCREEN = "不正なスクリーン破棄が発生しました。";
        public const string IMAGE_NOT_LOAD = "画像が Queue から読み込めませんでした。要求数:{0}";
    }
}
