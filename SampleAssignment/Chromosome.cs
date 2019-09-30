using System;
using System.Collections.Generic;

namespace SimpleAssignment
{
    public class Chromosome
    {
        // Assignments of facilities to locations
        private List<int> _assignment;
        private int _cost;

        public Chromosome(int size, Random rnd)
        {
            GenerateArbitraryAssignment(size, rnd);
        }

        public int GetCost()
        {
            return _cost;
        }

        public int Size()
        {
            return _assignment.Count;
        }

        public Chromosome(int size)
        {
            _assignment = new List<int>(new int[size]);
        }

        public void SetCost(int cost)
        {
            _cost = cost;
        }

        private void GenerateArbitraryAssignment(int size, Random rnd)
        {
            _assignment = new List<int>();

            for (int i = 0; i < size; i++)
            {
                _assignment.Add(i);
            }

            // Shuffle the array
            for (int i = 0; i < _assignment.Count; ++i)
            {
                int randomIndex = rnd.Next(_assignment.Count);
                int temp = _assignment[randomIndex];
                _assignment[randomIndex] = _assignment[i];
                _assignment[i] = temp;
            }
        }

        public void Print(int iteration)
        {
            Console.WriteLine("Iteration = " + iteration);
            Console.WriteLine("Total cost = " + _cost);

            for (var i = 0; i < _assignment.Count; ++i)
            {
                var location = i + 1;
                var facility = _assignment[i] + 1;

                Console.WriteLine("Location[" + location + "] -> Task[" + facility + "]");
            }
            Console.WriteLine();
        }

        public void Assign(int city, int factory)
        {
            _assignment[city] = factory;
        }

        public int GetFacility(int city)
        {
            return _assignment[city];
        }

        public int GetLocation(int facility)
        {
            var location = -1;

            for (int i = 0; i < _assignment.Count; ++i)
            {
                if (_assignment[i] == facility)
                {
                    location = i;
                    break;
                }
            }

            return location;
        }

        public void Crossover(Chromosome chr1, Chromosome chr2, Random rnd)
        {
            var size = _assignment.Count;

            int index1 = rnd.Next(0, size);
            var f1 = chr1.GetFacility(index1);

            int index2 = rnd.Next(0, size);
            var f2 = chr2.GetFacility(index2);

            var l1 = chr1.GetLocation(f2);
            var l2 = chr2.GetLocation(f1);

            chr1.Assign(index1, f2);
            chr1.Assign(l1, f1);

            chr2.Assign(index2, f1);
            chr2.Assign(l2, f2);
        }

        public void Mutation(Random rnd)
        {
            var selection = rnd.Next(0, 100);

            // Randomise two items
            if (selection < 40)
            {
                var size = _assignment.Count;

                int i1 = rnd.Next(0, size);
                int i2 = rnd.Next(0, size);

                while (i1 == i2)
                {
                    i2 = rnd.Next(0, size);
                }

                int v1 = _assignment[i1];
                int v2 = _assignment[i2];

                Assign(i1, v2);
                Assign(i2, v1);
            }
            // Randomise 3 items
            if (selection < 80)
            {
                var size = _assignment.Count;

                int i1 = rnd.Next(0, size);
                int i2 = rnd.Next(0, size);
                int i3 = rnd.Next(0, size);

                while (i1 == i2 || i1 == i3 || i2 == i3)
                {
                    i2 = rnd.Next(0, size);
                    i3 = rnd.Next(0, size);
                }

                int v1 = _assignment[i1];
                int v2 = _assignment[i2];
                int v3 = _assignment[i3];

                int choose = rnd.Next(2);

                if (choose == 0)
                {
                    Assign(i1, v2);
                    Assign(i2, v3);
                    Assign(i3, v1);
                }
                else
                {
                    Assign(i1, v3);
                    Assign(i2, v1);
                    Assign(i3, v2);
                }
            }
            else
            {
                // Shuffle the array
                for (int i = 0; i < _assignment.Count; ++i)
                {
                    int randomIndex = rnd.Next(_assignment.Count);
                    int temp = _assignment[randomIndex];
                    _assignment[randomIndex] = _assignment[i];
                    _assignment[i] = temp;
                }
            }
        }

        public int GetRandomFactory(Random rnd, int taskRange)
        {
            return rnd.Next(0, taskRange);
        }

        public void Copy(Chromosome chr)
        {
            _cost = chr._cost;

            for (var i = 0; i < _assignment.Count; ++i)
            {
                _assignment[i] = chr._assignment[i];
            }
        }

    }
}
