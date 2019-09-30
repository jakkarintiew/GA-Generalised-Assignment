using System;
using System.Collections.Generic;

public class GA<T>
{
    public List<Chromosome<T>> Population { get; private set; }
    public int Generation { get; private set; }
    public int BestFitness { get; private set; }
    public T[] BestGenes { get; private set; }

    public int Elitism;
    public float MutationRate;

    private List<Chromosome<T>> newPopulation;
    private Random random;
    private int chromoSize;
    private Func<T[]> getRandomGenes;
    private Func<int, int> fitnessFunction;

    // Constructor
    public GA(
        int populationSize, 
        int chromoSize, 
        Random random, 
        Func<T[]> getRandomGenes,
        Func<int, int> fitnessFunction,
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
            //Console.WriteLine("Best: " + Population[0].Fitness);
            //Console.WriteLine("Worst: " + Population[Population.Count - 1].Fitness);

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

                //Console.WriteLine("parent1 fitness: " + parent1.Fitness);
                //Console.WriteLine("parent1 Assignment: [ {0} ]\n", string.Join(", ", parent1.Genes));

                //Console.WriteLine("parent2 fitness: " + parent2.Fitness);
                //Console.WriteLine("parent2 Assignment: [ {0} ]\n", string.Join(", ", parent2.Genes));

                //Console.WriteLine("child fitness: " + child.Fitness);
                //Console.WriteLine("child Assignment: [ {0} ]\n\n", string.Join(", ", child.Genes));

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
        // Tournoment selection
        int num_competitors = 2; // Binary tournoment
        Chromosome<T> rand_selection;
        Chromosome<T> best = null;

        for (int i = 0; i < num_competitors; i++)
        {            
            rand_selection = Population[random.Next(Population.Count)];

            //Console.WriteLine(ind.Fitness);

            if (best == null || rand_selection.Fitness < best.Fitness)
            {
                best = rand_selection;
            }
        }

        return best;
    }
}
