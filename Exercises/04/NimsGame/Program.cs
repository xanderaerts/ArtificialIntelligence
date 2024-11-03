using System;

namespace NimsGame{
class Programs
{
    static void Main(string[] args)
    {

        string inputTowers = "",inputPlayer = "";
        int [] towers = new int[3];
        try{
            inputTowers = Console.ReadLine().Trim();

            inputPlayer = Console.ReadLine().Trim();

            towers = new int[3];

            for(int i = 0; i <3;i++ ){
                towers[i] = Convert.ToInt32(inputTowers.Split(" ")[i]);
            }

        }
        catch{
            throw new Exception("Crazy input");
        }

        try{
            int[] move = new int[2];        


            char player = 'A'; //A = starter


            Nims nims = new Nims(towers);

            if(inputPlayer == "human") nims.FysicalOponent = true;
            else if(inputPlayer == "computer") nims.FysicalOponent = false;
            else throw new Exception("Crazy input!");

            while(!nims.Wins()){  
                //Console.WriteLine(nims);
                

                //Console.WriteLine(player);

                if(player == 'A'){
                    try{
                        string[] s = Console.ReadLine().Trim().Split(" ");

                        for(int i = 0; i < s.Length; i++){
                            move[i] = Convert.ToInt32(s[i]);
                        }
                    }
                    catch{
                        throw new Exception("Crazy input!"); 
                    }
                }
                else if(player == 'B' && nims.FysicalOponent == true){
                    try{                   
                        string[] s = Console.ReadLine().Trim().Split(" "); 

                        for(int i = 0; i < s.Length; i++){
                            move[i] = Convert.ToInt32(s[i]);
                        }
                    }
                    catch{
                        throw new Exception("Crazy input!"); 
                    }
                }
                else{
                    move = nims.Play();
                    Console.WriteLine(string.Join(" ",move));
                }


                nims.Move(move);

                if(nims.Wins()){
                    Console.WriteLine($"{player} wins!");
                    break;
                }

                if(player == 'A') player = 'B';
                else player = 'A';
            }
    }
        catch(Exception e){
            Console.WriteLine(e.Message);
        }
    }
}
}
