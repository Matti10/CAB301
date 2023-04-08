using System;
using System.Collections.Generic;

namespace Assignment
{
    class Tool : iTool
    {
        // Initialise Varibles
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int NoBorrowings { get; set; }
        public int AvailableQuantity { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }

        List<iMember> Borrowers = new List<iMember>();

        static public string[] validCategories = { "Gardening", "Flooring", "Fencing", "Cleaning", "Painting", "Electronic", "Electricity", "Measuring", "Automotive" };

        public static string[][] allTypes = {
            new string[] {
                "Line Trimmers",
                "Lawn Mowers",
                "Hand Tools",
                "Wheelbarrows",
                "Garden Power Tools",
            },

            new string[] {
                "Scrapers",
                "Floor Lasers",
                "Floor Levelling Tools",
                "Floor Levelling Materials",
                "Floor Hand Tools",
                "Tiling Tools"
            },


            new string[] {
                "Hand Tools",
                "Electric Fencing",
                "Steel Fencing Tools",
                "Power Tools",
                "Fencing Accessories"
            },

            new string[] {
                "Draining",
                "Car Cleaning",
                "Vacuum",
                "Pressure Cleaners",
                "Pool Cleaning",
                "Floor Cleaning",
            },

            new string[] {
                "Sanding Tools",
                "Brushes",
                "Rollers",
                "Paint Removal Tools",
                "Paint Scrapers",
                "Sprayers",
            },

            new string[] {
                "Voltage Tester",
                "Oscilloscopes",
                "Thermal Imaging",
                "Data Test Tool",
                "Insulation Testers",
            },

            new string[] {
                "Test Equipment",
                "Safety Equipment",
                "Basic Hand tools",
                "Circuit Protection",
                "Cable Tools",
            },

            new string[] {
                "Distance Tools",
                "Laser Measurer",
                "Measuring Jugs",
                "Temperature & Humidity Tools",
                "Levelling Tools",
                "Markers",
            },

            new string[] {
                "Jacks",
                "Air Compressors",
                "Battery Chargers",
                "Socket Tools",
                "Braking",
                "Drivetrain",
            }
        };

        ///<summary>
        ///Constructor Func
        ///</summary>
        public Tool(string name, int quantity, string category, string type)
        {
            Name = name;
            Quantity = quantity;
            AvailableQuantity = quantity;
            Category = category;
            Type = type;
            NoBorrowings = 0;
        }

        ///<summary>
        /// get all the members who are currently holding this tool
        ///</summary>
        public iMemberCollection GetBorrowers
        {
            get;
        }

        ///<summary>
        ///add a member to the borrower list
        ///</summary>
        public void addBorrower(iMember aMember)
        {
            Borrowers.Add(aMember);
            NoBorrowings++;
        }

        ///<summary>
        ///delete a member from the borrower list
        ///</summary>
        public void deleteBorrower(iMember aMember)
        {
            Borrowers.Remove(aMember);
        }

        ///<summary>
        ///return a string containning the name and the available quantity quantity this tool 
        ///</summary>
        public override string ToString()
        {
            return "Name: " + Name + "\nAvailable Quantity(Out of Total Quantity): " + AvailableQuantity.ToString() + "/" + Quantity.ToString() + "\nBorrowed #: " + NoBorrowings.ToString();
        }

        public static bool checkType(string category, string type)
        {
            foreach (string validCategory in validCategories)
            {
                if (category == validCategory)
                {
                    foreach (string[] validType in allTypes)
                    {
                        foreach (string Type in validType)
                            if (Type == type)
                            {
                                return true;
                            }
                    }
                }
            }
            return false;
        }

    }
}
    