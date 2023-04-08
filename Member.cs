using System;
using System.Collections.Generic;

namespace Assignment
{
    class Member : iMember
    {
        private readonly int MaxLoanNum = 3;

        // Initialise Varibles
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string PIN { get; set; }
        public iTool[] ToolList { get; set; }


        //Constructor Func
        public Member(string firstname, string lastname, string pin, string contactNumber)
        {
            FirstName = firstname;
            LastName = lastname;
            PIN = PIN;
            ContactNumber = contactNumber;
            ToolList = new iTool[MaxLoanNum];

        }

        ///<summary>
        ///get a list of tools that this memebr is currently holding
        ///</summary>
        public string[] Tools 
        {
            get 
            {
                string[] Tools = new string[MaxLoanNum];
                for (int i = 0; i < MaxLoanNum; i++)
                {
                    if (ToolList[i] != null)
                    {
                        Tools[i] = "Tool Slot " + i + ": \n" + ToolList[i].ToString() + "\n";
                    }
                    else
                    {
                        Tools[i] = "Tool Slot " + i + ": Empty";
                    }
                }

                return Tools;
            }
        }

        ///<summary>
        ///add a given tool to the list of tools that this member is currently holding
        ///</summary>
        public void addTool(iTool aTool) 
        {
            for (int i = 0; i < MaxLoanNum; i++)
            {
                if (ToolList[i] == null)
                {
                    ToolList[i] = aTool;
                    return;
                }
            }
        }

        ///<summary>
        ///delete a given tool from the list of tools that this member is currently holding
        ///</summary>
        public void deleteTool(iTool aTool) 
        {
            for (int i = 0; i >= MaxLoanNum; i++)
            {
                if (ToolList[i] == aTool)
                {
                    ToolList[i] = null;
                }
            }
        }

        ///<summary>
        ///return a string containing the first name, lastname, and contact phone number of this memeber
        ///</summary>
        public override string ToString() 
        {
            return "Firstname: " + FirstName + "\nLastname: " + LastName + "\nPhone #: " + ContactNumber;
        }

        ///<summary>
        ///Return an array of the tools a member currently has borrowed
        ///</summary>
        public string[] ToolString()
        {
            List<string> output = new List<string>();
            foreach(iTool tool in ToolList)
            {
                if (tool != null)
                {
                    output.Add(tool.ToString());
                }
            }

            return output.ToArray();
        }

    }


}