using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractLocation
{
    class Program
    {
        static void Main(string[] args)
        {
            // write all document with location
            string dirDocument = @"E:\output_real\data_complete\data_document_v2-6_cleaned.txt";
            string dirNoDocument = @"D:\data_line_number_document.txt";
            string output = @"D:\data_document.txt";

            DocumentEditor de = new DocumentEditor();
            de.writeAllDocumentWithLocation(dirNoDocument, dirDocument, output);

            //// write distinct no document
            //string directory = @"D:\data_location_real_max.txt";
            //string output = @"D:\data_line_number_document.txt";

            //DocumentEditor de = new DocumentEditor();
            //de.writeNoDocument(directory, output);

            //// save real location
            //string dirLocationAll = @"D:\data_location_all_distinct.txt";
            //string dirLocationLabel = @"D:\daftar_kota_kab_all_label_v2.txt";
            //string output = @"D:\data_location_real_max.txt";

            //Extractor e = new Extractor();
            //e.writeRealLocation(dirLocationAll, dirLocationLabel, output);

            //// check getLocationName
            //string directory = @"D:\daftar_kota_kab_all_label_v2.txt";

            //Extractor e = new Extractor();
            //Console.WriteLine(e.getLocationLabel(directory).Count());

            //// write location label
            //string output = @"D:\daftar_kota_kab_all_label_v2.txt";

            //Extractor.writeLocationLabel(output);

            //// save distinct location
            //string directory = @"D:\data_location_all.txt";
            //string output = @"D:\data_location_all_distinct.txt";

            //Extractor.writeDistinctLocation(directory, output);

            //// check empty location document
            //int iter = 0;
            //string line = "";
            //List<string> no = new List<string>();
            //System.IO.StreamReader file = new System.IO.StreamReader(@"D:\data_location_all_distinct.txt");
            //while ((line = file.ReadLine()) != null)
            //{
            //    string[] lineSplit = line.Split('\t');
            //    no.Add(lineSplit[0]);
            //}

            //Console.WriteLine(no.Distinct().Count());

            //// check no of doc that empty
            //string dirDocument = @"E:\output_real\data_stage4_tokenization\";

            //for (int i = 1; i <= 10778; i++)
            //{
            //    string no = i.ToString();
            //    string document = System.IO.File.ReadLines(dirDocument + no + ".txt").Skip(0).Take(1).First();
            //    if (document == "")
            //        Console.WriteLine(no);
            //}

            //// check extraction location
            //string dirLocation = @"D:\daftar_kota_kab_all.txt";
            //string dirDocument = @"E:\output_real\data_stage4_tokenization\";
            //string output = @"D:\data_location_all.txt";
            
            //Extractor.initializedLocationDictionary(dirLocation);

            //int j = 1;
            //for (int i = 1; i <= 10778; i++)
            //{
            //    string no = i.ToString();
            //    string document = System.IO.File.ReadLines(dirDocument + no + ".txt").Skip(0).Take(1).First();

            //    if(document != "")
            //    {
            //        Extractor.extractLocation(document, j.ToString(), output);
            //        j++;
            //    }
            //}

            //// check remove string
            //string a = "one two three";
            //string b = "one two";

            //Console.WriteLine(a.Remove(a.IndexOf(b), b.Count()));

            //// check array string contains
            //string[] a = { "a", "b", "c", "d", "e" };
            //string[] b = { "a", "b", "c" };
            //string[] c = { "a", "b", "d" };
            //string[] d = { "a", "b" };

            //List<string> al = new List<string>() { "a", "b", "c", "d", "e" };

            //// check string cointains
            //string a = "one two three";
            //string b = "one";
            //string c = "two";
            //string d = "two three";

            //Console.WriteLine(a.Contains(d));

            //// check equality array of string
            //string[] a = { "a", "b", "c", "d", "e" };
            //string[] b = { "a", "b", "c" };
            //string[] c = { "a", "b", "d" };
            //string[] d = { "a", "b" };

            //Console.WriteLine(a.SequenceEqual(b));

            //// check initializedLocationDictionary
            //Extractor.initializedLocationDictionary(@"D:\daftar_kota_kab_all.txt");

            //// check delete list and array
            //string[] array = { "satu", "dua", "tiga", "empat" };
            //List<string> list = new List<string>();

            //list.Add("satu");
            //list.Add("dua");
            //list.Add("tiga");
            //list.Add("empat");

            //foreach(string item in list)
            //{
            //    Console.WriteLine(item);
            //}

            //list.RemoveAt(1);

            //foreach (string item in list)
            //{
            //    Console.WriteLine(item);
            //}

            //Console.WriteLine("\n\n\n" + list[1]);

            //// add to all location to list
            //string output = @"D:\daftar_kota_kab_all.txt";
            //Extractor e = new Extractor();
            //e.addKotaKabList(output);

            //// turn location into eng pre and eng suf
            //string directory = @"D:\daftar_kota_kab.txt";
            //Extractor e = new Extractor();

            //string line = "";
            //System.IO.StreamReader file = new System.IO.StreamReader(directory);
            //while ((line = file.ReadLine()) != null)
            //{
            //    string[] location = line.Split(' ');
            //    string engLocation = e.getEnglishPrefixSuffixLocation(location);
            //    DataController.addToFile(@"D:\daftar_kota_kab_all.txt", engLocation);
            //}

            //// check ambigu location
            //string directory = @"D:\daftar_kota_kab_tanpa_awalan.txt";

            //Dictionary<string, bool> dKotaKab = new Dictionary<string, bool>();

            //string line = "";
            //System.IO.StreamReader file = new System.IO.StreamReader(directory);
            //while ((line = file.ReadLine()) != null)
            //{
            //    if(!dKotaKab.ContainsKey(line))
            //    {
            //        dKotaKab.Add(line, true);
            //    }
            //    else
            //    {
            //        DataController.addToFile(@"D:\daftar_kota_kab_ambigu.txt", line);
            //    }
            //}

            //// extract location and save it in a file
            //string output = @"E:\output_location_real\extract_location_1_all.txt";

            //for (int i = 200; i <= 300; i++)
            //{
            //    string no = i.ToString();
            //    string directory = @"E:\output_real\data_stage2-3_author_removal\" + no + ".txt";
            //    string text = DataController.readFile(directory);

            //    List<string> locations = extractLocation(text);
            //    foreach (string location in locations)
            //    {
            //        DataController.addToFile(output, no + "\t" + location);
            //    }
            //}

            //DataController.addToFile(@"D:/output.txt", result);

            //// check insertSpaceAfterPoint & upperFirstCharacter
            //string text = "The Banyumas regency administration sealed two hotels in Purwokerto city, on Monday, for lacking building permits.Led by Banyumas Regent Ahmad Husen, public order officers sealed off the hotels, namely the seven-story Dominic Hotel and the five-story Wisata Niaga Hotel.“We gave three notices to the hotels’ managements to process the permits, but they ignored them. So, as from today, they cannot operate,” Husen said.He said the Wisata Niaga Hotel, which opened its doors five years ago, only had a permit for a two-story building while the Domini Hotel was a one-story building five years ago that was rebuilt five months ago without a permit.“If they continue to accept guests, the punishment will be harsher,” Husen said.Meanwhile, Dominic Hotel manager Tommy said he knew nothing about the building permit. “We are just new, but we will obey the regulation,” he added. (***)";

            //DocumentEditor de = new DocumentEditor();
            //Console.WriteLine(de.insertSpaceAfterPoint(text));

            //DataController.addToFile(@"D:/output.txt", de.upperFirstCharacter(text, "regency"));
            //DataController.addToFile(@"D:/output.txt", de.upperFirstCharacter(text, "city"));

            Console.WriteLine("\nSelesai!");
            Console.ReadLine();
        }

        public static List<string> extractLocation(string text)
        {
            DocumentEditor de = new DocumentEditor();
            string result = de.getErasedWhiteSpaceDocument(text);
            result = de.insertSpaceAfterPoint(result);
            result = de.upperFirstCharacter(result, "regency");
            result = de.upperFirstCharacter(result, "city");

            Extractor e = new Extractor();
            result = e.annotateLocationNER(result);

            List<string> locations = e.getLocationNER(result);

            return locations;
        }
    }
}
