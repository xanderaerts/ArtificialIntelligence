using System.IO;
using System;
using System.Runtime.InteropServices;
namespace Facebook{
class Program
{
    static void Main(string[] args)
    {
        try{

            string newsInput = Console.ReadLine();
            //string newsInput = "Climate Accord Reached After Marathon Negotiations at Global Summit. WASHINGTON (Reuters) - After days of intense negotiations, leaders from over 190 countries reached a landmark agreement aimed at addressing climate change during the Global....";
            
            PreProcessor prep = new PreProcessor();
            string preProcessedText = prep.PreProcess(newsInput);
            //Console.WriteLine("pre done");

            NewsClassification nwc = new NewsClassification();

            bool output = nwc.checkNews(preProcessedText);

            if(output) Console.WriteLine("True news!");
            else Console.WriteLine("Fake news!");
            //Console.WriteLine(output);          

        }
        catch(Exception e){
            Console.WriteLine("Crazy input");
            if(e.Message.Contains("LANG"))Console.WriteLine(e.Message);

        }

        
     

    }
}
}
