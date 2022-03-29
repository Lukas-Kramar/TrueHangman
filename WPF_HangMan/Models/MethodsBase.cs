using System.Collections.Generic;

namespace WPF_HangMan.Models
{
    internal class MethodsBase
    {
        public IEnumerable<int> AllIndexesOf( string str, string searchstring)
        {
            int minIndex = str.IndexOf(searchstring);
            while (minIndex != -1)
            {
                yield return minIndex;
                minIndex = str.IndexOf(searchstring, minIndex + searchstring.Length);
            }
        }
    }
}