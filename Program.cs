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

            String[] shellCommands = { "Get-LocalUser", "Get-CimInstance -ClassName Win32_UserAccount | Select-Object -Property Name" };
            EvaluatorAPI usersAPI = new EvaluatorAPI();

            RuleCommand rule = new RuleCommand("1", "1", "This is a test");
            RuleCommand rule1 = new RuleCommand("1", "1", "This is a test");
            RuleWAPI rule2 = new RuleWAPI("1", "1", "This is a test");

            rule.Inputcommand = "Get-LocalUser";
            rule1.Inputcommand = "Get-CimInstance -ClassName Win32_UserAccount | Select-Object -Property Name";
            rule2.Inputcommand = "netusers";
            //testObject tObject = new testObject("1", "1", "Administrador");
            List<IRule> rules = new List<IRule>();
            rules.Add(rule);
            rules.Add(rule1);
            rules.Add(rule2);
            usersAPI.LoadTests("C:\\Users\\diego\\OneDrive\\Documentos\\UCSC\\OperatingSystems\\Evaluator\\tests.txt");
            //usersAPI.testPS(shellCommands[0], rule, tObject);
            int j = 0, lenT = rules.Count; 
            Console.WriteLine(lenT);
            while (j < lenT)
            {
                usersAPI.TestRun(rules);
                j++;
            }
            ArrayList arr = usersAPI.response;
            //usersAPI.powerShellScriptWmi();
            Console.WriteLine(arr.Count);
           /* for (int i = 0; i < arr.Count; i++)
            {
                Result res = (Result)arr[i];
                Console.WriteLine(res.date);
                Console.WriteLine(res.ruleID);
                Console.WriteLine(res.testID);
                Console.WriteLine(res.status);
                Console.WriteLine(res.resStatus);
            }*/
            Console.WriteLine("The results are total tests"+ arr.Count);
            Console.WriteLine("The results are errors" + usersAPI.errors);
            Console.WriteLine("The results are found " + usersAPI.resFound);
            Console.WriteLine("The results are found " + usersAPI.resNotFound);

        }
    }
}
