using System;
namespace Planning
{

    public class TabuSearchAbstract
    {
        protected double currentSolution;
        protected double bestSoFarSolution;

        public void Solve(IProblem _pb)
        {

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

        protected abstract void AddToTabuList(double currentSolution);
        protected abstract List<double> RemoveSolutionsInTabuList(List<ISolution> Neighbours);
        protected abstract bool Done();
        protected abstract void UpdateSolution(double _bestSolution);
        protected abstract void Increment();
    }

}