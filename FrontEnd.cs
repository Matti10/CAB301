using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;

//                                  !!-IMPORTANT NOTE-!!
//**************************************************************************************************
//  A portion of this code is code that was written and submitted by me in a previous assesment. 
//        Feel free to get into contact if you require more info, n10467874@qut.edu.au
//**************************************************************************************************
//                                           !!

namespace Assignment
{
    class FrontEnd : FrontEndHelpers 
    {
        public static void Main(string[] args)
        {
            setup();
        }

        /// <summary>
        /// sets up the UI and initates the back end
        /// </summary>
        static void setup()
        { 
            Console.WriteLine("                                   Welcome to the Tool Library!                                  ");
            Console.WriteLine("|===============================================================================================|");
            Console.WriteLine("|===== At any time, enter \"H\" to return to the home screen, \"L\" to Log Out, or \"Q\" to quit =====|");
            Console.WriteLine("|===============================================================================================|");

            MainMenu();
        }

        static void LoginPrompt()
        {
            string msg = "\nPlease Select one of the Login Options:\nMember Login - 1 \nStaff Login - 2";
            List<Action> methods = (new Action[2] { Login.member, Login.staff }).ToList();
            DisplayOptions(msg, methods);

        }

        /// <summary>
        /// displays the main menu
        /// </summary>
        public static void MainMenu()
        {
            if (!Login.loggedIn)
            {
                LoginPrompt();
            }
            else if (currentUser != null)
            {
                MemberMenu();
            }
            else
            {
                StaffMenu();
            }
            
        }

        public static string[] selectCategory()
        {
            string category;
            string type;

            Console.WriteLine("Select Tool Category");
            Console.WriteLine("1. Gardening tools\n2.Flooring tools\n3.Fencing tools\n4.Measuring tools\n5.Cleaning tools\n6.Painting tools\n7.Electronic tools\n8.Electricity tools\n9.Automotive tools");

            string input = MenuInputParse();

            while (true)
            {
                if (Int32.TryParse(input, out int num))
                {
                    for (int i=0; i < Tool.validCategories.Length; i++)
                    {
                        category = Tool.validCategories[i];
                        Console.WriteLine("Select Tool Type");
                        string msg = "\n";
                        for (int j=0; j < Tool.allTypes[i].Length; j++)
                        {
                            msg += j+1 + ". " + Tool.allTypes[num-1][j] + "\n";
                        }
                        Console.WriteLine(msg);
                        input = MenuInputParse();
                        if (Int32.TryParse(input, out int num2))
                        {
                            type = Tool.allTypes[i][num2-1];
                            return (new string[] {category, type});
                        }
                        else
                        {
                            Console.WriteLine("Please Enter a number between 1 and {0} (Remember: H,Q and L can be used to return to home, quit, or logout!)", Tool.allTypes[i].Length);
                            input = MenuInputParse();
                        }
                    }
                    Console.WriteLine("Please Enter a number between 1 and {0} (Remember: H,Q and L can be used to return to home, quit, or logout!)", 9);
                    input = MenuInputParse();
                }
                else
                {
                    Console.WriteLine("Please Enter a number between 1 and {0} (Remember: H,Q and L can be used to return to home, quit, or logout!)", 9);
                    input = MenuInputParse();
                }
            }
        }

        /// <summary>
        /// displays the staff menu
        /// </summary>
        public static void StaffMenu()
        {
            string msg = "\nWould you like to:\nAdd a New Tool - 1 \nAdd new Pieces of a Tool - 2\nRemove Pieces of a Tool - 3\nRegeister a Member - 4\nRemove a Member - 5\nGet Members Contact Info - 6";
            List<Action> methods = (new Action[6] { AddToolMenu, AddQuantityMenu, RemoveQuantityMenu, AddMemberMenu, RemoveMemberMenu, ContactMenu}).ToList();
            DisplayOptions(msg, methods.ToList());
        }

        /// <summary>
        /// displays the mdmber menu
        /// </summary>
        public static void MemberMenu()
        {
            string msg = "\nWould you like to:\nDisplay Tool Info - 1 \nBorrow a Tool - 2\nReturn a Tool - 3\nList your Loaned Tools - 4\nVeiw Popular Tools - 5";
            List<Action> methods = (new Action[5] { ToolInfoMenu, LoanMenu, ReturnMenu, ViewLoanedMenu, HotToolsInYourAreaMenu }).ToList();
            DisplayOptions(msg, methods.ToList());
        }

        /// <summary>
        /// displays tool information
        /// </summary>
        public static void ToolInfoMenu()
        {
            string[] combined = selectCategory();
            string category = combined[0];
            string type = combined[1];
           
            toolLibrarySystem.displayTools(type);

            MainMenu();
        }

        /// <summary>
        /// provides and interface for users to loan a tool
        /// </summary>
        public static void LoanMenu()
        {
            selectCategory();
            Console.WriteLine("Please Enter the Full Name of the tool you wish to loan");
            
            toolLibrarySystem.borrowTool(currentUser, toolLibrarySystem.searchTool(MenuInputParse()));

            Console.WriteLine("Tool Loaned!");

            MainMenu();
        }

        /// <summary>
        /// menu to return a loaned tool
        /// </summary>
        public static void ReturnMenu()
        {
            Console.WriteLine("Please Enter the Full Name of the tool you wish to return");

            toolLibrarySystem.returnTool(currentUser, toolLibrarySystem.searchTool(MenuInputParse()));

            Console.WriteLine("Tool Returned!");

            MainMenu();
        }

        /// <summary>
        /// displays the tools currently loaned by the user
        /// </summary>
        public static void ViewLoanedMenu()
        {
            Console.WriteLine("Currently loaned tools are:");
            toolLibrarySystem.displayBorrowingTools(currentUser);

            MainMenu();
        }

        /// <summary>
        /// displays the top the most borrowed tools
        /// </summary>
        public static void HotToolsInYourAreaMenu()
        {
            toolLibrarySystem.displayTopThree();

            MainMenu();
        }

        /// <summary>
        /// allows staff to add a new tool to the system
        /// </summary>
        public static void AddToolMenu()
        {
            string[] combined = selectCategory();
            string category = combined[0];
            string type = combined[1];
            Console.WriteLine("Please Enter the Following details about the Tool:");
            Console.WriteLine("Name: ");
            string Name = MenuInputParse();
            Console.WriteLine("Quantity: ");
            int quantity = checkInput.checkInt(MenuInputParse());

            toolLibrarySystem.add(new Tool(Name, quantity, category, type));

            Console.WriteLine("Success");

            MainMenu();
        }

        /// <summary>
        /// allows staff to add additional quantity of a tool
        /// </summary>
        public static void AddQuantityMenu()
        {
            selectCategory();
            Console.WriteLine("Please Enter Name of the Tool: ");
            iTool tool = toolLibrarySystem.searchTool(MenuInputParse());
            
            Console.WriteLine("Quantity to Add: ");
            int quantity = checkInput.checkInt(MenuInputParse());

            toolLibrarySystem.add(tool, quantity);

            Console.WriteLine("Success");

            MainMenu();
        }

        /// <summary>
        /// allows staff to remove additional quantity of a tool
        /// </summary>
        public static void RemoveQuantityMenu()
        {
            selectCategory();
            Console.WriteLine("Please Enter Name of the Tool: ");
            iTool tool = toolLibrarySystem.searchTool(MenuInputParse());

            Console.WriteLine("Quantity to Remove: ");
            int quantity = checkInput.checkInt(MenuInputParse());

            toolLibrarySystem.add(tool, -quantity);

            Console.WriteLine("Success");

            MainMenu();
        }

        /// <summary>
        /// allows staff to add a new member to the system
        /// </summary>
        public static void AddMemberMenu()
        {
            Console.WriteLine("Please Enter the Following details about the Member:");
            Console.WriteLine("First Name: ");
            string FirstName = MenuInputParse();
            Console.WriteLine("Last Name: ");
            string LastName = MenuInputParse();
            Console.WriteLine("Phone #: ");
            string phone = MenuInputParse();
            Console.WriteLine("PIN: ");
            string pin = MenuInputParse();
            
            toolLibrarySystem.add(new Member(FirstName, LastName, pin, phone));
            Console.WriteLine("Member Added Succesufully");

            MainMenu();

        }

        /// <summary>
        /// allows staff to remove a member from the system
        /// </summary>
        public static void RemoveMemberMenu()
        {
            Console.WriteLine("Please Enter the Following details about the Member:");
            Console.WriteLine("First Name: ");
            string FirstName = MenuInputParse();
            Console.WriteLine("Last Name: ");
            string LastName = MenuInputParse();

            iMember member = memberCollection.SearchMember(FirstName, LastName);

            toolLibrarySystem.delete(member);
            Console.WriteLine("Member Removed Succesufully");

            MainMenu();
        }

        /// <summary>
        /// displays contact info for a member
        /// </summary>
        public static void ContactMenu()
        {
            Console.WriteLine("Please Enter the Following details about the Member:");
            Console.WriteLine("First Name: ");
            string FirstName = MenuInputParse();
            Console.WriteLine("Last Name: ");
            string LastName = MenuInputParse();

            iMember member = memberCollection.SearchMember(FirstName, LastName);

            Console.WriteLine(member.ToString());

            MainMenu();
        }

        /// <summary>
        /// provieds a method to quit the program
        /// </summary>
        static public void Quit()
        {
            //check user wishes to exit
            Console.WriteLine("Are you sure you want to exit? (y/n)");
            string input = MenuInputParse();

            if (input == "Y" | input == "y")
            {
                //Close window
                Environment.Exit(0);
            }
            else if (input == "n" | input == "N")
            {
                //return to main menu
                MainMenu();
            }
            else
            {
                Console.WriteLine("Please enter \"y\" or \"n\"");
            }


        }

    }
    class Login : FrontEnd
    {
        static public bool loggedIn = false; //global to check is user is logged in

        //staff details
        static readonly string staffUsername = "staff";
        static readonly string staffPassword = "today123";

        
        /// <summary>
        /// Displays login prompt and returns the entered details 
        /// </summary>
        public static Dictionary<string, string> displayMsg()
        {
            var loginDetails = new Dictionary<string, string>
            {
                { "u", "" },
                { "p", "" }
            };
            Console.WriteLine("Please Enter your Username: ");
            loginDetails["u"] = MenuInputParse();
            Console.WriteLine("Please Enter your Password:");
            loginDetails["p"] = MenuInputParse();

            return loginDetails;

        }

        /// <summary>
        /// staff login check
        /// </summary>
        static public void staff()
        {
            while (!loggedIn)
            {
                Dictionary<string, string> loginDetails = displayMsg();
                if (loginDetails["u"] == staffUsername)
                {
                    if (loginDetails["p"] == staffPassword)
                    {
                        loggedIn = true;
                        StaffMenu();
                    }
                    else
                    {
                        Console.Write("Username Inncorrect, Please try again\n");
                    }
                }
                else
                {
                    Console.Write("Username Inncorrect, Please try again\n");
                }
            }
        }

        /// <summary>
        /// member login check
        /// </summary>
        static public void member()
        {
            while (!loggedIn) //while the user isnt logged in
            {
                Dictionary<string, string> loginDetails = displayMsg();
                loggedIn = true;
                currentUser = toolLibrarySystem.searchMember(loginDetails["u"], loginDetails["p"]); //seardh for member in current collection
                MemberMenu();
            }
        }
    }

    public class typeSelctor
    {
        void gardenType()
        {
            string[] types = Tool.allTypes[1];
            string msg = "Please Select one of the following:";
            int i = 0;
            foreach (string type in types)
            {
                msg += "\n" + i + " - " + type;
            }

        }
    }

    class FrontEndHelpers : ToolLibrarySystem
    {
        public class checkInput
        {
            /// <summary>
            /// check string is valid and convert to int
            /// </summary>
            /// <param name="trans"></param>
            /// <returns></returns>
            public static int checkInt(string input)
            {
                int output;
                while (!Int32.TryParse(input, out output))
                {
                    Console.WriteLine("Please enter a number");
                    input = MenuInputParse();
                }

                return output;
            }
        }
        /// <summary>
        //check input to see if user wants to return to menu, logout or quit
        /// </summary>
        static public string MenuInputParse()
        {
            string input = Console.ReadLine();
            if (input == "Q" | input == "q")
            {
                FrontEnd.Quit();
            }
            else if (input == "H" | input == "h")
            {
                FrontEnd.MainMenu();
            }
            else if (input == "L" | input == "l")
            {
                Login.loggedIn = false;
                currentUser = null;
                FrontEnd.MainMenu();
            }
            return input;
        }

        /// <summary>
        // helper method to display the menu options
        /// </summary>
        static public void DisplayOptions(string message, List<Action> methods)
        {
            int numMethods = methods.Count;

            Console.WriteLine(message);

            string input = MenuInputParse();

            while (true)
            {
                if (Int32.TryParse(input, out int num))
                {
                    for (int i = 0; i <= numMethods; i++)
                    {
                        if (num == i + 1)
                        {
                            methods[i].Invoke();
                            return;
                        }
                    }
                    Console.WriteLine("Please Enter a number between 1 and {0} (Remember: H,Q and L can be used to return to home, quit, or logout!)", numMethods);
                    input = MenuInputParse();
                }
                else
                {
                    Console.WriteLine("Please Enter a number between 1 and {0} (Remember: H,Q and L can be used to return to home, quit, or logout!)", numMethods);
                    input = MenuInputParse();
                }
            }
        }
    }
}
