using System;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Collections;
using System.Management.Automation;
namespace Evaluator
{
    class EvaluatorAPI
    {
        public ArrayList response;


        public EvaluatorAPI()
        {
            response = new ArrayList();
        }
        public void LoadTests(string filePath)
        {
            String text = File.ReadAllText(filePath);
           // var test = JsonConvert.DeserializeObject<List<testObject>>(text);
        }

        public void testPS(String cmd, IRule rule, testObject input)
        {
            ArrayList tests = rule.testRule(cmd);
            bool finished = false;
            for (int i = 0; i < tests.Count; i++)
            {
                Console.WriteLine(tests[i]);
                try
                {
                    if (input.userName.Equals(tests[i].ToString()))
                    {
                        Result res1 = new Result(input.testID, "Finished", rule.ruleID);
                        res1.resStatus = (int)Result.resultCodes.detected;
                        finished = true;
                        response.Add(res1);
                        break;
                    }

                }
                catch (Exception e)
                {
                    Result res1 = new Result(input.testID, "Incomplete", rule.ruleID);
                    res1.resStatus = (int)Result.resultCodes.error;
                    response.Add(res1);
                    finished = true;
                }

            }
            if (!finished)
            {
                Result res = new Result(input.testID, "Finished", rule.ruleID);
                res.resStatus = (int)Result.resultCodes.Notdetected;
                response.Add(res);
            }


        }
        public ArrayList powerShellScript()
        {
            ArrayList arr = new ArrayList();

            PowerShell ps = PowerShell.Create();
            ps.AddCommand("Get-LocalUser");

            foreach (PSObject lu in ps.Invoke())
            {
                Console.WriteLine(lu);
                arr.Add(lu);
            }
            return arr;

        }

        public ArrayList powerShellScriptWmi()
        {
            ArrayList arr = new ArrayList();

            PowerShell ps = PowerShell.Create();
            ps.AddScript("Get-CimInstance -ClassName Win32_UserAccount | Select-Object -Property Name");

            foreach (PSObject lu in ps.Invoke())
            {
                Console.WriteLine(lu.ToString());
                arr.Add(lu);
            }
            return arr;

        }

        public void TestRun(testObject test)
        {

        }

        public void processTestList()
        {

        }
    }
}
