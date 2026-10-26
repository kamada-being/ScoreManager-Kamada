namespace ScoreManager {
    /// <summary>
    /// 名前、科目、点数のデータの保持だけを行う。業務ロジックは持たない。
    /// </summary>
    internal class StudentScore {
        // 各値プロパティを作成する。今回は、値の読み込みのみを行いたいので、set;は不要。
        /// <summary>
        /// 生徒の氏名を取得する。
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// 科目名を取得する。
        /// </summary>
        public string Subject { get; }
        /// <summary>
        /// 点数を取得する。
        /// </summary>
        public int Score { get; }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="vName">生徒の氏名</param>
        /// <param name="vSubject">科目名</param>
        /// <param name="vScore">点数</param>
        public StudentScore(string vName, string vSubject, int vScore) {
            this.Name = vName;
            this.Subject = vSubject;
            this.Score = vScore;
        }
    }
}