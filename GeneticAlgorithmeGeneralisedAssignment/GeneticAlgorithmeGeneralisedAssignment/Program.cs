using System;
using System.Diagnostics;

namespace GeneticAlgorithmeGeneralisedAssignment
{
    class Program
    {
        static void Main(string[] args)
        {

            GetAppInfo(); // Run GetAppInfo function to get info
            

            while (true)
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                int solution = 2075;
                int max_generations = 10000;

                Assignment asmt = new Assignment();
                asmt.Start();

                while (asmt.ga.Generation < max_generations && asmt.ga.BestFitness > solution || asmt.ga.BestFitness == 0)
                {
                    asmt.Update();
                    Console.Write("\r Current Generation: {0}, Current Best Fitness: {1}  ", asmt.ga.Generation, asmt.ga.BestFitness);
                }

                stopWatch.Stop();
                // Get the elapsed time as a TimeSpan value.
                TimeSpan ts = stopWatch.Elapsed;
                // Format and display the TimeSpan value.
                string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds);


                Console.WriteLine("\n====== Generation: {0} ======", asmt.ga.Generation);
                Console.WriteLine("Assignment: [ {0} ]", string.Join(", ", asmt.ga.BestGenes));
                Console.WriteLine("Best Fitness Score (cost): " + asmt.ga.BestFitness);
                Console.WriteLine("Best Population Count: " + asmt.ga.Population.Count);
                Console.WriteLine("RunTime: " + elapsedTime);
                Console.WriteLine("=============================\n");

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
            string appName = "Generalized Genetic Algorithm Console App Implementation";
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


