using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverSudoku
{
    public static class RowUtil
    {
        /// <summary>
        /// Ligne identique
        /// </summary>
        /// <param name="row1">ligne 1</param>
        /// <param name="row2">ligne 2</param>
        /// <returns></returns>
        public static bool RowEqual(int[] row1, int[] row2)
        {
            for (int i = 0; i < row1.Length; i++)
            {
                if (row1[i] != row2[i])
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Recherche une valeur utilisateur dans la ligne transmise
        /// </summary>
        /// <param name="row">Ligne</param>
        /// <param name="values">Valeur utilisateur, valeur non saisie = null</param>
        /// <returns></returns>
        public static bool SearchUserValueInRow(int[] row, int?[] values)
        {
            for (int i = 0; i < row.Length; i++)
            {
                if (values[i].HasValue && row[i] != values[i])
                    return false;
            }

            return true;
        }

        /// <summary>
        /// est ce une permutation
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static bool IsPermutation(int[] row)
        {
            for (int i = 0; i < row.Length; i++)
            {
                int z = 0;
                for (int j = 0; j < row.Length; j++)
                    if ( row[i] == row[j])
                    {
                        z++;
                    }
                if (z > 1)
                    return false;
            }

            return true;
        }
        
        public static void PrintConsole(List<int[]> row)
        {
            Console.WriteLine("------------------------------------");
            row.ForEach(r => Console.WriteLine(" "+string.Join(" | ", r)));
            Console.WriteLine("------------------------------------");
            Console.WriteLine("                  ");
        }

        /// <summary>
        /// on connait la position dans la sous matrice en ligne (flate) et on veut récuperer 
        /// les coordonnées dans la matrice principales
        /// </summary>
        /// <param name="index"></param>
        /// <param name="underL"></param>
        /// <param name="mainL"></param>
        /// <param name="mainC"></param>
        public static void getCoordMainMatrix(int index, int underL, out int mainL, out int mainC)
        {
            // par convention 
            // 0  1  2
            // 3  4  5
            // 6  7  8            

            int c_Start, l_Start;
            getIndexUnderMatrix(index, out c_Start, out l_Start);

            if (underL < 3)
            {
                mainL = l_Start + 0;
                mainC = c_Start + underL;

            }
            else if (underL < 6)
            {
                mainL = l_Start + 1;
                mainC = c_Start + underL-3;
            }
            else
            {
                mainL = l_Start + 2;
                mainC = c_Start + underL - 6;
            }

        }



        /// <summary>
        /// recupere une sous matrice à partir de la matrice principale
        /// </summary>
        /// <param name="index"></param>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static  int[] GetRowsbMatrixFlate(int index, int[,] matrix)
        {
            // par convention 
            // 0  1  2
            // 3  4  5
            // 6  7  8            
            var res = new int[Sudoku.SUB_MATRIX];
            int i_Start, j_Start;
            getIndexUnderMatrix(index, out i_Start, out j_Start);

            int k = 0;

            for (int j = j_Start; j < j_Start + 3; j++)
            {
                for (int i = i_Start; i < i_Start + 3; i++)
                {
                    res[k++] = matrix[j, i];

                }
            }
            return res;
        }

        /// <summary>
        /// recupere les coordonnées des sous matrices à partir de l'index
        ///par convention 
        // 0  1  2
        // 3  4  5
        // 6  7  8            
        /// </summary>
        /// <param name="index"></param>
        /// <param name="col_Start"></param>
        /// <param name="lin_Start"></param>
        private static void getIndexUnderMatrix(int index, out int col_Start, out int lin_Start)
        {
            col_Start = 0;
            lin_Start = 0;
            if (index == 1)
            {
                col_Start = 3;
            }
            else if (index == 2)
            {
                col_Start = 6;
            }
            else if (index == 3)
            {
                lin_Start = 3;
            }
            else if (index == 4)
            {
                col_Start = 3;
                lin_Start = 3;
            }
            else if (index == 5)
            {
                col_Start = 6;
                lin_Start = 3;
            }
            else if (index == 6)
            {
                lin_Start = 6;
            }
            else if (index == 7)
            {
                col_Start = 3;
                lin_Start = 6;
            }
            else if (index == 8)
            {
                col_Start = 6;
                lin_Start = 6;
            }
        }
    }
}
