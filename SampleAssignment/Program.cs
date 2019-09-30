﻿using System;

namespace SimpleAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            const int tasks = 5;

            int[,] flows = new int[tasks, tasks] { {0,  10, 15, 0,  7 },
                                                 {10, 0,  5,  6,  0 },
                                                {15, 5,  0,  4,  2 },
                                                {0,  6,  4,  0,  5 },
                                                {7,  0,  2,  5,  0 } };

            int[,] distances = new int[tasks, tasks] { {0,  3,  6,  4,  2 },
                                                    {3,  0,  2,  3,  3 },
                                                    {6,  2,  0,  3,  4 },
                                                    {4,  3,  3,  0,  1 },
                                                    {2,  3,  4,  1,  0 } };

            var matrix = new Matrix(tasks, flows, distances);
            var random = new Random();

            var alg = new Algorithm(random, 200, tasks, false);

            alg.Run(random, matrix, tasks);
        }
    }
}
