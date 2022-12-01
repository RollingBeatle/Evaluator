using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Management.Automation;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Evaluator
{
    public class RuleWAPI : IRule
    {
        public string testID { get; set; }
        public string ruleID { get; set; }
        public string description { get; set; }

        public RuleWAPI(String TestID, String ruleId, String Description)
        {
            testID = TestID;
            ruleID = ruleId;
            description = Description;

        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct USER_INFO_1
        {
            public string usri1_name;
            public string usri1_password;
            public int usri1_password_age;
            public int usri1_priv;
            public string usri1_home_dir;
            public string comment;
            public int usri1_flags;
            public string usri1_script_path;
        }
        public struct USER_INFO_0
        {
            public String Username;
        }

        [DllImport("Netapi32.dll")]
        public extern static int NetUserEnum([MarshalAs(UnmanagedType.LPWStr)]
       string servername,
       int level,
       int filter,
       out IntPtr bufptr,
       int prefmaxlen,
       out int entriesread,
       out int totalentries,
       out int resume_handle);

        public bool finnish;
        public ArrayList instancesFound;
        public ArrayList testsFound;

        public ArrayList enumerateUsers()
        {
            int EntriesRead;
            int TotalEntries;
            int Resume;
            IntPtr bufPtr;
            ArrayList arr = new ArrayList();

            NetUserEnum(null, 1, 0, out bufPtr, -1, out EntriesRead, out TotalEntries, out Resume);
            Console.WriteLine(TotalEntries);
            if (EntriesRead > 0)
            {
                USER_INFO_1[] Users = new USER_INFO_1[EntriesRead];
                IntPtr iter = bufPtr;
                for (int i = 0; i < EntriesRead; i++)
                {
                    Users[i] = (USER_INFO_1)Marshal.PtrToStructure(iter, typeof(USER_INFO_1)); //check iteration
                    arr.Add(Users[i].usri1_name);


                }


            }
            return arr;
        }




        public ArrayList testRule(string command)
        {
            ArrayList arr = new ArrayList();
            if (command.Equals("netusers"))
            {
                arr = enumerateUsers();
            }

            return arr;
        }


    }
}
