using System.Collections.Generic;

namespace Mekajiki.Data
{
    public static class TextUtils
    {
        public static string RemoveTextInBrackets(string text)
        {

            char[] data = text.ToCharArray();
            int parenthesisOpen = 0;
            List<char> output = new();

            foreach (var myChar in data)
            {
                switch (myChar)
                {
                    case '(':
                    case '[':
                    case '{':
                    {
                        parenthesisOpen++;
                        continue;
                    }
                    case ')':
                    case ']':
                    case '}':
                    {
                        parenthesisOpen--;
                        continue;
                    }
                }

                if (parenthesisOpen == 0)
                {
                    output.Add(myChar);
                }
            }

            return new string(output.ToArray()).Trim();
        }
    }
}