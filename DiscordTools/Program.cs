using DiscordTools.Data;
using DiscordTools.Tools;
using DiscordTools.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscordTools {
    class Program {
        static TokenHolder holder;
        static History history;
        static List<ITool> tools;
        static ToolMenu menu;

        static void Main(string[] args) {
            MainAsync(args);
        }

        static void MainAsync(string[] args) {
            // Create objects for data.
            history = new History("history.log");
            holder = ConfManager.LoadAsync("token.json",
                new TokenHolder()).Result;
            tools = ToolLoader.GetTools();
            menu = new ToolMenu(tools);
            menu.SelectedIndexChanged += indexChanged;

            Console.CursorVisible = false;
            Console.Title = "DiscordTools";
            Console.SetCursorPosition(0, 1);

            if (String.IsNullOrEmpty(holder.Token)) {
                Console.WriteLine(" Warning: No token specified.");
            }

            Console.WriteLine(Resources.Information);

            ConsoleKey key;
            do {
                Console.SetCursorPosition(1, 7);
                var result = menu.RunMenu(0);

                runTool(tools[result]);
            } while (!Console.KeyAvailable);
        }

        static void indexChanged(object sender, SelectedIndexChangedEventArgs e) {
            var maxLen = Console.WindowWidth - 30;
            var tool = tools[e.Index];

            if (maxLen < 0) {
                // Stop resizing your damn windows.
                Environment.Exit(0);
            }

            Drawing.ClearRegion(28, 7, maxLen, 3);

            var usesToken = (tool.RequiresToken ? "" : "no ") +
                "token required";
            var name = Drawing.TrimStr($"{tool.Name} ({usesToken})", maxLen);
            var desc = Drawing.TrimStr(tool.Description, maxLen);

            Console.SetCursorPosition(28, 7);
            Console.Write(name);

            Console.SetCursorPosition(28, 9);
            Console.Write(desc);
        }

        static async Task runTool(ITool tool) {
            var maxLen = Console.WindowWidth - 30;

            if (maxLen < 0) {
                // Stop resizing your damn windows.
                Environment.Exit(0);
            }

            string[] results = new string[tool.ParameterNames.Length];

            for (int i = 0; i < tool.ParameterNames.Length; i++) {
                var param = tool.ParameterNames[i];

                Console.SetCursorPosition(28, 11 + i);
                Console.Write($"{param}: ");
                results[i] = Drawing.Input(maxLen - param.Length - 2);
            }

            Console.SetCursorPosition(28, 13 + tool.ParameterNames.Length);
            Console.WriteLine("Result: ");

            Console.SetCursorPosition(28, 14 + tool.ParameterNames.Length);
            var data = await tool.GetData(holder, history, results);
            data = Drawing.TrimStr(data, maxLen);
            Console.WriteLine(data);
        }
    }
}
