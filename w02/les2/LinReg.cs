using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;

namespace les2
{
    public class LinReg
    {
       public double m {get; private set;}
       public double b {get;private set;}

       public double r2 {get;private set;}

       public void Train(double[] x, double[] y){

            double sumX =0, sumY = 0,sumXY = 0, sumX2 = 0;
            int n = x.Length;

            sumX = x.Sum();
            sumY = y.Sum();


            for(int i = 0; i < x.Length;i++){
                sumXY += x[i] * y[i];
                sumX2 += x[i] * x[i];
            }
           
            this.m = ((n * sumXY) - (sumX * sumY)) / (n * sumX2 - Math.Pow(sumX, 2));
	
            this.b = (sumY - (m * sumX)) / n;

            this.r2 = Math.Pow((n * sumXY - sumX * sumY) / Math.Sqrt((n * sumX2 - sumX * sumX) * (n * Math.Pow(sumY,2) - sumY * sumY)), 2);
            Console.WriteLine("r2" + this.r2);
       }

       public double Predict(double x){
            return m*x - b;
       } 
    }
}
