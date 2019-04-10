using DiscordTools.Data;
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
        Task<string> GetData(TokenHolder token,
            History hist, params object[] args);
    }
}
