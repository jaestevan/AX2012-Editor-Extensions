using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JAEE.AX.EditorExtensions
{
    [Serializable()]
    public class JAEEOutliningSettings
    {
        public int MaxRowsInTooltip { get; set; }

        public JAEEOutliningSettings()
        {
            this.MaxRowsInTooltip = 10;
        }
    }
}
