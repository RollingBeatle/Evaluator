using System;
using System.Collections.Generic;
using System.Text;
using System.Management.Automation;
using System.Collections;
namespace Evaluator
{
    public class RuleCommand : IRule
    {
        public String testID { get; set; }

        public String ruleID { get; set; }

        public String description { get; set; }

        public String Inputcommand { get; set; }

        public ArrayList testRule(String command)
        {
            ArrayList arr = new ArrayList();

            PowerShell ps = PowerShell.Create();
            ps.AddScript(command);
            Inputcommand = command;
            foreach (PSObject lu in ps.Invoke())
            {
                String str = lu.ToString();
                if (str.Contains("@"))
                {
                   
                    str = str.Remove(0, 7);
                    str = str.Remove(str.Length - 1, 1);
                }
                //Console.WriteLine(str);

                arr.Add(str);
            }

            return arr;
        }

        public RuleCommand(String TestID, String ruleId, String Description)
        {
            testID = TestID;
            ruleID = ruleId;
            description = Description;

        }

    }
}
