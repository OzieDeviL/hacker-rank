using System;
using System.Collections.Generic;
using System.Text;

namespace BalancedBrackets.Console
{
    class IndividualBalanceAlg
    {
        public bool? Check(BracketList bracketList, out bool isDetermined)
        {
            bool? isBalanced = null;
            int count = 0;
            //algorithm
            for (int j = 0; j < bracketList.List.Count; j++)
            {
                //check for too many right brackets
                if (bracketList.List[j] == bracketList.LeftBracket) { count++; }
                else if (bracketList.List[j] == bracketList.RightBracket) { count--; }
                //check for equal left and right brackets
                if (count < 0) { isBalanced = false; isDetermined = true; break; }
            }
            //if it is unbalanced, the answer for the input has been determined             
            if (count != 0) { isBalanced = false; isDetermined = true; }
            else { isDetermined = false; }
            return isBalanced;
        }
    }
}
