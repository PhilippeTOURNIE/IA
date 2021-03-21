using System;

namespace PathFinder
{
    public abstract  class Algorithm
    {
        // https://www.codeproject.com/Articles/1221034/Pathfinding-Algorithms-in-Csharp

        protected Graph graph;
        protected IHM ihm;

        public Algorithm(Graph _graph, IHM _ihm)
        {
            graph = _graph;
            ihm = _ihm;
        }

        public void Solve()
        {
            graph.Clear();
            Run();
            ihm.PrintResult(graph.ReconstructPath(), graph.ExitNode().DistanceFromBeginning);
        }

        protected abstract void Run();
    }
}
