using DiscordTools.Data;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

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
    }
}
