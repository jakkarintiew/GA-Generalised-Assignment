using System;

public class Assignment
{

    public int[,] costs;
    public int num_workers { get; private set; }
    public int num_jobs { get; private set; }
    private int[] assignment;

    public int populationSize = 100;
    public float mutationRate = 0.05f;
    public int elitism = 5;

    // Create an instance of GeneticAlgorithm class
    public GA<int> ga;
    private Random random;

    // Constructor
    public Assignment(int[,] costs)
    {
        this.costs = costs;
        num_workers = costs.GetLength(0);
        num_jobs = costs.GetLength(1);
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
    }

    private int[] GetRandomAssignment()
    {
        // Array of n jobs, each element could be a worker index, i
        assignment = new int[num_jobs];

        //TODO: Check feasibility when generating
        for (int j = 0; j < num_jobs; j++)
        {
            assignment[j] = random.Next(0, num_workers);
        }

        //Console.WriteLine("Rand Assignment: [ {0} ]", string.Join(", ", assignment));

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

    
}
