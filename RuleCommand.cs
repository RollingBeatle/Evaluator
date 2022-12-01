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
                Console.WriteLine(lu.ToString());
                arr.Add(lu);
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
