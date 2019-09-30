﻿using System;

public class Assignment
{

    private int[,] costs;
    public int num_workers { get; private set; }
    public int num_jobs { get; private set; }
    private int[] assignment;

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

        //costs = new int[,]
        //{
        //    { 90, 55, 180, 210, 55, 55, 45, 220, 95, 145 },
        //    { 110, 155, 110, 190, 160, 80, 140, 130, 120, 235 },
        //    { 195, 125, 195, 120, 250, 55, 140, 180, 75, 185 },
        //    { 90, 90, 125, 115, 155, 195, 65, 190, 155, 135 },
        //    { 80, 25, 80, 230, 65, 250, 170, 120, 90, 210 },
        //    { 80, 60, 75, 70, 250, 235, 130, 110, 210, 155 },
        //    { 100, 105, 230, 205, 100, 135, 135, 185, 80, 115 },
        //    { 220, 55, 135, 150, 155, 240, 50, 125, 115, 150 },
        //    { 145, 90, 150, 95, 200, 155, 130, 75, 95, 145 },
        //    { 105, 250, 225, 175, 115, 220, 125, 110, 120, 175 }
        //};

        costs = new int[,]
        {
            { 80, 70, 170, 120, 170, 145, 175, 25, 215, 150, 225, 215, 215, 125, 50, 75, 155, 195, 245, 105, 205, 85, 65, 155, 240, 35, 105, 150, 190, 25, 100, 190, 180, 35, 110, 120, 140, 125, 220, 90, 110, 85, 220, 170, 240, 180, 210, 100, 80, 85 },
            { 55, 195, 210, 50, 40, 60, 50, 135, 185, 70, 120, 70, 75, 170, 240, 185, 170, 95, 160, 25, 175, 175, 145, 65, 110, 65, 215, 145, 225, 195, 150, 205, 235, 45, 145, 110, 95, 170, 245, 160, 30, 60, 40, 130, 40, 215, 45, 95, 55, 100 },
            { 60, 190, 240, 130, 250, 185, 25, 195, 95, 225, 65, 240, 145, 135, 195, 80, 25, 250, 110, 50, 35, 200, 205, 195, 215, 140, 35, 45, 75, 40, 180, 120, 220, 140, 100, 60, 240, 100, 190, 55, 230, 120, 185, 170, 80, 75, 25, 100, 60, 55 },
            { 150, 50, 105, 45, 165, 135, 125, 45, 175, 210, 130, 40, 130, 190, 165, 60, 215, 85, 155, 70, 130, 115, 70, 30, 145, 220, 200, 160, 245, 175, 45, 175, 150, 95, 115, 75, 30, 30, 200, 105, 185, 60, 105, 70, 75, 225, 200, 165, 125, 170 },
            { 120, 25, 225, 155, 250, 165, 205, 245, 210, 250, 125, 150, 25, 25, 220, 240, 65, 60, 110, 205, 195, 240, 120, 85, 70, 200, 190, 95, 120, 215, 85, 225, 95, 90, 215, 45, 235, 65, 130, 40, 225, 55, 120, 25, 90, 100, 90, 125, 155, 160 },
            { 45, 170, 140, 200, 190, 50, 150, 110, 240, 160, 220, 245, 170, 205, 65, 140, 160, 115, 45, 100, 55, 45, 175, 110, 45, 135, 80, 115, 55, 235, 190, 155, 215, 40, 25, 140, 175, 90, 245, 145, 220, 50, 120, 250, 210, 220, 130, 40, 105, 125 },
            { 245, 175, 55, 150, 35, 70, 120, 185, 245, 110, 30, 70, 120, 105, 85, 90, 80, 60, 180, 125, 25, 165, 45, 175, 170, 190, 55, 190, 40, 195, 190, 115, 105, 30, 45, 70, 235, 110, 160, 50, 70, 195, 250, 240, 50, 185, 80, 175, 195, 90 },
            { 220, 85, 160, 150, 65, 210, 245, 50, 245, 150, 250, 95, 240, 180, 195, 150, 130, 70, 250, 160, 250, 215, 200, 230, 130, 80, 180, 225, 155, 30, 240, 130, 145, 130, 95, 90, 90, 170, 115, 250, 225, 45, 250, 185, 65, 125, 155, 90, 115, 165 },
            { 200, 145, 180, 140, 190, 60, 110, 120, 250, 235, 220, 125, 145, 55, 75, 140, 45, 200, 30, 135, 35, 225, 130, 60, 85, 70, 180, 60, 25, 55, 245, 120, 125, 235, 200, 160, 170, 185, 250, 160, 135, 220, 55, 175, 125, 30, 85, 100, 55, 90 },
            { 145, 170, 240, 45, 190, 95, 40, 115, 70, 115, 250, 225, 160, 110, 70, 115, 120, 245, 90, 85, 200, 60, 95, 200, 25, 60, 170, 230, 155, 95, 150, 250, 95, 30, 40, 165, 85, 215, 85, 85, 245, 125, 50, 45, 185, 50, 85, 190, 100, 145 },
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

        //Console.WriteLine("====== Generation: {0} ======", ga.Generation);
        //Console.WriteLine("Assignment: {0}", String.Join(", ", ga.BestGenes));
        //Console.WriteLine("Best Fitness Score (cost): {0}", ga.BestFitness);
        //Console.WriteLine("Best Population Count: {0}", ga.Population.Count);
        //Console.WriteLine("=============================\n");
    }

    private int[] GetRandomAssignment()
    {
        // Array of n jobs, each element could be a worker index, i
        assignment = new int[num_jobs];

        ////For 1 - to - 1 assignment:
        ////initate as a sorted array of[0, 1, 2, ..., m, 0, 1, ... ]
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

        //Console.WriteLine("Rand Assignment: [ {0} ]", string.Join(", ", assignment));

        for (int j = 0; j < num_jobs; j++)
        {
            assignment[j] = random.Next(num_workers);
        }

        return assignment;
    }

    private int FitnessFunction(int index)
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
