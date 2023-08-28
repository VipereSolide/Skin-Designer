using System.Collections.Generic;
using System.Collections;

using UnityEngine;

namespace Utility
{
    public static class StringExtensions
    {
        public static string ReplaceAll(this string value, string[] from, string to)
        {
            string output = value;

            foreach(string fromString in from)
            {
                output = output.Replace(fromString, to);
            }

            return output;
        }
    }
}