using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreManager {
    /// <summary>
    /// メニューの選択肢を表す列挙型。
    /// </summary>
    internal enum MenuEnum {
        Exit = 0,
        DisplayScores = 1,
        DisplayAverage = 2,
        DisplaySubjectAverage = 3,
        DisplayPassingScores = 4,
        FilterBySubject = 5,
        OutputCSV = 6
    }
}
