using Accord.Genetic;
using System;
using System.Collections.Generic;
using System.Text;
using TSP.Model;

namespace TSP
{
    public class FitnessFunction : IFitnessFunction
    {
        public double Evaluate(IChromosome chromosome)
        {
            var genes = (chromosome as Chromosome).Genes;
            var distance = 0;
            for (int i = 0; i < genes.Count - 1; i++)
            {
                distance += Travelling.getDistance(genes[i], genes[i + 1]);
            }
            
            return distance;
        }        
    }
}
