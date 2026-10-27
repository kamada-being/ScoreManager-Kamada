using System;

namespace ScoreManager {
    internal class Program {
        static void Main(string[] args) {

            Console.WriteLine("=== 成績管理アプリ ===");

            var wManager = new ScoreManager();

            wManager.ReadData();
        }
    }
}
