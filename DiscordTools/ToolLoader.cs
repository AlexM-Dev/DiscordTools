using DiscordTools.Data;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Text.RegularExpressions;

namespace DiscordTools.Tools {
    class ToolLoader {
        public TokenHolder Token { get; set; }

        public List<ITool> Tools { get; }

        public ToolLoader() {
            this.Token = new TokenHolder();
            this.Tools = fetchTools();
        }

        private List<ITool> fetchTools() {
            var assembly = Assembly.GetExecutingAssembly();
            var tools = new List<ITool>();

            foreach (var t in assembly.GetTypes()) {
                if (!t.IsInterface && typeof(ITool).IsAssignableFrom(t)) {
                    var tool = (ITool)Activator.CreateInstance(t);
                    tools.Add(tool);
                }
            }

            return tools;
        }

        public async Task<string> GetData(ITool tool, params object[] args) {
            return await tool.GetData(this.Token, args);
        }

        public async Task<string[]> GetData(string pattern, params object[] args) {
            return await Task.WhenAll(Tools
                .Where(t => Regex.IsMatch(t.Name, pattern))
                .Select(async t => await t.GetData(this.Token, args)));
        }
    }
}
