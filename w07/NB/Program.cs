namespace NBB;
class Program
{
    static void Main(string[] args)
    {
    NB nb = new NB("data.txt");

    string sms = "I am so really really happy this is working!";
    Console.WriteLine($"{sms} SPAM? {nb.CheckIfSpam(sms)}");

    sms = "Free beer and big jackpot to win!";
    Console.WriteLine($"{sms} SPAM? {nb.CheckIfSpam(sms)}");
    }

}

