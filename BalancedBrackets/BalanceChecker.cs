using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedBrackets.Console
{
    //class BalanceChecker
    //{
    //    private string rawInput;
    //    private char[] allBrackets;
    //    private BracketList squares;
    //    private BracketList curlies;
    //    private BracketList parentheses;

    //    private bool isDetermined = false;
    //    private bool? isBalanced;
                
    //    public BalanceChecker(string input)
    //    {
    //        rawInput = input;
    //    }

    //    public string CheckIsBalanced()
    //    {
    //        CheckSpecialCases(rawInput);
    //        if (!isDetermined)
    //        {
    //            List<BracketList> bracketLists = SplitRawInput();
    //            CheckSpecialCases(bracketLists);
    //            if (!isDetermined)
    //            {
    //                for (int i = 0; i < bracketLists.Count; i++)
    //                {
    //                    //determine whether it's worth it to go through one or two of the lists for indidivual balance validity, if they are 1 order of magnitude less than the next
    //                    decimal ratio;
    //                    if (i < bracketLists.Count - 1) { ratio = bracketLists[i + 1].List.Count / bracketLists[i].List.Count; }
    //                    else { ratio = allBrackets.Length / bracketLists[i].List.Count; };
    //                    if (ratio >= 10)
    //                    {
    //                        IndividualBalanceAlg individualBalanceAlg = new IndividualBalanceAlg();
    //                        isBalanced = individualBalanceAlg.Check(bracketLists[i], out isDetermined);
    //                        if (isDetermined) { break; };
    //                    }
    //                }
    //                if (!isDetermined)
    //                {
    //                    AllBracketsBalanceAlg allBracketsBalanceAlg = new AllBracketsBalanceAlg();
    //                    isBalanced = allBracketsBalanceAlg.Check(allBrackets
    //                        , squares.List.Count
    //                        , curlies.List.Count
    //                        , parentheses.List.Count
    //                        );
    //                    isDetermined = true;
    //                }
    //            }
    //        }
    //        return ConvertToCharForOutput();            
    //    }


    //    private List<BracketList> SplitRawInput()
    //    {
    //        allBrackets = new char[rawInput.Length];
    //        for (int i = 0; i < rawInput.Length; i++)
    //        {
    //            allBrackets[i] = rawInput[i];
    //        }
    //        squares = new BracketList(allBrackets.Where(el => el.Equals('[') || el.Equals(']')).ToList(), '[', ']');
    //        curlies = new BracketList(allBrackets.Where(el => el.Equals('{') || el.Equals('}')).ToList(), '{', '}');
    //        parentheses = new BracketList(allBrackets.Where(el => el.Equals('(') || el.Equals(')')).ToList(), '(', ')');
    //        List<BracketList> bracketLists = new List<BracketList>(3)
    //        {
    //            squares
    //            , curlies
    //            , parentheses
    //        };
    //        //sorting this by count gives performance gains for later iterations
    //        bracketLists.OrderBy(el => el.List.Count);            
    //        return bracketLists;
    //    }

    //    #region specialCases
    //    private void CheckSpecialCases(string rawInput)
    //    {
    //        if (rawInput.Length % 2 != 0) { isBalanced = false; isDetermined = true; };
    //    }

    //    private void CheckSpecialCases(List<BracketList> bracketLists)
    //    {
    //        for (int i = 0; i < bracketLists.Count; i++)
    //        {
    //            //check for disqualifing odd numbers in each bracket category
    //            if (bracketLists[i].List.Count % 2 != 0) { isBalanced = false; isDetermined = true; break; }
    //        }
    //    }
    //    #endregion  

    //    private string ConvertToCharForOutput()
    //    {
    //        //reset class for reuse
    //        bool? outputIsBalanced = ResetDetermination();            
    //        if (outputIsBalanced == true) { return "YES"; }
    //        else if (outputIsBalanced == false) { return "NO"; }
    //        else { throw new Exception("Balance determined but not set"); };
    //    }

    //    //make the instance reusable
    //    private bool? ResetDetermination()
    //    {
    //        bool? outputIsBalanced = isBalanced;
    //        isBalanced = null;
    //        isDetermined = false;
    //        return outputIsBalanced;
    //    }
    //}
}
