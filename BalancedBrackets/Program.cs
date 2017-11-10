using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BalancedBrackets.Console;

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
        //t = Convert.ToInt32(Console.ReadLine());
        int t = 3;
        //second console input will be a series of bracket strings
        List<string> hardCodedInputs = new List<string>(t)
        {
            //test1
            @"{[()]}"
            ,@"{[(])}"
            ,@"{{[[(())]]}}"
        };
        List<string> inputs = new List<string>(t);
        for (int i = 0; i < t; i++)
        {
            inputs.Add(hardCodedInputs[i]);
        }
        return inputs;
    }

}