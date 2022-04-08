using SprintPlanning.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintPlanning
{
    public class TabuSearch : TabuSearchAbstract
    {
        int nbIterationsWithoutUpdate = 1;
        int nbIterations = 1;

        private readonly int MAX_ITERATIONS_WITHOUT_UPDATE = 30;
        private readonly int MAX_ITERATIONS = 100;
        private readonly int TABU_SEARCH_MAX_SIZE = 50;

        List<ISolution> tabuSolutions = new List<ISolution>();

        public TabuSearch(int maxIterationWithoutUpdate=0, int maxIterations = 0, int tabuSearchMaxSize = 0)
        {
            if (maxIterationWithoutUpdate > 0)
                MAX_ITERATIONS_WITHOUT_UPDATE = maxIterationWithoutUpdate;
            if (maxIterations > 0)
                MAX_ITERATIONS = maxIterations;
            if(tabuSearchMaxSize>0)
                TABU_SEARCH_MAX_SIZE = tabuSearchMaxSize;
        }

        protected override bool Done()
        {
            return nbIterationsWithoutUpdate >= MAX_ITERATIONS_WITHOUT_UPDATE && nbIterations >= MAX_ITERATIONS;
        }

        protected override void UpdateSolution(ISolution _bestSolution)
        {
            if (!tabuSolutions.Contains(_bestSolution))
            {
                currentSolution = _bestSolution;
                AddToTabuList(_bestSolution);

                if (_bestSolution.Value > bestSoFarSolution.Value)
                {
                    bestSoFarSolution = _bestSolution;
                    nbIterationsWithoutUpdate = 0;
                }
            }
        }

        protected override void Increment()
        {
            nbIterations++;
            nbIterationsWithoutUpdate++;
        }

        protected override void AddToTabuList(ISolution currentSolution)
        {
            while (tabuSolutions.Count >= TABU_SEARCH_MAX_SIZE)
            {
                tabuSolutions.RemoveAt(0);
            }

            tabuSolutions.Add(currentSolution);
        }

        protected override List<ISolution> RemoveSolutionsInTabuList(List<ISolution> Neighbours)
        {
            return Neighbours.Except(tabuSolutions).ToList();
        }
    }
}
