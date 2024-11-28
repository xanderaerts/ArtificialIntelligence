using System;
using System.IO;
using LanguageDetection;
using Porter2Stemmer;
using System.Linq;

namespace Facebook
{
    public class PreProcessor
    {   
        private string[] stopwords;
        private EnglishPorter2Stemmer stemmer;
        private LanguageDetector detector;

        public PreProcessor(){
            stopwords = File.ReadAllLines("stop_words_english.txt");
            stemmer = new EnglishPorter2Stemmer();
            detector = new LanguageDetector();
            detector.AddAllLanguages();
        }     
        public string PreProcess(string input){

            input = input.ToLower();
            
            string language = CheckLanguage(input);
            //Console.WriteLine("taal:" + language);
            if(language != "eng" && language != "en"){
                throw new Exception("LANG = " + language);
            }

            //input = RemoveStopWords(input);
            //input = StemText(input);

            return input;
        }

        private string CheckLanguage(string input){

            
            string language = detector.Detect(input);

            return language;
        }

        private string RemoveStopWords(string input){
            
            string newText = "";

            foreach(string word in input.Split(" ")){
                if(!this.stopwords.Contains(word)){
                    newText += word + " ";
                }                
            }

            return newText;
        }

        private string StemText(string input){
            string stemmedText = "";

            

            foreach(string word in input.Split(" ")){
                string stemmed = stemmer.Stem(word).Value;
                stemmedText += stemmed + " ";
            }

            return stemmedText;
        }



    }
}
