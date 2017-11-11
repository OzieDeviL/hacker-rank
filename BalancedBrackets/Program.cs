using System;
using System.Collections.Generic;
using System.Linq;
//using BalancedBrackets.Console

class Solution
{
    static void Main(String[] args)
    {
        List<string> inputs = ReadInputs();  
        foreach (string input in inputs)
        {
            BalanceChecker balanceChecker = new BalanceChecker(input);
            string output = balanceChecker.CheckIsBalanced();
            Console.WriteLine(output);
        }
        Console.ReadLine();
    }

    private static List<string> ReadInputs()
    {
        //first console line will be the number of bracket strings to be read in
        //int t = Convert.ToInt32(Console.ReadLine());
        //int t = 3; //test1
        int t = 5; //test0
        //second console input will be a series of bracket strings
        List<string> hardCodedInputs = new List<string>(t)
        {
            //test1
            //@"{[()]}"
            //,@"{[(])}"
            //,@"{{[[(())]]}}"
            //test0
            @"}][}}(}][))]"
            ,@"[]"
            ,@"(){()}"
            ,@"()"  
            ,@"({}([][]))[] ()"
            ,@"{)[](}]}]}))}(())("
        };
        List<string> inputs = new List<string>(t);
        for (int i = 0; i < t; i++)
        {
            inputs.Add(hardCodedInputs[i]);
        }
        return inputs;
    }

}
public class AllBracketsBalanceAlg
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
public class BalanceChecker
{
    private string rawInput;
    private char[] allBrackets;
    private BracketList squares;
    private BracketList curlies;
    private BracketList parentheses;

    private bool isDetermined = false;
    private bool? isBalanced;

    public BalanceChecker(string input)
    {
        rawInput = input;
    }

    public string CheckIsBalanced()
    {
        CheckSpecialCases(rawInput);
        if (!isDetermined)
        {
            List<BracketList> bracketLists = SplitRawInput();
            CheckSpecialCases(bracketLists);
            if (!isDetermined)
            {                
                for (int i = 0; i < bracketLists.Count; i++)

                {
                    //make sure there's a list to check
                    if (bracketLists[i].List.Count > 0)
                    {
                    //determine whether it's worth it to go through one or two of the lists for indidivual balance validity, if they are 1 order of magnitude less than the next
                    decimal ratio;
                    if (i < bracketLists.Count - 1) { ratio = bracketLists[i + 1].List.Count / bracketLists[i].List.Count; }
                    else { ratio = allBrackets.Length / bracketLists[i].List.Count; };
                    if (ratio >= 10)
                    {
                        IndividualBalanceAlg individualBalanceAlg = new IndividualBalanceAlg();
                        isBalanced = individualBalanceAlg.Check(bracketLists[i], out isDetermined);
                        if (isDetermined) { break; };
                    }
                    }
                }
                if (!isDetermined)
                {
                    AllBracketsBalanceAlg allBracketsBalanceAlg = new AllBracketsBalanceAlg();
                    isBalanced = allBracketsBalanceAlg.Check(allBrackets
                        , squares.List.Count
                        , curlies.List.Count
                        , parentheses.List.Count
                        );
                    isDetermined = true;
                }
            }
        }
        return ConvertToCharForOutput();
    }


    private List<BracketList> SplitRawInput()
    {
        allBrackets = new char[rawInput.Length];
        for (int i = 0; i < rawInput.Length; i++)
        {
            allBrackets[i] = rawInput[i];
        }
        squares = new BracketList(allBrackets.Where(el => el.Equals('[') || el.Equals(']')).ToList(), '[', ']');
        curlies = new BracketList(allBrackets.Where(el => el.Equals('{') || el.Equals('}')).ToList(), '{', '}');
        parentheses = new BracketList(allBrackets.Where(el => el.Equals('(') || el.Equals(')')).ToList(), '(', ')');
        List<BracketList> bracketLists = new List<BracketList>(3)
            {
                squares
                , curlies
                , parentheses
            };
        //sorting this by count gives performance gains for later iterations
        bracketLists.OrderBy(el => el.List.Count);
        return bracketLists;
    }

    #region specialCases
    private void CheckSpecialCases(string rawInput)
    {
        if (rawInput.Length % 2 != 0) { isBalanced = false; isDetermined = true; };
    }

    private void CheckSpecialCases(List<BracketList> bracketLists)
    {
        for (int i = 0; i < bracketLists.Count; i++)
        {
            //check for disqualifing odd numbers in each bracket category
            if (bracketLists[i].List.Count % 2 != 0) { isBalanced = false; isDetermined = true; break; }
        }
    }
    #endregion

    private string ConvertToCharForOutput()
    {
        //reset class for reuse
        bool? outputIsBalanced = ResetDetermination();
        if (outputIsBalanced == true) { return "YES"; }
        else if (outputIsBalanced == false) { return "NO"; }
        else { throw new Exception("Balance determined but not set"); };
    }

    //make the instance reusable
    private bool? ResetDetermination()
    {
        bool? outputIsBalanced = isBalanced;
        isBalanced = null;
        isDetermined = false;
        return outputIsBalanced;
    }
}
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
public class IndividualBalanceAlg
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