using System;
using System.Collections.Generic;
using System.Text;

namespace Evaluator
{
    public class Result
    {
        public String testID { get; set; }
        public DateTime date { get; set; }
        public String status { get; set; }
        public String ruleID { get; set; }

        public int resStatus { get; set; }

        public enum resultCodes
        {
            Notdetected = 0,
            detected = 1,
            error = 2,
            resgistryError = 3


        }

        public Result(String TestId, String Status, String RuleID)
        {
            testID = TestId;
            status = Status;
            ruleID = RuleID;
            date = DateTime.Now;

        }


    }
}
