using System;

namespace KNN{

class Program
{
    static void Main(string[] args)
    {
        try{

            string inputFile = Console.ReadLine().Trim();
            string inputValues = Console.ReadLine().Trim();
            int K = Convert.ToInt32(Console.ReadLine().Trim());

            /*string inputFile = "music_genre_dataset.csv";
            string inputValues = "130 C 4 -10 6 7";
            int K = 7;*/

            KNN knn = new KNN(inputFile);

            string[] splitted = inputValues.Split(" ");
            double[] inputDoubles = new double[splitted.Length];

            if(K == splitted.Length){
                throw new Exception("K cannot be a factor of the number of unique classes!");
            }


                for(int i = 0; i < splitted.Length;i++){
                    inputDoubles[i] = knn.CastToDouble(splitted[i]);
                }
                
            Console.WriteLine(knn.Classify(inputDoubles,K));
        }
        catch(Exception e){
            Console.WriteLine(e.Message);
        }

    }
}
}
