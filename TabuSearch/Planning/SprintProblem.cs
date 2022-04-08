using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SprintPlanning.Base;

namespace SprintPlanning
{
    public class SprintProblem : IProblem
    {
        protected List<Ticket> sprint = null;
        public double MaxWeight { get; set; }
        public static Random randomGenerator = null;
        public const int NB_NEIGHBOURS = 30;

        

        public SprintProblem()
        {
            sprint = new List<Ticket>();

            sprint.Add(new Ticket("A", 4, 15));
            sprint.Add(new Ticket("B", 7, 15));
            sprint.Add(new Ticket("C", 10, 20));
            sprint.Add(new Ticket("D", 3, 10));
            sprint.Add(new Ticket("E", 6, 11));
            sprint.Add(new Ticket("F", 12, 16));
            sprint.Add(new Ticket("G", 11, 12));
            sprint.Add(new Ticket("H", 16, 22));
            sprint.Add(new Ticket("I", 5, 12));
            sprint.Add(new Ticket("J", 14, 21));
            sprint.Add(new Ticket("K", 4, 10));
            sprint.Add(new Ticket("L", 3, 7));

            MaxWeight = 20;
            if (randomGenerator == null)
            {
                randomGenerator = new Random();
            }
        }

        public SprintProblem(int _nbSprints, double _maxWeight, double _maxValue)
        {
            sprint = new List<Ticket>();
            MaxWeight = _maxWeight;
            if (randomGenerator == null)
            {
                randomGenerator = new Random();
            }

            for (int i = 0; i < _nbSprints; i++)
            {
                sprint.Add(new Ticket(i.ToString(), randomGenerator.NextDouble() * _maxWeight, randomGenerator.NextDouble() * _maxValue));
            }
        }

        public ISolution BestSolution(List<ISolution> _listSolutions)
        {
            return _listSolutions.Where(x => x.Value.Equals(_listSolutions.Max(y => y.Value))).FirstOrDefault();
        }

        public List<ISolution> Neighbourhood(ISolution _currentSolution)
        {
            List<ISolution> neighbours = new List<ISolution>();
            List<Ticket> possibleBoxes = sprint;

            for (int i = 0; i < NB_NEIGHBOURS; i++)
            {
                SprintSolution newSol = new SprintSolution((SprintSolution)_currentSolution);
                int index = randomGenerator.Next(0, newSol.LoadedContent.Count);
                newSol.LoadedContent.RemoveAt(index);

                double enableSpace = MaxWeight - newSol.Weight;
                List<Ticket> availableBoxes = possibleBoxes.Except(newSol.LoadedContent).Where(x => (x.Priority <= enableSpace)).ToList();

                while (enableSpace > 0 && availableBoxes.Count != 0)
                {
                    index = randomGenerator.Next(0, availableBoxes.Count);
                    newSol.LoadedContent.Add(availableBoxes.ElementAt(index));
                    enableSpace = MaxWeight - newSol.Weight;
                    availableBoxes = possibleBoxes.Except(newSol.LoadedContent).Where(x => (x.Priority <= enableSpace)).ToList();
                }

                neighbours.Add(newSol);
            }
            return neighbours;

        }

        public ISolution RandomSolution()
        {
            SprintSolution solution = new SprintSolution();
            List<Ticket> possibleSprints = sprint;

            double enableSpace = MaxWeight;
            List<Ticket> availableSprints = possibleSprints.Where(x => (x.Priority <= enableSpace)).ToList();

            while (enableSpace > 0 && availableSprints.Count != 0)
            {
                int index = randomGenerator.Next(0, availableSprints.Count);
                solution.LoadedContent.Add(availableSprints.ElementAt(index));
                enableSpace = MaxWeight - solution.Weight;
                availableSprints = possibleSprints.Except(solution.LoadedContent).Where(x => (x.Priority <= enableSpace)).ToList();
            }

            return solution;
        }
    }
}
