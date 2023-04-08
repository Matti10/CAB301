using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Net;

namespace Assignment
{
    class MemberCollection : iMemberCollection
    {

        //Define Members List
        List<iMember> Members = new List<iMember>();

        // get the number of members in the community library
        public int Number { get { return Members.Count; } }

        ///<summary>
        ///add a new member to this member collection, make sure there are no duplicates in the member collection
        ///</summary>
        public void add(iMember aMember)
        {
            Members.Add(aMember);
        }

        ///<summary>
        ///delete a given member from this member collection, a member can be deleted only when the member currently is not holding any tool
        ///</summary>
        public void delete(iMember aMember)
        {
            Members.Remove(aMember);
        }

        ///<summary>
        ///search a given member in this member collection. Return true if this memeber is in the member collection; return false otherwise.
        ///</summary>
        public Boolean search(iMember aMember)
        {
            if (Members.BinarySearch(aMember) >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        ///<summary>
        /// check a member exists using thier name
        ///</summary>
        public bool SearchMemberName(string firstname, string lastname)
        {
            if (Searching.binaryFullName(toSortedArray(), 0, Members.Count + 1, firstname, lastname) >= 0)
            {
                return true;
            }

            return false;

        }

        ///<summary>
        /// search for a member using thier name 
        ///</summary>
        public iMember SearchMember(string firstname, string lastname)
        {
            
            return toSortedArray()[Searching.binaryFullName(toSortedArray(), 0, Members.Count - 1, firstname, lastname)];
        }



        ///<summary>
        // output the memebers in this collection to an array of iMember
        ///</summary>
        public iMember[] toArray()
        {
            return Members.ToArray();
        }

        ///<summary>
        /// output the memebers in this collection to an array of iMember - Sorted by member name
        ///</summary>
        public iMember[] toSortedArray()
        {
            return Sorting.MergeSortFullName.mergeSort(Members.ToArray(), 0, Members.Count - 1);
        }



        public class Sorting
        {
            //this algorithm is a modified version of the merge mergeSort algorithm published by "GeeksforGeeks". URL: https://www.geeksforgeeks.org/merge-mergeSort/
            public class MergeSortFirstName
            {        
                ///<summary>
                /// merge two arrays
                ///</summary>
                static void mergeArrs(iMember[] unsortedArr, int left, int middle, int right)
                {
                    // Find sizes of two
                    // subarrays to be merged
                    int len1 = middle - left + 1;
                    int len2 = right - middle;

                    // Create temp arrays
                    iMember[] leftArray = new iMember[len1];
                    iMember[] rightArray = new iMember[len2];
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
                        if (String.Compare(leftArray[i].FirstName, rightArray[j].FirstName) > 0)
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
                /// Merge sort the given array by firstname
                ///</summary>
                public static iMember[] mergeSort(iMember[] unsortedArr, int left, int right)
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

            public class MergeSortLastName
            {
                
                ///<summary>
                /// merge two arrays
                ///</summary>
                static void mergeArrs(iMember[] unsortedArr, int left, int middle, int right)
                {
                    // Find sizes of two
                    // subarrays to be merged
                    int len1 = middle - left + 1;
                    int len2 = right - middle;

                    // Create temp arrays
                    iMember[] leftArray = new iMember[len1];
                    iMember[] rightArray = new iMember[len2];
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
                        //if (leftArray[i].LastName <= rightArray[j].LastName)
                        if (String.Compare(leftArray[i].LastName, rightArray[j].LastName) > 0)
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
                /// Merge sort the given array by lastname
                ///</summary>
                public static iMember[] mergeSort(iMember[] unsortedArr, int left, int right)
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

            public class MergeSortFullName
            { //COULD ALSO BE IMPLEMENTED BY COMBINEING FIRST AND LAST NAME INTO A "FULLMANE" AND COMPARING
            
                ///<summary>
                /// merge arrays
                ///</summary>
                static void mergeArrs(iMember[] unsortedArr, int left, int middle, int right)
                {
                    // Find sizes of two
                    // subarrays to be merged
                    int len1 = middle - left + 1;
                    int len2 = right - middle;

                    // Create temp arrays
                    iMember[] leftArray = new iMember[len1];
                    iMember[] rightArray = new iMember[len2];
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
                        //if (leftArray[i].LastName <= rightArray[j].LastName)
                        if (String.Compare(leftArray[i].LastName, rightArray[j].LastName) == 0)
                        {
                            //if lastnames are the same, sort by firstnames
                            if (String.Compare(leftArray[i].FirstName, rightArray[j].FirstName) > 0)
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
                        else if (String.Compare(leftArray[i].LastName, rightArray[j].LastName) > 0)
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
                /// Merge sort the given array by fullname
                ///</summary>
                public static iMember[] mergeSort(iMember[] unsortedArr, int left, int right)
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
            /// perform a binary search using the members fullname
            ///</summary>
            public static int binaryFullName(iMember[] inputArray, int leftIndex, int rightIndex, string firstname, string lastname)
            {
                try
                {
                    if (rightIndex >= leftIndex)
                    {
                        int middleIndex = leftIndex + (rightIndex - 1) / 2;

                        int compValue = String.Compare(inputArray[middleIndex].LastName + inputArray[middleIndex].FirstName, lastname + firstname);

                        if (compValue == 0)
                        {
                            return middleIndex;
                        }
                        else if (compValue < 0)
                        {
                            return binaryFullName(inputArray, leftIndex, middleIndex - 1, firstname, lastname);
                        }
                        else if (compValue > 0)
                        {
                            return binaryFullName(inputArray, middleIndex, rightIndex, firstname, lastname);
                        }
                    }

                    return -1; // value not found2
                }
                catch
                {
                    Console.WriteLine("Search Failed, please try again");
                    return -1;
                }

            }
                
        }
    }
}
