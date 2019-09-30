using System;
using System.Collections.Generic;


public class Assignment
{

    private int[,] costs;
    public int num_workers { get; private set; }
    public int num_jobs { get; private set; }
    private int[] assignment;

    public int max_generations = 100000;
    private int populationSize = 500;
    private float mutationRate = 0.10f;
    private int elitism = 5;

    // Create an instance of GeneticAlgorithm class
    public GA<int> ga;
    private Random random;

    // Constructor
    public Assignment()
    {
        //costs = new int[,]
        //{
        //    {250,  400,  350 },
        //    {400,  600,  350 },
        //    {200,  400,  250 }
        //};

        //costs = new int[,]
        //{
        //    {90,  75,  75,  80  },
        //    {35,  85,  55,  65  },
        //    {125, 95,  90,  105 },
        //    {45,  110, 95,  115 }
        //};

        costs = new int[,]
        {
            { 58, 114, 116, 146, 78, 145, 80, 68, 51, 113, 79, 62, 94, 83, 50, 124, 66, 61 },
            { 121, 88, 136, 119, 97, 123, 139, 83, 103, 131, 88, 50, 59, 124, 142, 145, 146, 57 },
            { 117, 135, 86, 145, 87, 124, 69, 87, 61, 109, 90, 92, 103, 51, 101, 137, 73, 110 },
            { 132, 132, 117, 82, 113, 85, 92, 112, 97, 60, 85, 86, 99, 143, 102, 137, 60, 75 },
            { 83, 124, 108, 102, 132, 67, 56, 130, 136, 111, 93, 68, 105, 143, 133, 71, 61, 109 },
            { 117, 100, 63, 65, 93, 50, 94, 55, 138, 121, 64, 122, 66, 66, 73, 70, 58, 82 },
            { 79, 86, 75, 127, 60, 76, 95, 134, 128, 94, 130, 74, 133, 123, 88, 150, 134, 63 },
            { 73, 74, 134, 61, 76, 74, 74, 100, 82, 91, 132, 96, 65, 103, 83, 54, 100, 132 },
            { 133, 65, 149, 140, 123, 83, 134, 123, 110, 54, 86, 105, 64, 102, 115, 148, 107, 62 },
            { 119, 111, 131, 86, 74, 75, 59, 148, 64, 60, 76, 140, 98, 79, 51, 86, 125, 137 }
        };

        num_workers = costs.GetLength(0);
        num_jobs = costs.GetLength(1);

        Console.WriteLine("====== Assignment Problem Description ======");
        Console.WriteLine("Costs matrix: ");
        Print2DArray(costs);
        Console.WriteLine("Number of workers: {0}", num_workers);
        Console.WriteLine("Number of jobs: {0}\n\n\n", num_jobs);

    }

    public void Start()
    {
        random = new Random();

        // Initialize ga instance
        ga = new GA<int>(populationSize, num_jobs, random, GetRandomAssignment, FitnessFunction, elitism, mutationRate);
    }

    public void Update()
    {
        ga.NewGeneration();

        Console.WriteLine("====== Generation: {0} ======", ga.Generation);
        Console.WriteLine("Assignment: {0}", String.Join(", ", ga.BestGenes));
        Console.WriteLine("Best Fitness Score (cost): {0}", ga.BestFitness);
        Console.WriteLine("Best Population Count: {0}", ga.Population.Count);
        Console.WriteLine("=============================\n");
    }

    private int[] GetRandomAssignment()
    {
        // Array of n jobs, each element could be a worker index, i
        assignment = new int[num_jobs];

        random = new Random();

        //// For 1-to-1 assignment: 
        //// initate as a sorted array of [ 0, 1, 2, ..., m, 0 , 1, ... ]
        //for (int j = 0; j < num_jobs; j++)
        //{
        //    assignment[j] = j % num_workers;
        //}

        //// Shuffle the array
        //for (int j = 0; j < num_jobs; j++)
        //{
        //    int randomIndex = random.Next(num_jobs);
        //    int temp = assignment[randomIndex];
        //    assignment[randomIndex] = assignment[j];
        //    assignment[j] = temp;
        //}

        for (int j = 0; j < num_jobs; j++)
        {
            assignment[j] = random.Next(num_workers);
        }

        return assignment;
    }

    private double FitnessFunction(int index)
    {
        int fitness = 0;
        Chromosome<int> chrmsm = ga.Population[index];

        // Calculate total cost
        for (int i = 0; i < num_jobs; i++)
        {
            fitness += costs[chrmsm.Genes[i], i];
        }

        return fitness;
    }

    public static void Print2DArray<T>(T[,] matrix)
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
