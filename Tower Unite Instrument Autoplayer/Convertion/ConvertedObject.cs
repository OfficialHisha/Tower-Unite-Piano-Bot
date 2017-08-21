using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tower_Unite_Instrument_Autoplayer.Core;

namespace Tower_Unite_Instrument_Autoplayer.Convertion
{
    public class ConvertedObject
    {
        public string Notes { get; set; }
        public Dictionary<int, char> NoteModifiers { get; set; } = new Dictionary<int, char>();
        public Dictionary<int, char> Breaks { get; set; } = new Dictionary<int, char>();
        public int Speed { get; set; }
    }
}
