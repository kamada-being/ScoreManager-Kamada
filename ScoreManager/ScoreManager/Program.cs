using System;
using System.IO;
using System.Linq;

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

            // 2.全教科の平均点表示
            Console.WriteLine("=== 全教科の平均点 ===");
            Console.WriteLine($"平均点: {wManager.CalcAverageScore():F2}");

            // 3.科目別平均点表示
            Console.WriteLine("=== 科目別平均点 ===");
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
            Console.WriteLine("=== 合格者一覧 ===");
            var wPassingScores = wManager.GetPassingScores(wManager.Scores);
            if (wPassingScores.Count == 0) {
                Console.WriteLine("合格者はいません。");
            } else {
                Console.WriteLine($"合格者 {wPassingScores.GroupBy(x => x.Name).Count()} 名");
                wManager.DisplayScores(wPassingScores);
            }

            // 5.科目でフィルタして表示
            Console.WriteLine("=== 科目でフィルタ ===");
            Console.Write("科目名を入力：");
            var wSubject = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(wSubject)) {
                Console.WriteLine("科目名が入力されていません。");
                return;
            }

            var wFilteredScores = wManager.FilterScoresBySubject(wSubject);
            
            if (wFilteredScores.Count == 0) {
                Console.WriteLine($"「{wSubject}」の成績はありません。");
            } else {
                Console.WriteLine($"{wSubject} の成績一覧");
                wManager.DisplayScores(wFilteredScores);
            }
        }
    }
}