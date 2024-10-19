namespace KNN;
class Program
{
    static void Main(string[] args)
    {
        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        KNN knn = new KNN("iris.dat");

        Console.WriteLine(knn.Classify(new double[] {6.1,2.6,5.6,1.4}));

        knn = new KNN("iris.dat");

        Console.WriteLine(knn.Classify(new double[] {6.1,2.6,5.6,1.4}));

    
    }
}
