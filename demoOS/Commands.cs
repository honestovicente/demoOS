using System.Net;
using Cosmos.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel.Design;

namespace demoOS
{
    public class Commands
    {
        public static void Acceptcmd()
        {
            string language = Kernel.menu_language_options[Kernel.selectedMenuLanguage];

            Console.Write("\nInput: ");
            var input = Console.ReadLine();
            string cmd = input.Split(" ")[0];
            Console.Clear();

            // Switch statement to handle different commands
            switch (cmd)
            {
                case "1":
                    shutDown();
                    break;
                case "2":
                    reboot();
                    break;
                case "3":
                    upperTab("Showing Kernel Version");
                    Console.WriteLine("Kernel Version: " + Kernel.version);
                    returnMenu();
                    break;
                case "4":
                    upperTab("Displaying Date and Time");
                    systemInformation.getTimeDay();
                    break;
                case "5":
                    upperTab("Displaying current IP Address");
                    DisplayNetworkInfo();
                    returnMenu();
                    break;
                case "6":
                    systemInformation.sysInfo();
                    break;
                case "7":
                    upperTab("List File Contents");
                    fileRelated.fileContents();
                    break;
                case "8":
                    upperTab("View CONTENTS of a .txt file");
                    fileRelated.viewTXTfile();
                    break;
                case "9":
                    upperTab("Add a .txt file");
                    fileRelated.addTXTfile();
                    break;
                case "10":
                    upperTab("Remove a .txt file");
                    fileRelated.removeTXTfile();
                    break;
                case "11":
                    upperTab("Menu Display");
                    displayMenu(language);
                    break;
                case "help": 
                    upperTab("General Tips");
                    help();
                    returnMenu();
                    break;
                case "settings":
                    customize();
                    break;
                default:
                    upperTab("Menu Display");
                    Console.WriteLine("Invalid input. Please refer to the displayed menu for input.");
                    returnMenu();
                    break;
            }
        }

        public static void customize()
        {
            string temp;
            string[] choices = new string[3];
            for (int j = 0; j < 3; j++)
            {
                Console.Clear();
                upperTab("Customize OS Experience");
                Console.WriteLine("Type 'cancel' to cancel and return to menu\n");
                switch (j)
                {
                    case 0:
                        Console.WriteLine("Change TIME Format\n\n OPTIONS: ");
                        for (int i = 0; i < Kernel.time_format_options.Length; i++)
                        {
                            Console.WriteLine("   > Enter " + i + " for " + Kernel.time_format_options[i] + " Ex: " + systemInformation.currentTime.ToString(Kernel.time_format_options[i]));
                        }
                        break;
                    case 1:
                        Console.WriteLine("Change DATE Format\n\n OPTIONS: ");
                        for (int i = 0; i < Kernel.date_format_options.Length; i++)
                        {
                            Console.WriteLine("   > Enter " + i + " for " + Kernel.date_format_options[i] + " Ex: " + systemInformation.currentDate.ToString(Kernel.date_format_options[i]));
                        }
                        break;
                    case 2:
                        Console.WriteLine("Change MENU Language\n\n OPTIONS: ");
                        for (int i = 0; i < Kernel.menu_language_options.Length; i++)
                        {
                            Console.WriteLine("   > Enter " + i + " for " + Kernel.menu_language_options[i]);
                        }
                        break;
                }

                Console.Write("\nChoice: ");

                temp = Console.ReadLine();

                if ((temp != "0" && temp != "1") || (temp.ToLower() == "cancel"))
                {
                    Console.WriteLine("\nWill return to menu.");
                    returnMenu();
                    return;
                }
                choices[j] = temp;
            }

            //
            string[] filePaths = Directory.GetFiles(@"0:\");
            var name = "set_config";
            string filePath = $"0:\\{name}.txt";

            // Check if the file exists
            if (File.Exists(filePath))
            {
                // Delete existing config
                File.Delete(filePath);
            }

            // Create new sys_config.txt
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int j = 0; j < choices.Length; j++)
                {
                    writer.WriteLine(choices[j]);
                }
            }
            Console.WriteLine("\nNew CONFIGURATION successfully saved.");
            Commands.returnMenu();
        }

        static void DisplayNetworkInfo()
        {
            // Use the Net class to get network information
            string networkInfo = Net.GetInfo();

            // Display the network information
            Console.WriteLine(networkInfo);
        }

        // Method to return to the menu
        public static void returnMenu()
        {
            Console.Write("\nPress any key to return to menu...");
            Console.ReadKey(true);
            Console.Clear();
            return;
        }

        // Method to format the upper part of the console
        public static void upperTab(string current)
        {
            Console.Clear();
            Console.WriteLine("\nWelcome to demoOS brought by Group 5\n");
            Console.WriteLine(current);
            Console.WriteLine("----------------------------------------------------------------------");
        }

        // Method to display the main menu
        public static void displayMenu(string language)
        {
            switch (language) {
                case "english":
                    Console.WriteLine("Type 1 (+ enter) to SHUTDOWN");
                    Console.WriteLine("Type 2 (+ enter) to REBOOT");
                    Console.WriteLine("Type 3 (+ enter) to SHOW kernel version");
                    Console.WriteLine("Type 4 (+ enter) to SHOW Current Date and Time");
                    Console.WriteLine("Type 5 (+ enter) to DISPLAY IP Address");
                    Console.WriteLine("Type 6 (+ enter) to DISPLAY System Info");
                    Console.WriteLine("Type 7 (+ enter) to LIST File System Contents");
                    Console.WriteLine("Type 8 (+ enter) to VIEW contents of .txt file");
                    Console.WriteLine("Type 9 (+ enter) to ADD .txt file");
                    Console.WriteLine("Type 10 (+ enter) to REMOVE .txt file");
                    Console.WriteLine("Type 11 (+ enter) to REFRESH");
                    break;
                case "tagalog":
                    Console.WriteLine("Type 1 (+ enter) para ISARA ANG OS");
                    Console.WriteLine("Type 2 (+ enter) para I-REBOOT ANG OS");
                    Console.WriteLine("Type 3 (+ enter) para IPAKITA ang Bersyon ng Kernel");
                    Console.WriteLine("Type 4 (+ enter) para IPAKITA ang Kasalukuyang Petsa at Oras");
                    Console.WriteLine("Type 5 (+ enter) para IPAKITA ang IP Address");
                    Console.WriteLine("Type 6 (+ enter) para IPAKITA ang Impormasyon ng System");
                    Console.WriteLine("Type 7 (+ enter) para IPAKITA ang Nilalaman ng File System");
                    Console.WriteLine("Type 8 (+ enter) para TINGNAN ang nilalaman ng .txt file");
                    Console.WriteLine("Type 9 (+ enter) para MAGDAGDAG ng .txt file");
                    Console.WriteLine("Type 10 (+ enter) para MAGTANGGAL ng .txt file");
                    Console.WriteLine("Type 11 (+ enter) para I-REFRESH");
                    break;
                default:
                    Console.WriteLine("set_config.txt corrupted. Contact dev for assistance.");
                    break;
            }
        }

        // Method to shut down the system
        public static void shutDown()
        {
            upperTab("Confirm shut down");
            Console.Write("\nConfirm to shut down? (y/n): ");
            var choice = Console.ReadLine();
            if (choice == "y")
            {
                Console.Write("\nPreparing to shut down");
                Kernel.threedotsEffect();
                Cosmos.System.Power.Shutdown();
            }
        }

        // Method to reboot the system
        public static void reboot()
        {
            upperTab("Confirm reboot");
            Console.Write("\nConfirm to reboot? (y/n): ");
            var choice1 = Console.ReadLine();
            if (choice1 == "y")
            {
                Console.Write("\nPreparing to Reboot");
                Kernel.threedotsEffect();
                Cosmos.System.Power.Reboot();
            }
        }

        public static void help()
        {
            Console.WriteLine("Welcome to demoOS Help Center!\nHere are some tips to enhance your experience:\n");

            Console.WriteLine(" > Use the provided menu to navigate through various commands.");
            Console.WriteLine(" > Follow the on-screen instructions to provide necessary inputs.");
            Console.WriteLine(" > Take advantage of the 'refresh' command to refresh the display.");
            Console.WriteLine(" > For file naming, use alphanumeric, avoid special characters or spaces.");
            Console.WriteLine(" > For a quick system overview, use '6' command.");
            Console.WriteLine(" > Type 'settings' for customization options to demoOS.");

            Console.WriteLine("\nFor an in-depth reference, please refer to our Manual.");
        }
    }
}

