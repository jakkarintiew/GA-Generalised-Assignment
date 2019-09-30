using System;
using System.Collections.Generic;

public class GA<T>
{
    public List<Chromosome<T>> Population { get; private set; }
    public int Generation { get; private set; }
    public double BestFitness { get; private set; }
    public T[] BestGenes { get; private set; }

    public int Elitism;
    public float MutationRate;

    private List<Chromosome<T>> newPopulation;
    private Random random;
    private double fitnessSum;
    private int chromoSize;
    private Func<T[]> getRandomGenes;
    private Func<int, double> fitnessFunction;

    // Constructor
    public GA(
        int populationSize, 
        int chromoSize, 
        Random random, 
        Func<T[]> getRandomGenes,
        Func<int, double> fitnessFunction,
        int elitism, 
        float mutationRate
        )
    {
        Generation = 1;
        Elitism = elitism;
        MutationRate = mutationRate;
        Population = new List<Chromosome<T>>(populationSize);
        newPopulation = new List<Chromosome<T>>(populationSize);
        this.random = random;
        this.chromoSize = chromoSize;
        this.getRandomGenes = getRandomGenes;
        this.fitnessFunction = fitnessFunction;

        BestGenes = new T[chromoSize];

        for (int i = 0; i < populationSize; i++)
        {
            Population.Add(new Chromosome<T>(chromoSize, random, getRandomGenes, fitnessFunction, shouldInitGenes: true));
        }
    }

    public void NewGeneration(int numNewDNA = 0, bool crossoverNewDNA = false)
    {
        int finalCount = Population.Count + numNewDNA;

        if (finalCount <= 0)
        {
            return;
        }

        if (Population.Count > 0)
        {
            CalculateFitness();
            Population.Sort(CompareFitness);
        }

        newPopulation.Clear();

        for (int i = 0; i < Population.Count; i++)
        {
            // Keep only top individuals of the previous generation
            if (i < Elitism && i < Population.Count)
            {
                newPopulation.Add(Population[i]);
            }
            else if (i < Population.Count || crossoverNewDNA)
            {
                Chromosome<T> parent1 = ChooseParent();
                Chromosome<T> parent2 = ChooseParent();

                Chromosome<T> child = parent1.Crossover(parent2);

                child.Mutate(MutationRate);

                newPopulation.Add(child);
            }
            else
            {
                newPopulation.Add(new Chromosome<T>(chromoSize, random, getRandomGenes, fitnessFunction, shouldInitGenes: true));
            }
        }

        List<Chromosome<T>> tmpList = Population;
        Population = newPopulation;
        newPopulation = tmpList;

        Generation++;
    }

    // TODO: implement minimize or maximize boolean argument
    private int CompareFitness(Chromosome<T> a, Chromosome<T> b)
    {
        // -1: second arg is greater
        //  1: first arg is greater
        //  0: equal

        if (a.Fitness < b.Fitness)
        {
            return -1;
        }
        else if (a.Fitness > b.Fitness)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public void CalculateFitness()
    {
        // initialize fitnessSum to be 0
        fitnessSum = 0;

        // initialize best individual to be the first element of the Population
        Chromosome<T> best = Population[0];
        best.CalculateFitness(0);

        for (int i = 0; i < Population.Count; i++)
        {
            Population[i].CalculateFitness(i);

            if (Population[i].Fitness < best.Fitness)
            {
                best = Population[i];
            }
        }

        BestFitness = best.Fitness;

        // C# function, copy an array to another array starting at a specified location
        best.Genes.CopyTo(BestGenes, 0);
    }

    // Chosse parent by randomly choose from Population
    private Chromosome<T> ChooseParent()
    {
        // Multiply sum of fitnesses with a random number
        double randomNumber = random.NextDouble() * fitnessSum;

        for (int i = 0; i < Population.Count; i++)
        {
            if (randomNumber < Population[i].Fitness)
            {
                return Population[i];
            }

            randomNumber -= Population[i].Fitness;
        }

        return null;
    }
}
