using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution
{

    static void Main(String[] args)
    {
        string[] tokens_m = "6 5".Split(' ');
        int m = Convert.ToInt32(tokens_m[0]);
        int n = Convert.ToInt32(tokens_m[1]);
        string mString = "zahk dp apdz clo e dk awfvf osb qr sa cqjq zgr nvxtb abjy axa ili wdyw soqku buwcl qcub sautu ii vkrzl bdob nona al zg ombzc c dbun f xkuo lsax hfki j dfft uce ugj ywz vucgg xq udrkt ypy tmxgc ty gar kty dc bznj pzzx clo apdz nvxtb clo sa clo zahk awfvf soqku udrkt udrkt e ypy xkuo tmxgc ombzc wdyw al axa lsax clo abjy osb apdz bdob pzzx zahk c bznj gar osb xkuo zahk zg uce zg clo e apdz gar xq dbun buwcl ili bznj clo osb dc dbun ywz";
        string rString = "buwcl qr axa ypy zahk nvxtb dp hfki ii uce dc zg dbun ypy ty cqjq zg kty bznj zg zahk dp c al ugj ywz qcub ywz wdyw hfki gar e axa dp qr kty bznj clo ty vucgg qcub al vkrzl qcub j awfvf soqku lsax bdob nvxtb";
        //check strings for equality
        bool isDeterminedCase = CheckInitialSpecialCases(mString, rString, out bool magazineQualifies);
        if (!isDeterminedCase)
        {
            IEnumerable<string> magazineEnumerable = mString.Split(' ');
            IEnumerable<string> ransomEnumerable = rString.Split(' ');
            //initial qualification checks for special cases to avoid creating dictionaries
            isDeterminedCase = CheckForSpecialCases(magazineEnumerable, ransomEnumerable, out magazineQualifies);
            if (!isDeterminedCase)
            {
                Dictionary<string, int> ransom = PopulateDictionary(ransomEnumerable);
                Dictionary<string, int> magazine = PopulateDictionary(magazineEnumerable);
                magazine = magazine
                    .Where(el => ransom.ContainsKey(el.Key))
                    .ToDictionary(el => el.Key, el => el.Value);
                //first check that the number of multiple word entries is the same in the ransom note and magazine
                if (ransom.Count > magazine.Count)
                {
                    magazineQualifies = false;
                } else
                { 
                    //then check each entry in the dictionaries
                    magazineQualifies = CheckWordCount(magazine, ransom); 
                }
            }
        }
        if (magazineQualifies)
        {
            Console.WriteLine("Yes");
        }
        else
        {
            Console.WriteLine("No");
        }
        Console.ReadLine();
    }

    private static Dictionary<string, int> PopulateDictionary(IEnumerable<string> source)
    {
        //we already know that any words that appear in the ransom note appear in the magazine at least once
        //so now we're only interested in words that appera more than once
        Dictionary<string, int> dictionary = source.GroupBy(x => x)
                            .Where(g => g.Count() > 1)
                            .ToDictionary(x => x.Key, y => y.Count());
        return dictionary;
    }

    private static bool CheckWordCount(Dictionary<string, int> magazine, Dictionary<string, int> ransom)
    {
        //check for words in the ransom note that appear more frequently than the same in the magazine
        IEnumerable<string> filteredRansom =
            from r in ransom
            join m in magazine on r.Key equals m.Key
            where r.Value > m.Value
            select r.Key;
        if (filteredRansom.Count() > 0)
        {           
            return false;
        } else
        {
            return true;
        }
            
    }

    #region checkSpecialCases
    private static bool CheckInitialSpecialCases(string mString
                                            , string rString
                                            , out bool magazineQualifies)
    {
        //check that the strings aren't identical
        if (mString == rString)
        {
            magazineQualifies = true;
            return true;
        }
        else
        {
            magazineQualifies = true;
            return false;
        }
    }

    private static bool CheckForSpecialCases(IEnumerable<string> magazine
                                            , IEnumerable<string> ransom
                                            , out bool magazineQualifies)
    {
        //check that the length of the ransom note is not greater than the magazine length
        if (ransom.Count() > magazine.Count()
            //and check that all words in the ransom note appear in the magazine
            || ransom.Except(magazine).Count() > 0
            )
        {
            magazineQualifies = false;
            return true;
        } 
        //no special conditions met
        else
        {
            magazineQualifies = true;
            return false;
        }
    }

    private static bool CheckForSpecialCase(Dictionary<string, int> magazine
                                        , Dictionary<string, int> ransom
                                        , out bool magazineQualifies)
    {
        //check that the length of the ransom note is not greater than the magazine length
        if (ransom.Count() > magazine.Count())
        {
            magazineQualifies = false;
            return true;
        }
        //no special conditions met
        else
        {
            magazineQualifies = true;
            return false;
        }
    }
    #endregion
}
