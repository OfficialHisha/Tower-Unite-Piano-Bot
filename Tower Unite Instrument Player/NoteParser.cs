using System;
using System.Collections.Generic;
using Tower_Unite_Instrument_Player.Exceptions;
using Tower_Unite_Instrument_Player.Notes;
using WindowsInput.Native;

namespace Tower_Unite_Instrument_Player
{
    public class NoteParser
    {
        /// <summary>
        /// If this is set to true, some exceptions are skipped rather than thrown
        /// Due to some checks making some legacy files unable to load this can be set
        /// to allow those to be loaded.
        /// Notes that would result in an error are skipped instead.
        /// </summary>
        public bool AllowUnsafe { get; set; } = false;

        readonly Dictionary<char, VirtualKeyCode> r_virtualDictionary = new Dictionary<char, VirtualKeyCode>()
        {
            #region Characters
            ['A'] = VirtualKeyCode.VK_A,
            ['B'] = VirtualKeyCode.VK_B,
            ['C'] = VirtualKeyCode.VK_C,
            ['D'] = VirtualKeyCode.VK_D,
            ['E'] = VirtualKeyCode.VK_E,
            ['F'] = VirtualKeyCode.VK_F,
            ['G'] = VirtualKeyCode.VK_G,
            ['H'] = VirtualKeyCode.VK_H,
            ['I'] = VirtualKeyCode.VK_I,
            ['J'] = VirtualKeyCode.VK_J,
            ['K'] = VirtualKeyCode.VK_K,
            ['L'] = VirtualKeyCode.VK_L,
            ['M'] = VirtualKeyCode.VK_M,
            ['N'] = VirtualKeyCode.VK_N,
            ['O'] = VirtualKeyCode.VK_O,
            ['P'] = VirtualKeyCode.VK_P,
            ['Q'] = VirtualKeyCode.VK_Q,
            ['R'] = VirtualKeyCode.VK_R,
            ['S'] = VirtualKeyCode.VK_S,
            ['T'] = VirtualKeyCode.VK_T,
            ['U'] = VirtualKeyCode.VK_U,
            ['V'] = VirtualKeyCode.VK_V,
            ['W'] = VirtualKeyCode.VK_W,
            ['X'] = VirtualKeyCode.VK_X,
            ['Y'] = VirtualKeyCode.VK_Y,
            ['Z'] = VirtualKeyCode.VK_Z,
            #endregion
            #region Numbers
            ['0'] = VirtualKeyCode.VK_0,
            ['1'] = VirtualKeyCode.VK_1,
            ['2'] = VirtualKeyCode.VK_2,
            ['3'] = VirtualKeyCode.VK_3,
            ['4'] = VirtualKeyCode.VK_4,
            ['5'] = VirtualKeyCode.VK_5,
            ['6'] = VirtualKeyCode.VK_6,
            ['7'] = VirtualKeyCode.VK_7,
            ['8'] = VirtualKeyCode.VK_8,
            ['9'] = VirtualKeyCode.VK_9,
            #endregion
            #region Symbols
            [' '] = VirtualKeyCode.SPACE,
            ['='] = VirtualKeyCode.VK_0,
            ['!'] = VirtualKeyCode.VK_1,
            ['"'] = VirtualKeyCode.VK_2,
            ['#'] = VirtualKeyCode.VK_3,
            ['¤'] = VirtualKeyCode.VK_4,
            ['%'] = VirtualKeyCode.VK_5,
            ['&'] = VirtualKeyCode.VK_6,
            ['/'] = VirtualKeyCode.VK_7,
            ['('] = VirtualKeyCode.VK_8,
            [')'] = VirtualKeyCode.VK_9
            #endregion
        };

        readonly List<char> r_alwaysHighNotes = new List<char>()
        {
            '=',
            '!',
            '"',
            '#',
            '¤',
            '%',
            '&',
            '/',
            '(',
            ')',
        };

        public INote[] ParseNotes(string noteString, Dictionary<char, int> breaks)
        {
            List<INote> notes = new List<INote>();
            int cursor = -1;
            int multinoteStartPos = -1;
            int fastnoteStartPos = -1;
            bool multinoteIsHighNote = false;
            List<Note> multinoteBuffer = new List<Note>();

            foreach (char note in noteString)
            {
                cursor++;

                if (breaks.TryGetValue(note, out int breakTime))// Break
                {
                    if (breakTime < 0)
                        throw new AutoplayerException($"Error at position '{cursor}': Delay value less than 0!");

                    notes.Add(new BreakNote(note, breakTime));
                    continue;
                }

                switch (note)
                {
                    case '[':// Multinote start
                        if (multinoteStartPos != -1)
                            throw new AutoplayerException($"Error at position '{cursor}': A multinote cannot be defined whithin a multinote definition! Outer multinote starts at position '{multinoteStartPos}'");

                        multinoteBuffer = new List<Note>();
                        multinoteStartPos = cursor;
                        break;
                    case ']':// Multinote end
                        if (multinoteStartPos == -1)
                            throw new AutoplayerException($"Error at position '{cursor}': Missing start of multinote!");

                        if (multinoteBuffer.Count == 0)
                            break;

                        notes.Add(new MultiNote(multinoteBuffer.ToArray(), multinoteIsHighNote));

                        multinoteStartPos = -1;
                        multinoteBuffer.Clear();
                        break;
                    case '{':// Fast section start
                        if (multinoteStartPos != -1)
                            throw new AutoplayerException($"Error at position '{cursor}': A fast speed section cannot be performed whithin a multinote definition! Multinote starts at position '{multinoteStartPos}'");
                        if (fastnoteStartPos != -1)
                            throw new AutoplayerException($"Error at position '{cursor}': A fast speed section cannot be defined within a fast speed section! Outer section starts at position '{fastnoteStartPos}'");

                        fastnoteStartPos = cursor;
                        break;
                    case '}':// Fast section end
                        if (multinoteStartPos != -1)
                            throw new AutoplayerException($"Error at position '{cursor}': A fast speed section cannot be performed whithin a multinote definition! Multinote starts at position '{multinoteStartPos}'");
                        if (fastnoteStartPos == -1)
                            throw new AutoplayerException($"Error at position '{cursor}': Missing start of fast speed section!");
                        
                        fastnoteStartPos = -1;
                        break;
                    case '\n':// Skip newline characters
                    case '\r':
                        break;
                    default:
                        VirtualKeyCode vk;
                        try
                        {
                            r_virtualDictionary.TryGetValue(char.ToUpper(note), out vk);

                            if (vk == 0)
                            {
                                break;
                            }
                        }
                        catch (ArgumentNullException)
                        {
                            break;
                        }

                        //This will check if the note is an uppercase letter, or if the note is in the list of high notes
                        bool isHighNote = char.IsUpper(note) || r_alwaysHighNotes.Contains(note);

                        if (multinoteStartPos != -1)
                        {
                            if (multinoteBuffer.Count == 0)
                                multinoteIsHighNote = char.IsUpper(note) || r_alwaysHighNotes.Contains(note);

                            if (isHighNote != multinoteIsHighNote)
                                if (AllowUnsafe)
                                    break;
                                else
                                    throw new AutoplayerNoteCreationFailedException($"Error at position '{cursor}': A multinote cannot contain both high and low keys!");

                            if (fastnoteStartPos != -1)
                                multinoteBuffer.Add(new Note(note, vk, isHighNote, Autoplayer.FastSpeed));
                            else
                                multinoteBuffer.Add(new Note(note, vk, isHighNote, Autoplayer.NormalSpeed));
                        }
                        else
                        {
                            if (fastnoteStartPos != -1)
                                notes.Add(new Note(note, vk, isHighNote, Autoplayer.FastSpeed));
                            else
                                notes.Add(new Note(note, vk, isHighNote, Autoplayer.NormalSpeed));
                        }
                        break;
                }
            }

            return notes.ToArray();
        }
    }
}
