using DiscordTools.Tools;
using DiscordTools.UI;
using System;
using System.Collections.Generic;

namespace DiscordTools {
    class Program {
        static void Main(string[] args) {
            Console.SetCursorPosition(1, 1);
            Console.WriteLine(Resources.Information);
            Console.SetCursorPosition(1, 5);
            Console.CursorVisible = false;

            var menu = new ToolMenu(ToolLoader.GetTools());
            menu.RunMenu(0);
        }
    }
}
