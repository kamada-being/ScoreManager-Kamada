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

            var wIsFirst = true;
            while (true) {
                // メニュー選択の待機画面
                if (wIsFirst == false) {
                    Console.WriteLine("Enterキーを押すとメニュー画面を表示します");

                    while (Console.ReadKey(true).Key != ConsoleKey.Enter) {
                    }
                    Console.WriteLine();
                }
                wIsFirst = false;

                Console.WriteLine("=== メニュー一覧 ===");
                Console.WriteLine("1. 成績一覧表示");
                Console.WriteLine("2. 全教科の平均点表示");
                Console.WriteLine("3. 科目別平均点表示");
                Console.WriteLine("4. 合格者一覧表示");
                Console.WriteLine("5. 科目でフィルタ");
                Console.WriteLine("6. CSV出力");
                Console.WriteLine("0. 終了");
                Console.WriteLine();
                Console.Write("メニュー番号を入力してください: ");

                string wSelectNumber = Console.ReadLine()?.Trim();
                Console.WriteLine();

                switch (wSelectNumber) {
                    // 0.アプリ終了
                    case "0":
                    case "０":
                        Console.WriteLine("アプリを終了します。");
                        Environment.Exit(0);
                        break;

                    // 1.成績一覧表示
                    case "1":
                    case "１":
                        Console.WriteLine("=== 成績一覧 ===");
                        wManager.DisplayScores(wManager.Scores);
                        Console.WriteLine();
                        break;

                    // 2.全教科の平均点表示
                    case "2":
                    case "２":
                        Console.WriteLine("=== 全教科の平均点 ===");
                        Console.WriteLine($"平均点: {wManager.CalcAverageScore():F2}");
                        Console.WriteLine();
                        break;

                    // 3.科目別平均点表示
                    case "3":
                    case "３":
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
                        Console.WriteLine();
                        break;

                    // 4.合格者一覧表示
                    case "4":
                    case "４":
                        Console.WriteLine("=== 合格者一覧 ===");
                        var wPassingScores = wManager.GetPassingScores(wManager.Scores);
                        if (wPassingScores.Count == 0) {
                            Console.WriteLine("合格者はいません。");
                        } else {
                            Console.WriteLine($"合格者 {wPassingScores.GroupBy(x => x.Name).Count()} 名");
                            wManager.DisplayScores(wPassingScores);
                        }
                        Console.WriteLine();
                        break;

                    // 5.科目でフィルタして表示
                    case "5":
                    case "５":
                        Console.WriteLine("=== 科目でフィルタ ===");
                        Console.Write("科目名を入力：");
                        var wSubject = Console.ReadLine()?.Trim();

                        if (string.IsNullOrEmpty(wSubject)) {
                            Console.WriteLine("科目名が入力されていません。");
                            Console.WriteLine();
                            continue;
                        }

                        var wFilteredScores = wManager.FilterScoresBySubject(wSubject);

                        if (wFilteredScores.Count == 0) {
                            Console.WriteLine($"「{wSubject}」の成績はありません。");
                        } else {
                            Console.WriteLine($"{wSubject} の成績一覧");
                            wManager.DisplayScores(wFilteredScores);
                        }
                        Console.WriteLine();
                        break;

                    // 6.CSV出力
                    case "6":
                    case "６":
                        Console.WriteLine("=== CSV出力 ===");
                        var wOutputFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output_scores.csv");
                        wManager.OutputCSV(wOutputFilePath);
                        Console.WriteLine();
                        break;

                    default:
                        Console.WriteLine("無効な入力です。");
                        Console.WriteLine();
                        break;
                }
            }
        }
    }
}