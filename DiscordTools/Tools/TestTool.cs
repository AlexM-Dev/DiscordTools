using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordTools.Tools {
    class TestTool : ITool {
        public string Name => "Hello";

        public string Description => "Alex's testing tool";

        public string[] ParameterNames => new[] { "Param 1", "Param 2" };

        public string ReturnName => "Message";

        public bool RequiresToken => false;

        public async Task<string> GetData(params object[] args) {
            return "Hello, world";
        }
    }
}
