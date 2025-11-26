using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Melanchall.DryWetMidi.Core;
using InputManager;
using Timer = System.Threading.Timer;

namespace LyreBot
{
    public class ActionManager
    {
        public const uint WM_KEYDOWN = 0x0100;
        public const uint WM_KEYUP = 0x0101;
        public const uint WM_SETTEXT = 0x000C;

        private static Dictionary<int, Keys> lyreNotes = new Dictionary<int, Keys>
        {
            { 48, Keys.Z },
            { 49, Keys.None },
            { 50, Keys.X },
            { 51, Keys.None },
            { 52, Keys.C },
            { 53, Keys.V },
            { 54, Keys.None },
            { 55, Keys.B },
            { 56, Keys.None },
            { 57, Keys.N },
            { 58, Keys.None },
            { 59, Keys.M },
            { 60, Keys.A },
            { 61, Keys.None },
            { 62, Keys.S },
            { 63, Keys.None },
            { 64, Keys.D },
            { 65, Keys.F },
            { 66, Keys.None },
            { 67, Keys.G },
            { 68, Keys.None },
            { 69, Keys.H },
            { 70, Keys.None },
            { 71, Keys.J },
            { 72, Keys.Q },
            { 73, Keys.None },
            { 74, Keys.W },
            { 75, Keys.None },
            { 76, Keys.E },
            { 77, Keys.R },
            { 78, Keys.None },
            { 79, Keys.T },
            { 80, Keys.None },
            { 81, Keys.Y },
            { 82, Keys.None },
            { 83, Keys.U },
        };

        public static int activeScale = 0;

        public static bool PlayNote(NoteOnEvent note, bool enableVibrato, bool transposeNotes)
        {
            var noteId = (int)note.NoteNumber;
            if (!lyreNotes.ContainsKey(noteId))
            {
                if (transposeNotes)
                {
                    if (noteId < lyreNotes.Keys.First())
                    {
                        noteId = lyreNotes.Keys.First() + noteId % 12;
                    }
                    else if (noteId > lyreNotes.Keys.Last())
                    {
                        noteId = lyreNotes.Keys.Last() - 15 + noteId % 12;
                    }
                }
                else
                {
                    return false;
                }
            }

            PlayNote(noteId, enableVibrato, transposeNotes);
            return true;
        }

        public static void PlayNote(int noteId, bool enableVibrato, bool transposeNotes)
        {
            var lyreNote = lyreNotes[noteId];
            KeyTap(lyreNote);
        }

        public static void KeyTap(Keys key)
        {
            Keyboard.KeyDown(key);
            Keyboard.KeyUp(key);
        }

        public static void KeyHold(Keys key, TimeSpan time)
        {
            Keyboard.KeyDown(key);
            new Timer(state => Keyboard.KeyUp(key), null, time, Timeout.InfiniteTimeSpan);
        }

        public static bool OnSongPlay()
        {
            return true;
        }

        public static bool IsWindowFocused(IntPtr windowPtr)
        {
            return true;
        }

        public static bool IsWindowFocused(string windowName)
        {
            return true;
        }
    }
}
