using edu.stanford.nlp.ie.crf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExtractLocation
{
    class Extractor
    {
        public static Dictionary<string, List<string[]>> dLocation;
        //public static List<string[]> locationList;

        public string annotateLocationNER (string text)
        {
            // Path to the folder with classifiers models
            var jarRoot = @"D:\Perkuliahan\Tugas Akhir\Data\stanford-ner-2015-12-09";
            var classifiersDirecrory = jarRoot + @"\classifiers";

            // Loading 3 class classifier model
            var classifier = CRFClassifier.getClassifierNoExceptions(
                classifiersDirecrory + @"\english.all.3class.distsim.crf.ser.gz");

            //var s3 = "As many as 17 Russian divers have been deployed to the waters of Karimata Strait, Kalimantan, to help the search and recovery operation for AirAsia QZ8501 which crashed while en route to Singapore from Surabaya on Dec. 28.Antara reported from Panglima Utar seaport in West Kotawaringin, Central Kalimantan, that the patrol vessel KP Balam, belonging to the National Police’s water police directorate, transported the 17 Russian divers to offshore supply ship Crest Onyx early on Sunday before they were later transferred to warship KRI Banda Aceh, which was carrying out the search and recovery operation at ground zero.The National Search and Rescue Agency (Basarnas) said with support from the Russian divers, more bodies would be found and the AirAsia black box would soon be recovered.Basarnas commander Air Chief Marshal FH Bambang Soelistyo said two Russian aircraft dispatched to help the AirAsia search operation had arrived.He said the Russian aircraft, which were one their way to the search site, would assist the joint search and rescue (SAR) team currently carrying out searching operations in the Karimata Strait.Soelistyo said that one of the Russian aircraft was able to land on water, making it easier to recover bodies and debris from the downed plane. “Such operations can run smoothly only if the height of the waves does not get too high,” he said.On Sunday, the SAR team recovered one more body adrift in the sea, bringing the total number of bodies recovered to 31. All of the bodies were reported to have been sent to Surabaya for an identification process by the National Police’s Disaster Victim Identification (DVI) team at Bhayangkara Police Hospital in Surabaya. Six of the bodies have been identified and returned to their families for burial.";
            string result = classifier.classifyWithInlineXML(text);

            return result;
        }

        public List<string> getLocationNER(string text)
        {
            // get location in text
            List<string> locations = new List<string>();

            Regex regex = new Regex(@"(?<=<LOCATION>).*?(?=</LOCATION>)");
            foreach (var match in regex.Matches(text))
            {
                 locations.Add(match.ToString());
            }

            return locations;
        }

        public void addKotaKabList(string output)
        {
            string dirEngPreEngSuf = @"D:\daftar_kota_kab_eng_presuf.txt";
            string dirEngPreIndSuf = @"D:\daftar_kota_kab_eng_pre_ind_suf.txt";
            string dirEngSuf = @"D:\daftar_kota_kab_eng_tanpa_awalan.txt";
            string dirIndSuf = @"D:\daftar_kota_kab_ind_tanpa_awalan.txt";

            Dictionary<string, bool> dLocation = new Dictionary<string, bool>();
            for (int i = 1; i <= 514; i++)
            {
                // read location in each file
                string[] location = new string[4];
                location[0] = System.IO.File.ReadLines(dirEngPreEngSuf).Skip(i - 1).Take(1).First();
                location[1] = System.IO.File.ReadLines(dirEngPreIndSuf).Skip(i - 1).Take(1).First();
                location[2] = System.IO.File.ReadLines(dirEngSuf).Skip(i - 1).Take(1).First();
                location[3] = System.IO.File.ReadLines(dirIndSuf).Skip(i - 1).Take(1).First();

                // save location in dictionary
                for (int j = 0; j < location.Count(); j++)
                {
                    if (!dLocation.ContainsKey(location[j]))
                    {
                        dLocation.Add(location[j], true);
                    }
                }
            }

            // save dictionary to file
            string[] locationArr = dLocation.Keys.ToArray();
            System.IO.File.WriteAllLines(output, locationArr);
        }

        public string getEnglishPrefixSuffixLocation(string[] location)
        {
            string result = "";
            string suffix = "";     // kota/kab in english
            string prefix = "";     // barat/timur/selatan... in english
            int numberWordName = location.Count();

            // change kota/kab in english
            if(location[0] == "Kota")
            {
                suffix = "city";
            }
            else if (location[0] == "Kabupaten")
            {
                suffix = "regency";
            }

            // change barat/timur/.. in english
            switch (location[location.Count() - 1])
            {
                case "Utara":
                    {
                        prefix = "north";
                        numberWordName--;
                        break;
                    }
                case "Barat":
                    {
                        prefix = "west";
                        numberWordName--;
                        break;
                    }
                case "Timur":
                    {
                        prefix = "east";
                        numberWordName--;
                        break;
                    }
                case "Selatan":
                    {
                        prefix = "south";
                        numberWordName--;
                        break;
                    }
                case "Tenggara":
                    {
                        prefix = "southeast";
                        numberWordName--;
                        break;
                    }
                case "Laut":
                    {
                        if (location[location.Count() - 2] == "Barat")
                        {
                            prefix = "northwest";
                            numberWordName -= 2;
                        }
                        else if (location[location.Count() - 2] == "Timur")
                        {
                            prefix = "northeast";
                            numberWordName -= 2;
                        }
                        break;
                    }
                case "Daya":
                    {
                        if (location[location.Count() - 2] == "Barat")
                        {
                            prefix = "southwest";
                            numberWordName -= 2;
                        }
                        break;
                    }
                case "Tengah":
                    {
                        prefix = "central";
                        numberWordName--;
                        break;
                    }
                default:
                    break;
            }

            // combine the result
            if(prefix != "")
            {
                result += prefix;
                for (int i = 1; i < numberWordName; i++)
                {
                    result += " " + location[i].ToLower();
                }
            }
            else
            {
                result += location[1].ToLower();
                for (int i = 2; i < numberWordName; i++)
                {
                    result += " " + location[i].ToLower();
                }
            }

            result += " " + suffix;

            return result;
        }

        public static void extractLocation(string document, string no, string output)
        {
            // initial condition: locationList must have been initialized
            string[] words = document.Split(' ');

            int iter = 0;
            while(iter < words.Count())
            {
                string word = words[iter];

                // check if the result is a location or "" (empty string)
                if(dLocation.ContainsKey(word))
                {
                    string location = getMatchLocation(words, iter);
                    if(location != "")      // there is a location match!
                    {
                        DataController.addToFile(output, no + "\t" + location);
                        iter += location.Split(' ').Count();
                    }
                    else    // no location match
                    {
                        iter++;
                    }
                }
                else
                {
                    iter++;
                }
            }
        }

        public static string getMatchLocation (string[] words, int iter)
        {
            string location = "";
            string word = words[iter];
            int wordsCount = words.Count();

            // check each location with prefix in var word
            int i = 0;
            bool found = false;
            while (!found && i < dLocation[word].Count())
            {
                string[] locationWord = dLocation[word][i];
                int countLocationWord = locationWord.Count();
               
                // check each word in current location
                bool same = true;
                int j = 0;
                while (same && (j < countLocationWord))
                {
                    if((iter + j) < wordsCount)
                    {
                        if (words[iter + j] != locationWord[j])
                        {
                            same = false;
                        }
                    }
                    else
                    {
                        same = false;
                    }
                    
                    j++;
                }

                // check if sequence of location word same
                if (same)
                {
                    found = true;
                    location = string.Join(" ", locationWord);
                }
                else    // if not found, continue searching (check the next string[])
                {
                    i++;
                }
            }

            return location;
        }

        public static void initializedLocationDictionary(string directory)
        {
            List<string[]> locationList = getSortedListArrayLocation(directory);

            // save location into dLocation
            dLocation = new Dictionary<string, List<string[]>>();
            foreach (var item in locationList)
            {
                if(!dLocation.ContainsKey(item[0]))
                {
                    dLocation.Add(item[0], new List<string[]>());
                    dLocation[item[0]].Add(item);
                }
                else
                {
                    dLocation[item[0]].Add(item);
                }
            }
        }

        private static List<string[]> getSortedListArrayLocation (string directory)
        {
            List<string[]> locationList = new List<string[]>();
            string[] locationArr = System.IO.File.ReadAllLines(directory);

            foreach (string location in locationArr)
            {
                string[] word = location.Split(' ');
                locationList.Add(word);
            }

            locationList = locationList.OrderByDescending(u => u.Count()).ToList();

            return locationList;
        }

        public static void writeDistinctLocation (string directory, string output)
        {
            string line = "";
            string no = "1";
            Dictionary<string, int> dLocation = new Dictionary<string, int>();
            System.IO.StreamReader file = new System.IO.StreamReader(directory);
            while ((line = file.ReadLine()) != null)
            {
                string[] lineSplit = line.Split('\t');
                if(no == lineSplit[0])      // still in same no
                {
                    if(!dLocation.ContainsKey(lineSplit[1]))
                    {
                        dLocation.Add(lineSplit[1], 1);
                    }
                    else
                    {
                        dLocation[lineSplit[1]] += 1;
                    }
                }
                else    // different no, save the location info in file, then make new dictionary
                {
                    // save location in file
                    foreach(var location in dLocation)
                    {
                        DataController.addToFile(output, no + "\t" + location.Key + "\t" + location.Value.ToString());
                    }

                    // reset dictionary
                    no = lineSplit[0];
                    dLocation = new Dictionary<string, int>();
                    dLocation.Add(lineSplit[1], 1);
                }
            }

            // save the last location in file
            foreach (var location in dLocation)
            {
                DataController.addToFile(output, no + "\t" + location.Key + "\t" + location.Value.ToString());
            }
        }

        public static void writeLocationLabel(string output)
        {
            string dirEngPreEngSuf = @"D:\daftar_kota_kab_eng_presuf.txt";
            string dirEngPreIndSuf = @"D:\daftar_kota_kab_eng_pre_ind_suf.txt";
            string dirEngSuf = @"D:\daftar_kota_kab_eng_tanpa_awalan.txt";
            string dirIndSuf = @"D:\daftar_kota_kab_ind_tanpa_awalan.txt";

            Dictionary<string, bool> dLocation = new Dictionary<string, bool>();
            for (int i = 1; i <= 514; i++)
            {
                // read location in each file
                string[] location = new string[4];
                location[0] = System.IO.File.ReadLines(dirEngPreEngSuf).Skip(i - 1).Take(1).First();
                location[1] = System.IO.File.ReadLines(dirEngPreIndSuf).Skip(i - 1).Take(1).First();
                location[2] = System.IO.File.ReadLines(dirEngSuf).Skip(i - 1).Take(1).First();
                location[3] = System.IO.File.ReadLines(dirIndSuf).Skip(i - 1).Take(1).First();

                // save location in dictionary
                for (int j = 0; j < location.Count(); j++)
                {
                    if (!dLocation.ContainsKey(location[j]))
                    {
                        dLocation.Add(location[j], true);
                        DataController.addToFile(output, i.ToString() + "\t" + location[j]);
                    }
                }
            }
        }

        public void writeRealLocation(string dirLocationAll, string dirLocationLabel, string output)
        {
            // get location label
            Dictionary<string, string> dLocationLabel = getLocationLabel(dirLocationLabel);

            // get location name
            Dictionary<string, string> dLocationName = getLocationName(dirLocationLabel);

            // get real location based on highest sum label
            string line = "";
            string no = "1";
            Dictionary<string, int> dLocation = new Dictionary<string, int>();
            System.IO.StreamReader file = new System.IO.StreamReader(dirLocationAll);
            while ((line = file.ReadLine()) != null)
            {
                string[] lineSplit = line.Split('\t');
                if (no == lineSplit[0])      // still in same no
                {
                    string label = dLocationLabel[lineSplit[1]];
                    string locationName = dLocationName[label];
                    int sum = Int32.Parse(lineSplit[2]);

                    if (!dLocation.ContainsKey(locationName))
                    {
                        dLocation.Add(locationName, sum);
                    }
                    else
                    {
                        dLocation[locationName] += sum;
                    }
                }
                else    // different no, save the location info in file, then make new dictionary
                {
                    int max = dLocation.Values.Max();
                    var locations = from l in dLocation where l.Value == max select l;

                    // save location in file
                    foreach (var location in locations)
                    {
                        DataController.addToFile(output, no + "\t" + location.Key + "\t" + location.Value.ToString());
                    }

                    // reset dictionary and save current location
                    no = lineSplit[0];
                    dLocation = new Dictionary<string, int>();

                    string label = dLocationLabel[lineSplit[1]];
                    string locationName = dLocationName[label];
                    int sum = Int32.Parse(lineSplit[2]);

                    dLocation.Add(locationName, sum);
                }
            }

            // save the last location in file
            foreach (var location in dLocation)
            {
                DataController.addToFile(output, no + "\t" + location.Key + "\t" + location.Value.ToString());
            }
        }

        private Dictionary<string, string> getLocationLabel(string directory)
        {
            Dictionary<string, string> dLocationLabel = new Dictionary<string, string>();
            
            string line = "";
            System.IO.StreamReader file = new System.IO.StreamReader(directory);
            while ((line = file.ReadLine()) != null)
            {
                string[] lineSplit = line.Split('\t');
                dLocationLabel.Add(lineSplit[1], lineSplit[0]);
            }

            return dLocationLabel;
        }

        private Dictionary<string, string> getLocationName(string directory)
        {
            string line = "";
            string no = "0";
            Dictionary<string, string> dLocationName = new Dictionary<string,string>();
            System.IO.StreamReader file = new System.IO.StreamReader(directory);
            while ((line = file.ReadLine()) != null)
            {
                string[] lineSplit = line.Split('\t');
                if (no == lineSplit[0])      // still in same no
                {
                    // do nothing
                }
                else    // different no, save the location info in file, then make new dictionary
                {
                    // save location in dictionary
                    dLocationName.Add(lineSplit[0], lineSplit[1]);
                    no = lineSplit[0];
                }
            }

            return dLocationName;
        }
    }
}
