using System;
using System.Xml;

namespace KNN
{
    public class KNN
    {
        private string[] InputFile;
        List<double[]> data = new List<double[]>();
        List<string> labels = new List<string>();


        public KNN(string filename){

            foreach(string line in File.ReadAllLines(filename)){
                string[] items = line.Split(',');
                double[] array = new double[items.Length-1];

                for(int i = 0; i < array.Length;i++){
                    array[i] = double.Parse(items[i]);
                }
                data.Add(array);
                labels.Add(items[items.Length - 1]);
            }
        }


        private double CalcDistance(double[] p, double[] q){
            double distance = 0;
            for(int i = 0;i < p.Length;i++){
                distance += Math.Pow(p[i] - q[i],2);
            }

            return Math.Sqrt(distance);
        }

       internal string Classify(double[] doubles){

            double minDistance = double.MaxValue;
            string label = "";
            
            for(int i = 0; i < data.Count;i++){
                double distance = CalcDistance(data[i],doubles);

                if(distance < minDistance){
                    minDistance = distance;
                    label = this.labels[i];
                }
            }

            return label;
       
       }
    }
}
