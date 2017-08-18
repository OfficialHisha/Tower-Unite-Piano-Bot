using System.Collections.Generic;
using System.IO;

namespace ABC_Utility
{
    public static class ABCTool
    {
        /// <summary>
        /// This method will take an ABC file and convert it to an ABC object
        /// </summary>
        public static ABCObject ImportABC(string fileLocation)
        {
            StreamReader sr = new StreamReader(fileLocation);

            string xField = "NULL";
            string title = "NULL";
            string composer = "NULL";
            string meter = "NULL";
            string key = "NULL";
            string noteLength = "NULL";
            List<string> notes = new List<string>();

            string currentLine = sr.ReadLine();

            while (currentLine != null)
            {
                if (currentLine == "" || currentLine.Contains("%"))
                {
                    currentLine = sr.ReadLine();
                    continue;
                }

                if (key != "NULL")
                {
                    notes.Add(currentLine);
                }

                if (xField == "NULL" && currentLine.Contains("X:"))
                {
                    xField = currentLine.Substring(2);
                    //Get rid of excess space in case there is one (applies for the other cases as well)
                    xField = xField.StartsWith(" ") ? xField.Substring(1) : xField;
                }

                if (title == "NULL" && currentLine.Contains("T:"))
                {
                    title = currentLine.Substring(2);
                    title = title.StartsWith(" ") ? title.Substring(1) : title;
                }
                
                if (composer == "NULL" && currentLine.Contains("C:"))
                {
                    composer = currentLine.Substring(2);
                    composer = composer.StartsWith(" ") ? composer.Substring(1) : composer;
                }
                if (meter == "NULL" && currentLine.Contains("M:"))
                {
                    meter = currentLine.Substring(2);
                    meter = meter.StartsWith(" ") ? meter.Substring(1) : meter;
                }
                if (key == "NULL" && currentLine.Contains("K:"))
                {
                    key = currentLine.Substring(2);
                    key = key.StartsWith(" ") ? key.Substring(1) : key;
                }
                if (noteLength == "NULL" && currentLine.Contains("L:"))
                {
                    noteLength = currentLine.Substring(2);
                    noteLength = noteLength.StartsWith(" ") ? noteLength.Substring(1) : noteLength;
                }
                currentLine = sr.ReadLine();
            }
            sr.Close();
            return new ABCObject(xField, title, composer, meter, key, noteLength, notes.ToArray());
        }

        /// <summary>
        /// This takes an ABC object and save it to an ABC file to the given file location
        /// </summary>
        public static void ExportABC(string fileLocation, ABCObject abcToExport)
        {
            StreamWriter sw = new StreamWriter(fileLocation);

            sw.WriteLine($"X:{abcToExport.XField}");
            sw.WriteLine($"T:{abcToExport.Title}");
            sw.WriteLine($"C:{abcToExport.Composer}");
            sw.WriteLine($"M:{abcToExport.Meter}");
            sw.WriteLine($"L:{abcToExport.NoteLength}");
            sw.WriteLine($"K:{abcToExport.Key}");
            foreach (string line in abcToExport.Notes)
            {
                sw.WriteLine(line);
            }

            sw.Dispose();
            sw.Close();
        }
    }
}
