using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


///<summary>
///ScoreManagerクラス:「データを保持するだけ」のクラス。名前、科目、点数のデータを保持する。コンストラクタを作成し、各データを引数で受け取れるようにする。
/// </summary>
namespace ScoreManager {
    internal class StudentScore {
        //各値のプロパティの作成　→今回は、値の読み込みのみを行いたいので、set;は不要。
        public string Name { get; }
        public string Subject { get; }
        public int Score { get; }


        //コンストラクタの作成
        public StudentScore(string name, string subject, int score) {
            Name = name;
            Subject = subject;
            Score = score;
        }
    }
}