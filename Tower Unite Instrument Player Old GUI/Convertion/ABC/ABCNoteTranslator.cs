using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABC_Utility;
using Tower_Unite_Instrument_Autoplayer.Convertion;

namespace Tower_Unite_Instrument_Autoplayer.ABC
{
    public static class ABCNoteTranslator
    {
        static Dictionary<string, char> noteToVirtualDictionary = new Dictionary<string, char>()
        {
            ["A,,"] = '6',
            ["B,,"] = '7',
            ["C,,"] = '1',
            ["D,,"] = '2',
            ["E,,"] = '3',
            ["F,,"] = '4',
            ["G,,"] = '5',
            ["A,"] = 'e',
            ["B,"] = 'r',
            ["C,"] = '8',
            ["D,"] = '9',
            ["E,"] = '0',
            ["F,"] = 'q',
            ["G,"] = 'w',
            ["A"] = 'p',
            ["B"] = 'a',
            ["C"] = 't',
            ["D"] = 'y',
            ["E"] = 'u',
            ["F"] = 'i',
            ["G"] = 'o',
            ["a"] = 'j',
            ["b"] = 'k',
            ["c"] = 's',
            ["d"] = 'd',
            ["e"] = 'f',
            ["f"] = 'g',
            ["g"] = 'h',
            ["a'"] = 'b',
            ["B'"] = 'n',
            ["c'"] = 'l',
            ["d'"] = 'z',
            ["e'"] = 'x',
            ["f'"] = 'c',
            ["g'"] = 'v',
            ["c''"] = 'm'
        };

        static char[] commonDelayCharacters = { '-', '_', ',', ';', '.', ':', '\'', '*', '¨', '^', '`', '´', '+', '?', '<', '>', '@', '£', '€', '$' };

        public static ConvertedObject TranslateNotes(ABCObject abcObject)
        {
            ConvertedObject virtualObject = new ConvertedObject();

            StringBuilder sb = new StringBuilder();
            virtualObject.Speed = (int)(MathExtension.FractionToDouble(abcObject.NoteLength) * 1000);

            foreach (string line in abcObject.Notes)
            {
                char[] noteBits = line.ToCharArray();
                string number = "";
                bool isModifier = false;
                bool isBreak = false;
                char curNote = '\0';


                foreach (char bit in noteBits)
                {
                    if (isModifier)
                    {
                        if (char.IsNumber(bit))
                        {
                            int num;
                            if (int.TryParse(bit.ToString(), out num))
                            {
                                number += num;
                            }
                        }
                        else
                        {
                            sb = AddNote(virtualObject, sb, number, curNote, true);
                            number = "";
                            isModifier = false;
                            isBreak = false;
                            curNote = '\0';
                        }
                    }



                    if (curNote != '\0' || isBreak)
                    {
                        int num;
                        if (int.TryParse(bit.ToString(), out num))
                        {
                            number += num;
                            continue;
                        }
                        else if (bit == '/')
                        {
                            isModifier = true;
                            continue;
                        }

                        if (isBreak)
                        {
                            sb = AddBreak(virtualObject, sb, number);
                        }
                        else
                        {
                            sb = AddNote(virtualObject, sb, number, curNote, false);
                        }

                        number = "";
                        isModifier = false;
                        isBreak = false;
                        curNote = '\0';
                    }
                    else if (bit == 'z')
                    {
                        isBreak = true;
                    }
                    else
                    {
                        noteToVirtualDictionary.TryGetValue(bit.ToString(), out curNote);

                        if (curNote != '\0')
                        {
                            char modifier = GetModifier(abcObject.Key, char.ToUpper(bit).ToString());
                            if (modifier != '\0')
                            {
                                curNote = GetUpper(curNote);
                            }
                        }
                    }
                }
            }
            virtualObject.Notes = sb.ToString();
            return virtualObject;
        }

        private static StringBuilder AddBreak(ConvertedObject obj, StringBuilder sb, string number)
        {
            if (number == "")
            {
                number = "1";
            }

            int actualNumber;
            if (int.TryParse(number, out actualNumber))
            {
                actualNumber *= obj.Speed;
                if (!obj.Breaks.ContainsKey(actualNumber))
                {
                    foreach (char character in commonDelayCharacters)
                    {
                        if (!obj.NoteModifiers.ContainsValue(character) && !obj.Breaks.ContainsValue(character))
                        {
                            obj.Breaks.Add(actualNumber, character);
                            sb.Append(character.ToString());
                            break;
                        }
                    }
                }
                else
                {
                    sb.Append(obj.Breaks[actualNumber].ToString());
                }
            }
            return sb;
        }

        private static StringBuilder AddNote(ConvertedObject obj, StringBuilder sb, string number, char note, bool modifier)
        {
            if (number == "")
            {
                if (modifier)
                {
                    number = "2";
                }
                else
                {
                    number = "1";
                }
            }

            int actualNumber;
            if (int.TryParse(number, out actualNumber))
            {
                int speed = modifier ? obj.Speed / actualNumber : obj.Speed * actualNumber;
                if (!obj.NoteModifiers.ContainsKey(speed))
                {
                    foreach (char character in commonDelayCharacters)
                    {
                        if (!obj.NoteModifiers.ContainsValue(character) && !obj.Breaks.ContainsValue(character))
                        {
                            obj.NoteModifiers.Add(speed, character);
                            sb.Append(note + character.ToString());
                            break;
                        }
                    }
                }
                else
                {
                    sb.Append(note + obj.NoteModifiers[speed].ToString());
                }
            }
            return sb;
        }

        private static char GetModifier(string key, string note)
        {
            switch (key)
            {
                case "A":
                    if (note == "C" || note == "F" || note == "G")
                    {
                        return '^';
                    }
                    break;

                case "B":
                    if (note == "C" || note == "D" || note == "F" || note == "G" || note == "A")
                    {
                        return '^';
                    }
                    break;

                case "D":
                    if (note == "F" || note == "C")
                    {
                        return '^';
                    }
                    break;

                case "E":
                    if (note == "F" || note == "G" || note == "C" || note == "D")
                    {
                        return '^';
                    }
                    break;

                case "F":
                    if (note == "B")
                    {
                        return '_';
                    }
                    break;

                case "G":
                    if (note == "F")
                    {
                        return '^';
                    }
                    break;

                case "Bm":
                    if (note == "C" || note == "F")
                    {
                        return '^';
                    }
                    break;

                case "Cm":
                    if (note == "E" || note == "A" || note == "B")
                    {
                        return '_';
                    }
                    break;

                case "Dm":
                    if (note == "B")
                    {
                        return '_';
                    }
                    break;

                case "Em":
                    if (note == "F")
                    {
                        return '^';
                    }
                    break;

                case "Fm":
                    if (note == "A" || note == "B" || note == "D" || note == "E")
                    {
                        return '_';
                    }
                    break;

                case "Gm":
                    if (note == "B" || note == "E")
                    {
                        return '_';
                    }
                    break;
                default:
                    break;
            }
            return '\0';
        }

        private static char GetUpper(char character)
        {
            if (char.IsLetter(character))
            {
                return char.ToUpper(character);
            }
            else if (char.IsDigit(character))
            {
                Dictionary<int, char> digitDict = new Dictionary<int, char>()
                {
                    [0] = '=',
                    [1] = '!',
                    [2] = '"',
                    [3] = '#',
                    [4] = '¤',
                    [5] = '%',
                    [6] = '&',
                    [7] = '/',
                    [8] = '(',
                    [9] = ')'
                };
                char realChar;
                digitDict.TryGetValue(int.Parse(character.ToString()), out realChar);
                return realChar;
            }
            throw new FormatException($"{character} is neither a letter or a digit!");
        }
    }
}
