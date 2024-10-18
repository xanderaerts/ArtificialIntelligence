namespace SocialMedia;
class Program
{
    static void Main(string[] args)
    {
        //string var1 = Console.ReadLine();
        //string var2 = Console.ReadLine();

        Predictions pred = new Predictions();
        //pred.Predict(var1,var2);
        pred.Predict("Sentiment","Hours");
        
        Console.WriteLine(pred.b);
        Console.WriteLine(pred.m);
        Console.WriteLine(pred.r2);
    }
}
