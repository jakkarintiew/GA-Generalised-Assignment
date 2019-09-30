using System;

public class Chromosome<T>
{
    // Gene array is type T array, where T is any type
    public T[] Genes { get; private set; }

    // Fitness of each individual
    public double Fitness { get; private set; }

    private Random random;
    private Func<T[]> getRandomGenes;
    private Func<int, double> fitnessFunction;

    // Construtor
    public Chromosome(
        int size, 
        Random random, 
        Func<T[]> getRandomGenes, 
        Func<int, double> fitnessFunction, 
        bool shouldInitGenes = true)
    {
        // Create the Gene array with size
        Genes = new T[size];
        this.random = random;
        this.getRandomGenes = getRandomGenes;
        this.fitnessFunction = fitnessFunction;

        if (shouldInitGenes)
        {
            Genes = getRandomGenes();
        }

    }

    public double CalculateFitness(int index)
    {
        Fitness = fitnessFunction(index);
        return Fitness;
    }

    // Crossover function
    public Chromosome<T> Crossover(Chromosome<T> otherParent)
    {
        // create new child Chromosome with same gene array size as parent
        Chromosome<T> child = new Chromosome<T>(Genes.Length, random, getRandomGenes, fitnessFunction, shouldInitGenes: false);

        for (int i = 0; i < Genes.Length; i++)
        {
            child.Genes = random.NextDouble() < 0.5 ? Genes : otherParent.Genes;
        }

        return child;
    }

    // Mutation function
    public void Mutate(float mutationRate)
    {

        if (random.NextDouble() < mutationRate)
        {
            // Generate a random gene
            Genes = getRandomGenes();
        }
        
    }
}
