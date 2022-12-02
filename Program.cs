using System;
using System.Collections;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace Evaluator
{
    class Program
    {
        static void Main(string[] args)
        {

            EvaluatorAPI usersAPI = new EvaluatorAPI();

            List<IRule> rules = new List<IRule>();
            Console.WriteLine("Enter test.tx file directory (with test.txt):");
            string filepath = Console.ReadLine();
            usersAPI.LoadTests(filepath);
            
            int k = 0, lenTest = usersAPI.testIDs.Count;
            while (k < lenTest)
            {
                RuleCommand rulest = new RuleCommand(usersAPI.testIDs[k].testID, k.ToString(), "PW localuser");
                RuleCommand rules1 = new RuleCommand(usersAPI.testIDs[k].testID, k.ToString(), "PW CimIns");
                RuleWAPI rules2 = new RuleWAPI(usersAPI.testIDs[k].testID, k.ToString(), "CMD net users");

                rulest.Inputcommand = "Get-LocalUser";
                rules1.Inputcommand = "Get-CimInstance -ClassName Win32_UserAccount | Select-Object -Property Name";
                rules2.Inputcommand = "netusers";
                rules.Add(rulest);
                rules.Add(rules1);
                rules.Add(rules2);
                k++;
            }



            int j = 0, lenT = rules.Count; 
            Console.WriteLine(lenT);

                usersAPI.TestRun(rules);

            ArrayList arr = usersAPI.response;

            Console.WriteLine(arr.Count);
          
            Console.WriteLine("The results are total tests"+ arr.Count);
            Console.WriteLine("The results are errors" + usersAPI.errors);
            Console.WriteLine("The results are found " + usersAPI.resFound);
            Console.WriteLine("The results are not found " + usersAPI.resNotFound);

        }
    }
}
