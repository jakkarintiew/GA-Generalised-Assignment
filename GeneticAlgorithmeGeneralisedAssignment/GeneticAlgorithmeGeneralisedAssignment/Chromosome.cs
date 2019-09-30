using System;

public class Chromosome<T>
{
    // Gene array is type T array, where T is any type
    public T[] Genes { get; private set; }

    // Fitness of each individual
    public int Fitness { get; private set; }

    private Random random;
    private Func<T[]> getRandomGenes;
    private Func<int, int> fitnessFunction;

    // Construtor
    public Chromosome(
        int size, 
        Random random, 
        Func<T[]> getRandomGenes, 
        Func<int, int> fitnessFunction, 
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
            //Console.WriteLine("[{0}]", string.Join(", ", Genes));
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

        //for (int i = 0; i < Genes.Length; i++)
        //{
        //    child.Genes[i] = random.NextDouble() < 0.5 ? Genes[i] : otherParent.Genes[i];
        //}


        double prob;

        if (Genes == otherParent.Genes)
        {
            child.Genes = Genes;
        }
        else
        {
            for (int i = 0; i < Genes.Length; i++)
            {
                if (Fitness < otherParent.Fitness)
                {
                    // If parent 1 has better fitness
                    // Higher probability to take gene from parent 1
                    prob = (double)otherParent.Fitness / (Fitness + otherParent.Fitness);
                    //Console.WriteLine("Fitness = {0} < otherParent.Fitness = {1}, prob = {2}", Fitness, otherParent.Fitness, prob);

                    child.Genes[i] = random.NextDouble() < prob ? Genes[i] : otherParent.Genes[i];
                }
                else
                {
                    prob = (double)Fitness / (Fitness + otherParent.Fitness);
                    //Console.WriteLine("Fitness = {0} > otherParent.Fitness = {1}, prob = {2}", Fitness, otherParent.Fitness, prob);
                    child.Genes[i] = random.NextDouble() < prob ? Genes[i] : otherParent.Genes[i];
                }

            }
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
