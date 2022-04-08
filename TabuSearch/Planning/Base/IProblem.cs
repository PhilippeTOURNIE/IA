using System;
namespace SprintPlanning.Base
{
    public interface IProblem
    {
        List<ISolution> Neighbourhood(ISolution _currentSolution);
        ISolution RandomSolution();
        ISolution BestSolution(List<ISolution> _neighbours);
    }
}
