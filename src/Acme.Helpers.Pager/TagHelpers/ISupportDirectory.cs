using System;

namespace Acme.Helpers.TagHelpers
{
    public enum DirectoryDisplayMode { Letters, Numbers, LettersNumbers, NumbersLetters, };

    public interface ISupportDirectory
    {
        /// <summary>
        /// Alphabet that will be displayed if mode selected. Defaults to "ABCDEFGHIJKLMNOPQRSTUVWXYZ".
        /// </summary>
        string DirectoryAlphabet { get; set; }
        /// <summary>
        /// Class attribute set against the pager.
        /// </summary>
        string DirectoryClass { get; set; }
        /// <summary>
        /// Sets the display orientation of the letters and numbers. Defaults to letters.
        /// </summary>
        DirectoryDisplayMode DirectoryDisplayMode { get; set; }
        /// <summary>
        /// Numbers that will be displayed if mode selected. Defaults to "0123456789".
        /// </summary>
        string DirectoryNumbers { get; set; }
        /// <summary>
        /// Request parameter that will be used (added) to the url. Defaults to "current".
        /// </summary>
        string DirectoryParam { get; set; }
        /// <summary>
        /// Show the active element by setting the "active" style.
        /// </summary>
        bool DirectoryShowActive { get; set; }
        /// <summary>
        /// Which letter of the defined alphabet should be the start position. 
        /// Defaults to first element of the specified alphabet or numbers dependent on the mode setting.
        /// </summary>
        string DirectoryStartAt { get; set; }
    }
}
