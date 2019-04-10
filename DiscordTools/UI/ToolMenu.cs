using DiscordTools.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordTools.UI {
    class ToolMenu {
        public int PageMax { get; set; } = 5;
        public List<ITool> Tools { get; set; }

        private readonly object _lock = new object();

        public event EventHandler<SelectedIndexChangedEventArgs>
            SelectedIndexChanged = (o, e) => { };

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
                SelectedIndexChanged(this,
                    new SelectedIndexChangedEventArgs(index));
                var bounds = getBounds(index);

                // User input.
                ConsoleKey curKey;

                do {
                    var page = index / PageMax + 1;
                    var max = (Tools.Count - 1) / PageMax + 1;
                    Console.SetCursorPosition(x, y);
                    Console.WriteLine($"Page {page} / {max}\t");
                    // Render.
                    for (int i = bounds.lower; i <= bounds.upper; i++) {
                        Console.SetCursorPosition(x, y + 3 * (i - bounds.lower) + 1);
                        if (index == i) {
                            Console.BackgroundColor = fg;
                            Console.ForegroundColor = bg;
                        }

                        var trim = Drawing.TrimStr(Tools[i].Name, 20);
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
                                if (index < bounds.upper) {
                                    index++;
                                    SelectedIndexChanged(this,
                                        new SelectedIndexChangedEventArgs(index));
                                }
                                break;
                            }
                        case ConsoleKey.UpArrow: {
                                if (index > bounds.lower) {
                                    index--;
                                    SelectedIndexChanged(this,
                                        new SelectedIndexChangedEventArgs(index));
                                }
                                break;
                            }
                        case ConsoleKey.RightArrow: {
                                var newBound = getBounds(bounds.upper + 1);
                                if (newBound.lower <= newBound.upper) {
                                    if (newBound.upper - newBound.lower <
                                        bounds.upper - bounds.lower) {
                                        Drawing.ClearRegion(x, y, 24, 3 *
                                            (bounds.upper - bounds.lower + 1));
                                    }
                                    bounds = newBound;
                                    index = bounds.lower;
                                    SelectedIndexChanged(this,
                                        new SelectedIndexChangedEventArgs(index));
                                }
                                break;
                            }
                        case ConsoleKey.LeftArrow: {
                                var newBound = getBounds(bounds.lower - 1);
                                if (bounds.lower - 1 >= 0) {
                                    bounds = newBound;
                                    index = bounds.lower;
                                    SelectedIndexChanged(this,
                                        new SelectedIndexChangedEventArgs(index));
                                }
                                break;
                            }
                    }
                } while (curKey != ConsoleKey.Enter);

                Drawing.ClearRegion(x, y, 24, 3 * (bounds.upper + 1) + 1);
                return index;
            }
        }

        private (int lower, int upper) getBounds(int index) {
            int lower = (int)(Math.Floor((double)index / PageMax) * PageMax);
            int upper = lower + PageMax - 1;
            upper = upper > Tools.Count - 1 ? Tools.Count - 1 : upper;

            return (lower, upper);
        }
    }
}
