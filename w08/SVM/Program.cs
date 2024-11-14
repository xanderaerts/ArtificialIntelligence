using System.Runtime.CompilerServices;

namespace SVM{
    internal class Program{
        static void Main(string[] args){
            
            static(double[][],int[]) Read(string filename){
                    List<double[]> doubles = new List<double[]>();
                    List<int> ints = new List<int>();

                foreach(var item in File.ReadAllLines(filename).Skip(1)){
                    string[] split = item.Split(',');

                    doubles.Add(new double[] {Convert.ToDouble(split[0]),Convert.ToDouble(split[1]),Convert.ToDouble(split[2])});

                    ints.Add(Convert.ToInt32(split[3]));
                }

                return(doubles.ToArray(),ints.ToArray());
            }



            var svm = new SimpleSVM(3);
            double[][] inputs = { new double[] { 1, 2,3 }, new double[] { 2, 3,4 }, new double[] { 3, 3,5 } };
            int[] labels = { 1, -1, -1 };


            var prediction = svm.Predict(new double[] { 1.5, 2.5,3.5 });
            Console.WriteLine($"Prediction: {prediction}");

            prediction = svm.Predict(new double[] { 1.1, 1.9,3.2 });
            Console.WriteLine($"Prediction: {prediction}");

            prediction = svm.Predict(new double[] { 2.5, 3.5,4.5 });
            Console.WriteLine($"Prediction: {prediction}");


            (inputs,labels) = Read("Large_SVM_Test_Dataset.csv");
            svm.Train(inputs, labels, 0.01, 1000);

            prediction = svm.Predict(new double[] { 1.1, 1.9, 3.2 });
            Console.WriteLine($"Prediction IMS : {prediction}");

            prediction = svm.Predict(new double[] { -2.6,-3.5,-1.6 });
            Console.WriteLine($"Prediction IMS : {prediction}");


            var DSPS = new DSPS(3);
            DSPS.Train(inputs,labels,0.01,1000);

            prediction = svm.Predict(new double[] { 1.1, 1.9, 3.2 });
            Console.WriteLine($"Prediction DSPS : {prediction}");

            prediction = svm.Predict(new double[] { -2.6,-3.5,-1.6 });
            Console.WriteLine($"Prediction DSPS : {prediction}");


        }
    }
}