using System;
using System.IO;

namespace ScoreManager {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("=== 成績管理アプリ ===");
            Console.WriteLine(".exeと同階層にあるCSVファイルから成績データを読み込みます。");

            var wManager = new ScoreManager();
            var wFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scores.csv");
            wManager.ReadData(wFilePath);

            // 1.一覧表示
            Console.WriteLine("=== 成績一覧 ===");
            wManager.DisplayScores(wManager.Scores);

            // 2.平均点表示
            Console.WriteLine($"平均点: {wManager.CalcAverageScore():F2}");

            // 3.科目別平均点表示
            Console.WriteLine("科目別平均点:");
            var wSubjectAverages = wManager.CalcSubjectAverages(wManager.Scores);

            if (wSubjectAverages.Count == 0) {
                Console.WriteLine("データがありません。");
            } else {
                foreach (var wPair in wSubjectAverages) {
                    Console.WriteLine($"{wPair.Key}: {wPair.Value:F2}");
                }
            }

            // 4.合格者一覧表示
            var wPassers = wManager.FilterPassers(wManager.Scores);
            Console.WriteLine($"合格者 {wPassers.Count} 名");
            wManager.DisplayScores(wPassers);
        }
    }
}