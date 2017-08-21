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
        static Dictionary<Tuple<string, string>, char> keyToNoteDictionary = new Dictionary<Tuple<string, string>, char>();
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
            #region Special cases
                keyToNoteDictionary.Add(new Tuple<string, string>("A", "C"), '^');
                keyToNoteDictionary.Add(new Tuple<string, string>("A", "F"), '^');
                keyToNoteDictionary.Add(new Tuple<string, string>("A", "G"), '^');
                keyToNoteDictionary.Add(new Tuple<string, string>("B", "C"), '^');
                keyToNoteDictionary.Add(new Tuple<string, string>("B", "D"), '^');
                keyToNoteDictionary.Add(new Tuple<string, string>("B", "F"), '^');
                keyToNoteDictionary.Add(new Tuple<string, string>("B", "G"), '^');
                keyToNoteDictionary.Add(new Tuple<string, string>("B", "A"), '^');
                keyToNoteDictionary.Add(new Tuple<string, string>("D", "F"), '^');
                keyToNoteDictionary.Add(new Tuple<string, string>("D", "C"), '^');
                keyToNoteDictionary.Add(new Tuple<string, string>("E", "F"), '^');
                keyToNoteDictionary.Add(new Tuple<string, string>("E", "G"), '^');
                keyToNoteDictionary.Add(new Tuple<string, string>("E", "C"), '^');
                keyToNoteDictionary.Add(new Tuple<string, string>("E", "D"), '^');
                keyToNoteDictionary.Add(new Tuple<string, string>("F", "B"), '_');
                keyToNoteDictionary.Add(new Tuple<string, string>("G", "F"), '^');
                keyToNoteDictionary.Add(new Tuple<string, string>("Bm", "C"), '^');
                keyToNoteDictionary.Add(new Tuple<string, string>("Bm", "F"), '^');
                keyToNoteDictionary.Add(new Tuple<string, string>("Cm", "E"), '_');
                keyToNoteDictionary.Add(new Tuple<string, string>("Cm", "A"), '_');
                keyToNoteDictionary.Add(new Tuple<string, string>("Cm", "B"), '_');
                keyToNoteDictionary.Add(new Tuple<string, string>("Dm", "B"), '_');
                keyToNoteDictionary.Add(new Tuple<string, string>("Em", "F"), '^');
                keyToNoteDictionary.Add(new Tuple<string, string>("Fm", "A"), '_');
                keyToNoteDictionary.Add(new Tuple<string, string>("Fm", "B"), '_');
                keyToNoteDictionary.Add(new Tuple<string, string>("Fm", "D"), '_');
                keyToNoteDictionary.Add(new Tuple<string, string>("Fm", "E"), '_');
                keyToNoteDictionary.Add(new Tuple<string, string>("Gm", "B"), '_');
                keyToNoteDictionary.Add(new Tuple<string, string>("Gm", "E"), '_');
            #endregion

            ConvertedObject virtualObject = new ConvertedObject();

            StringBuilder sb = new StringBuilder();
            virtualObject.Speed = (int)(MathExtension.FractionToDouble(abcObject.NoteLength) * 1000);
            
            foreach (string line in abcObject.Notes)
            {
                string[] notes = line.Split(' ');
                
                foreach (string note in notes)
                {
                    char val, modifier = '\0';

                    keyToNoteDictionary.TryGetValue(new Tuple<string, string>(abcObject.Key, note.ToUpper()), out modifier);

                    if (noteToVirtualDictionary.TryGetValue(note, out val))
                    {
                        if (modifier != '\0')
                        {
                            sb.Append(GetUpper(val));
                        }
                        else
                        {
                            sb.Append(val);
                        }
                    }
                    else
                    {
                        char[] noteBits = note.ToCharArray();
                        string number = "";
                        bool isModifier = false;
                        bool isBreak = false;

                        char curNote = '\0';
                        foreach (char bit in noteBits)
                        {
                            if (curNote != '\0')
                            {
                                int num;
                                if (int.TryParse(bit.ToString(), out num))
                                {
                                    number += num;
                                }
                                else if (bit == '/')
                                {
                                    isModifier = true;
                                }
                                else if (bit == 'z')
                                {
                                    isBreak = true;
                                }
                            }
                            else
                            {
                                noteToVirtualDictionary.TryGetValue(bit.ToString(), out curNote);

                                if (curNote != '\0')
                                {
                                    modifier = '\0';
                                    keyToNoteDictionary.TryGetValue(new Tuple<string, string>(abcObject.Key, char.ToUpper(bit).ToString()), out modifier);
                                    if (modifier != '\0')
                                    {
                                        curNote = GetUpper(curNote);
                                    }
                                }
                            }
                        }

                        if (number == "")
                        {
                            if (isBreak)
                            {
                                number = "1";
                            }
                            else
                            {
                                number = "2";
                            }
                        }

                        if (curNote != '\0')
                        {
                            int actualNumber;
                            if (int.TryParse(number, out actualNumber))
                            {
                                int speed = isModifier ? virtualObject.Speed / actualNumber : virtualObject.Speed * (actualNumber - 1);
                                if (!virtualObject.NoteModifiers.ContainsKey(speed))
                                {
                                    foreach (char character in commonDelayCharacters)
                                    {
                                        if (!virtualObject.NoteModifiers.ContainsValue(character) && !virtualObject.Breaks.ContainsKey(character))
                                        {
                                            virtualObject.NoteModifiers.Add(speed, character);
                                            sb.Append(curNote + character.ToString());
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    sb.Append(curNote + virtualObject.NoteModifiers[speed]);
                                }
                            }
                        }
                        else
                        {
                            if (isBreak)
                            {
                                int actualNumber;
                                if (int.TryParse(number, out actualNumber))
                                {
                                    actualNumber *= virtualObject.Speed;
                                    if (!virtualObject.NoteModifiers.ContainsKey(actualNumber))
                                    {
                                        foreach (char character in commonDelayCharacters)
                                        {
                                            if (!virtualObject.NoteModifiers.ContainsValue(character) && !virtualObject.Breaks.ContainsKey(character))
                                            {
                                                virtualObject.Breaks.Add(actualNumber, character);
                                                sb.Append(character.ToString());
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        sb.Append(virtualObject.Breaks[actualNumber]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            virtualObject.Notes = sb.ToString();
            return virtualObject;
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
