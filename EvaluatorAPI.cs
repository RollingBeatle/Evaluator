using System;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Collections;
using System.Management.Automation;
using System.Reflection;

namespace Evaluator
{
    class EvaluatorAPI
    {
        public ArrayList response;

        public int resFound;

        public int errors;

        public List<testObject> testIDs;

        public int resNotFound;

        public EvaluatorAPI()
        {
            response = new ArrayList();
            resFound = 0;
            errors = 0;
            testIDs = new List<testObject>();
            resNotFound = 0;
        }
        public void LoadTests(string filePath)
        {
            
            foreach (string line in System.IO.File.ReadLines(filePath))
            {
                System.Console.WriteLine(line);
                string[] words = line.Split(',');
                testObject cTest = new testObject(words[0], words[1], words[1]);
                testIDs.Add(cTest);
            }
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
                        resFound++;
                        break;
                    }

                }
                catch (Exception e)
                {
                    Result res1 = new Result(input.testID, "Incomplete", rule.ruleID);
                    res1.resStatus = (int)Result.resultCodes.error;
                    response.Add(res1);
                    finished = true;
                    errors++;
                }

            }
            if (!finished)
            {
                Result res = new Result(input.testID, "Finished", rule.ruleID);
                res.resStatus = (int)Result.resultCodes.Notdetected;
                resNotFound++;
                response.Add(res);
            }


        }

        public void testWMApi(String cmd, IRule rule, testObject input)
        {
            ArrayList tests = rule.testRule(cmd);
            bool finished = false;
            for (int i = 0; i < tests.Count; i++)
            {

            }
        }

        public void TestRun(List<IRule> rules)
        {
            int i = 0, lenT = testIDs.Count;
            int j = 0, lenR = rules.Count;

            while (i < lenT)
            {
                while(j< lenR)
                {
                    testPS(rules[j].Inputcommand, rules[j], testIDs[i]);
                    j++;
                }
                i++;
            }
            
        }

        public void processResults()
        {
            Console.WriteLine("Results");
            Console.WriteLine(resFound);
        }
    }
}
