using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ScoreManager {
    /// <summary>
    /// 成績データの読み込み・保持・集計・出力など、業務ロジックの中心を担う。
    /// </summary>
    internal class ScoreManager {

        /// <summary>
        /// StudentScoreのデータを保持するリスト
        /// </summary>
        private List<StudentScore> FScores = new List<StudentScore>();

        /// <summary>
        /// 「氏名, 科目名, 点数」の3列形式であることを検証する正規表現。readonlyにして、正規表現が書き換えられるのを防いでいる
        /// </summary>
        static readonly Regex G_CsvLineRegex = new Regex(
            // （学習）[ \t\u3000]*  → 空白(半角スペース・タブ・全角スペース)を許容する
            // [\p{IsHiragana}\p{IsKatakana}\p{IsCJKUnifiedIdeographs}a-zA-Z]+  → 氏名(ひらがな/カタカナ/漢字/英字のうち、いずれか1文字以上)
            // [\p{IsHiragana}\p{IsKatakana}\p{IsCJKUnifiedIdeographs}]+        → 科目名(ひらがな/カタカナ/漢字のうち、いずれか1文字以上)
            // (100|[1-9][0-9]|[0-9])   → 点数(0-100までの値のうち、いずれかの値)
            @"^[ \t\u3000]*[\p{IsHiragana}\p{IsKatakana}\p{IsCJKUnifiedIdeographs}a-zA-Z]+[ \t\u3000]*," +
            @"[ \t\u3000]*[\p{IsHiragana}\p{IsKatakana}\p{IsCJKUnifiedIdeographs}]+[ \t\u3000]*," +
            @"[ \t\u3000]*(100|[1-9][0-9]|[0-9])[ \t\u3000]*$"
        );

        /// <summary>
        /// CSVファイルの読み込みを行う。
        /// </summary>
        public void ReadData() {
            var wFilePath = @"C:\Users\kamada\Documents\アプリ開発①\scores.csv";

            if (!File.Exists(wFilePath)) {
                Console.WriteLine($"ファイルが見つかりませんでした: {wFilePath}");
                return;
            }

            // 取得したデータの削除
            FScores.Clear();

            using (var wReader = new StreamReader(wFilePath)) {
                while (!wReader.EndOfStream) {
                    var wRawLine = wReader.ReadLine();

                    //空行（未入力・空白のみの行）をスキップする
                    if (string.IsNullOrWhiteSpace(wRawLine)) {
                        continue;
                    }

                    //文字列の前後の空白を取り除く
                    var wTrimLine = wRawLine.Trim();

                    // コメント行(#から始まる）をスキップする
                    if (wTrimLine.StartsWith("#")) {
                        continue;
                    }

                    //正規表現に一致しない行をスキップ
                    if (!G_CsvLineRegex.IsMatch(wTrimLine)) {
                        Console.WriteLine("不正な行: " + wTrimLine);
                        continue;
                    }

                    var wColumns = wTrimLine.Split(',').Select(vCol => vCol.Trim()).ToArray();

                    var wName = wColumns[0];
                    var wSubject = wColumns[1];
                    var wScore = int.Parse(wColumns[2]);

                    FScores.Add(new StudentScore(wName, wSubject, wScore));
                }
                Console.WriteLine($"{FScores.Count} 件のデータを読み込みました。");
            }
        }
    }
}