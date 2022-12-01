using System;
using System.Collections.Generic;
using System.Text;

namespace Evaluator
{
    public class testObject
    {
        public String testID { get; set; }
        public String RID { get; set; }
        public String userName { get; set; }

        public testObject(String TestID, String R_ID, String UserName)
        {
            testID = TestID;
            RID = R_ID;
            userName = UserName;
        }

    }
}
