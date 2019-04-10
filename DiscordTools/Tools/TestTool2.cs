using DiscordTools.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordTools.Tools {
    class TestTool2 : ITool {
        public string Name => "Hello number 2";

        public string Description => "Alex's testing tool that I think should be extended to have a massively long redundant description that no one cares about yep. Sorry about that.";

        public string[] ParameterNames => new[] { "Param 1", "Param 2" };

        public string ReturnName => "Message";

        public bool RequiresToken => true;

        public async Task<string> GetData(TokenHolder token, 
            History hist, params object[] args) {
            return "Hello, world";
        }
    }
}
