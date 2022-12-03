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

        public String resultPath;

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
                
                string[] words = line.Split(',');
                testObject cTest = new testObject(words[0], words[1], words[2]);
                testIDs.Add(cTest);
            }
        }

        public void testPS(String cmd, IRule rule, testObject input)
        {
            ArrayList tests = rule.testRule(cmd);
            bool finished = false;
            for (int i = 0; i < tests.Count; i++)
            {
                
                try
                {
                    if (input.userName.Equals(tests[i].ToString()))
                    {
                        Result res1 = new Result(input.testID, "Finished", rule.ruleID);
                        res1.resStatus = (int)Result.resultCodes.detected;
                        finished = true;
                        response.Add(res1);
                        resFound++;
                        processResults(res1);
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
                    processResults(res1);
                    break;
                }

            }
            if (!finished)
            {
                Result res = new Result(input.testID, "Finished", rule.ruleID);
                res.resStatus = (int)Result.resultCodes.Notdetected;

                resNotFound++;
                processResults(res);
                response.Add(res);
            }


        }


        public void TestRun(List<IRule> rules)
        {
            int i = 0, lenT = testIDs.Count;
            int j = 0, lenR = rules.Count;

            while (i < lenT)
            {
                //Console.WriteLine("TESTING USER" + testIDs[i].userName);
                while(j< lenR )
                {

                    if (rules[j].testID != testIDs[i].testID) { break;  }
                    testPS(rules[j].Inputcommand, rules[j], testIDs[i]);
                    j++;
                }
                i++;
            }
            
        }

        public void processResults(Result res)
        {
          
            // This text is added only once to the file.
            if (!File.Exists(resultPath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(resultPath))
                {
                    sw.WriteLine("Test ID " + res.testID + " Rule ID " + res.ruleID + " Status Num" + res.resStatus + "  " + res.status);
                    sw.WriteLine("found " + resFound + " not found " + resNotFound);

                }
            }

            // This text is always added, making the file longer over time
            // if it is not deleted.
            using (StreamWriter sw = File.AppendText(resultPath))
            {
                sw.WriteLine("Test ID " + res.testID + " Rule ID " + res.ruleID + " Status Num" + res.resStatus + "  " + res.status);
                sw.WriteLine("found " + resFound + " not found " + resNotFound);

            }

        }
    }
}
