using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DiscordTools.UI {
    class Drawing {
        public static string TrimStr(string str, int len) {
            if (str.Length <= len)
                return str + new string(' ', len - str.Length);

            var trim = str.Substring(0, len - 3);
            trim += "...";

            return trim;
        }
        public static void ClearRegion(int x, int y, int w, int h) {
            int ox = Console.CursorLeft,
                oy = Console.CursorTop;

            for (int iy = 0; iy <= h; iy++) {
                Console.SetCursorPosition(x, y + iy);
                Console.Write(new string(' ', w));
            }

            Console.SetCursorPosition(ox, oy);
        }

        public static string Input(int max) {
            var builder = new StringBuilder();

            ConsoleKeyInfo keyInfo;
            do {
                keyInfo = Console.ReadKey(true);

                var write = false;
                switch (keyInfo.Key) {
                    case ConsoleKey.Backspace:
                        if (builder.Length > 0) {
                            Console.CursorLeft--;
                            Console.Write(' ');
                            Console.CursorLeft --;
                            builder.Remove(builder.Length - 1, 1);
                        }
                        break;
                    default:
                        write = true;
                        break;
                }

                if (write && builder.Length < max) {
                    Console.Write(keyInfo.KeyChar);
                    builder.Append(keyInfo.KeyChar);
                }
            } while (keyInfo.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return builder.ToString();
        }
    }
}
