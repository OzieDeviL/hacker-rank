using System;
using System.Collections.Generic;
using System.Text;

namespace BalancedBrackets.Console
{
    public class BracketList
    {
        public BracketList(List<char> list, char leftBracket, char rightBracket)
        {
            List = list;
            LeftBracket = leftBracket;
            RightBracket = rightBracket;
        }

        public List<char> List { get; set; }
        public char LeftBracket { get; set; }
        public char RightBracket { get; set; }
    }
}
