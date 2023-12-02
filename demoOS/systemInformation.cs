using Cosmos.Core;
using demoOS;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoOS
{
    public class systemInformation
    {
        // Variables to store the current time and date
        public static DateTime currentTime = DateTime.Now;
        public static DateTime currentDate = DateTime.Now;

        // Method to get and display the current time and day
        public static void getTimeDay()
        {

            // Get the day of the week
            string dayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(currentDate.DayOfWeek);

            // Display the current date and time
            Commands.upperTab("Displaying Current Date and Time");

            Console.WriteLine("Current Time: " + currentTime.ToString(Kernel.time_format_options[Kernel.selectedTimeFormat]));

            // Update time before displaying date. Take into account display delay.
            currentTime = currentTime.AddSeconds(1);
            Console.WriteLine("Current Date: " + currentDate.ToString(Kernel.date_format_options[Kernel.selectedDateFormat]) + " (" + dayOfWeek + ")");
            Commands.returnMenu();
        }

        // Method to display system information
        public static void sysInfo()
        {
            Kernel kernel = new Kernel();

            // Get information about the file system
            string[] filePaths = Directory.GetFiles(@"0:\");
            var drive = new DriveInfo("0");

            Commands.upperTab("Displaying System Information");

            // Get information about the CPU and RAM
            uint installedRAM = CPU.GetAmountOfRAM();
            uint reservedRAM = installedRAM - (uint)GCImplementation.GetAvailableRAM();
            double usedRAM = (GCImplementation.GetUsedRAM() / (1024.0 * 1024.0)) + reservedRAM;

            string processorBrand = $"Processor Brand: {CPU.GetCPUVendorName()}";
            string processorModel = $"Processor Model: {CPU.GetCPUBrandString()}";
            string cpuUpTime = $"Processor UpTime: {TimeSpan.FromSeconds(CPU.GetCPUUptime() / 3200000000).ToString(@"hh\:mm\:ss")}";

            // Display system information
            Console.WriteLine("SYSTEM INFORMATION\n");
            Console.WriteLine("Operating System: demoOS");
            Console.WriteLine("Developer: Group 5");
            Console.WriteLine("Kernel Version: " + Kernel.version);
            Console.WriteLine("\nRAM: " + usedRAM.ToString("0.00") + "/" + installedRAM.ToString("0.00") + " MB (" + (int)((usedRAM / installedRAM) * 100) + "%)");
            Console.WriteLine("Display Resolution: " + System.Console.WindowWidth + "x" + System.Console.WindowHeight);
            Console.WriteLine(processorBrand);
            Console.WriteLine(processorModel);
            Console.WriteLine(cpuUpTime);
            Console.WriteLine($"\nTotal Size: {drive.TotalSize / (1024 * 1024)} MB");
            Console.WriteLine($"Free Space: {drive.AvailableFreeSpace / (1024 * 1024)} MB free");

            Commands.returnMenu();
        }
    }
}
