using System;

class Program
{
    static void Main(string[] args)
    {
        string playAgain;
        do
        {
            Random rand = new Random();
            int magicNumber = rand.Next(1, 101);
            int guess = -1;
            int attempts = 0;

            Console.WriteLine("Guess the magic number between 1 and 100!");

            while (guess != magicNumber)
            {
                Console.Write("What is your guess? ");
                guess = int.Parse(Console.ReadLine());
                attempts++;

                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine($"You guessed it in {attempts} attempts!");
                }
            }

            Console.Write("Do you want to play again (yes/no)? ");
            playAgain = Console.ReadLine().ToLower();
        }
        while (playAgain == "yes");
    }
}