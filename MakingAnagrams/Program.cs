using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Solution
{

    static void Main(String[] args)
    {
        //read input
        //Console.Write("cde");
        string aStr = Console.ReadLine();
        //Console.Write("abc");
        string bStr = Console.ReadLine();
        //convert input strings to charArrays
        List<char> a = aStr.ToList<char>();
        List<char> b = bStr.ToList<char>();
        //input error checking
        //special cases
        bool isSpecialCase = checkForSpecialCases(aStr, bStr, a, b);
        //determine the count
        int finalCount = 0;
        if (isSpecialCase)
        {
            finalCount = 0;
        } else 
        {
            finalCount = AnagramCounter(a, b);
        }
        //print the count
        Console.WriteLine(finalCount);        
        string[] nextRunArgs = { aStr, bStr };
        Main(nextRunArgs);
    }

    private static bool checkForSpecialCases(string aStr, string bStr, List<char> a, List<char> b)
    {
        //bool of special cases
        if (aStr == bStr)
        {
            return true;
        } else
        {
            return false;
        }
    }

    private static int AnagramCounter(List<char> a, List<char> b)
    {
        int runningCount = 0;
        List<char> longerInput = null;
        List<char> shorterInput = null;
        //determine the longer of the arrays
        if (a.Count >= b.Count)
        {
            longerInput = a;
            shorterInput = b;
        } else
        {
            longerInput = b;
            shorterInput = a;
        }
        //loop through the shorter of the two, increasing the runningCount for each entry
        runningCount = AnagramCounterWithShort(longerInput, shorterInput, runningCount);
        
        return runningCount;
    }

    private static int AnagramCounterWithShort(List<char> longerInput, List<char> shorterInput, int runningCount)
    {
        char i = shorterInput[0];
        int shorterFilteredCount = shorterInput.Count(el => el == i);
        int longerFilteredCount = longerInput.Count(el => el == i);
        runningCount += Math.Abs(longerFilteredCount - shorterFilteredCount);
        //remove those elements from the lists to save processing in later iterations
        shorterInput.RemoveAll(el => el == i);
        longerInput.RemoveAll(el => el == i);
        if (shorterInput.Count > 0)
        {
            runningCount = AnagramCounterWithShort(longerInput, shorterInput, runningCount);
        } else if (longerInput.Count > 0) 
        {
            runningCount = AnagramCounterWithoutShort(longerInput, runningCount);
        }
        return runningCount;        
    }

    private static int AnagramCounterWithoutShort(List<char> longerInput, int runningCount)
    {
        char i = longerInput[0];
        runningCount += longerInput.Count(el => el == i);
        longerInput.RemoveAll(el => el == i);
        if (longerInput.Count > 0)
        {
            runningCount = AnagramCounterWithoutShort(longerInput, runningCount);
        }
        return runningCount;
    }
}
