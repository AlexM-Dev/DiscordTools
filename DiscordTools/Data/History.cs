using DiscordTools.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DiscordTools.Data {
    class History : IDisposable {
        public string Path { get; set; }
        private StreamWriter writer;

        public History(string path) {
            this.Path = path;

            this.writer = new StreamWriter(path);
        }

        public void WriteHistory(Source src) {
            writer.WriteLine(src.ToString());
        }

        public void Dispose() {
            writer.Dispose();
        }
    }

    struct Source {
        public DateTime Timestamp { get; set; }
        public string ToolName { get; set; }
        public string Action { get; set; }

        public Source(ITool tool, string action) {
            this.Timestamp = DateTime.Now;
            this.ToolName = tool.Name;
            this.Action = action;
        }

        public override string ToString() =>
            $"{Timestamp.ToString("G")}: {ToolName}, {Action}";
    }
}
