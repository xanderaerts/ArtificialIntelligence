using System;

namespace NimsGame
{
    public class Nims
    {
        public int[] towers {get;set;}
        public bool FysicalOponent {get;set;}

        public Nims(int[] towers){
            this.towers = towers;
        }


        public bool TowerEmpty(int tower){
            if(this.towers[tower] == 0){
                return false;
            }
 
            return true;            
        }

        public bool Wins(){
            if(this.towers[0] == 0 && this.towers[1] == 0 && this.towers[2] == 0){
               return true;
            }
            return false;
        }

        public void Move(int[] move){
            if(move[0] > 3 || move[0] < 1){
                throw new Exception("Invalid pile number.");
            }
            int newValue = this.towers[move[0] - 1] - move[1];

            if(newValue < 0){
                throw new Exception("Not enough items in the pile.");
            }



            this.towers[move[0] - 1] = newValue;
        } 

        public int[] Play(){
            int bestScore = Int32.MinValue,bestValue = -1,bestTower = -1; //value represents the amount of items you take from the given tower
            int towerCounter = 0;

            foreach(int tower in this.towers){ // tower hosts the value of each tower
                for(int i = 1; i <= tower;i++){
                    int towervalue = tower;

                    this.towers[towerCounter] -= i;
                   // Console.WriteLine(this);

                    int score = MinMax(false);

                    this.towers[towerCounter] = towervalue;

                    if(score > bestScore){
                        bestScore = score;
                        bestValue = i;
                        bestTower = towerCounter;
                    }

                }
                towerCounter++;
            }

            int[] bestMove = new int[2]{bestTower+1,bestValue};

            return bestMove;
        }

        public int MinMax(bool isMax){
            
            if(Wins()){
                if(isMax) return -1;
                else return 1;
            }

            if(isMax){
                int towerCounter = 0,bestScore = int.MinValue;

                foreach(int tower in this.towers){
                    for(int i = 1; i <= tower; i++){
                        int towervalue = tower;

                        this.towers[towerCounter] -= i;
                        
                        int score = MinMax(false);

                        this.towers[towerCounter] = towervalue;

                        bestScore = Math.Max(score,bestScore);
                            
                    }
                    towerCounter++;
                }

                return bestScore;
            }
            else{
                int bestScore = Int32.MaxValue, towerCounter = 0;

                foreach(int tower in this.towers){
                    for(int i = 1; i <= tower; i++){
                        int towervalue = tower;

                        this.towers[towerCounter] -= i;
                        
                        int score = MinMax(true);

                        this.towers[towerCounter] = towervalue;

                        bestScore = Math.Min(score,bestScore);
                            
                    }
                    towerCounter++;
                }

                return bestScore;
            }
        }


        public override string ToString()
        {
            string s = "";

            for(int i = 0; i < this.towers.Length;i++){
                s += this.towers[i] + " | ";
            }
            return s;
        }

    }
}
