using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace DiscordTools {
    static class Resources {        
        public static string Information { get; }

        static Resources() {
            var assembly = Assembly.GetExecutingAssembly();

            Information = getResourceStr(assembly,
                "DiscordTools.Resources.Information.txt");
        }

        private static string getResourceStr(Assembly a, string name) {
            using (var stream = a.GetManifestResourceStream(name))
            using (var reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }
    }
}
