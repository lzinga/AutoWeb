using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWeb
{
    /// <summary>
    /// Representations of keys able to be pressed that are not text keys for sending to the browser.
    /// </summary>
    /// <summary>
    /// Representations of keys able to be pressed that are not text keys for sending to the browser.
    /// </summary>
    public enum Key
    {
        /// <summary>
        /// Represents the NUL keystroke.
        /// </summary>
        Null = 0xE000,

        /// <summary>
        /// Represents the Cancel keystroke.
        /// </summary>
        Cancel = 0xE001,

        /// <summary>
        /// Represents the Help keystroke.
        /// </summary>
        Help = 0xE002,

        /// <summary>
        /// Represents the Backspace key.
        /// </summary>
        Backspace = 0xE003,

        /// <summary>
        /// Represents the Tab key.
        /// </summary>
        Tab = 0xE004,

        /// <summary>
        /// Represents the Clear keystroke.
        /// </summary>
        Clear = 0xE005,

        /// <summary>
        /// Represents the Return key.
        /// </summary>
        Return = 0xE006,

        /// <summary>
        /// Represents the Enter key.
        /// </summary>
        Enter = 0xE007,

        /// <summary>
        /// Represents the Shift key.
        /// </summary>
        Shift = 0xE008,

        /// <summary>
        /// Represents the Shift key.
        /// </summary>
        LeftShift = 0xE008,

        /// <summary>
        /// Represents the Control key.
        /// </summary>
        Control = 0xE009,

        /// <summary>
        /// Represents the Control key.
        /// </summary>
        LeftControl = 0xE009,

        /// <summary>
        /// Represents the Alt key.
        /// </summary>
        Alt = 0xE00A,

        /// <summary>
        /// Represents the Alt key.
        /// </summary>
        LeftAlt = 0xE00A,

        /// <summary>
        /// Represents the Pause key.
        /// </summary>
        Pause = 0xE00B,

        /// <summary>
        /// Represents the Escape key.
        /// </summary>
        Escape = 0xE00C,

        /// <summary>
        /// Represents the Spacebar key.
        /// </summary>
        Space = 0xE00D,

        /// <summary>
        /// Represents the Page Up key.
        /// </summary>
        PageUp = 0xE00E,

        /// <summary>
        /// Represents the Page Down key.
        /// </summary>
        PageDown = 0xE00F,

        /// <summary>
        /// Represents the End key.
        /// </summary>
        End = 0xE010,

        /// <summary>
        /// Represents the Home key.
        /// </summary>
        Home = 0xE011,

        /// <summary>
        /// Represents the left arrow key.
        /// </summary>
        Left = 0xE012,

        /// <summary>
        /// Represents the left arrow key.
        /// </summary>
        ArrowLeft = 0xE012, // alias

        /// <summary>
        /// Represents the up arrow key.
        /// </summary>
        Up = 0xE013,

        /// <summary>
        /// Represents the up arrow key.
        /// </summary>
        ArrowUp = 0xE013, // alias

        /// <summary>
        /// Represents the right arrow key.
        /// </summary>
        Right = 0xE014,

        /// <summary>
        /// Represents the right arrow key.
        /// </summary>
        ArrowRight = 0xE014, // alias

        /// <summary>
        /// Represents the down arrow key.
        /// </summary>
        Down = 0xE015,

        /// <summary>
        /// Represents the down arrow key.
        /// </summary>
        ArrowDown = 0xE015, // alias

        /// <summary>
        /// Represents the Insert key.
        /// </summary>
        Insert = 0xE016,

        /// <summary>
        /// Represents the Delete key.
        /// </summary>
        Delete = 0xE017,

        /// <summary>
        /// Represents the semi-colon key.
        /// </summary>
        Semicolon = 0xE018,

        /// <summary>
        /// Represents the equal sign key.
        /// </summary>
        Equal = 0xE019,

        // Number pad keys

        /// <summary>
        /// Represents the number pad 0 key.
        /// </summary>
        NumberPad0 = 0xE01A,

        /// <summary>
        /// Represents the number pad 1 key.
        /// </summary>
        NumberPad1 = 0xE01B,

        /// <summary>
        /// Represents the number pad 2 key.
        /// </summary>
        NumberPad2 = 0xE01C,

        /// <summary>
        /// Represents the number pad 3 key.
        /// </summary>
        NumberPad3 = 0xE01D,

        /// <summary>
        /// Represents the number pad 4 key.
        /// </summary>
        NumberPad4 = 0xE01E,

        /// <summary>
        /// Represents the number pad 5 key.
        /// </summary>
        NumberPad5 = 0xE01F,

        /// <summary>
        /// Represents the number pad 6 key.
        /// </summary>
        NumberPad6 = 0xE020,

        /// <summary>
        /// Represents the number pad 7 key.
        /// </summary>
        NumberPad7 = 0xE021,

        /// <summary>
        /// Represents the number pad 8 key.
        /// </summary>
        NumberPad8 = 0xE022,

        /// <summary>
        /// Represents the number pad 9 key.
        /// </summary>
        NumberPad9 = 0xE023,

        /// <summary>
        /// Represents the number pad multiplication key.
        /// </summary>
        Multiply = 0xE024,

        /// <summary>
        /// Represents the number pad addition key.
        /// </summary>
        Add = 0xE025,

        /// <summary>
        /// Represents the number pad thousands separator key.
        /// </summary>
        Separator = 0xE026,

        /// <summary>
        /// Represents the number pad subtraction key.
        /// </summary>
        Subtract = 0xE027,

        /// <summary>
        /// Represents the number pad decimal separator key.
        /// </summary>
        Decimal = 0xE028,

        /// <summary>
        /// Represents the number pad division key.
        /// </summary>
        Divide = 0xE029,

        // Function keys

        /// <summary>
        /// Represents the function key F1.
        /// </summary>
        F1 = 0xE031,

        /// <summary>
        /// Represents the function key F2.
        /// </summary>
        F2 = 0xE032,

        /// <summary>
        /// Represents the function key F3.
        /// </summary>
        F3 = 0xE033,

        /// <summary>
        /// Represents the function key F4.
        /// </summary>
        F4 = 0xE034,

        /// <summary>
        /// Represents the function key F5.
        /// </summary>
        F5 = 0xE035,

        /// <summary>
        /// Represents the function key F6.
        /// </summary>
        F6 = 0xE036,

        /// <summary>
        /// Represents the function key F7.
        /// </summary>
        F7 = 0xE037,

        /// <summary>
        /// Represents the function key F8.
        /// </summary>
        F8 = 0xE038,

        /// <summary>
        /// Represents the function key F9.
        /// </summary>
        F9 = 0xE039,

        /// <summary>
        /// Represents the function key F10.
        /// </summary>
        F10 = 0xE03A,

        /// <summary>
        /// Represents the function key F11.
        /// </summary>
        F11 = 0xE03B,

        /// <summary>
        /// Represents the function key F12.
        /// </summary>
        F12 = 0xE03C,

        /// <summary>
        /// Represents the function key META.
        /// </summary>
        Meta = 0xE03D,

        /// <summary>
        /// Represents the function key COMMAND.
        /// </summary>
        Command = 0xE03D

    }
}
