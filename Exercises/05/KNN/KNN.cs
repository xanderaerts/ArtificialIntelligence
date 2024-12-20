using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;


namespace KNN
{
    public class KNN
    {
        private string[] InputFile;
        List<double[]> data = new List<double[]>();
        List<string> labels = new List<string>();
        private List<string> categories = new List<string>();


        public KNN(string filename){

            try{ 
                foreach(string line in File.ReadAllLines(filename).Skip(1)){
                    string[] items = line.Split(',');
                    double[] array = new double[items.Length-1];

                    for(int i = 0; i< array.Length;i++){
                        array[i] = CastToDouble(items[i]);
                    }
                    data.Add(array);
                    labels.Add(items[items.Length - 1]);

                // Console.WriteLine(string.Join(" ",array));
                }
            }
            catch{
                throw new Exception("File not found!");
            }
        }


        public double CastToDouble(string item){

            if(double.TryParse(item,out double val)){
                return val;
            }
            else if(int.TryParse(item,out int intval)){
                return intval;
            }
            else if(item.Trim() == "yes"){
                return 1;
            }
            else if(item.Trim() == "no"){
                return 0;
            }
            else{

                int ascii = 0;

                foreach(char c in item){
                    ascii += (int)c;
                }

                return ascii;
            }
        }


        private double CalcDistance(double[] p, double[] q){
            double distance = 0;
            for(int i = 0;i < p.Length;i++){
                distance += Math.Pow(p[i] - q[i],2);
            }

            return Math.Sqrt(distance);
        }

       internal string Classify(double[] doubles,int k){

            double minDistance = double.MaxValue;
            string label = "";
            
            for(int j = 0;j <= k; j++){

                for(int i = 0; i < data.Count;i++){
                    double distance = CalcDistance(data[i],doubles);

                    if(distance < minDistance){
                        minDistance = distance;
                        label = this.labels[i];
                    }
                }
            }

            return label;
       
       }
    }
}
