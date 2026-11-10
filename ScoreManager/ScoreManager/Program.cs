using System;
using System.IO;
using System.Linq;
using System.Text;

namespace ScoreManager {
    internal class Program {
        static internal ScoreManager wManager = new ScoreManager();

        static void Main(string[] args) {
            Console.WriteLine("=== 成績管理アプリ ===");
            Console.WriteLine(".exeと同階層にあるCSVファイルから成績データを読み込みます。");


            var wFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scores.csv");
            wManager.ReadData(wFilePath);

            while (true) {
                Console.WriteLine("=== 成績管理メニュー ===");
                Console.WriteLine($"{MenuEnum.Exit}. 一覧表示");
                Console.WriteLine($"{MenuEnum.DisplayAverage}. 平均点表示");
                Console.WriteLine($"{MenuEnum.DisplaySubjectAverage}. 科目別平均点表示");
                Console.WriteLine($"{MenuEnum.DisplayPassingScores}. 合格者一覧");
                Console.WriteLine($"{MenuEnum.FilterBySubject}. 科目でフィルタ");
                Console.WriteLine($"{MenuEnum.OutputCSV}. CSV出力");
                Console.WriteLine($"{MenuEnum.Exit}. 終了");
                Console.WriteLine();
                Console.Write("選択：");

                string wInput = Console.ReadLine()?.Trim().Normalize(NormalizationForm.FormKC);

                if (int.TryParse(wInput, out int wSelectNumber) && Enum.IsDefined(typeof(MenuEnum), wSelectNumber)) {
                    var wMenuNumber = (MenuEnum)wSelectNumber;

                    switch (wMenuNumber) {
                        case MenuEnum.Exit:
                            ExitApplication();
                            return;

                        case MenuEnum.DisplayScores:
                            DisplayScores();
                            break;

                        case MenuEnum.DisplayAverage:
                            DisplayAverage();
                            break;

                        case MenuEnum.DisplaySubjectAverage:
                            DisplaySubjectAverage();
                            break;

                        case MenuEnum.DisplayPassingScores:
                            DisplayPassingScores();
                            break;

                        case MenuEnum.FilterBySubject:
                            FilterBySubject();
                            break;

                        case MenuEnum.OutputCSV:
                            OutputCSV();
                            break;

                        default:
                            ErrorMessage();
                            break;
                    }
                    // メニュー選択の待機画面
                    Console.WriteLine("Enterキーを押すとメニュー画面を表示します");

                    while (Console.ReadKey(true).Key != ConsoleKey.Enter) {
                    }
                    Console.WriteLine();
                }
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
            wManager.DisplayScores(wManager.Scores);
            Console.WriteLine();
        }

        /// <summary>
        /// 全教科の平均点を表示する
        /// </summary>
        /// <param name="wManager"></param>
        private static void DisplayAverage() {
            Console.WriteLine($"平均点: {wManager.CalcAverageScore():F2}");
            Console.WriteLine();
        }

        /// <summary>
        /// 各教科の平均点を表示する
        /// </summary>
        private static void DisplaySubjectAverage() {
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
        }

        /// <summary>
        /// 合格者の成績一覧を表示する
        /// </summary>
        private static void DisplayPassingScores() {
            var wPassingScores = wManager.GetPassingScores(wManager.Scores);
            if (wPassingScores.Count == 0) {
                Console.WriteLine("合格者はいません。");
            } else {
                Console.WriteLine($"合格者 {wPassingScores.GroupBy(x => x.Name).Count()} 名");
                wManager.DisplayScores(wPassingScores);
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

            var wFilteredScores = wManager.FilterScoresBySubject(wSubject);

            if (wFilteredScores.Count == 0) {
                Console.WriteLine($"「{wSubject}」の成績はありません。");
            } else {
                Console.WriteLine($"{wSubject} の成績一覧");
                wManager.DisplayScores(wFilteredScores);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// 成績データをCSVファイルに出力する
        /// </summary>
        private static void OutputCSV() {
            var wOutputFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output_scores.csv");
            wManager.OutputCSV(wOutputFilePath);
            Console.WriteLine();
        }

        /// <summary>
        /// エラー時のメッセージを表示する
        /// </summary>
        private static void ErrorMessage() {
            Console.WriteLine("無効な入力です。");
            Console.WriteLine();
        }
    }
}