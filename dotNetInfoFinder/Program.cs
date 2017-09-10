using dotNetVersionFinder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace dotNetVersionFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseLocation = string.Empty;
            string filename = string.Empty;

            var useReflection = false;

            Console.WriteLine("Use Reflection? [Y/N]");
            var result = Console.ReadLine();
            if (result.ToLower() == "y")
            {
                useReflection = true;
            }

            //baseLocation = @"D:\Branches\";
            baseLocation = Directory.GetCurrentDirectory();

            filename = "results";

            Writer.WriteToFile(Searcher.DLLInfoCSV(baseLocation, useReflection), Directory.GetCurrentDirectory() + "\\" + filename + ".csv");
        }
    }
}
