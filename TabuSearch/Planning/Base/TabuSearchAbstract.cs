using System;
namespace SprintPlanning.Base
{

    public abstract class TabuSearchAbstract
    {
        protected ISolution currentSolution;
        protected ISolution bestSoFarSolution;
        protected IProblem pb;

        public void Solve(IProblem _pb)
        {
            pb = _pb;
            currentSolution = pb.RandomSolution();
            bestSoFarSolution = currentSolution;
            AddToTabuList(currentSolution);

            while (!Done())
            {
                List<ISolution> Neighbours = pb.Neighbourhood(currentSolution);
                if (Neighbours != null)
                {
                    Neighbours = RemoveSolutionsInTabuList(Neighbours);
                    ISolution bestSolution = pb.BestSolution(Neighbours);
                    if (bestSolution != null)
                    {
                        UpdateSolution(bestSolution);
                    }
                }
                Increment();
            }

            SendResult();
        }

        protected abstract void AddToTabuList(ISolution currentSolution);
        protected abstract List<ISolution> RemoveSolutionsInTabuList(List<ISolution> Neighbours);
        protected abstract bool Done();
        protected abstract void UpdateSolution(ISolution _bestSolution);
        protected abstract void Increment();

        public  string SendResult()
        {
            if (bestSoFarSolution!=null)
                return bestSoFarSolution.ToString();
            return "";
        }

    }
}