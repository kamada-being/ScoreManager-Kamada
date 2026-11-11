using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ScoreManager {
    internal class Program {
        private static readonly ScoreManager FManager = new ScoreManager();
        static void Main(string[] args) {
            Console.WriteLine("=== 成績管理アプリ ===");
            Console.WriteLine(".exeと同階層にあるCSVファイルから成績データを読み込みます。");

            var wFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scores.csv");
            FManager.ReadData(wFilePath);

            var wMenuItems = new List<MenuItem> {
                new MenuItem("1", "一覧表示", DisplayScores),
                new MenuItem("2", "平均点表示", DisplayAverage),
                new MenuItem("3", "科目別平均点表示", DisplaySubjectAverage),
                new MenuItem("4", "合格者一覧", DisplayPassingScores),
                new MenuItem("5", "科目でフィルタ", FilterBySubject),
                new MenuItem("6", "CSV出力", OutputCSV),
            };

            while (true) {
                Console.WriteLine("=== 成績管理メニュー ===");
                foreach (var wMenuItem in wMenuItems) {
                    Console.WriteLine($"{wMenuItem.MenuNumber}. {wMenuItem.MenuText}");
                }
                Console.WriteLine("0. 終了");
                Console.WriteLine();
                Console.Write("選択：");

                string wSelectedNumber = Console.ReadLine()?.Trim().Normalize(NormalizationForm.FormKC);

                if (wSelectedNumber == "0") {
                    ExitApplication();
                    return;
                }

                var wSelectedMenuItem = wMenuItems.FirstOrDefault(x => x.MenuNumber == wSelectedNumber);

                if (wSelectedMenuItem != null) {
                    wSelectedMenuItem.Execute();
                } else {
                    ErrorMessage();
                }
                WaitForEnterKey();
            }
        }

        /// <summary>
        /// アプリケーションを終了する
        /// </summary>
        private static void ExitApplication() {
            Console.WriteLine("アプリを終了します。任意のキーを押してください。");
            Console.ReadKey(true);
        }

        /// <summary>
        /// 成績一覧を表示する
        /// </summary>
        private static void DisplayScores() {
            Console.WriteLine("=== 成績一覧 ===");
            FManager.DisplayScores(FManager.Scores);
            Console.WriteLine();
        }

        /// <summary>
        /// 全教科の平均点を表示する
        /// </summary>
        private static void DisplayAverage() {
            Console.WriteLine($"平均点: {FManager.CalcAverageScore():F2}");
            Console.WriteLine();
        }

        /// <summary>
        /// 各教科の平均点を表示する
        /// </summary>
        private static void DisplaySubjectAverage() {
            Console.WriteLine("科目別平均点:");
            var wSubjectAverages = FManager.CalcSubjectAverages(FManager.Scores);

            if (wSubjectAverages.Count == 0) {
                Console.WriteLine("データがありません。");
            } else {
                foreach (var wPair in wSubjectAverages) {
                    Console.WriteLine($"{wPair.Key}: {wPair.Value:F2}");
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// 合格者の成績一覧を表示する
        /// </summary>
        private static void DisplayPassingScores() {
            var wPassingScores = FManager.GetPassingScores(FManager.Scores);
            if (wPassingScores.Count == 0) {
                Console.WriteLine("合格者はいません。");
            } else {
                Console.WriteLine($"合格者 {wPassingScores.GroupBy(x => x.Name).Count()} 名");
                FManager.DisplayScores(wPassingScores);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// ユーザーから入力された科目別でフィルタして、成績一覧を表示する
        /// </summary>
        private static void FilterBySubject() {
            Console.Write("科目名を入力：");
            var wSubject = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(wSubject)) {
                Console.WriteLine("科目名が入力されていません。");
                Console.WriteLine();
                return;
            }

            var wFilteredScores = FManager.FilterScoresBySubject(wSubject);

            if (wFilteredScores.Count == 0) {
                Console.WriteLine($"「{wSubject}」の成績はありません。");
            } else {
                Console.WriteLine($"{wSubject} の成績一覧");
                FManager.DisplayScores(wFilteredScores);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// 成績データをCSVファイルに出力する
        /// </summary>
        private static void OutputCSV() {
            var wOutputFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output_scores.csv");
            FManager.OutputCSV(wOutputFilePath);
            Console.WriteLine();
        }

        /// <summary>
        /// エラー時のメッセージを表示する
        /// </summary>
        private static void ErrorMessage() {
            Console.WriteLine("無効な入力です。");
            Console.WriteLine();
        }

        /// <summary>
        /// メニュー選択の待機画面を表示する
        /// </summary>
        private static void WaitForEnterKey() {
            Console.WriteLine("Enterキーを押すとメニュー画面を表示します");

            while (Console.ReadKey(true).Key != ConsoleKey.Enter) {
            }
            Console.WriteLine();
        }
    }
}