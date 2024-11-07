using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace NBB
{
    public class NB
    {

        private double total = 0.0,spamcount = 0.0,hamcount = 0.0;



        List<string> spamWords = new List<string>();
        List<string> hamWords = new List<string>();


        public NB(string filename){
            string[] input = File.ReadAllLines(filename);

            foreach(string line in input){
                total++;
                string s = Regex.Replace(line.ToLower(),@"[^a-z+]"," ");

                string[] words = s.Split(" ");

                if(words.Length > 0){
                    if(words[0] == "spam"){
                        spamWords.AddRange(words.Skip(1));
                        spamcount++;
                    }
                    else{
                        hamWords.AddRange(words.Skip(1));
                        hamcount++;
                    }
                }
            }
        }


        internal string CheckIfSpam(string sms){
            string s = Regex.Replace(sms.ToLower(),@"[^a-z+]"," ");

            string[] words = s.Split(" ");

            double upper = 1, lower = 1;

            foreach(string word in words){
                int spam = spamWords.Count(s => s.Equals(word));
                int ham = hamWords.Count(s => s.Equals(word));

                if(spam > 0) upper *= spam / spamcount;
                if(ham > 0)lower *= ham / hamcount;
            }

            upper *= spamcount / total;
            lower *= hamcount / total;

            if(upper/lower > 1) return "SPAM";
            return "HAM";
        }

    }
}
