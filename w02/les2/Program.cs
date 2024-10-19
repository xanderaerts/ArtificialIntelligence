using System.ComponentModel;
using System.Linq.Expressions;

namespace les2;

class Program
{
    static void Main(string[] args)
    {



/*
        
        double[] x = {1,2,3,4,5,6,7,8};
        double[] y = {30,45,51,57,60,65,70,71};

        LinReg linreg = new LinReg();

        linreg.Train(x,y);

        Console.WriteLine(linreg.b);
        Console.WriteLine(linreg.m);
        

        Console.WriteLine(linreg.Predict(5));*/

        

        string[] lines = File.ReadAllLines("social-media.csv");
        double[] x = new double[lines.Length - 1];
        double[] y = new double[lines.Length - 1];



        int i = 0;


        foreach(var item in lines.Skip(1)){
            x[i] = Convert.ToDouble(item.Split(",")[1]);
            y[i] = Convert.ToDouble(item.Split(",")[4]);
            i++;
        }

        Console.WriteLine(string.Join("\n test",x));
        Console.WriteLine(string.Join("\n",y));
        LinReg linreg = new LinReg();

        linreg.Train(x,y);

        Console.WriteLine(linreg.Predict(4));

        Console.WriteLine(linreg.b);
        Console.WriteLine(linreg.m);
    }
}
