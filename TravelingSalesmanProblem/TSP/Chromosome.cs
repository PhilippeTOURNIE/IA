using Accord.Genetic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSP.Model;

namespace TSP
{
    public class Chromosome : IChromosome
    {


        public List<City> Genes = new List<City>();

        public List<City> GenesEvaluated ;

        public Chromosome()
        {
            Generate();
        }
             

        private double m_fitness;

        public double Fitness => m_fitness;



        public IChromosome Clone()
        {
            var c = new Chromosome();
            c.Genes = Genes;
            return c;
        }

        public int CompareTo(object obj)
        {
            if (obj != null)
            {                
                var chro = obj as Chromosome;
                if (this.Fitness < chro.Fitness) return -1;
                if (this.Fitness > chro.Fitness) return 1;
                if (this.Fitness == chro.Fitness) return 0;
            }
            return -1;
        }

        public IChromosome CreateNew()
        {
            var c = new Chromosome();
            c.Generate();
            return c;
        }

        public void Crossover(IChromosome pair)
        {
            var p = pair as Chromosome;
            var newGene = new List<City>();
            // cutting point 
            Random rnd = new Random();
            int pt = rnd.Next(0, Travelling.cities.Count) ;
            newGene = this.Genes.Take(pt).ToList();

            foreach (var c in p.Genes)
            {
                if (!newGene.Contains(c))
                {
                    newGene.Add(c);
                }
            }

            return;
        }

        public void Evaluate(IFitnessFunction function)
        {
            m_fitness = function.Evaluate(this);
            GenesEvaluated = this.Genes.ToList();
        }

        public void Generate()
        {
            Random rnd = new Random();
            this.Genes = new List<City>();

            while (Genes.Count < Travelling.cities.Count)
            {
                int pt = rnd.Next(0, Travelling.cities.Count) ;
                if (!this.Genes.Contains(Travelling.cities[pt]))
                {
                    this.Genes.Add(Travelling.cities[pt]);
                }
            }
        }

        public void Mutate()
        {
            Random rnd = new Random();
            int pt1 = rnd.Next(0, Travelling.cities.Count) ;
            int pt2 = rnd.Next(0, Travelling.cities.Count) ;

            var c = Genes.ElementAt(pt1);
            Genes.RemoveAt(pt1);
            Genes.Insert(pt2, c);
        }
    }
}
