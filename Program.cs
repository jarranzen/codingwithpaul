﻿using System;
using System.IO;

namespace SearchFile
{
    static class Stuff
    {
        public static void Main()
        {
            /* Script that takes in a directory, a pattern which determines
             * the files to search inside of, and a search term: it produces
             * a list of matching lines from within the matching files inside
             * of the given directory
             */

            // TODO: Put a simple GUI around it: select file, write search term etc
            // TODO: verify directory: exists? OK. underneath current 
            //     : directory? append... in_home_directory? Also OK.
            // TODO: remember these choices from last time used? (Text file??)
            // TODO: colour the output appropriately

            // (Where is this thing running?)
            // Console.WriteLine(Environment.CurrentDirectory);

            string SearchTerm = " wegen";
            Console.Write($"Search for what? (Hit return to use '{SearchTerm}') : ");
            string CandidateSearch = Console.ReadLine();
            if (CandidateSearch.Length > 0)
            {
                SearchTerm = CandidateSearch;
            }

            // string SearchDir = Environment.CurrentDirectory; // Where this thing is running!

            string SearchDir = "/Users/pmccann/Dropbox/german";
            Console.Write($"In which directory? (Hit return to use current directory) : ");
            string CandidateDir = Console.ReadLine();
            if (CandidateDir.Length > 0)
            {
                SearchDir = CandidateDir.TrimEnd(' '); // Delete any extra space (eg from drag'n'drop)
            }

            string FileMatching = "*.txt";
            Console.Write($"Type of files to search? (Hit return for '{FileMatching}') : ");
            string CandidateType = Console.ReadLine();
            if (CandidateType.Length > 0)
            {
                FileMatching = CandidateType;
            }

            Console.WriteLine($"Searching for '{SearchTerm}' within the directory {SearchDir}\n");

            // TODO: Check that directory exists...
            // TODO: Catch access problems... (suggest sudo)?

          

            int TotalHitCount = 0; // Total number of lines the search term is found in
            int GlobalFileCount = 0; // Total number of files in which the term appears

            foreach (var file in Directory.EnumerateFiles(SearchDir,FileMatching,SearchOption.AllDirectories))
            {
                int LineCount = 0; // The line number (to show where a file matches)
                int FileCount = 0; // The number of times a given file matches the search term

                foreach (var line in File.ReadLines(file))
                {
                    LineCount++; // So the first line of the file reports as "Line 1", etc

                    if (line.Contains(SearchTerm))
                    {
                        if (FileCount == 0) {
                            Console.WriteLine($"\n{file}\n================================================");
                        }
                        FileCount++; // One more in this file
                        TotalHitCount++; // One more overall

                        Console.WriteLine($"line {LineCount} : {line}"); // Report the find
                    }
                }
                if (FileCount > 0) // We found something in *this* file
                {
                    Console.WriteLine($"A total of {FileCount} lines found\n================================================");
                    GlobalFileCount++;
                    // TODO: Make this "line" if there's only one
                }
            }
            if (TotalHitCount == 0) // Nothing found matching search term in any file we examined
            {
                Console.WriteLine($"No occurrences of {SearchTerm} found in {SearchDir}");
            } else
            {
                Console.WriteLine($"A total of {TotalHitCount} occurrences in {GlobalFileCount} files");
            }
        }

    }
    /* Here's how LINQ could be used to search... But we don't use this
           * instead using the built-in capability
           *
           *  var files = from file in Directory.EnumerateFiles(SearchDir, FileMatching, SearchOption.AllDirectories)
           *          from line in File.ReadLines(file)
           *           where line.Contains(SearchTerm)
           *           select new { myline = line, myfile = file };
           */
}
