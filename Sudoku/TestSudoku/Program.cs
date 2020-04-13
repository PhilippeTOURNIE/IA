using SolverSudoku;
using System;
using System.Collections.Generic;

namespace TestSudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Fact> faits = new List<Fact>();
            faits.Add(new Fact(0, new int?[] { null, 9, null, null, null, null, null, null, null })); //1
            faits.Add(new Fact(1, new int?[] { 5, null, 8, 7, 9, null, null, null, 3})); //1
            faits.Add(new Fact(2, new int?[] { null, null, 3, 6, 5, 8, null, null, null })); //1
            faits.Add(new Fact(3, new int?[] { null, 4, null, null, null, null, null, null, null })); //1
            faits.Add(new Fact(4, new int?[] { null, null, 1, 3, null, 7, 5, null,4 })); //1
            faits.Add(new Fact(5, new int?[] { 3, null, null, 4, null, 6, null, null, 2})); //1
            faits.Add(new Fact(6, new int?[] { null, 8, null, 1, 7, 2, null, null, 5})); //1
            faits.Add(new Fact(7, new int?[] { null, null, 5, null, null, 3, null, 4, 8 })); //1
            faits.Add(new Fact(8, new int?[] { null, 3, null, 8, 4, 5, 7, 2, 6})); //1

            var expert = new ExpertSudoku(faits);

            Console.ReadKey();

        }
    }
}
