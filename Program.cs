using System;
using System.Collections.Generic;
using System.IO;

namespace Liar_Liar
{
    class Program
    {
        /// <summary>
        /// Main entry point of application
        /// </summary>
        /// <param name="args">Input file name</param>
        static void Main(string[] args)
        {
            // Pull our file path out of the argument list
            string inputPath = args[0];

            // Does the file exist?
            if (!File.Exists(inputPath))
            {
                Console.WriteLine("Input file does not exist.");
                Environment.Exit(0);
            }

            // Open our file
            StreamReader file = new StreamReader(inputPath);
            
            // Read the number of veteran members
            int snitchCount = Convert.ToInt32(file.ReadLine());

            // Some variables for our data:
            ISet<string> group1 = new HashSet<string>();
            ISet<string> group2 = new HashSet<string>();
            bool firstPerson = true;
            string inputLine;
            int m;

            // Iterate through our veteran members
            for (int memberID = 1; memberID <= snitchCount; memberID++)
            {
                inputLine = file.ReadLine();
                m = Convert.ToInt32(inputLine.Substring(IndexOfM(inputLine), inputLine.Length - IndexOfM(inputLine)));

                IList<string> buffer = new List<string>();
                bool matchGroup1 = false;

                // Iterate through the member's accusations
                for (int accusedID = 1; accusedID <= m; accusedID++)
                {
                    buffer.Add(file.ReadLine());
                }

                // if this is the first veteran, then we can throw them into the first group
                // otherwise, we need to see if they match group 1, or should they get dumped in group 2?
                if (firstPerson)
                {
                    matchGroup1 = true;
                    firstPerson = false;
                }
                else
                {
                    foreach (string accusedName in buffer)
                    {
                        if (group1.Contains(accusedName))
                        {
                            matchGroup1 = true;
                            break;
                        }
                    }
                }

                if (matchGroup1)
                {
                    LoadGroup(buffer, group1);
                }
                else
                {
                    LoadGroup(buffer, group2);
                }
            }

            int largerGroup;
            int smallerGroup;

            if (group1.Count > group2.Count)
            {
                largerGroup = group1.Count;
                smallerGroup = group2.Count;
            }
            else
            {
                largerGroup = group2.Count;
                smallerGroup = group1.Count;
            }

            Console.WriteLine("{0} {1}", largerGroup, smallerGroup);
        }


        static int IndexOfM(string input)
        {
            return input.IndexOfAny("0123456789".ToCharArray());
        }


        static void LoadGroup(IList<string> source, ISet<string> target)
        {
            foreach (string buffer in source)
            {
                target.Add(buffer);
            }
        }
    }
}