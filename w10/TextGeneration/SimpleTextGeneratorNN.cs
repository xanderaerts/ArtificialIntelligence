using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace TextGeneration
{
    public class SimpleTextGeneratorNN
    {
        public double[,] weights;
        private const int NumberOfCharacters = 26;
        private double learningRate = 0.1;
        private const int epochs = 1000;

        public SimpleTextGeneratorNN(){
            Random random = new Random();

            this.weights = new double[NumberOfCharacters,NumberOfCharacters];

            for(int i = 0; i < NumberOfCharacters;i++){
                for(int j = 0; j < NumberOfCharacters;j++){
                    this.weights[i,j] = random.NextDouble();
                }
            }
            

        }

        public void Train(string v){
            string text = Regex.Replace(v.ToLower(),@"[^a-z]",""); 

            for(int count = 0; count < epochs;count++){
                for(int i = 0; i < text.Length-1;i++){
                    double[,] input = CharToOneHot(text[i]);
                    double[,] target = CharToOneHot(text[i+1]);

                    double[,] output = Matrix.DotProduct(input,weights);
                    double[,] error = Matrix.Substract(target,output);

                    for(int j = 0; j < NumberOfCharacters;j++){
                        for(int k = 0; k < NumberOfCharacters;k++){
                            weights[j,k] += learningRate * error[0,k] * input[0,j];
                        }
                    }
                }
            }
        }
        private double[,] CharToOneHot(char c){
            double[,] vector = new double[1,NumberOfCharacters];
            int index = c - 'a';

            vector[0,index] = 1;
            return vector;
        }
        public char PredictNextChar(char inputChar){
            double[,] input = CharToOneHot(inputChar);
            double[,] output = Matrix.DotProduct(input,weights);

            double max = double.MinValue;
            int index = -1;

            for(int i = 0; i < NumberOfCharacters;i++){
                if(output[0,i] > max){
                   max = output[0,i];
                   index = i;
                }
            }

            return (char)('a'+index);



        }
    }
}
