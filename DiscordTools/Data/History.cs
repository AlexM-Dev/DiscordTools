using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordTools.Data {
    class History {
        public List<HistorySource> Actions { get; set; }
    }
    struct HistorySource {
        public DateTime Timestamp { get; set; }
        public string ToolName { get; set; }
        public string Action { get; set; }
    }
}
