using System;
using System.Collections.Generic;
using System.Text;

namespace BalancedBrackets.Console
{
    class AllBracketsBalanceAlg
    {
        private bool isDetermined = false;
        private bool? isBalanced = null;

        internal bool? Check(
            char[] allBrackets
            , int squareListSize
            , int curlyListSize
            , int parenthesesListSize
            )
        {
            Stack<int> squareMins = new Stack<int>(squareListSize);
            Stack<int> curlyMins = new Stack<int>(curlyListSize);
            Stack<int> parenthesesMins = new Stack<int>(parenthesesListSize);
            int squareCount = 0;
            int curlyCount = 0;
            int parenthesesCount = 0;
            for (int i = 0; i < allBrackets.Length; i++)
            {
                switch (allBrackets[i])
                {
                    case '[':
                        squareCount++;
                        NewMinsEntry(ref curlyCount, curlyMins, ref parenthesesCount, parenthesesMins);
                        break;
                    case '{':
                        curlyCount++;
                        NewMinsEntry(ref squareCount, squareMins, ref parenthesesCount, parenthesesMins);
                        break;
                    case '(':
                        parenthesesCount++;
                        NewMinsEntry(ref squareCount, squareMins, ref curlyCount, curlyMins);
                        break;
                    case ']':
                        squareCount--;
                        CloseBracket(ref squareCount, ref curlyCount, ref parenthesesCount, squareMins);
                        break;
                    case '}':
                        curlyCount--;
                        CloseBracket(ref curlyCount, ref squareCount, ref parenthesesCount, curlyMins);
                        break;
                    case ')':
                        parenthesesCount--;
                        CloseBracket(ref parenthesesCount, ref curlyCount, ref squareCount, parenthesesMins);
                        break;
                    default:
                        break;
                }
                if (isDetermined)
                {
                    break;
                }
            }
            //if it survives all of that, it is verified balanced
            if (!isDetermined) { isDetermined = true; isBalanced = true; }
            bool? outputIsBalanced = isBalanced;
            //reset before returning so the same instance can be used to evaluate more than one input
            outputIsBalanced = ResetDetermination();
            return outputIsBalanced;
        }

        private void NewMinsEntry(ref int countA, Stack<int> aMins, ref int countB, Stack<int> bMins)
        {
            if (countA != 0)
            {
                aMins.Push(countA);
                countA = 0;
            }
            if (countB != 0)
            {
                bMins.Push(countB);
                countB = 0;
            }
        }

        private void CloseBracket(ref int thisCount, ref int otherCountA, ref int otherCountB, Stack<int> thisMins)
        {
            //check that this isn't closing off while a section of other bracket types is unclosed
            if (otherCountA > 0
                || otherCountB > 0
                )
            {
                isBalanced = false;
                isDetermined = true;
                return;
            }
            else if (thisCount < 0)
            {
                //check that the individual bracket hasn't become unbalanced
                if (thisMins.Count == 0)
                {
                    isBalanced = false;
                    isDetermined = true;
                    return;
                }
                else
                {
                    //poping this restores the previous count, minus the decrement from this closing
                    thisCount += thisMins.Pop();
                }
            }
        }

        private bool? ResetDetermination()
        {
            bool? outputIsBalanced = isBalanced;
            isBalanced = null;
            isDetermined = false;
            return outputIsBalanced;
        }

    }
}
