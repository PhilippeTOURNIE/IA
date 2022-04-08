using SprintPlanning.Base;

namespace SprintPlanning
{
    public class Program
    {
        static void Main(string[] args)
        {
            var problem = new SprintProblem();
            var algo = new TabuSearch();
            IProblem kspbRandom = new SprintProblem(100, 30, 20);
            algo.Solve(problem);
            Console.WriteLine("resultat " + algo.SendResult());
            Console.ReadKey();
        }
    }
}