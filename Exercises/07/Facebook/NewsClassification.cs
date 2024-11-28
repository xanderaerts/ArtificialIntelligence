using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Porter2Stemmer;
using LanguageDetection;
using System.IO;
using System.Linq;
namespace Facebook
{
    public class NewsClassification
    {

        private List<string> trueNewsWords = new List<string>();
        private List<string> fakeNewsWords = new List<string>();
        private double trueCount = 0.0, fakeCount = 0.0;

        public NewsClassification(){

            string[] stopwords = File.ReadAllLines("stop_words_english.txt");
            string[] truelines = File.ReadAllLines("True.csv").Skip(1).ToArray();

            foreach(string line in truelines){
                string s = Regex.Replace(line.ToLower(),@"^[a-z]"," ");

                //string stemmed = stemmer.Stem(s).Value;

                this.trueNewsWords.AddRange(s.Split(" "));
                trueCount++;
             
             /*
                foreach(string word in s.Split(" ")){
                    
                    if(!stopwords.Contains(word.ToLower())){
                        this.trueNewsWords.Add(word);
                    }

                }
                */
            }

            string[] fakelines = File.ReadAllLines("Fake.csv").Skip(1).ToArray();
            foreach(string line in fakelines){
                string s = Regex.Replace(line.ToLower(),@"^[a-z]"," ");

                this.fakeNewsWords.AddRange(s.Split(" "));
                fakeCount++;


                //string stemmed = stemmer.Stem(s).Value;

                /*foreach(string word in s.Split(" ")){

                    if(!stopwords.Contains(word.ToLower())){

                        this.fakeNewsWords.Add(word);
                    }
                } */
            }

        }
        public bool checkNews(string inputNews){
            inputNews = Regex.Replace(inputNews.ToLower(),@"^[a-z]"," ");

            double upper = 1,lower=1;

            foreach(string word in inputNews.Split(" ")){
                int trueNews = trueNewsWords.Count(s => s.Equals(word));
                int fakeNews = fakeNewsWords.Count(s => s.Equals(word));

                if(trueNews> 0) upper *= trueNews / this.trueCount; 
                if(fakeCount> 0) upper *= fakeCount / this.fakeCount; 
            }

            upper *= trueCount / (this.trueCount + this.fakeCount);
            lower *= fakeCount / (this.trueCount + this.fakeCount);

            if(upper/lower > 1) return false;
            else return true;


        }
    }
}
