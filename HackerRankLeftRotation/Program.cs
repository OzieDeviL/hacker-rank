using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution
{

    static void Main(String[] args)
    {
        string[] tokens_n = "5 4".Split(' ');
        int n = Convert.ToInt32(tokens_n[0]);
        int k = Convert.ToInt32(tokens_n[1]);
        string[] a_temp = "1 2 3 4 5".Split(' ');
        int[] a = Array.ConvertAll(a_temp, Int32.Parse);
        //error checking, in a more realistic scenario        
        //edge-case checking
        bool isEdgeCase = CheckEdgeCase(n, k);
        if (isEdgeCase)
        {
            Console.WriteLine(a_temp);
        }
        else
        {
            //main work
            int[] rotatedArray = LeftRotateArr(k, a);
            PrintArray(rotatedArray);
        }
    }

    private static bool CheckEdgeCase(int n, int rotations)
    {
        if (n == 0
           || n == 1           
           || rotations == 0
           || n % rotations == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private static int[] LeftRotateArr(int rotations, int[] inArr)
    {
        if (rotations > inArr.Length)
        {
            rotations = inArr.Length % rotations;
        }
        ArraySegment<int> iGreaterThanRotations = new ArraySegment<int>(inArr
                                                                    , rotations
                                                                    , inArr.Length - rotations);
        ArraySegment<int> iLessThanRotations = new ArraySegment<int>(inArr
                                                                    , 0
                                                                    , rotations);
        int[] rotatedArr = iGreaterThanRotations.Concat<int>(iLessThanRotations).ToArray<int>();
        return rotatedArr;        
    }

    private static void PrintArray(int[] rotatedArr)
    {
        string[] rotatedStringArr = Array.ConvertAll<int, string>(rotatedArr, el => el.ToString());
        string output = string.Join(" ", rotatedStringArr);
        Console.WriteLine(output);
    }

}
