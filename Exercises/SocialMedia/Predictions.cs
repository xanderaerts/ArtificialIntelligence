using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
namespace SocialMedia
{
    public class Predictions
    {

        private string[] inputFileLines {get; set;}
        private int inputLenght {get; set;}

        public double m1 {get; set;}
        public double m2{get; set;}
        public double b {get; set;}
        public double r2 {get; set;}

        private string inputFile = "sentiment.csv";

        private double[] x1 {get; set;}
        private double[] x2 {get; set;}
        private double[] y {get; set;}
        

        private double[] predictedValues;
        
        public void Predict(string var1,string var2){
            
            string[] columns = {"Sentiment","Platform","Text","Hours","Hashtags","Retweets"};
        
            if(var1 == columns[0] ) this.Sentiment(1);
            if(var1 == columns[1] ) this.Platform(1);
            if(var1 == columns[2] ) this.Text(1);
            if(var1 == columns[3] ) this.Hours(1);
            if(var1 == columns[4] ) this.Hashtags(1);
            if(var1 == columns[5] ) this.Retweets(1);

                
            if(var2 == columns[0] ) this.Sentiment(2);
            if(var2 == columns[1] ) this.Platform(2);
            if(var2 == columns[2] ) this.Text(2);
            if(var2 == columns[3] ) this.Hours(2);
            if(var2 == columns[4] ) this.Hashtags(2);
            if(var2 == columns[5] )this.Retweets(2);

            getY();
            Train();
        }

        public Predictions(){
            this.inputFileLines = File.ReadAllLines(this.inputFile);
            this.inputFileLines = this.inputFileLines.Skip(1).ToArray(); //remove first header line
            this.inputLenght = this.inputFileLines.Length;
        }

        public void Train(){
            
            double sumX1 = 0,sumX2 = 0, sumY = 0, sumX1Y = 0, sumX2X2 = 0,sumX2Y=0,sumX1X1 = 0,sumX1X2 = 0,meanY = 0;

            int n = this.x1.Length;

            sumX1 = this.x1.Sum();
            sumX2 = this.x2.Sum();
            sumY = this.y.Sum();
            meanY = this.y.Average();
            
            for(int i = 0; i < this.x1.Length;i++){
                sumX1Y += x1[i] * y[i];
                sumX2Y += x2[i] * y[i];

                sumX1X1 += x1[i] * x1[i];
                sumX2X2 += x2[i] * x2[i];

                sumX1X2 += x1[i] * x2[i];
            }
            this.m1 = ((n * sumX1Y - sumX1 * sumY) - (sumX1X2 * (n * sumX2Y - sumX2 * sumY)) / (n * sumX2X2 - Math.Pow(sumX2, 2))) / (n * sumX1X1 - Math.Pow(sumX1, 2));
            this.m2 = ((n * sumX2Y - sumX2 * sumY) - (sumX1X2 * (n * sumX1Y - sumX1 * sumY)) / (n * sumX1X1 - Math.Pow(sumX1, 2)))/ (n * sumX2X2 - Math.Pow(sumX2, 2));
	
            this.b = (sumY - (m1 * sumX1)- (m2*sumX2)) / n;

            this.PredictAll();

            double ssTot = 0,ssRes = 0;

            for (int i = 0; i < n; i++)
            {
                ssTot += Math.Pow(this.y[i] - meanY, 2);
                ssRes += Math.Pow(this.y[i] - this.predictedValues[i], 2);
            }
            this.r2 = 1 - (ssRes / ssTot);
        }

        private void PredictAll(){
            double[] predictedY = new double[this.y.Length];
            for (int i = 0; i < this.y.Length; i++)
            {
                predictedY[i] = this.b + this.m1 * this.x1[i] + this.m2 * this.x2[i];
            }

            this.predictedValues = predictedY;
        }

        private void getY(){
            double[] y = new double[this.inputLenght];

            int i = 0; 

            foreach(string line in this.inputFileLines){
                y[i] = Convert.ToDouble(line.Split(";")[9]);
                i++;
            }

            this.y = y;
        }

        private void Sentiment(int pos){

            /*
                Positive = 1
                Neutral = 0
                Negative = -1
            */

            List<string> positive = new List<string>();
            List<string> neutral = new List<string>();
            List<string> negative = new List<string>();

            string[] lines = File.ReadAllLines("sentiments.txt");

            for(int i = 0; i < lines.Length;i++){
                if (lines[i] == "Positive"){
                    while (lines[i] != "" && i < lines.Length){
                        positive.Add (lines[i]);
                        i++;
                    }
                }
                else if (lines[i] == "Neutral"){
                    while (lines[i] != "" && i < lines.Length){
                        neutral.Add(lines[i]);
                        i++;
                    }
                }
                else if (lines[i] == "Negative"){
                    while (lines[i] != "" && i < lines.Length-1){
                        negative.Add (lines[i]);
                        i++;
                    }
                }
            }
            List<double> sentimentScores = new List<double>();

            foreach(string line in this.inputFileLines){
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
         if(pos == 2) this.x2 = sentimentScores.ToArray();
        }

        private void Hours(int pos){
            

            double[] hours = new double[this.inputLenght];

            int i = 0;

            foreach(string line in this.inputFileLines){
                hours[i] = Convert.ToDouble(line.Split(";")[14]);
                i++;
            }

            if(pos == 1) this.x1 = hours;
            if(pos == 2) this.x2 = hours;

        } 
    
        private void Platform(int pos){
            /*
                Instagram = 1
                Twitter = 2
                Facebook = 3
            */

            double[] scores = new double[this.inputLenght];
            int i = 0;

            foreach(string line in this.inputFileLines){
                string platform = line.Split(";")[6];

                if(platform == "Instagram")scores[i] = 1;
                if(platform == "Twitter") scores[i] = 2;
                if(platform == "Facebook") scores[i] = 3;
                i++;
            }

            if(pos == 1) this.x1 = scores;
            if(pos == 2) this.x2 = scores;
        }

        private void Text(int pos){
            double[] length = new double[this.inputLenght];
            int i = 0; 

            foreach(string line in this.inputFileLines){
                length[i] = line.Split(";")[2].Count();
                i++;
            }

            if(pos == 1) this.x1 = length;
            if(pos == 2) this.x2 = length;
        }
        private void Hashtags(int pos){
            double[] hashtags = new double[inputLenght];

            int i = 0; 
            foreach(string line in this.inputFileLines){
                char tag = '#';
                string text = line.Split(";")[2];
                int count = text.Count(c => c == tag);

                hashtags[i] = count;
                i++;
            }

            if(pos == 1) this.x1 = hashtags;
            if(pos == 2) this.x2 = hashtags;
        }

        private void Retweets(int pos){
            double[] retweets = new double[this.inputLenght];

            int i = 0;

            foreach(string line in this.inputFileLines){
                retweets[i] = Convert.ToDouble(line.Split(";")[9]);
                i++;
            }

            if(pos == 1) this.x1 = retweets;
            if(pos == 2) this.x2 = retweets;
            
        }
    
    }

}
