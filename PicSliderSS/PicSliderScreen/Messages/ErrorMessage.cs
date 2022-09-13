using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSliderSS.PicSliderScreen.Massages
{
    class ErrorMessage
    {
        public const string SELECT_SCREEN_ERROR_RANDOM = "Screen の選択中に、ランダム数値取得処理で想定外のパターンが発生しました。";
        public const string NOT_IMPLEMENT_SCREEN = "指定されたスクリーンは未実装です。Screen数:{0}, ShapeTypes:{1}";
        public const string NOT_ENOUGH_IMAGE = "Setする画像数が不足しています。要求数：{0}, 渡数：{1}";
    }
}
