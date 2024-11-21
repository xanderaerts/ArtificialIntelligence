using System;

namespace NeuralNetworks
{
    public class NN
    {
        private double[,] training_input;
        private double[,] training_output;

        public double[,] Weights;

        private const int epochs = 1000;

        public NN(double[,] input, double[,] output){
            this.training_input = input;
            this.training_output = output;

            FillWeights();

        }

        private void FillWeights(){
            Random random = new Random();

            double[,] gewichten = new double[this.training_input.GetLength(1),1];

            for(int i = 0; i < this.training_input.GetLength(1); i++){
                gewichten[i,0] = random.NextDouble() * 2 - 1;
            }

            this.Weights =  gewichten;

        }

        public double[,] Predict(double[,] doubles){
            double[,] dotproduct = Matrix.DotProduct(doubles,Weights);

            for(int i = 0; i < dotproduct.GetLength(0);i++){
                dotproduct[i,0] =  Sigmoid(dotproduct[i,0]);
            }

            return dotproduct;
        }

        public void Train(){ 
            for(int count = 0; count < epochs; count++){
                // stap 1 & 2
                double[,] output_predictions = Predict(training_input);

                //stap 3
                double[,] errors = Matrix.Substract(training_output,output_predictions);

                //step 4
                double[,] sigmoid_derivative = new double[training_input.GetLength(0),1];

                for(int i = 0; i < sigmoid_derivative.GetLength(0);i++){
                    sigmoid_derivative[i,0] = output_predictions[i,0] * (1 - output_predictions[i,0]);
                }

                //stap 5
                double[,] multiply = Matrix.Multiplication(errors,sigmoid_derivative);

                //stap 6
                double[,] adjustments = Matrix.DotProduct(Matrix.Transpose(training_input),multiply);

                //stap 7
                Weights = Matrix.Sum(adjustments,Weights);

            }
        }

        private double Sigmoid(double value){
            return 1 / (1 + Math.Pow(Math.E,-value));
        }
    }
}
