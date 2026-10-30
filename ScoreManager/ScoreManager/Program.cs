using System;
using System.IO;

namespace ScoreManager {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("=== 成績管理アプリ ===");
            Console.WriteLine(".exeと同階層にあるCSVファイルから成績データを読み込みます。");

            var wManager = new ScoreManager();
            string wFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scores.csv");
            wManager.ReadData(wFilePath);

            wManager.DisplayScores();

            Console.WriteLine($"平均点: {wManager.CalcAverageScore():F2}");
        }
    }
}
