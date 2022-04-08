using System;
namespace Planning
{
    public interface IProblem
    {
        List<double> Neighbourhood(double _currentSolution);
        double RandomSolution();
        double BestSolution(List<double> _neighbours);
    }
}
