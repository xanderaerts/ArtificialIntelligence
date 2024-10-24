using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Linq.Expressions;

namespace SocialMedia{
class Program
{
    static void Main(string[] args)
    {
        string var1 = Console.ReadLine();
        string var2 = Console.ReadLine();

        try{
            Predictions pred = new Predictions();
            
            //pred.Train();
            
            
            pred.Predict(var1.Trim().ToLower(),var2.Trim().ToLower());
            //pred.Predict("Sentiment","Hours");

            Console.WriteLine(pred.b);
            Console.WriteLine(pred.m1 + " " + pred.m2);
            Console.WriteLine(pred.r2);
        }
        catch(Exception e){
            Console.WriteLine(e.Message);
        }
        
        
    }
}
}
