using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC_Utility
{
    public class ABCObject
    {
        public string XField { get; private set; }
        public string Title { get; private set; }
        public string Composer { get; private set; }
        public string Meter { get; private set; }
        public string Key { get; private set; }
        public string NoteLength { get; private set; }
        public string[] Notes { get; private set; }

        //TEMP MUST BE REMOVED!
        public ABCObject()
        {

        }
        
        public ABCObject(string xField, string title, string composer, string meter, string key, string noteLength, string[] notes)
        {
            XField = xField;
            Title = title;
            Composer = composer;
            Meter = meter;
            Key = key;
            NoteLength = noteLength;
            Notes = notes;
        }
    }
}
