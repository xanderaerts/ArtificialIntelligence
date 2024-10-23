using Microsoft.VisualBasic;

namespace NimsGame;
class Program
{
    static void Main(string[] args)
    {
        string input = Console.ReadLine();

        int[] towers = new int[3];

        for(int i = 0; i <3;i++ ){
            towers[i] = Convert.ToInt32(input.Split(" ")[i]);
        }

        Nims nims = new Nims(towers);
    }
}
