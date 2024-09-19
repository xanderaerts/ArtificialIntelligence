namespace Random;

class Program
{
    static void Main(string[] args)
    {
        LCG lCG = new LCG();

        for(int i = 0;i < 10; i++){
            Console.WriteLine(lCG.Next());
            Console.WriteLine(lCG.Next(50));
        }
    }
}
