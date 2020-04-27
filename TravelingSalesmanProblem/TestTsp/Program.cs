using System;
using System.Collections.Generic;
using TSP.Model;

namespace TestTsp
{
    class Program
    {
        static void Main(string[] args)
        {
            // init tsp
            Travelling.cities = new List<City>()
            {
                new City("Paris"),
                new City("Lyon"),
                new City("Marseille"),
                new City("Nantes"),
                new City("Bordeaux"),
                new City("Toulouse"),
                new City("Lille")
            };

            Travelling.distances = new int[Travelling.cities.Count][];
            Travelling.distances[0] = new int[] { 0, 462, 772, 379, 546, 678, 215 }; // Paris
            Travelling.distances[1] = new int[] { 462, 0, 326, 598 , 842 , 506, 664 }; //Lyon
            Travelling.distances[2] = new int[] { 772, 326,0,909 , 555, 407,1005}; // Marseille
            Travelling.distances[3] = new int[] { 379, 598 , 909, 0, 338, 540, 584}; // Nantes
            Travelling.distances[4] = new int[] { 546, 842, 555, 338, 0, 250,792 }; // Bordeaux
            Travelling.distances[5] = new int[] { 678, 506, 407, 540, 250, 0, 926 }; // Toulouse
            Travelling.distances[6] = new int[] { 215, 664, 1005, 584, 792, 926,0 }; // Lille

            var solver = new TSP.TspSolver();
            solver.Solve(2579);

        }
    }
}
