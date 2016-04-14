using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractLocation
{
    class DataController
    {
        public static void addToFile(string outputFile, string text)
        {
            File.AppendAllText(outputFile, text + Environment.NewLine);
        }

        public static string readFile(string directory)
        {
            string content = File.ReadAllText(directory);

            return content;
        }
    }
}
