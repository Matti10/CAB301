using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Xml.Serialization;

namespace Assignment
{
    class ToolCollection : iToolCollection
    {
        // Initialise Varibles
        public int Number { get; set; } = Tools.Count;
        static List<iTool> Tools = new List<iTool>();
        
        ///<summary>
        ///add a new tool to this tool collection, make sure there are no duplicates in the tool collection
        ///</summary>
        public void add(iTool aTool)
        {
            Tools.Add(aTool);
        }

        ///<summary>
        ///delete a given tool from this tool collection, a tool can be deleted only when the tool currently is not holding any tool
        ///</summary>
        public void delete(iTool aTool)
        {
            Tools.Remove(aTool);
        }

        ///<summary>
        ///search a given tool in this tool collection. Return true if this memeber is in the tool collection; return false otherwise.
        ///</summary>
        public Boolean search(iTool aTool)
        {
            if (Tools.BinarySearch(aTool) >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        ///<summary>
        ///search tool collection for a tool by the tools name, and return the tool
        ///</summary>
        public iTool searchTool(string name)
        {
            iTool[] sortedArray = toSortedArray();
            return sortedArray[Searching.binaryName(sortedArray, 0, Tools.Count + 1, name)];
        }

        ///<summary>
        ///output the memebers in this collection to an array of iTool
        ///</summary>
        public iTool[] toArray()
        {
            return Tools.ToArray();
        }

        ///<summary>
        ///output the memebers in this collection to a sorted array of iTool
        ///</summary>
        public iTool[] toSortedArray()
        {
            return Sorting.MergeSortName.mergeSort(Tools.ToArray(), 0, Tools.Count - 1);
        }

        ///<summary>
        ///return a list of all tools, sorted by how many times they've been borrowed
        ///</summary>
        public void SortByNoBorrowers()
        {
            Tools = Sorting.MergeSortNoBorrowings.mergeSort(toArray(), 0, toArray().Length - 1).ToList();
        }

        public class Sorting
        {
            //this algorithm is a modified version of the merge mergeSort algorithm published by "GeeksforGeeks". URL: https://www.geeksforgeeks.org/merge-mergeSort/
            public class MergeSortNoBorrowings
            {
                
                ///<summary>
                /// Merge/Split arrays to sort by number of borrowings
                ///</summary>
                static void mergeArrs(iTool[] unsortedArr, int left, int middle, int right)
                {
                    // Find sizes of two
                    // subarrays to be merged
                    int len1 = middle - left + 1;
                    int len2 = right - middle;

                    // Create temp arrays
                    iTool[] leftArray = new iTool[len1];
                    iTool[] rightArray = new iTool[len2];
                    int i, j;

                    // Copy data to temp arrays
                    for (i = 0; i < len1; ++i)
                        leftArray[i] = unsortedArr[left + i];
                    for (j = 0; j < len2; ++j)
                        rightArray[j] = unsortedArr[middle + 1 + j];

                    // Merge the temp arrays

                    // Initial indexes of first
                    // and second subarrays
                    i = 0;
                    j = 0;

                    // Initial index of merged
                    // subarry array
                    int k = left;
                    while (i < len1 && j < len2)
                    {
                        if (leftArray[i].NoBorrowings <= rightArray[j].NoBorrowings)
                        {
                            unsortedArr[k] = leftArray[i];
                            i++;
                        }
                        else
                        {
                            unsortedArr[k] = rightArray[j];
                            j++;
                        }
                        k++;
                    }

                    // Copy remaining elements
                    // of leftArray[] if any
                    while (i < len1)
                    {
                        unsortedArr[k] = leftArray[i];
                        i++;
                        k++;
                    }

                    // Copy remaining elements
                    // of rightArray[] if any
                    while (j < len2)
                    {
                        unsortedArr[k] = rightArray[j];
                        j++;
                        k++;
                    }

                }
                
                ///<summary>
                /// merge sort array by number of borrowings
                ///</summary>
                public static iTool[] mergeSort(iTool[] unsortedArr, int left, int right)
                {
                    if (left < right)
                    {
                        // Find the middle
                        // point
                        int middle = left + (right - left) / 2;

                        // Sort first and
                        // second halves
                        mergeSort(unsortedArr, left, middle);
                        mergeSort(unsortedArr, middle + 1, right);

                        // Merge the sorted halves
                        mergeArrs(unsortedArr, left, middle, right);
                         
                    }

                    return unsortedArr; //return the now sorted array
                }
            }

            public class MergeSortName
            {
                
                ///<summary>
                /// split and merge array to sort by name
                ///</summary>
                static void mergeArrs(iTool[] unsortedArr, int left, int middle, int right)
                {
                    // Find sizes of two
                    // subarrays to be merged
                    int len1 = middle - left + 1;
                    int len2 = right - middle;

                    // Create temp arrays
                    iTool[] leftArray = new iTool[len1];
                    iTool[] rightArray = new iTool[len2];
                    int i, j;

                    // Copy data to temp arrays
                    for (i = 0; i < len1; ++i)
                        leftArray[i] = unsortedArr[left + i];
                    for (j = 0; j < len2; ++j)
                        rightArray[j] = unsortedArr[middle + 1 + j];

                    // Merge the temp arrays

                    // Initial indexes of first
                    // and second subarrays
                    i = 0;
                    j = 0;

                    // Initial index of merged
                    // subarry array
                    int k = left;
                    while (i < len1 && j < len2)
                    {
                        if (String.Compare(leftArray[i].Name, rightArray[j].Name) > 0)
                        {
                            unsortedArr[k] = leftArray[i];
                            i++;
                        }
                        else
                        {
                            unsortedArr[k] = rightArray[j];
                            j++;
                        }
                        k++;
                    }

                    // Copy remaining elements
                    // of leftArray[] if any
                    while (i < len1)
                    {
                        unsortedArr[k] = leftArray[i];
                        i++;
                        k++;
                    }

                    // Copy remaining elements
                    // of rightArray[] if any
                    while (j < len2)
                    {
                        unsortedArr[k] = rightArray[j];
                        j++;
                        k++;
                    }

                }
                
                ///<summary>
                /// Merge sort by tool name
                ///</summary>
                public static iTool[] mergeSort(iTool[] unsortedArr, int left, int right)
                {
                    if (left < right)
                    {
                        // Find the middle
                        // point
                        int middle = left + (right - left) / 2;

                        // Sort first and
                        // second halves
                        mergeSort(unsortedArr, left, middle);
                        mergeSort(unsortedArr, middle + 1, right);

                        // Merge the sorted halves
                        mergeArrs(unsortedArr, left, middle, right);

                    }

                    return unsortedArr; //return the now sorted array
                }
            }

            public class MergeSortType
            {
                
                ///<summary>
                /// split and merge arrays accordingl to type for merge sort
                ///</summary>
                static void mergeArrs(iTool[] unsortedArr, int left, int middle, int right)
                {
                    // Find sizes of two
                    // subarrays to be merged
                    int len1 = middle - left + 1;
                    int len2 = right - middle;

                    // Create temp arrays
                    iTool[] leftArray = new iTool[len1];
                    iTool[] rightArray = new iTool[len2];
                    int i, j;

                    // Copy data to temp arrays
                    for (i = 0; i < len1; ++i)
                        leftArray[i] = unsortedArr[left + i];
                    for (j = 0; j < len2; ++j)
                        rightArray[j] = unsortedArr[middle + 1 + j];

                    // Merge the temp arrays

                    // Initial indexes of first
                    // and second subarrays
                    i = 0;
                    j = 0;

                    // Initial index of merged
                    // subarry array
                    int k = left;
                    while (i < len1 && j < len2)
                    {
                        if (String.Compare(leftArray[i].Type, rightArray[j].Type) > 0)
                        {
                            unsortedArr[k] = leftArray[i];
                            i++;
                        }
                        else
                        {
                            unsortedArr[k] = rightArray[j];
                            j++;
                        }
                        k++;
                    }

                    // Copy remaining elements
                    // of leftArray[] if any
                    while (i < len1)
                    {
                        unsortedArr[k] = leftArray[i];
                        i++;
                        k++;
                    }

                    // Copy remaining elements
                    // of rightArray[] if any
                    while (j < len2)
                    {
                        unsortedArr[k] = rightArray[j];
                        j++;
                        k++;
                    }

                }
                
                ///<summary>
                /// Merge sort by type
                ///</summary>
                public static iTool[] mergeSort(iTool[] unsortedArr, int left, int right)
                {
                    if (left < right)
                    {
                        // Find the middle
                        // point
                        int middle = left + (right - left) / 2;

                        // Sort first and
                        // second halves
                        mergeSort(unsortedArr, left, middle);
                        mergeSort(unsortedArr, middle + 1, right);

                        // Merge the sorted halves
                        mergeArrs(unsortedArr, left, middle, right);

                    }

                    return unsortedArr; //return the now sorted array
                }
            }
        }

        public class Searching
        {
        
            ///<summary>
            /// Binary search using tool name
            ///</summary>
            public static int binaryName(iTool[] inputArray, int leftIndex, int rightIndex, string name)
            {
                try
                { 
                    if (rightIndex >= leftIndex)
                    {
                        int middleIndex = leftIndex + (rightIndex - 1) / 2;

                        int compValue = String.Compare(inputArray[middleIndex].Name, name);

                        if (compValue == 0)
                        {
                            return middleIndex;
                        }
                        else if (compValue < 0)
                        {
                            return binaryName(inputArray, leftIndex, middleIndex - 1, name);
                        }
                        else if (compValue > 0)
                        {
                            return binaryName(inputArray, middleIndex, rightIndex, name);
                        }
                    }

                    return -1; // value not found
                }
                catch
                {
                    Console.WriteLine("Search Failed, please try again");
                    return -1;
                }
            }

            ///<summary>
            /// find all tools with a certain name
            ///</summary>
            public static iTool[] findAllName(iTool[] inputArray, string name)
            {
                return Array.FindAll(Sorting.MergeSortName.mergeSort(inputArray, 0, inputArray.Length - 1), element => element.Name == name);
            }

            ///<summary>
            /// Find all tools of a certain type
            ///</summary>
            public static iTool[] findAllType(iTool[] inputArray, string type)
            {
                return Array.FindAll(Sorting.MergeSortType.mergeSort(inputArray, 0, inputArray.Length - 1), element => element.Type == type);
            }
        }
    }
}


