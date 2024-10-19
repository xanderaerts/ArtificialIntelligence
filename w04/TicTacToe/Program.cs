using System.Runtime.InteropServices;

namespace TicTacToe;
class Program
{

    static void Main(string[] args)
    {
        TicTacToe ttt = new TicTacToe();
        int position = -1;
        char player = 'X';
        
        
        Console.WriteLine("Welcome by TicTacToe! \n\t1.Play Against bot\n\t2.Play against other player");

        string ?menu_Input = Console.ReadLine();

        while(menu_Input != "1" && menu_Input != "2")
        {
            Console.WriteLine("Invalid input, please try again!");
            menu_Input = Console.ReadLine();
        }

        if(menu_Input == "1"){
            Console.WriteLine("\n\t1.Easy\n\t2.Difficult");
            menu_Input = Console.ReadLine();

            while(menu_Input != "1" && menu_Input != "2"){
                Console.WriteLine("Invalid input, please try again!");
                menu_Input = Console.ReadLine();
            }

            if(menu_Input == "1"){
                ttt.bot = "NaivePlayer";
               // position = ttt.NaivePlayer();
            }
            else{
                ttt.bot = "SmartPlayer";
                //position = ttt.SmartPlayer();
            }
            
            Console.WriteLine(ttt);
            
            while (!ttt.Full())
            {   
                Console.WriteLine(ttt.bot);

                if(ttt.bot == "SmartPlayer") position = ttt.SmartPlayer();
                //if(ttt.bot == "NaivePlayer") position = ttt.NaivePlayer();

                if (player == 'X') {
                    Console.Write($"Place {player} on: ");
                    position = Convert.ToInt32(Console.ReadLine());
                }

                ttt.Place(player, position);
                Console.WriteLine(ttt);
                
                if (ttt.Wins(player)) {
                    Console.WriteLine($"{player} is the winner!");
                    break;
                }

                if (player == 'X') player = 'O';
                else player = 'X';
            }
        }
        else{
            Console.WriteLine(ttt);
    
            while (!ttt.Full())
            {
                Console.Write($"Place {player} on: ");
                position = Convert.ToInt32(Console.ReadLine());

                ttt.Place(player, position);
                Console.WriteLine(ttt);

                if (ttt.Wins(player)) {
                    Console.WriteLine($"{player} is the winner!");
                    break;
                }

                if (player == 'X') player = 'O';
                else player = 'X';
            }
        }
    }
}
