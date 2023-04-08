using System;
using System.Collections.Generic;

namespace Assignment
{
    class ToolLibrarySystem : iToolLibrarySystem
    {
        //Initialie Varibles, and create new instances of all the inernal systems
        public static iMember currentUser;
        public static iToolLibrarySystem toolLibrarySystem = new ToolLibrarySystem();
        public static iToolCollection toolCollection = new ToolCollection();
        public static iMemberCollection memberCollection = new MemberCollection();
        
        ///<summary>
        /// add a new toolCollection to the system
        ///</summary>
        public void add(iTool aTool)
        {
           toolCollection.add(aTool);
        }

        ///<summary>
        ///add new pieces of an existing toolCollection to the system
        ///</summary>
        public void add(iTool aTool, int quantity)
        {
            aTool.Quantity += quantity;
        }

        ///<summary>
        ///delte a given toolCollection from the system
        ///</summary>
        public void delete(iTool aTool)
        {
            toolCollection.delete(aTool);
        }

        ///<summary>
        ///remove some pieces of a toolCollection from the system
        ///</summary>
        public void delete(iTool aTool, int quantity)
        {
            aTool.Quantity -= quantity;
        }

        ///<summary>
        ///add a new memeber to the system
        ///</summary>
        public void add(iMember aMember)
        {
            memberCollection.add(aMember);
        }

        ///<summary>
        ///delete a member from the system
        ///</summary>
        public void delete(iMember aMember)
        {
            memberCollection.delete(aMember);
        }

        ///<summary>
        ///given a member, display all the toolCollections that the member are currently renting
        ///</summary>
        public void displayBorrowingTools(iMember aMember)
        {
            foreach(string tool in aMember.Tools)
            {
                Console.WriteLine(tool);
            }
        }

        ///<summary>
        /// display all the toolCollections of a toolCollection type selected by a member
        ///</summary>
        public void displayTools(string aToolType)
        {
            iTool[] tools = ToolCollection.Searching.findAllType(toolCollection.toArray(), aToolType);
            if (tools.Length >= 1)
            {
                foreach (iTool tool in tools)
                {
                    Console.WriteLine(tool.ToString());
                }
            }
            else
            {
                Console.WriteLine("No Tools of this Type found, please try again.");
            }

        }

        ///<summary>
        ///a member borrows a toolCollection from the toolCollection library
        ///</summary>
        public void borrowTool(iMember aMember, iTool aTool)
        {
            aTool.addBorrower(aMember);
            aMember.addTool(aTool);
        }

        ///<summary>
        ///a member return a toolCollection to the toolCollection library
        ///</summary>
        public void returnTool(iMember aMember, iTool aTool)
        {
            aMember.deleteTool(aTool);
            aTool.deleteBorrower(aMember);
        }

        ///<summary>
        ///get a list of toolCollections that are currently held by a given member
        ///</summary>
        public string[] listTools(iMember aMember)
        {
            return aMember.ToolString();
        }

        ///<summary>
        ///Display top three most frequently borrowed toolCollections by the members in the descending order by the number of times each toolCollection has been borrowed.
        ///</summary>
        public void displayTopThree()
        {
            Console.WriteLine("Top Three Most Used Tools Are:");
            toolCollection.SortByNoBorrowers(); //sort tools by most borrowed
            iTool[] toolArr = toolCollection.toArray();
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("\n#{0} {1}", i , toolArr[toolArr.Length - i - 1].ToString());
            }
        }

        ///<summary>
        /// search for a tool by the tools name
        ///</summary>
        public iTool searchTool(string name)
        {
            try
            {
                return toolCollection.searchTool(name);
            }
            catch
            {
                Console.WriteLine("Tool with name \"{0}\" does not exist. Please try again", name);
                return searchTool(FrontEndHelpers.MenuInputParse());
            }
        }

        ///<summary>
        /// Search for a member by their name
        ///</summary>
        public iMember searchMember(string firstname, string lastname)
        {
            try
            {
                return memberCollection.SearchMember(firstname, lastname);
            }
            catch
            {
                Console.WriteLine("Member with name \"{0}\" does not exist. Please try again", firstname + " " + lastname);
                return searchMember();
            }
        }

        ///<summary>
        /// Search for a member by their name, prompts from first and lastname
        ///</summary>
        public iMember searchMember()
        {
            Console.WriteLine("Please Enter Firstname");
            string firstname = Console.ReadLine();
            Console.WriteLine("Please Enter Lastname");
            string lastname = Console.ReadLine();
            try
            {
                return memberCollection.SearchMember(firstname, lastname);
            }
            catch
            {
                Console.WriteLine("User with name \"{0}\" does not exist. Please try again", firstname+" "+lastname);
                return searchMember();
            }
        }

        
    }
}