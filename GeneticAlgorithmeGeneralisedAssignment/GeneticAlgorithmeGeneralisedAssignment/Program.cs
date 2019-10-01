using System;
using System.Diagnostics;
using System.Text;
using System.IO;


namespace GeneticAlgorithmeGeneralisedAssignment
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            GetAppInfo(); // Run GetAppInfo function to get info
            
            while (true)
            {
                // Start timer 
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                // Set stopping conditions
                int solution = 2075;
                int max_generations = 100000;

                // Initialize assignment problem
                Assignment asmt = new Assignment();
                asmt.Start();

                // Print problem description
                Console.WriteLine("====== Assignment Problem Description ======");
                Console.WriteLine("Costs matrix: ");
                Print2DArray(asmt.costs);
                Console.WriteLine("Number of workers: {0}", asmt.num_workers);
                Console.WriteLine("Number of jobs: {0}\n", asmt.num_jobs);

                // Initialize csv writer
                string filePath = @"..\..\..\out.csv";
                File.Delete(filePath);
                var csv = new StringBuilder();
                var newLine = string.Format("Generation,Fitness");
                csv.AppendLine(newLine);

                // Update algorithm until stopping conditions are met
                while (asmt.ga.Generation < max_generations && asmt.ga.BestFitness > solution || asmt.ga.BestFitness == 0)
                {
                    // Prevent cursor flickering
                    Console.CursorVisible = false;

                    // Print current best genes
                    var sb = new StringBuilder();
                    sb.AppendLine("-------------------------------");
                    sb.AppendFormat("Current Generation: {0}\n", asmt.ga.Generation);
                    sb.AppendFormat("Current Best Fitness: {0}\n", asmt.ga.BestFitness);
                    sb.AppendFormat("Current Assignment: [ {0} ]\n", string.Join(", ", asmt.ga.BestGenes));
                    sb.AppendLine("-------------------------------");
                    Console.Write(sb);
                    Console.SetCursorPosition(0, Console.CursorTop - 5);

                    // Update 
                    asmt.Update();

                    // Append to csv
                    newLine = string.Format("{0},{1}", asmt.ga.Generation, asmt.ga.BestFitness);
                    csv.AppendLine(newLine);
                }

                Console.CursorVisible = true;

                // Get the elapsed time as a TimeSpan value and format the TimeSpan value.
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);

                // Print solution and run time
                Console.SetCursorPosition(0, Console.CursorTop + 5);
                Console.WriteLine("\n====== Generation: {0} ======", asmt.ga.Generation);
                Console.WriteLine("Assignment: [ {0} ]", string.Join(", ", asmt.ga.BestGenes));
                Console.WriteLine("Best Fitness Score (cost): " + asmt.ga.BestFitness);
                Console.WriteLine("Best Population Count: " + asmt.ga.Population.Count);
                Console.WriteLine("RunTime: " + elapsedTime);
                Console.WriteLine("=============================\n");

                // Save as csv
                File.AppendAllText(filePath, csv.ToString());

                PrintColourMessage(ConsoleColor.Green, "SOLVED!!!");

                // Ask to run again
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

        static void Print2DArray<T>(T[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }


    }
}


