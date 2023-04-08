﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment
{
    //The specification of ToolCollection ADT, which is used to store and manipulate a collection of tools
    interface iToolCollection
    {
        int Number // get the number of the types of tools in the community library
        {
            get;
        }
        void add(iTool aTool); //add a given tool to this tool collection
        void delete(iTool aTool); //delete a given tool from this tool collection
        Boolean search(iTool aTool); //search a given tool in this tool collection. Return true if this tool is in the tool collection; return false otherwise
        public iTool searchTool(string name); //search for a tool by name
        iTool[] toArray(); // output the tools in this tool collection to an array of iTool
        public iTool[] toSortedArray(); //output the memebers in this collection to a sorted array of iTool
        void SortByNoBorrowers(); //return a list of all tools, sorted by how many times they've been borrowed
    }
}