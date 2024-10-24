﻿namespace DECTREE;
class Program
{
    static void Main(string[] args)
    {
        int[] X = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        int[] Y = { 0, 0, 0, 1, 1, 1, 1, 1, 1 };

        Tree tree = new Tree();
        Node root = tree.Train(X, Y,0,2);

        foreach(int value in new int[] { 2, 5, 9 })
        {
            Console.WriteLine($"Predict {value} --> {tree.Predict(root,value)}");
        }

        tree.Print(root);
    }
}
