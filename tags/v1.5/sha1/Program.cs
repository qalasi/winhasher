/* Program.cs
 * 
 * PROGRAMMER:    Jeffrey T. Darlington
 * DATE:          August 31, 2007
 * PROJECT:       WinHasher SHA1 console application
 * .NET VERSION:  2.0
 * REQUIRES:      com.gpfcomics.WinHasher.Core
 * REQUIRED BY:   (None)
 * 
 * The command-line version of WinHasher SHA1.  This console app computes the SHA1 hash of one or
 * more files.  In single file mode, it prints out the hash to the screen.  In multi-file mode,
 * it compares the hash of each file to the hashes of the others:  if all match, all the files
 * are said to be the same; if one or more do not match, the entire batch fails.   This is
 * supposed to be roughly compatible to something like OpenSSL's command-line SHA1 digest option.
 * See the usage method for details on using this program.  For more hashing algorithms, use the
 * command-line "hash" program or the full WinHasher GUI.
 *  
 * UPDATE June 19, 2008 (1.3):  Added -base64 switch and Base64 output option.
 * 
 * UPDATE February 12, 2009 (1.4):  Added -hexcaps switch and all-caps hexadcimal output, as well
 * as -bubbab and Bubble Babble.
 * 
 * This program is Copyright 2009, Jeffrey T. Darlington.
 * E-mail:  jeff@gpf-comics.com
 * Web:     http://www.gpf-comics.com/
 * 
 * This program is free software; you can redistribute it and/or modify it under the terms of
 * the GNU General Public License as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See theGNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License along with this program;
 * if not, write to the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
 * Boston, MA  02110-1301, USA.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using com.gpfcomics.WinHasher.Core;

namespace com.gpfcomics.WinHasher.sha1console
{
    class Program
    {
        // Get our version number from the assembly:
        private static string version = "WinHasher SHA1 v. " +
            Assembly.GetExecutingAssembly().GetName().Version.ToString();

        // Our main method, which is pretty simple:
        static void Main(string[] args)
        {
            // Set the console title for a little bit of advertising:
            Console.Title = version;
            // If called with no command-line arguments, print out the usage statement:
            if (args.Length == 0)
            {
                Usage();
                return;
            }
            // Default to hexadecimal output:
            OutputType outputType = OutputType.Hex;
            // Look to see if we got the Base64 flag and, if so, turn it on:
            while (args.Length > 0 && args[0].StartsWith("-"))
            {
                if (args[0].ToLower() == "-base64") outputType = OutputType.Base64;
                else if (args[0].ToLower() == "-hexcaps") outputType = OutputType.CapHex;
                else if (args[0].ToLower() == "-bubbab") outputType = OutputType.BubbleBabble;
                string[] args2 = new string[args.Length - 1];
                Array.Copy(args, 1, args2, 0, args.Length - 1);
                args = args2;
            }
            // Test again for files:
            if (args.Length == 0)
            {
                Console.WriteLine();
                Console.WriteLine("ERROR:  No files specified, nothing to do");
                Usage();
                return;
            }
            // Treat all arguments as file paths.  If only one argument is specified, assume
            // we are to read in that file, compute the SHA1 hash, and spit out the hex dump
            // to the screen.
            else if (args.Length == 1)
            {
                // We could throw some exceptions here, so ignore Yoda's advice and give
                // it a try:
                try
                {
                    // This should be simple enough:
                    Console.WriteLine();
                    Console.WriteLine(HashEngine.SHA1HashFile(args[0], outputType));
                }
                #region Catch Exceptions
                // Our hash engine can throw its own exceptions, which usually are just other
                // exceptions wrapped in our own message format.  We'll look for those first
                // and foremost:
                catch (HashEngineException hee)
                {
                    Console.WriteLine();
                    Console.WriteLine("ERROR: " + hee.Message);
                    Usage();
                }
                // Console.WriteLine() can throw this one:
                catch (IOException)
                {
                    Console.WriteLine();
                    Console.WriteLine("ERROR: An unknown I/O error has occured.");
                    Usage();
                }
                // A catch-all to handle anything else:
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("ERROR: " + ex.ToString());
                    Usage();
                }
                #endregion
            }
            // If we receive more than one argument, will treat each one as a file path and
            // reach each one in turn, computing its hash.  The hash of each file is compared
            // against the hash of the first file.  If all the hashes match, we'll print out
            // a happy congratulatory message.  If just one of the hashes doesn't match the
            // others, we say the whole batch fails.
            else
            {
                try
                {
                    if (HashEngine.CompareSHA1Hashes(args))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Congratulations!  All " + args.Length + " files match!");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("WARNING! One or more of these " + args.Length + " files do not match!");
                    }
                }
                #region Catch Exceptions
                // Same as above:
                catch (HashEngineException hee)
                {
                    Console.WriteLine();
                    Console.WriteLine("ERROR: " + hee.Message);
                    Usage();
                }
                catch (IOException)
                {
                    Console.WriteLine();
                    Console.WriteLine("ERROR: An unknown I/O error has occured.");
                    Usage();
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("ERROR: " + ex.ToString());
                    Usage();
                }
                #endregion
            }
        }

        // Print out our usage statement. The primary purpose of this is to help the user to learn
        // how to use the program.
        static void Usage()
        {
            // Get our copyright information.  It seems a bit silly to do it this way,
            // but this seems to be the only way to do it that I can find.  We'll pull this
            // from the assembly so we only need to change it in one place, and it can be
            // automatically fetched from SVN.
            string copyright = null;
            object[] obj = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            if (obj != null && obj.Length > 0)
                copyright = ((AssemblyCopyrightAttribute)obj[0]).Copyright;
            Console.WriteLine();
            Console.WriteLine(version);
            if (copyright != null) Console.WriteLine(copyright);
            Console.WriteLine("http://www.gpf-comics.com/dl/winhasher/");
            Console.WriteLine();
            //*****************123456789012345678901234567890123456789012345678901234567890123456789012345
            Console.WriteLine("Usage: sha1 [-base64|-hexcaps|-bubbab] filename1 [filename2 ...]");
            Console.WriteLine();
            Console.WriteLine("WinHasher SHA-1 is a command-line SHA-1 cryptographic hash generator for files.");
            Console.WriteLine("It runs in one of two modes:  single file hashing and multi-file comparison.");
            Console.WriteLine();
            Console.WriteLine("In single file mode, WinHasher computes the SHA-1 hash of the given file and");
            Console.WriteLine("prints it to the screen.  The \"-base64\" switch causes WinHasher to output");
            Console.WriteLine("hashes in MIME Base64 (RFC 2045) format rather than hexadecimal, \"-hexcaps\"");
            Console.WriteLine("outputs hexadecimal with all capital letters, and \"-bubbab\" uses Bubble");
            Console.WriteLine("Babble encoding.");
            Console.WriteLine();
            Console.WriteLine("In multi-file comparison mode, WinHasher computes the SHA-1 hash for each file");
            Console.WriteLine("given and compares the results.  If the hash of every file matches, then all");
            Console.WriteLine("files in the batch are declared to be the same.  If one or more hashes do not");
            Console.WriteLine("match the others, a warning will be displayed indicating as such.  In this");
            Console.WriteLine("way, you can determine whether two or more files share the same contents");
            Console.WriteLine("despite file name, path, and modification time differences.");
        }
    }
}