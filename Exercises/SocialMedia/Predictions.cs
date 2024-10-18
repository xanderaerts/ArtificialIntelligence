using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Security;

namespace SocialMedia
{
    public class Predictions
    {

        public double m {get; set;}
        public double b {get; set;}
        public double r2 {get; set;}

        private string inputFile = "sentiment.csv";

        private double[] x1 {get;set;}
        private double[] x2 {get;set;}
        private double[] y {get;set;}
        
        public void Predict(string var1,string var2){
            
            string[] columns = {"Sentiment","Platform","Text","Hours","Hashtags","Retweets"};
        
            if(var1 == columns[0] ) this.Sentiment(1);
            if(var1 == columns[1] ) {}
            if(var1 == columns[2] ) {}
            if(var1 == columns[3] ) this.Hours(1);
            if(var1 == columns[4] ) {}
            if(var1 == columns[5] ) {} 

                
            if(var2 == columns[0] ) this.Sentiment(2);
            if(var2 == columns[1] ) {}
            if(var2 == columns[3] ) this.Hours(2);
            if(var2 == columns[4] ) {}
            if(var2 == columns[5] ){}

            getY();
            Train();

            
        
        }

        public void Train(){
            
            double sumX = 0, sumY = 0, sumXY = 0, sumX2 = 0;
            double[] x = new double[this.x1.Length];

            for(int i = 0; i < this.x1.Length;i++){
                x[i] = this.x1[i] + this.x2[1];
            }

            int n = x.Length;

            sumX = x.Sum();
            sumY = this.y.Sum();


            for(int i = 0; i < x.Length;i++){
                sumXY += x[i] * y[i];
                sumX2 += x[i] * x[i];
            }

            this.m = ((n * sumXY) - (sumX * sumY)) / (n * sumX2 - Math.Pow(sumX, 2));
	
            this.b = (sumY - (m * sumX)) / n;

            this.r2 = Math.Pow((n * sumXY - sumX * sumY) / Math.Sqrt((n * sumX2 - sumX * sumX) * (n * Math.Pow(sumY,2) - sumY * sumY)), 2);
        }

        private void getY(){
            string[] lines = File.ReadAllLines(this.inputFile);
            double[] y = new double[lines.Length - 1 ];

            int i = 0; 

            foreach(string line in lines.Skip(1)){
                y[i] = Convert.ToDouble(line.Split(";")[9]);
                i++;
            }

            this.y = y;
        }

        private void Sentiment(int pos){

           Console.WriteLine("test sentiment start");

            List<string> positive = new List<string>();
            List<string> neutral = new List<string>();
            List<string> negative = new List<string>();

            string[] lines = File.ReadAllLines("sentiments.txt");

            for(int i = 0; i < lines.Length;i++){
                if(lines[i] == "Positive"){
                    while(lines[i] != "" && i < lines.Length){
                        positive.Add(lines[i]);
                        i++;
                    }
                }
                else if(lines[i] == "Neutral"){
                    while(lines[i] != "" && i < lines.Length){
                        neutral.Add(lines[i]);
                        i++;
                    }
                }
                else if(lines[i] == "Negative"){
                    while(lines[i] != "" && i < lines.Length-1){
                        negative.Add(lines[i]);
                        i++;
                    }
                }
            }

            lines = File.ReadAllLines(this.inputFile);

            List<double> sentimentScores = new List<double>();

            foreach(string line in lines.Skip(1)){
                string sentiment = line.Split(";")[3].Trim();

                if(positive.Contains(sentiment)){
                    sentimentScores.Add(1);
                }
                else if(neutral.Contains(sentiment)){
                    sentimentScores.Add(0);
                }
                else if(negative.Contains(sentiment)){
                    sentimentScores.Add(-1);
                }
                else{
                    sentimentScores.Add(0);
                }
            }

            if(pos == 1) this.x1 = sentimentScores.ToArray();
            else if(pos == 2) this.x2 = sentimentScores.ToArray();
        }

        private void Hours(int pos){

            Console.WriteLine("test hours start");

            string[] lines = File.ReadAllLines(this.inputFile);

            double[] hours = new double[lines.Length - 1];

            int i = 0;

            foreach(string line in lines.Skip(1)){
                hours[i] = Convert.ToDouble(line.Split(";")[14]);
                i++;
            }

            if(pos == 1) this.x1 = hours;
            if(pos == 2) this.x2 = hours;

        } 
    }
}
