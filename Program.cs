using System;
using System.Collections;

namespace Evaluator
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] shellCommands = { "Get-LocalUser", "Get-CimInstance -ClassName Win32_UserAccount | Select-Object -Property Name" };
            EvaluatorAPI usersAPI = new EvaluatorAPI();
            RuleCommand rule = new RuleCommand("1", "1", "This is a test");
            testObject tObject = new testObject("1", "1", "Administrador");
            usersAPI.testPS(shellCommands[0], rule, tObject);
            ArrayList arr = usersAPI.response;
            //usersAPI.powerShellScriptWmi();
            for (int i = 0; i < arr.Count; i++)
            {
                Result res = (Result)arr[i];
                Console.WriteLine(res.date);
                Console.WriteLine(res.ruleID);
                Console.WriteLine(res.testID);
                Console.WriteLine(res.status);
                Console.WriteLine(res.resStatus);
            }

        }
    }
}
