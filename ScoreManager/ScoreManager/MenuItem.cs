using System;

namespace ScoreManager {
    internal class MenuItem {
        /// <summary>
        /// ユーザーが選択した番号を取得する。
        /// </summary>
        public string SelectedNumber { get; }

        /// <summary>
        /// メニューの表示テキストを取得する。
        /// </summary>
        public string MenuText { get; }

        /// <summary>
        /// 実行する処理を取得する。
        /// </summary>
        public Action Execute { get; }

        /// <summary>
        /// メニュー一覧表示に必要な情報を保持する
        /// </summary>
        /// <param name="vMenuNumber">ユーザーが選択するメニュー番号</param>
        /// <param name="vMenuText">表示するメニューのテキスト</param>
        /// <param name="vExecute">実行する処理</param>
        public MenuItem(string vMenuNumber, string vMenuText, Action vExecute) {
            this.SelectedNumber = vMenuNumber;
            this.MenuText = vMenuText;
            this.Execute = vExecute;
        }
    }
}
