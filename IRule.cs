using System;
using System.Collections;
using System.Text;

namespace Evaluator
{
    interface IRule
    {
        public ArrayList testRule(String command);

        public String testID { get; set; }

        public String ruleID { get; set; }

        public String description { get; set; }

        public String Inputcommand { get; set; }

    }
}
