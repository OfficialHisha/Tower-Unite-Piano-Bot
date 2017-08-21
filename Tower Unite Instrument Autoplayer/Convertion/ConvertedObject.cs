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
        public List<Tuple<char, int>> Delays { get; set; } = new List<Tuple<char, int>>();
        public int Speed { get; set; }
    }
}
