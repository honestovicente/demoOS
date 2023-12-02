using Cosmos.Core.IOGroup;
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using System.IO;
using System.Threading;
using System.Runtime.CompilerServices;
using demoOS;
using System.Xml.Linq;

namespace demoOS
{
    public class Kernel : Sys.Kernel
    {
        // Version of the operating system
        public static string version = "dev1.0";

        public static bool useNetwork = false;

        public static string[] time_format_options = new string[] {"hh:mm tt", "HH:mm:ss" };
        public static string[] date_format_options = new string[] { "MMMM dd, yyyy", "MM-dd-yyyy" };
        public static string[] menu_language_options = new string[] { "english","tagalog" };

        public static int selectedTimeFormat;
        public static int selectedDateFormat;
        public static int selectedMenuLanguage;

        public static void getSetSys()
        {
            string[] filePaths = Directory.GetFiles(@"0:\");

            string filePath = $"0:\\set_config.txt";

            string[] system_reference = new string[3];

            // Default values
            if (File.Exists(filePath))
            {
                system_reference = File.ReadAllLines(filePath);
                if (system_reference.Length == 3)
                {                 
                    selectedTimeFormat = Convert.ToInt32(system_reference[0]);
                    selectedDateFormat = Convert.ToInt32(system_reference[1]);
                    selectedMenuLanguage = Convert.ToInt32(system_reference[2]);
                } else
                {
                    Console.WriteLine("Invalid system configuration. Boot up: Default");
                    //Default
                    selectedTimeFormat = 0;
                    selectedDateFormat = 0;
                    selectedMenuLanguage = 0;
                }
            }
            else
            {
                //Default
                selectedTimeFormat = 0;
                selectedDateFormat = 0;
                selectedMenuLanguage = 0;
    } 
        }

// File system instance
private Sys.FileSystem.CosmosVFS fs;

        // Method called before the operating system starts running
        protected override void BeforeRun()
        {
            initializeFileSys();
            Console.Clear();
            intro();
        }

        public static void initializeFileSys()
        {
            // Create a new instance of the Cosmos Virtual File System
            var fs = new Sys.FileSystem.CosmosVFS();

            // Register the file system with the Virtual File System Manager
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
        }

        // Method called during the operating system runtime
        protected override void Run()
        {
            // Display the menu and handle user commands
            Commands.upperTab("Menu Display");
            getSetSys();
            Commands.displayMenu(menu_language_options[selectedMenuLanguage]);
            Commands.Acceptcmd();
        }

        // Method to display the introduction screen
        public static void intro()
        {
            Console.WriteLine("\n\n");
            Console.WriteLine("                               #######  #####  ");
            Console.WriteLine("   #####  ###### #    #  ####  #     # #     # ");
            Console.WriteLine("   #    # #      ##  ## #    # #     # #       ");
            Console.WriteLine("   #    # #####  # ## # #    # #     #  #####  ");
            Console.WriteLine("   #    # #      #    # #    # #     #       # ");
            Console.WriteLine("   #    # #      #    # #    # #     # #     # ");
            Console.WriteLine("   #####  ###### #    #  ####  #######  #####  ");
            Console.WriteLine("\n   Developed by Group 5");
            Console.Write("\n   Welcome to demoOS");
            threedotsEffect();
            Thread.Sleep(4000);
        }

        // Method to display a loading effect
        public static void threedotsEffect()
        {
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(1300);
                Console.Write(".");
            }
        }
    }
}
