using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ScoreManager {
    /// <summary>
    /// 成績データの読み込み・保持・集計・出力など、業務ロジックの中心を担う。
    /// </summary>
    internal class ScoreManager {
        /// <summary>
        /// StudentScoreのデータを保持するリスト。
        /// </summary>
        private List<StudentScore> FScores = new List<StudentScore>();

        /// <summary>
        /// 「氏名, 科目名, 点数」の3列形式であることを検証する正規表現。
        /// </summary>
        static readonly Regex C_CsvLineRegex = new Regex(
            // [ \t\u3000]*  → 空白(半角スペース・タブ・全角スペース)を許容する
            // [\p{IsHiragana}\p{IsKatakana}\p{IsCJKUnifiedIdeographs}a-zA-Z]+  → 氏名(ひらがな/カタカナ/漢字/英字のうち、いずれか1文字以上)
            // [\p{IsHiragana}\p{IsKatakana}\p{IsCJKUnifiedIdeographs}]+        → 科目名(ひらがな/カタカナ/漢字のうち、いずれか1文字以上)
            // (100|[1-9][0-9]|[0-9])   → 点数(0-100までの値のうち、いずれかの値)
            @"^[ \t\u3000]*(?<name>[\p{IsHiragana}\p{IsKatakana}\p{IsCJKUnifiedIdeographs}a-zA-Z]+)[ \t\u3000]*," +
            @"[ \t\u3000]*(?<subject>[\p{IsHiragana}\p{IsKatakana}\p{IsCJKUnifiedIdeographs}]+)[ \t\u3000]*," +
            @"[ \t\u3000]*(?<score>100|[1-9][0-9]|[0-9])[ \t\u3000]*$"
        );

        /// <summary>
        /// CSVファイルの読み込みを行う。
        /// </summary>
        public void ReadData(string vFilePath) {
            if (!File.Exists(vFilePath)) {
                Console.WriteLine($"ファイルが見つかりませんでした: {vFilePath}");
                return;
            }

            FScores.Clear();

            using (var wReader = new StreamReader(vFilePath)) {
                while (!wReader.EndOfStream) {
                    var wRawLine = wReader.ReadLine();

                    if (string.IsNullOrWhiteSpace(wRawLine)) {
                        continue;
                    }

                    var wTrimLine = wRawLine.Trim();

                    if (wTrimLine.StartsWith("#")) {
                        continue;
                    }

                    var wMatch = C_CsvLineRegex.Match(wTrimLine);
                    if (!wMatch.Success) {
                        Console.WriteLine("不正な行: " + wTrimLine);
                        continue;
                    }

                    var wName = wMatch.Groups["name"].Value;
                    var wSubject = wMatch.Groups["subject"].Value;
                    var wScore = int.Parse(wMatch.Groups["score"].Value);
                    FScores.Add(new StudentScore(wName, wSubject, wScore));
                }
                Console.WriteLine($"{FScores.Count} 件のデータを読み込みました。");
            }
        }
        /// <summary>
        /// 成績データの一覧表示を行う。
        /// </summary>
        public void wDisplayScores() {
            foreach (var wScores in FScores) {
                Console.WriteLine($"{wScores.Name}　| { wScores.Subject} | {wScores.Score}");
            }
        }
    }
}