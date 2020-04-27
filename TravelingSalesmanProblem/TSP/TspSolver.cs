using Accord.Genetic;
using System;

namespace TSP
{
    /// <summary>
    /// Traveling Saleman
    /// </summary>
    public class TspSolver
    {

        public void Solve(int minDistance)
        {

            var population = new Population(20,new Chromosome(),new FitnessFunction(),new  EliteSelection());

            population.CrossoverRate = 0;
            population.MutationRate = 0.3;
            //population.RandomSelectionPortion = 0.8;

            int i = 0;
            while (i < 2500 || population.BestChromosome.Fitness > minDistance)
            {
                i++;
                population.RunEpoch();
                Console.WriteLine("distance " + population.BestChromosome.Fitness.ToString());
            }

        }
    }
}
