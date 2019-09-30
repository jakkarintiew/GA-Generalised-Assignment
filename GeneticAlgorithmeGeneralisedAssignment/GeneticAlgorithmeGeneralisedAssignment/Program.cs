using System;

namespace GeneticAlgorithmeGeneralisedAssignment
{
    class Program
    {
        static void Main(string[] args)
        {

            GetAppInfo(); // Run GetAppInfo function to get info

            while (true)
            {
                int solution = 230;
                Assignment asmt = new Assignment();
                asmt.Start();

                while (asmt.ga.Generation < asmt.max_generations && asmt.ga.BestFitness != solution) {
                    asmt.Update();
                }

                PrintColourMessage(ConsoleColor.Green, "SOLVED!!!");

                // Ask to play again
                PrintColourMessage(ConsoleColor.Yellow, "Run again? [Y or N]");

                // Get answer
                string answer = Console.ReadLine().ToUpper();
                if (answer == "Y")
                {
                    continue;
                }
                else if (answer == "N")
                {
                    return;
                }
                else
                {
                    return;
                }
            }
        
        }

        static void GetAppInfo()
        {
            // Set app vars
            string appName = "Generic Genetic Algorithm Console App Implementation";
            string appVersion = "1.0.0";
            string appAuthor = "Jakkarin Sae-Tiew";

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("{0}: Version {1} by {2}", appName, appVersion, appAuthor);
            Console.ResetColor();
        }

        // Print color message
        static void PrintColourMessage(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }


    }
}


