using demoOS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoOS
{
    public class fileRelated
    {
        private static DateTime currentTime = DateTime.Now;
        private static DateTime currentDate = DateTime.Now;

        // Display the contents of the files in the 0:\ directory
        public static void fileContents()
        {
            string[] filePaths = Directory.GetFiles(@"0:\");
            var drive = new DriveInfo("0");
            Console.WriteLine("Directory of " + @"0:\");

            // Display the list of file system contents and their total size
            listFile();
            Console.WriteLine($"\n  Total Size: {drive.TotalSize / (1024 * 1024)} MB");
            Console.WriteLine($"  Free Space: {drive.AvailableFreeSpace / (1024 * 1024)} MB free");
            Commands.returnMenu();
        }

        // Add a new .txt file to 0:\ with user-inputted contents
        public static void addTXTfile()
        {
            Console.WriteLine("NOTE: Do not include .txt when typing the name of file.");
            Console.WriteLine("      Type 'done' when finished typing contents.");
            Console.WriteLine("      Type 'cancel' to cancel and return to menu.");

            Console.Write("\nName of File: ");
            var temp_name = Console.ReadLine();

            //Filename validation
            char[] invalidChars = { '\\', '/', ':', '*', '?', '"', '<', '>', '|', '.' };

            foreach (char invalidChar in invalidChars)
            {
                if (temp_name.Contains(invalidChar) || temp_name == "" || temp_name == null)
                {
                    Console.WriteLine("\nInvalid Filename. Avoid using chararacters (\\ / : * ? \" < > | .)");
                    Commands.returnMenu();
                    return;
                }
            }
            // If no invalid characters were found, the filename is considered valid
            var name = temp_name;

            //Returns to menu when user types in 'cancel' as name
            if (name == "cancel")
            {
                return;
            }

            Console.WriteLine("\nCONTENTS");

            bool anotherLine = true;

            int maxLines = 12;
            string[] linesContents = new string[maxLines];
            int i = 0;

            // Allow the user to input multiple lines until they type 'done'
            while (anotherLine)
            {
                int line_ref = i + 1;
                Console.Write(" > Line " + line_ref + ": ");

                string temp = Console.ReadLine();

                //Used a temporary variable to store line input
                //Temp will go thru input filters before added to array of contents

                if (temp == "done")
                {
                    anotherLine = false;
                }

                //Display warning. Maxline reached.
                if (i == maxLines - 2)
                {
                    Console.WriteLine("PS. Maxline reached. Type 'done'. Else, won't save and will return to menu.");
                }

                //Run when attempted to cross maxline. Terminate save and return to menu.
                //Also when user types in 'cancel'
                if ((i == maxLines - 1 && temp != "done") || temp == "cancel")
                {
                    Commands.returnMenu();
                    return;
                }
                linesContents[i] = temp;
                i++;
            }

            string filePath = $"0:\\{name}.txt";

            // Check if the file already exists
            if (File.Exists(filePath))
            {
                Console.WriteLine($"\nText file '{name}.txt' already exists at '0:\\'.");
                Commands.returnMenu();
            }
            else
            {
                // Write the contents to the file
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    
                    writer.WriteLine("DateTimeCreated: " + currentDate.ToString("MM-dd-yyyy") + " @" + currentTime.ToString("HH:mm:ss"));
                    writer.WriteLine("-------------------------------------");
                    
                    for (int j = 0; j < i - 1; j++)
                    {
                        writer.WriteLine(linesContents[j]);
                    }
                }

                Console.WriteLine($"\nText file '{name}.txt' created successfully at '0:\\'.");
                Commands.returnMenu();
            }
        }

        // Remove a .txt file from 0:\
        public static void removeTXTfile()
        {
            string[] filePaths = Directory.GetFiles(@"0:\");

            Console.WriteLine("NOTE: Do not include .txt when typing the name of file.");
            Console.WriteLine("      Type 'cancel' to cancel and return to menu.");

            listFile();

            Console.Write("\nName of File to Delete: ");
            var name = Console.ReadLine();

            if (name == "cancel")
            {
                return;
            }

            string filePath = $"0:\\{name}.txt";

            // Check if the file exists
            if (File.Exists(filePath))
            {
                // Delete the file
                File.Delete(filePath);
                Console.WriteLine($"\nText file '{name}.txt' deleted successfully.");
                Commands.returnMenu();
            }
            else
            {
                Console.WriteLine($"\nFile '{name}.txt' not found.");
                Commands.returnMenu();
                return;
            }
        }

        // View the contents of a .txt file from 0:\
        public static void viewTXTfile()
        {
            string[] filePaths = Directory.GetFiles(@"0:\");

            Console.WriteLine("NOTE: Refer to list provided below.");
            Console.WriteLine("      Type 'cancel' to return to menu");
            Console.WriteLine("      PS. Do not include .txt in filename");

            listFile();
            Console.Write("\nName of File to Display: ");
            var name = Console.ReadLine();

            if (name == "cancel")
            {
                return;
            }

            string filePath = $"0:\\{name}.txt";

            // Check if the file exists
            if (File.Exists(filePath))
            {
                // Read and display the contents of the file
                Console.Clear();
                Commands.upperTab("View CONTENTS of a .txt file");

                string[] lines = File.ReadAllLines(filePath);
                Console.WriteLine($"Contents of '{name}.txt':\n");

                foreach (string line in lines)
                {
                    Console.WriteLine("  " + line);
                }
                Commands.returnMenu();
            }
            else
            {
                Console.WriteLine($"\nFile '{name}.txt' not found.");
                Commands.returnMenu();
                return;
            }
        }

        public static void listFile()
        {
            string[] filePaths = Directory.GetFiles(@"0:\");

            Console.Write("\nList of File System Contents: \n");
            for (int i = 0; i < filePaths.Length; ++i)
            {
                string path = filePaths[i];

                if (System.IO.Path.GetFileName(path) != "set_config.txt")
                {
                    Console.WriteLine(" > " + System.IO.Path.GetFileName(path));
                }
            }
        }
    }
}

