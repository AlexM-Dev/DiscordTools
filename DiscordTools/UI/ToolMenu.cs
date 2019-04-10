using DiscordTools.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordTools.UI {
    class ToolMenu {
        public int PageMax { get; set; } = 5;
        public List<ITool> Tools { get; set; }
        private readonly object _lock = new object();
        public ToolMenu(List<ITool> tools) {
            this.Tools = tools;
        }

        public int RunMenu(int selectedIndex) {
            lock (_lock) {
                // Formatting
                int x = Console.CursorLeft,
                    y = Console.CursorTop;

                ConsoleColor bg = Console.BackgroundColor,
                             fg = Console.ForegroundColor;

                string buff = "  ",
                       line = new string(' ', 24);

                // Pagination & index tracking.
                int index = selectedIndex;
                var bounds = getBounds(index);

                // User input.
                ConsoleKey curKey;

                do {
                    var page = index / PageMax + 1;
                    var max = (Tools.Count - 1) / PageMax + 1;
                    Console.SetCursorPosition(x, y);
                    Console.WriteLine($"Page {page} / {max}\t");
                    // Render.
                    for (int i = bounds.Item1; i <= bounds.Item2; i++) {
                        Console.SetCursorPosition(x, y + 3 * (i - bounds.Item1) + 1);
                        if (index == i) {
                            Console.BackgroundColor = fg;
                            Console.ForegroundColor = bg;
                        }
                        var trim = trimStr(Tools[i].Name, 20);
                        Console.WriteLine(line);
                        Console.CursorLeft = x;
                        Console.WriteLine($"{buff}{trim}{buff}");
                        Console.CursorLeft = x;
                        Console.WriteLine(line);
                        Console.CursorLeft = x;
                        Console.BackgroundColor = bg;
                        Console.ForegroundColor = fg;
                    }
                    // User input.
                    curKey = Console.ReadKey(true).Key;

                    // Input processing.
                    switch (curKey) {
                        case ConsoleKey.DownArrow: {
                                if (index < bounds.Item2) {
                                    index++;
                                }
                                break;
                            }
                        case ConsoleKey.UpArrow: {
                                if (index > bounds.Item1) {
                                    index--;
                                }
                                break;
                            }
                        case ConsoleKey.RightArrow: {
                                var newBound = getBounds(bounds.Item2 + 1);
                                if (newBound.Item1 <= newBound.Item2) {
                                    if (newBound.Item2 - newBound.Item1 <
                                        bounds.Item2 - bounds.Item1) {
                                        clearRegion(x, y, 24, 3 *
                                            (bounds.Item2 - bounds.Item1 + 1));
                                    }
                                    bounds = newBound;
                                    index = bounds.Item1;
                                }
                                break;
                            }
                        case ConsoleKey.LeftArrow: {
                                var newBound = getBounds(bounds.Item1 - 1);
                                if (bounds.Item1 - 1 >= 0) {
                                    bounds = newBound;
                                    index = bounds.Item1;
                                }
                                break;
                            }
                    }
                } while (curKey != ConsoleKey.Enter);

                return 0;
            }
        }

        private void clearRegion(int x, int y, int w, int h) {
            int ox = Console.CursorLeft,
                oy = Console.CursorTop;

            for (int iy = 0; iy <= h; iy++) {
                Console.SetCursorPosition(x, y + iy);
                Console.Write(new string(' ', w));
            }

            Console.SetCursorPosition(ox, oy);
        }

        private (int, int) getBounds(int index) {
            int lower = (int)(Math.Floor((double)index / PageMax) * PageMax);
            int upper = lower + PageMax - 1;
            upper = upper > Tools.Count - 1 ? Tools.Count - 1 : upper;

            return (lower, upper);
        }

        private string trimStr(string str, int len) {
            if (str.Length <= len)
                return str + new string(' ', len - str.Length);

            var trim = str.Substring(0, len - 3);
            trim += "...";

            return trim;
        }
    }
}
