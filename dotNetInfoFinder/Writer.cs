using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetVersionFinder
{
    class Writer
    {
        public static void WriteToFile(StringBuilder content, string fileName)
        {
            try
            {
                using (var file = new StreamWriter(fileName))
                {
                    file.WriteLine(content);
                }
            }
            catch { }
        }

    }
}
