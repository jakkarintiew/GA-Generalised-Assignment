using System;

namespace SimpleAssignment
{
    public class Algorithm
    {
        private Population _population;

        public Algorithm(Random rnd, int populationSize, int tasks, bool maximise)
        {
            _population = new Population(rnd, populationSize, tasks, maximise);
        }

        public void Run(Random rnd, Matrix costMatrix, int tasks)
        {
            var iteration = 1;
            _population.Evaluate(costMatrix, iteration);

            while (iteration < 10000)
            {
                _population.StoreBestSolution(tasks);
                _population.Mutate(rnd);
                _population.ApplyCrossover(rnd, tasks);

                _population.SeedBestSolution(rnd);
                _population.Evaluate(costMatrix, iteration);
                _population.Selection(rnd);

                iteration++;
            }
        }
    }
}
