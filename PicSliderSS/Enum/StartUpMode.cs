namespace PicSliderSS.Enum
{
    public enum StartUpMode
    {
        /// <summary>
        /// ノーマルモード：Exeをクリック、引数なし
        /// </summary>
        Normal = 1,
        
        /// <summary>
        /// 設定モード：引数 /c で起動
        /// </summary>
        Config = 2,
        
        /// <summary>
        /// スクリーンセーバモード：引数 /s で起動
        /// </summary>
        ScreenSaver = 3
    }
}