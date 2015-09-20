using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acme.Helpers.TagHelpers
{
    public static class DirectoryDefaults
    {
        public static string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string DefaultClass = "pagination";
        public static string DefaultParameter = "current";
        public static DirectoryDisplayMode Mode = DirectoryDisplayMode.Letters;
        public static string Numbers = "0123456789";
        public static bool ShowActive = true;
    }
}
