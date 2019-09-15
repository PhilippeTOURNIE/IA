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
            faits.Add(new Fact(0, new int?[] { 1, 8, 2, null, 9, null, null, null, null })); //1
            faits.Add(new Fact(1, new int?[] { null, null, null, null, null, null, null, null, null })); //1
            faits.Add(new Fact(2, new int?[] { null, null, null, null, null, 4, null, 3, 1 })); //1
            faits.Add(new Fact(3, new int?[] { null, null, null, null, null, null, null, 5, 6 })); //1
            faits.Add(new Fact(4, new int?[] { null, 4, null, 2, null, null, null, null, 9 })); //1
            faits.Add(new Fact(5, new int?[] { 9, null, null, null, null, 3, 1, null, null })); //1
            faits.Add(new Fact(6, new int?[] { 4, 2, 5, 8, null, null, 9, null, null })); //1
            faits.Add(new Fact(7, new int?[] { null, null, 9, null, null, null, 8, null, 7 })); //1
            faits.Add(new Fact(8, new int?[] { null, 3, 7, null, null, null, 5, null, null })); //1

            var expert = new ExpertSudoku(faits);

            Console.ReadKey();

        }
    }
}
