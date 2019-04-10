using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordTools.Tools {
    interface ITool {
        string Name { get; }
        string Description { get; }
        string[] ParameterNames { get; }
        string ReturnName { get; }
        bool RequiresToken { get; }
        Task<string> GetData(params object[] args);
    }
}
