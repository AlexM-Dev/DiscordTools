using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordTools.UI {
    class SelectedIndexChangedEventArgs : EventArgs {
        public int Index { get; set; }
        public SelectedIndexChangedEventArgs(int index) {
            this.Index = index;
        }
    }
}
