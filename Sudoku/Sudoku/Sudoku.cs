using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverSudoku
{
    /// <summary>
    /// Un sudoku est une liste de 8 lignes
    /// </summary>
    public class Sudoku
    {
        public const int SIZE = 9;
        public const int SUB_MATRIX = 9;

        public int[,] Matrix;

        public int[] GetRowsHorizontal(int index)
        {
            var res = new int[Matrix.GetLength(1)];
            for (int i = 0; i < Matrix.GetLength(1); i++)
            {
                res[i] = Matrix[index, i];
            }
            return res;
        }
        public int[] GetRowVertical(int index)
        {
            var res = new int[Matrix.GetLength(0)];
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                res[i] = Matrix[i, index];
            }
            return res;
        }

        

        public Sudoku(List<int[]> rowsHori)
        {

            // init the matrix
            Matrix = new int[rowsHori.Count, rowsHori[0].Length];

            // fill
            for (int i = 0; i < rowsHori.Count; i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    Matrix[i, j] = rowsHori[i][j];
                }
            }
        }
    }
}
