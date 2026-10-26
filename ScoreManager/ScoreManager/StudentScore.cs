namespace ScoreManager {
    ///<summary>
    ///名前、科目、点数のデータの保持だけを行う。業務ロジックは持たない。
    /// </summary>
    internal class StudentScore {
        //各値のプロパティの作成　→今回は、値の読み込みのみを行いたいので、set;は不要。
        public string Name { get; }
        public string Subject { get; }
        public int Score { get; }


        //コンストラクタの作成
        public StudentScore(string vName, string vSubject, int vScore) {
            Name = vName;
            Subject = vSubject;
            Score = vScore;
        }
    }
}