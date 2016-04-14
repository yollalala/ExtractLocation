using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExtractLocation
{
    class DocumentEditor
    {
        public string insertSpaceAfterPoint(string text)
        {
            string result = Regex.Replace(text, @"(?<=\w\.)(?=\w)", " ");

            return result;
        }

        public string upperFirstCharacter(string text, string word)
        {
            string result = Regex.Replace(text, @word, word.First().ToString().ToUpper() + word.Substring(1));

            return result;
        }

        public string getErasedWhiteSpaceDocument(string document)
        {
            // remove white space in the end document
            string content = Regex.Replace(document, @"\s+$", string.Empty);

            // remove white space in the start document
            content = Regex.Replace(content, @"^\s+", string.Empty);

            return content;
        }

        // write no document that has location
        public void writeNoDocument(string directory, string output)
        {
            string line = "";
            List<string> lNoDocument = new List<string>();
            System.IO.StreamReader file = new System.IO.StreamReader(directory);
            while ((line = file.ReadLine()) != null)
            {
                string[] lineSplit = line.Split('\t');
                lNoDocument.Add(lineSplit[0]);
            }

            // distinct the list
            string[] lines = lNoDocument.Distinct().ToArray();

            // write no document to file
            System.IO.File.WriteAllLines(output, lines);
        }

        public void writeAllDocumentWithLocation(string dirNoDocument, string dirDocument, string output)
        {
            string[] lNoDocument = System.IO.File.ReadAllLines(dirNoDocument);

            foreach(string item in lNoDocument)
            {
                int no = Int32.Parse(item);
                string text = System.IO.File.ReadLines(dirDocument).Skip(no - 1).Take(1).First();
                DataController.addToFile(output, text);
            }
        }
    }
}
